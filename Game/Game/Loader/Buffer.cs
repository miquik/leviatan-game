using System;
using System.Collections.Generic;

using GLSpec;

namespace SharpGL
{
	public unsafe partial class CSGL
	{
		public void BindBuffer(BaseBuffer B)
		{
			gl.BindBuffer((uint)B.BType,B.B);
		}

		public void Draw(DrawMode Mode,IndexBuffer IB)
		{
			if(IB.Vertex == null || IB == null)
				throw new Exception("Must have Vertex and Index Buffer");

			VertexBuffer VB	= IB.Vertex;

			if(VB.Normal != null)
			{
				gl.EnableClientState(NORMAL_ARRAY);

				gl.BindBuffer((uint)VB.Normal.BType,VB.Normal.B);
				gl.NormalPointer((uint)VB.Normal.T,0,null);
			}
			else gl.DisableClientState(NORMAL_ARRAY);
			
			if(VB.TexCoord != null)
			{
				gl.EnableClientState(TEXTURE_COORD_ARRAY);

				gl.BindBuffer((uint)VB.TexCoord.BType,VB.TexCoord.B);
				gl.TexCoordPointer(VB.TexCoord.Size,(uint)VB.TexCoord.T,0,null);
			}
			else gl.DisableClientState(TEXTURE_COORD_ARRAY);
			
			if(VB != null)
			{
				gl.EnableClientState(VERTEX_ARRAY);

				gl.BindBuffer((uint)VB.BType,VB.B);
				gl.VertexPointer(VB.Size,(uint)VB.T,0,null);
			}
			else gl.DisableClientState(VERTEX_ARRAY);
			
			gl.EnableClientState(INDEX_ARRAY);

			gl.BindBuffer((uint)IB.BType,IB.B);
			gl.IndexPointer((uint)IB.T,0,null);
			gl.DrawElements((uint)Mode,IB.Count,(uint)IB.T,null);

			gl.DisableClientState(INDEX_ARRAY);
			gl.DisableClientState(VERTEX_ARRAY);
			gl.DisableClientState(NORMAL_ARRAY);
			gl.DisableClientState(TEXTURE_COORD_ARRAY);
		}
		public void Draw(DrawMode Mode,VertexBuffer VB)
		{
			if(VB == null)
				throw new Exception("Must have Vertex Buffer");

			if(VB.Normal != null)
			{
				gl.EnableClientState(NORMAL_ARRAY);

				gl.BindBuffer((uint)VB.Normal.BType,VB.Normal.B);
				gl.NormalPointer((uint)VB.Normal.T,0,null);
			}
			else gl.DisableClientState(NORMAL_ARRAY);
			
			if(VB.TexCoord != null)
			{
				gl.EnableClientState(TEXTURE_COORD_ARRAY);

				gl.BindBuffer((uint)VB.TexCoord.BType,VB.TexCoord.B);
				gl.TexCoordPointer(VB.TexCoord.Size,(uint)VB.TexCoord.T,0,null);
			}
			else gl.DisableClientState(TEXTURE_COORD_ARRAY);

			gl.EnableClientState(VERTEX_ARRAY);

			gl.BindBuffer((uint)VB.BType,VB.B);
			gl.VertexPointer(VB.Size,(uint)VB.T,0,null);

			gl.DrawArrays((uint)Mode,0,VB.Count);
			
			gl.DisableClientState(VERTEX_ARRAY);
			gl.DisableClientState(NORMAL_ARRAY);
			gl.DisableClientState(TEXTURE_COORD_ARRAY);
		}

