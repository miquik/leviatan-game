using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using GLSpec;

namespace SharpGL
{
	public unsafe partial class CSGL
	{
		Program usedProgram	= null;
		public Program UsedProgram
		{
			get
			{
				return usedProgram;
			}
			set
			{
				usedProgram	= value;
				if(value != null)
					gl.UseProgram(value.P);
				else gl.UseProgram(0);
			}
		}

		public class Shader : GLObject,IMustInit,IMustExit
		{
			readonly uint T;
			readonly string Src;
			internal Shader(CSGL gl,uint SType,string Source) : base(gl)
			{
				T	= SType;
				Src	= Source;

				Initial();
			}

			internal uint S	= 0;
			void IMustInit.Init(GL gl)
			{
				S	= gl.CreateShader(T);
				fixed(byte* pB	= Encoding.UTF8.GetBytes(Src + '\0'))
				{
					sbyte* ppB	= (sbyte*)pB;
					gl.ShaderSource(S,1,&ppB,null);

				}

				gl.CompileShader(S);
			}
			void IMustExit.Exit(GL gl)
			{
				gl.DeleteShader(S);
				S	= 0;
			}
		}

		public class VertexShader : Shader
		{
			public VertexShader(CSGL gl,string Source) : base(gl,VERTEX_SHADER,Source)
			{
			}
		}
		public class FragmentShader : Shader
		{
			public FragmentShader(CSGL gl,string Source) : base(gl,FRAGMENT_SHADER,Source)
			{
			}
		}

		public class Attribute
		{
			readonly int Loc;
			public readonly string Name;
			internal Attribute(int Location,string AName)
			{
				Loc	= Location;
				Name	= AName;
			}
		}

		public class Uniform : GLParameter
		{
			readonly int Loc;
			public readonly string Name;
			internal Uniform(GL gl,int Location,string UName) : base(gl)
			{
				Loc	= Location;
				Name	= UName;
			}

			public void SetValue(int X)
			{
				gl.Uniform1i(Loc,X);
			}

			public void SetValue(int X,int Y)
			{
				gl.Uniform2i(Loc,X,Y);
			}

			public void SetValue(int X,int Y,int Z)
			{
				gl.Uniform3i(Loc,X,Y,Z);
			}

			public void SetValue(float X)
			{
				gl.Uniform1f(Loc,X);
			}

			public void SetValue(float X,float Y)
			{
				gl.Uniform2f(Loc,X,Y);
			}

			public void SetValue(float X,float Y,float Z)
			{
				gl.Uniform3f(Loc,X,Y,Z);
			}
		}

		public class Program : GLObject,IMustInit,IMustExit
		{
			internal uint P	= 0;
			public readonly Dictionary<string,Uniform> Uniforms	= new Dictionary<string,Uniform>();
			public readonly Dictionary<string,Attribute> Attributes	= new Dictionary<string,Attribute>();
			void IMustInit.Init(GL gl)
			{
				P	= gl.CreateProgram();

				gl.AttachShader(P,VShader.S);
				gl.AttachShader(P,FShader.S);

				gl.LinkProgram(P);

				////////////////////////////////////////////////////////////////

				int ACount	= 0;
				gl.GetProgramiv(P,ACTIVE_ATTRIBUTES,&ACount);
				if(ACount >= 0)
				{
					uint i	= 0;
					while(i < ACount)
					{
						uint T;
						int Lng,Sze;
						sbyte* pName	= stackalloc sbyte[64];
						gl.GetActiveAttrib(P,i,64,&Lng,&Sze,&T,pName);

						int Location	= gl.GetAttribLocation(P,pName);
						if(Location >= 0)
						{
							string AName	= new string(pName);
							Attributes[AName]	= new Attribute(Location,AName);
						}
						i++;
					}
				}
				
				int UCount	= 0;
				gl.GetProgramiv(P,ACTIVE_UNIFORMS,&UCount);
				if(UCount >= 0)
				{
					uint i	= 0;
					while(i < UCount)
					{
						int Lng,Sze;
						uint T;
						sbyte* pName	= stackalloc sbyte[64];
						gl.GetActiveUniform(P,i,64,&Lng,&Sze,&T,pName);

						int Location	= gl.GetUniformLocation(P,pName);
						if(Location >= 0)
						{
							string UName	= new string(pName);
							Uniforms[UName]	= new Uniform(gl,Location,UName);
						}

						i++;
					}
				}
			}

			void IMustExit.Exit(GL gl)
			{
				gl.DeleteProgram(P);
				P	= 0;
			}

			readonly VertexShader VShader;
			readonly FragmentShader FShader;
			public Program(CSGL gl,string VSFileName,string FSFileName) : base(gl)
			{
				StreamReader SR;

				SR	= new StreamReader(VSFileName);
				VShader	= new CSGL.VertexShader(gl,SR.ReadToEnd());
				SR.Close();

				SR	= new StreamReader(FSFileName);
				FShader	= new CSGL.FragmentShader(gl,SR.ReadToEnd());
				SR.Close();

				Initial();
			}

			public Program(CSGL gl,VertexShader VS,FragmentShader FS) : base(gl)
			{
				VShader	= VS;
				FShader	= FS;

				Initial();
			}
		}
	}
}