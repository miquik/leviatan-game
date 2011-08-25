using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game.Render
{
	public class Renderable
	{
		private static uint _gRendereableIDs = 0;
			
		private uint _id;		
		private Material _mat;
		private VertexBuffer _vb;
		private IndexBuffer _ib;
		
		public Renderable()
		{
			_id = _gRendereableIDs++;
			_mat = null;
			_vb = null;
			_ib = null;
			// _mat = new Material("Mat");
			// _vb = new VertexBuffer();
			// _ib = new IndexBuffer();
		}
		
		public Material Material
		{
			get	{	return _mat;	}
		}
				
		public uint ID
		{
			get	{	return _id;	}			
		}
		
		public VertexBuffer VB
		{
			get	{	return _vb;	}
		}
		
		public IndexBuffer IB
		{
			get	{	return _ib;	}
		}
		
		public virtual void BeforeRender(int curpass)
		{
			// setup material
		}
		
		public virtual void AfterRender(int curpass)
		{
			// do nothing?
		}
		
		public virtual void RenderStageOne(int curpass)
		{
		}
		
	}
}

