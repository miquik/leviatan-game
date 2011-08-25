using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game.Render
{
	public class IndexBuffer : IDisposable
	{
		private static int _gSWIBuffer = 0;
		private int _vbo;
		private VBOType _useVbo;
		private BufferUsage _usage;
		private ushort[] _indicies;
		private int _idxNum;
		private DrawMode _dmode;

		public IndexBuffer (VBOType vbo, int numIndicies, BufferUsage usage, DrawMode dmode)
		{
			_vbo = -1;
			_useVbo = vbo;
			_usage = usage;
			_indicies = null;
			_idxNum = numIndicies;
			_dmode = dmode;
			
			int sizeInBytes = Marshal.SizeOf(typeof(ushort)) * numIndicies;
			if (_useVbo == VBOType.Core) {
				BufferUsageHint buh = BufferUsageHint.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC)
					buh = BufferUsageHint.DynamicDraw;
				GL.GenBuffers (1, out _vbo);
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, _vbo);
				GL.BufferData (BufferTarget.ElementArrayBuffer, new IntPtr (sizeInBytes), IntPtr.Zero, buh);
			} else if (_useVbo == VBOType.ARB) {
				BufferUsageArb buh = BufferUsageArb.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC)
					buh = BufferUsageArb.DynamicDraw;
				GL.Arb.GenBuffers (1, out _vbo);
				GL.Arb.BindBuffer (BufferTargetArb.ElementArrayBuffer, _vbo);
				GL.Arb.BufferData (BufferTargetArb.ElementArrayBuffer, new IntPtr (sizeInBytes), IntPtr.Zero, buh);
			} else {
				_vbo = _gSWIBuffer++;
				_indicies = new ushort[numIndicies];
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
		
		public DrawMode DrawMode {
			get { return _dmode; }
		}
		

		public ushort[] Indicies {
			get { return _indicies; }
		}

		public bool ReadData (int offset, int length, ushort[] data)
		{
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, _vbo);
				GL.GetBufferSubData<ushort> (BufferTarget.ElementArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ElementArrayBuffer, _vbo);
				GL.Arb.GetBufferSubData<ushort> (BufferTargetArb.ElementArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
			} else {
				for (int i = 0; i < _indicies.Length; i++) {
					// slow
					data[i] = _indicies[i];
				}
			}			
			return true;
		}

		public void WriteData (int offset, int length, ushort[] data)
		{
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, _vbo);
				if (offset == 0 && length == data.Length)
				{
					BufferUsageHint buh = BufferUsageHint.StaticDraw;
					if (_usage == BufferUsage.DYNAMIC)
						buh = BufferUsageHint.DynamicDraw;
					GL.BufferData<ushort> (BufferTarget.ElementArrayBuffer, new IntPtr (length), data, buh);
				} else {
					GL.BufferSubData<ushort> (BufferTarget.ElementArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
				}
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ElementArrayBuffer, _vbo);
				if (offset == 0 && length == data.Length)
				{
					BufferUsageArb buh = BufferUsageArb.StaticDraw;
					if (_usage == BufferUsage.DYNAMIC)
						buh = BufferUsageArb.DynamicDraw;
					GL.Arb.BufferData<ushort> (BufferTargetArb.ElementArrayBuffer, new IntPtr (length), data, buh);
				} else {
					GL.Arb.BufferSubData<ushort> (BufferTargetArb.ElementArrayBuffer, new IntPtr (offset), new IntPtr (length), data);
				}
				
			} else {
				for (int i = 0; i < _indicies.Length; i++) {
					// slow
					_indicies[i] = data[i];
				}
			}
		}
		
		private BeginMode ConvertDrawMode()
		{
			switch (_dmode)
			{
				case DrawMode.Lines : 
						return BeginMode.Lines; 
				case DrawMode.LineStrip: 
						return BeginMode.LineStrip; 
				case DrawMode.TriangleFan :
						return BeginMode.TriangleFan; 
				case DrawMode.Triangles : 
						return BeginMode.Triangles; 
				case DrawMode.TriangleStrip : 
						return BeginMode.TriangleStrip; 
			}
			return BeginMode.Triangles;
		}


		public void Render ()
		{
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, _vbo);
				GL.DrawElements(ConvertDrawMode(), _idxNum, DrawElementsType.UnsignedShort, 0); 
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, _vbo);
				GL.DrawElements(ConvertDrawMode(), _idxNum, DrawElementsType.UnsignedShort, 0); 
			} else 
			{
				GL.DrawElements<ushort>(ConvertDrawMode(), _idxNum, DrawElementsType.UnsignedShort, _indicies); 
			}
			
			/*
			// UNBIND
			if (_useVbo == VBOType.Core) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			} else if (_useVbo == VBOType.ARB) {
				GL.Arb.BindBuffer (BufferTargetArb.ArrayBuffer, 0);
			};*/			
		}
		
		public void Dispose()
		{
			GL.DeleteRenderbuffers(1, ref _vbo);
		}
	}
/*	
	
	public class IndexBuffer
	{
		private static int _gSWIBuffer = 0;
		private int _vbo;
		private VBOType _useVbo;
		private BufferUsage _usage;
		private ushort[] _indicies;
		private DrawMode _mode;
		
		public IndexBuffer ()
		{
			_vbo = uint.MaxValue;
			_useVbo = VBOType.No;
			_usage = BufferUsage.STATIC;
			_mode = BeginMode.Triangles;
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
		
		public uint[] Indicies
		{
			get { return _indicies;	}
		}
		
		public BeginMode BeginMode
		{
			get	{ return _mode;	}
			set	{ _mode = value;}
		}
				
		public uint Create(VBOType _type, BufferUsage _usage, uint idxnum)
		{
			if (_useVbo == VBOType.Core)
			{
				GL.GenBuffers(1, out _vbo);
			} else if (_useVbo == VBOType.ARB)
			{
				GL.Arb.GenBuffers(1, out _vbo);
			} else
			{
				_vbo = _gSWIBuffer++;
			}
			_indicies = new uint[idxnum];			                       
			return _vbo;
		}
		
		public void Render(BeginMode _mode)
		{
			if (_useVbo == VBOType.Core)
			{
				BufferUsageHint buh = BufferUsageHint.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC) buh = BufferUsageHint.DynamicDraw;
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vbo);
				GL.BufferData<uint>(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(uint) * _indicies.Length), 
				                    _indicies, buh);
				GL.DrawElements(_mode, 3, DrawElementsType.UnsignedInt, 0); 
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			} else if (_useVbo == VBOType.ARB)
			{
				BufferUsageArb buh = BufferUsageArb.StaticDraw;
				if (_usage == BufferUsage.DYNAMIC) buh = BufferUsageArb.DynamicDraw;
				GL.Arb.BindBuffer(BufferTargetArb.ElementArrayBuffer, _vbo);
				GL.Arb.BufferData<uint>(BufferTargetArb.ElementArrayBuffer, (IntPtr)(sizeof(uint) * _indicies.Length),
				                        _indicies, buh);
				GL.DrawElements(_mode, 3, DrawElementsType.UnsignedInt, 0); 				
				GL.Arb.BindBuffer(BufferTargetArb.ElementArrayBuffer, 0);
			} else 
			{
				// glDrawElements(GL_TRIANGLES, 3, GL_UNSIGNED_SHORT, BUFFER_OFFSET(0));   //The starting point of the IBO
				GL.DrawElements(_mode, 3, DrawElementsType.UnsignedInt, _indicies); 
			}
		}
		
	}
	*/
}

