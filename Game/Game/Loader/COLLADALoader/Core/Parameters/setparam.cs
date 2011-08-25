using System;
using System.Xml;

namespace COLLADALoader
{
	public class setparam : IHasRef,IHasChildNode
	{
		string Ref;
		string IHasRef.Ref
		{
			set{Ref	= value;}
		}

		object Value;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			int i	= 0;
			char[] Splitter	= {' ','\n'};
			switch(Child.Name)
			{
				case "bool":
					Value	= bool.Parse(Child.Value);
					break;

				case "bool2":
				case "bool3":
				case "bool4":
					string[] B	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
					Value	= new bool[B.Length];
					
					i	= 0;
					while(i < B.Length)
					{
						(Value as bool[])[i]	= bool.Parse(B[i]);
						i++;
					}
					break;

				case "int":
					Value	= int.Parse(Child.Value);
					break;

				case "int2":
				case "int3":
				case "int4":
					string[] I	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
					Value	= new int[I.Length];
					
					i	= 0;
					while(i < I.Length)
					{
						(Value as int[])[i]	= int.Parse(I[i]);
						i++;
					}
					break;

				case "float":
					Value	= float.Parse(Child.Value);
					break;

				case "float2":
				case "float3":
				case "float4":
					string[] F	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
					Value	= new float[F.Length];
					
					i	= 0;
					while(i < F.Length)
					{
						(Value as float[])[i]	= float.Parse(F[i]);
						i++;
					}
					break;

				case "float2x1":
				case "float2x2":
				case "float2x3":
				case "float2x4":
					F	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

					int Row	= 2;
					Value	= new float[Row,F.Length / Row];
					
					i	= 0;
					while(i < Row)
					{
						int j	= 0;
						while(Row < F.Length / Row)
						{
							(Value as float[,])[i,j]	= float.Parse(F[(i * Row) + j]);
							j++;
						}
						i++;
					}
					break;

				case "float3x1":
				case "float3x2":
				case "float3x3":
				case "float3x4":
					F	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

					Row	= 3;
					Value	= new float[Row,F.Length / Row];
					
					i	= 0;
					while(i < Row)
					{
						int j	= 0;
						while(Row < F.Length / Row)
						{
							(Value as float[,])[i,j]	= float.Parse(F[(i * Row) + j]);
							j++;
						}
						i++;
					}
					break;

				case "float4x1":
				case "float4x2":
				case "float4x3":
				case "float4x4":
					F	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

					Row	= 4;
					Value	= new float[Row,F.Length / Row];
					
					i	= 0;
					while(i < Row)
					{
						int j	= 0;
						while(Row < F.Length / Row)
						{
							(Value as float[,])[i,j]	= float.Parse(F[(i * Row) + j]);
							j++;
						}
						i++;
					}
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
