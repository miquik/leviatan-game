using System;
using OpenTK;
using OpenTK.Graphics;

namespace Game.Render
{
	public class Material
	{
        public Color4 DiffuseColor;
        public Color4 AmbientColor;
        public Color4 SpecularColor;
        public float Shininess;
		
        private string _name;
        
		private bool _useSpecularColor;
        private int _idTexture;
		private float _transp;
		
		public Material(string name)
		{
			_name = name;
			DiffuseColor = new Color4(0, 0, 0, 1);
			AmbientColor = new Color4(0, 0, 0, 1);
			SpecularColor = new Color4(0, 0, 0, 1);
			Shininess = 0.0f;
			_useSpecularColor = false;
			_idTexture = -1;
			_transp = 0;
		}
		
		public string Name
		{
			get	{	return _name;	}			
			set	{	_name = value;	}
		}
		
		public bool UseTexture
		{
			get	{	return _idTexture != -1;	}
		}
		
		public bool UseSpecularColor
		{
			get	{ return _useSpecularColor;	}			
		}
		
		public int TextureID
		{
			get	{ return _idTexture;	}
			set	{ _idTexture = value;	}
		}
		
		public float Transparency
		{
			get	{ return _transp;	}
			set	{ _transp = value;	}
		}
	}
}