		public abstract class BaseBuffer : GLObject,IMustInit,IMustExit
		{
			readonly Usage U;
			internal readonly DataType T;
			internal readonly ArrayBuffer BType;
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,sbyte[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.SByte;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,byte[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.Byte;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,short[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.Short;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,ushort[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.UShort;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,int[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.Int;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,uint[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.UInt;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,float[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.Float;

				Initial();
			}
			protected BaseBuffer(CSGL gl,ArrayBuffer BufferType,double[] BufferData,Usage Use) : base(gl)
			{
				U	= Use;
				Data	= BufferData;
				BType	= BufferType;
				T	= DataType.Double;

				Initial();
			}

			Array Data;
			internal uint B	= 0;
			void IMustInit.Init(GL gl)
			{
				if(B == 0)
				{
					fixed(uint* pB	= &B)
						gl.GenBuffers(1,pB);

					if(B == 0)
						throw new Exception("GLObject Gen fails");
				}

				gl.BindBuffer((uint)BType,B);
				switch(T)
				{
					case DataType.SByte :
						fixed(sbyte* pD	= Data as sbyte[])
							gl.BufferData((uint)BType,sizeof(sbyte) * Data.Length,pD,(uint)U);
						break;

					case DataType.Byte:
						fixed(byte* pD	= Data as byte[])
							gl.BufferData((uint)BType,sizeof(byte) * Data.Length,pD,(uint)U);
						break;

					case DataType.Short:
						fixed(short* pD	= Data as short[])
							gl.BufferData((uint)BType,sizeof(short) * Data.Length,pD,(uint)U);
						break;

					case DataType.UShort:
						fixed(ushort* pD	= Data as ushort[])
							gl.BufferData((uint)BType,sizeof(ushort) * Data.Length,pD,(uint)U);
						break;

					case DataType.Int:
						fixed(int* pD	= Data as int[])
							gl.BufferData((uint)BType,sizeof(int) * Data.Length,pD,(uint)U);
						break;

					case DataType.UInt:
						fixed(uint* pD	= Data as uint[])
							gl.BufferData((uint)BType,sizeof(uint) * Data.Length,pD,(uint)U);
						break;

					case DataType.Float:
						fixed(float* pD	= Data as float[])
							gl.BufferData((uint)BType,sizeof(float) * Data.Length,pD,(uint)U);
						break;

					case DataType.Double:
						fixed(double* pD	= Data as double[])
							gl.BufferData((uint)BType,sizeof(double) * Data.Length,pD,(uint)U);
						break;
				}

//				Data	= null;
			}

			void IMustExit.Exit(GL gl)
			{
				fixed(uint* pB	= &B)
					gl.DeleteBuffers(1,pB);
			}
		}

		////////////////////////////////////////////////////////////////

		public class IndexBuffer : BaseBuffer
		{
			public VertexBuffer Vertex;
			internal readonly int Count	= 0;
			public IndexBuffer(CSGL gl,byte[] BData,Usage U) : base(gl,ArrayBuffer.Element,BData,U)
			{
				Count	= BData.Length;
			}
			public IndexBuffer(CSGL gl,ushort[] BData,Usage U) : base(gl,ArrayBuffer.Element,BData,U)
			{
				Count	= BData.Length;
			}
			public IndexBuffer(CSGL gl,uint[] BData,Usage U) : base(gl,ArrayBuffer.Element,BData,U)
			{
				Count	= BData.Length;
			}
		}
		
		////////////////////////////////////////////////////////////////

		public class VertexBuffer : BaseBuffer
		{
			public NormalBuffer Normal	= null;
			public TexCoordBuffer TexCoord	= null;
			public readonly Dictionary<string,VertexAttribBuffer> Attributes	= new Dictionary<string,VertexAttribBuffer>();

			internal readonly int Size	= 0;
			internal readonly int Count	= 0;
			public VertexBuffer(CSGL gl,uint SizePerVertex,short[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
				Count	= BData.Length / Size;
			}
			public VertexBuffer(CSGL gl,uint SizePerVertex,int[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
				Count	= BData.Length / Size;
			}
			public VertexBuffer(CSGL gl,uint SizePerVertex,float[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
				Count	= BData.Length / Size;
			}
			public VertexBuffer(CSGL gl,uint SizePerVertex,double[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
				Count	= BData.Length / Size;
			}

			void Exit(GL gl)
			{
				if(TexCoord != null)
					TexCoord.Dispose();
				if(Normal != null)
					Normal.Dispose();
			}
		}
		
		////////////////////////////////////////////////////////////////

		public class TexCoordBuffer : BaseBuffer
		{
			internal readonly int Size	= 0;
			public TexCoordBuffer(CSGL gl,uint SizePerVertex,short[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
			}
			public TexCoordBuffer(CSGL gl,uint SizePerVertex,int[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
			}
			public TexCoordBuffer(CSGL gl,uint SizePerVertex,float[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
			}
			public TexCoordBuffer(CSGL gl,uint SizePerVertex,double[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				Size	= (int)SizePerVertex;
			}
		}

		public class NormalBuffer : BaseBuffer
		{
			public NormalBuffer(CSGL gl,sbyte[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
			}
			public NormalBuffer(CSGL gl,short[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
			}
			public NormalBuffer(CSGL gl,int[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
			}
			public NormalBuffer(CSGL gl,float[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
			}
			public NormalBuffer(CSGL gl,double[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
			}
		}

		public class VertexAttribBuffer : BaseBuffer
		{
			internal readonly int S	= 0;
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,sbyte[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,byte[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,ushort[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,short[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,uint[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,int[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,float[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
			public VertexAttribBuffer(CSGL gl,uint SizePerVertex,double[] BData,Usage U) : base(gl,ArrayBuffer.Data,BData,U)
			{
				S	= (int)SizePerVertex;
			}
		}
	}
}