using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game.Render
{
	public struct Vertex
	{
		// Coord : 0
		public Vector3 Position;
		// Normal: 12
		public Vector3 Normal;
		// Color: 24
		public Color4 Color;
		// Texture: 40
		public Vector2 UV;
	}

	public enum BufferUsage
	{
		STATIC = 1,
		DYNAMIC = 2
	}

	public enum DrawMode
	{
		Triangles = 1,
		TriangleFan = 2,
		TriangleStrip = 3,
		Lines = 4,
		LineStrip = 5
	}

	public class VertexBuffer : IDisposable
	{
		private static int _gSWVBuffer = 0;
		private int _vbo;
		private VBOType _useVbo;
		private BufferUsage _usage;
		private Vertex[] _vertices;
		private int _vertsNum;

		public VertexBuffer (VBOType vbo, int numVertices, BufferUsage usage)
		{
			_vbo = -1;
			_useVbo = vbo;
			_usage = usage;
			_vertices = null;
			_vertsNum = numVertices;
			
			int sizeInBytes = Marshal.SizeOf (typeof(Vertex)) * numVertices;
			if (_useVbo == VBOType.Core) {
				BufferUsageHint buh = BufferUsageHint.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC)
					buh = BufferUsageHint.DynamicDraw;
				GL.GenBuffers (1, out _vbo);
				GL.BindBuffer (BufferTarget.ArrayBuffer, _vbo);
				GL.BufferData (BufferTarget.ArrayBuffer, new IntPtr (sizeInBytes), IntPtr.Zero, buh);
			} else if (_useVbo == VBOType.ARB) {
				BufferUsageArb buh = BufferUsageArb.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC)
					buh = BufferUsageArb.DynamicDraw;
				GL.Arb.GenBuffers (1, out _vbo);
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, _vbo);
				GL.Arb.BufferData (BufferTargetArb.ArrayBuffer, new IntPtr (sizeInBytes), IntPtr.Zero, buh);
			} else {
				_vbo = _gSWVBuffer++;
				_vertices = new Vertex[numVertices];
			}
		}

		public int BufferID {
			get { return _vbo; }
		}

		public VBOType VBO {
			get { return _useVbo; }
		}

		public BufferUsage Usage {
			get { return _usage; }
		}

		public Vertex[] Vertices {
			get { return _vertices; }
		}

		public bool ReadData (int offset, int length, Vertex[] data)
		{
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, _vbo);
				GL.GetBufferSubData<Vertex> (BufferTarget.ArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, _vbo);
				GL.Arb.GetBufferSubData<Vertex> (BufferTargetArb.ArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
			} else {
				for (int i = 0; i < _vertices.Length; i++) {
					// slow
					data[i] = _vertices[i];
				}
			}			
			return true;
		}

		public void WriteData (int offset, int length, Vertex[] data)
		{
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, _vbo);
				if (offset == 0 && length == data.Length)
				{
					BufferUsageHint buh = BufferUsageHint.StaticDraw;
					if (_usage == BufferUsage.DYNAMIC)
						buh = BufferUsageHint.DynamicDraw;
					GL.BufferData<Vertex> (BufferTarget.ArrayBuffer, new IntPtr (length), data, buh);
				} else {
					GL.BufferSubData<Vertex> (BufferTarget.ArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
				}
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, _vbo);
				if (offset == 0 && length == data.Length)
				{
					BufferUsageArb buh = BufferUsageArb.StaticDraw;
					if (_usage == BufferUsage.DYNAMIC)
						buh = BufferUsageArb.DynamicDraw;
					GL.Arb.BufferData<Vertex> (BufferTargetArb.ArrayBuffer, new IntPtr (length), data, buh);
				} else {
					GL.Arb.BufferSubData<Vertex> (BufferTargetArb.ArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
				}
				
			} else {
				for (int i = 0; i < _vertices.Length; i++) {
					// slow
					_vertices[i] = data[i];
				}
			}
		}


		public void Bind ()
		{
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, _vbo);
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, _vbo);
			};
			
			if (_useVbo != VBOType.No) {
				GL.TexCoordPointer (2, TexCoordPointerType.Float, Marshal.SizeOf (typeof(Vertex)), new IntPtr (40));
				GL.NormalPointer (NormalPointerType.Float, Marshal.SizeOf (typeof(Vertex)), new IntPtr (12));
				GL.ColorPointer (4, ColorPointerType.Float, Marshal.SizeOf (typeof(Vertex)), new IntPtr (24));
				GL.VertexPointer (3, VertexPointerType.Float, Marshal.SizeOf (typeof(Vertex)), new IntPtr (0));
			} else {
				
				GL.VertexPointer (3, VertexPointerType.Float, BlittableValueType.StrideOf (typeof(Vertex)), ref _vertices[0].Position);
				GL.NormalPointer (NormalPointerType.Float, BlittableValueType.StrideOf (typeof(Vertex)), ref _vertices[0].Normal);
				GL.ColorPointer (4, ColorPointerType.Float, BlittableValueType.StrideOf (typeof(Vertex)), ref _vertices[0].Color);
				GL.TexCoordPointer (2, TexCoordPointerType.Float, BlittableValueType.StrideOf (typeof(Vertex)), ref _vertices[0].UV);
			}
		}
			/*
			// UNBIND
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, 0);
			};*/	
		
		public void Dispose()
		{
			GL.DeleteRenderbuffers(1, ref _vbo);
		}
		
	}
	
	/*
	public class VertexBuffer
	{
		private static uint _gSWVBuffer = 0;
		private VBOType _useVbo;
		private uint _vbo;
		private BufferUsage _usage;
		private Vertex[] _vertices;
		
		public VertexBuffer()			
		{			
			_vbo = uint.MaxValue;
			_useVbo = VBOType.No;
			_usage = BufferUsage.STATIC;
		}	
		
		public uint BufferID
		{
			get	{ return _vbo; }
		}
		
		public VBOType VBO
		{
			get { return _useVbo; }
		}
		
		public BufferUsage Usage
		{
			get { return _usage;	}			
		}
		
		public Vertex[] Vertices
		{
			get { return _vertices;	}
		}
		
		
		public uint Create(VBOType _type, BufferUsage _usage, uint vertsnum)
		{
			if (_useVbo == VBOType.Core)
			{
				GL.GenBuffers(1, out _vbo);
			} else if (_useVbo == VBOType.ARB)
			{
				GL.Arb.GenBuffers(1, out _vbo);
			} else
			{
				_vbo = _gSWVBuffer++;
			}
			_vertices = new Vertex[vertsnum];			                       
			return _vbo;
		}
		
		public void Bind()
		{
			if (_useVbo == VBOType.Core)
			{
				BufferUsageHint buh = BufferUsageHint.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC) buh = BufferUsageHint.DynamicDraw;
				GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
				GL.BufferData<Vertex>(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf(typeof(Vertex))* _vertices.Length), 
				                      _vertices, buh);
			} else if (_useVbo == VBOType.ARB)
			{
				BufferUsageArb buh = BufferUsageArb.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC) buh = BufferUsageArb.DynamicDraw;
				GL.Arb.BindBuffer(BufferTargetArb.ArrayBuffer, _vbo);
				GL.Arb.BufferData<Vertex>(BufferTargetArb.ArrayBuffer, (IntPtr)(Marshal.SizeOf(typeof(Vertex))* _vertices.Length),
				                          _vertices, buh);
			};
			
			if (_useVbo != VBOType.No)
			{
				GL.TexCoordPointer(2, TexCoordPointerType.Float, Marshal.SizeOf(typeof(Vertex)), new IntPtr(40));
				GL.NormalPointer(NormalPointerType.Float, Marshal.SizeOf(typeof(Vertex)), new IntPtr(12));
				GL.ColorPointer(4, ColorPointerType.Float, Marshal.SizeOf(typeof(Vertex)), new IntPtr(24));
				GL.VertexPointer(3, VertexPointerType.Float, Marshal.SizeOf(typeof(Vertex)), new IntPtr(0));				
			}
			else
			{
			*/	
	/*
				// UFFA!!! unsafe code?!?! Non si può fare meglio di così??
				unsafe
				{
    				fixed (float* pvertices = _vertices)
    				{
						int sV3 = sizeof(Vector3);
						int sV4 = sizeof(Color4);
						GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(vertices), pvertices);										
						GL.NormalPointer(NormalPointerType.Float, BlittableValueType.StrideOf(vertices), pvertices + sV3);
						GL.ColorPointer(4, ColorPointerType.Float, BlittableValueType.StrideOf(vertices), pvertices + sV3*2);
						GL.TexCoordPointer(2, TexCoordPointerType.Float, BlittableValueType.StrideOf(vertices), pvertices + sV3*2 + sV4);						
				    }
				}
				*/	
	/*
				GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(typeof(Vertex)), ref _vertices[0].Position);										
				GL.NormalPointer(NormalPointerType.Float, BlittableValueType.StrideOf(typeof(Vertex)), ref _vertices[0].Normal);
				GL.ColorPointer(4, ColorPointerType.Float, BlittableValueType.StrideOf(typeof(Vertex)), ref _vertices[0].Color);
				GL.TexCoordPointer(2, TexCoordPointerType.Float, BlittableValueType.StrideOf(typeof(Vertex)), ref _vertices[0].UV);				
			}						
			
			// UNBIND
			if (_useVbo == VBOType.Core)
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			} else if (_useVbo == VBOType.ARB)
			{
				GL.Arb.BindBuffer(BufferTargetArb.ArrayBuffer, 0);
			};			
		}				
		
	}
	*/	
	
}

