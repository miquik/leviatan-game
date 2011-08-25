using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game.Render
{
	public enum VBOType
	{
		No = 0,
		Core = 1,
		ARB = 2
	}
		
	public class RenderEngine
	{
   		private static RenderEngine instance;
		
		private VBOType _vbotype;
		private List<int> _textures;
		
		public RenderEngine()
		{
			// check if VBO are supported
			string vboCoreStr = "GL_vertex_buffer_object";
			string vboARBStr = "GL_ARB_vertex_buffer_object";
			
			_vbotype = VBOType.No;
			string exts = GL.GetString(StringName.Extensions);
			if (exts.Contains(vboCoreStr))
			{
				_vbotype = VBOType.Core;				
			} else if (exts.Contains(vboARBStr))
			{
				_vbotype = VBOType.ARB;
			}
			
		}
		
   		public static RenderEngine Instance
   		{
      		get 
      		{
         		if (instance == null)
         		{
            		instance = new RenderEngine();
         		}
         		return instance;
      		}
		}
		
		public VBOType VBO
		{
			get	{	return _vbotype;	}
		}
		
		// LoadTextureFromFile
		public int LoadTexture(string filename)
		{
			int genTex = -1;
			//check if the file exists
			if (System.IO.File.Exists(filename))
			{
				Image img = Image.FromFile(filename);
    			//make a bitmap out of the file on the disk
    			Bitmap TextureBitmap = new Bitmap(img);
    			//get the data out of the bitmap
    			System.Drawing.Imaging.BitmapData TextureData = 
				TextureBitmap.LockBits(
            			new System.Drawing.Rectangle(0,0,TextureBitmap.Width,TextureBitmap.Height),
            			System.Drawing.Imaging.ImageLockMode.ReadOnly,
            			System.Drawing.Imaging.PixelFormat.Format24bppRgb
        		);
 
    			//Code to get the data to the OpenGL Driver
 
    			//generate one texture and put its ID number into the "Texture" variable
    			GL.GenTextures(1,out genTex);
    			//tell OpenGL that this is a 2D texture
    			GL.BindTexture(TextureTarget.Texture2D, genTex);
 
    			//the following code sets certian parameters for the texture
    			GL.TexEnv(TextureEnvTarget.TextureEnv,
				TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate);
    			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.LinearMipmapLinear);
    			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);				
 /*
			    //load the data by telling OpenGL to build mipmaps out of the bitmap data
    			Glu.Build2DMipmap(TextureTarget.Texture2D,
        					(int)PixelInternalFormat.Three, TextureBitmap.Width, TextureBitmap.Height,
							PixelFormat.Bgr, PixelType.UnsignedByte,
							TextureData.Scan0);
 */
    			//free the bitmap data (we dont need it anymore because it has been passed to the OpenGL driver
    			TextureBitmap.UnlockBits(TextureData);
				_textures.Add(genTex);
 			} else 
			{
				// ERROR FILE NOT FOUND
			}
			return genTex;
		}
	}
}

