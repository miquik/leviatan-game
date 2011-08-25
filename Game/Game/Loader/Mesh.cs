using System;
using System.Threading;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using COLLADALoader;
using Game.Render;

namespace Game.Loader
{
	class Primitive : Render.Renderable
	{		
		Render.DrawMode DM;
		// readonly CSGL.VertexBuffer VB;
		public Primitive(primitive_element PElement)
		{
			switch(PElement.GetType().Name)
			{
				case "triangles":
					DM	= DrawMode.Triangles;
					break;

				case "trifans":
					DM	= DrawMode.TriangleFan;
					break;

				case "tristrips":
					DM	= DrawMode.TriangleStrip;
					break;

				case "lines":
					DM	= DrawMode.Lines;
					break;

				case "lineStrip":
					DM	= DrawMode.LineStrip;
					break;

				default:
					throw new NotImplementedException();
			}

////////////////////////////////////////////////////////////////

			int Length	= PElement.P.GetLength(1);

			// float[][] Data	= new float[PElement.Inputs.Count][];
			Vertex[] Data = new Vertex[PElement.Inputs.Count];			
			/*
			CSGL.VertexBuffer Vertex	= null;
			CSGL.NormalBuffer Normal	= null;
			CSGL.TexCoordBuffer TexCoord	= null;
			 */
			int i	= 0;
			foreach(input S in PElement.Inputs)
			{
				uint Stride	= ((source)S).Accessor.Stride;
				Data[i]	= new float[Length * Stride];
				
				source Src	= ((source)S);
				float[] ArrayData	= Src.Array.Values as float[];

				int j	= 0;
				while(j < Length)
				{
					uint DI	= PElement.P[i,j] * Stride;

					int k	= 0;
					while(k < Stride)
					{
						Data[i][(j * Stride) + k]	= ArrayData[DI + k];

						k++;
					}

					j++;
				}

				switch(S.Semantic)
				{
					case "VERTEX":
						Vertex	= new CSGL.VertexBuffer(gl,Stride,Data[i],Usage.StaticDraw);
						break;

					case "NORMAL":
						Normal	= new CSGL.NormalBuffer(gl,Data[i],Usage.StaticDraw);
						break;

					case "TEXCOORD":
						TexCoord	= new CSGL.TexCoordBuffer(gl,Stride,Data[i],Usage.StaticDraw);
						break;

					default:
						throw new NotImplementedException();
				}
				
				i++;
			}

			VB	= Vertex;
			VB.Normal	= Normal;
			VB.TexCoord	= TexCoord;
		}

		public void Draw()
		{
			// gl.Draw(DM,VB);
		}

		public void Dispose()
		{
			// VB.Dispose();
		}
	}

	public class Mesh
	{
		Primitive[] P;
		public Mesh(mesh MeshData)
		{
			P	= new Primitive[MeshData.Primitives.Count];

			uint i	= 0;
			foreach(primitive_element PE in MeshData.Primitives)
			{
				P[i]	= new Primitive(PE);
				i++;
			}
		}

		public void Draw()
		{
			uint i	= 0;
			while(i < P.Length)
			{
				P[i].Draw();
				i++;
			}
		}

		public void Dispose()
		{
			uint i	= 0;
			while(i < P.Length)
			{
				P[i].Dispose();
				i++;
			}
		}
	}
}
