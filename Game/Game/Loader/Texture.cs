using System;
using System.Drawing;
using System.Drawing.Imaging;

using GLSpec;

namespace SharpGL
{
	public unsafe partial class CSGL
	{
		public void BindTexture(uint Index,Texture TT)
		{
			gl.ActiveTexture(TEXTURE0 + Index);
			if(TT != null)
				gl.BindTexture(TEXTURE_2D,TT.T);
			else gl.BindTexture(TEXTURE_2D,0);
		}

		public class Texture : GLObject,IMustInit,IMustExit
		{
			public readonly string FileName;
			public Texture(CSGL gl,string ImageFileName) : base(gl)
			{
				FileName	= ImageFileName;
				Initial();
			}

			internal uint T;
			public Rectangle Bound;
			void IMustInit.Init(GL gl)
			{
				Bitmap Bmp	= Bitmap.FromFile(FileName) as Bitmap;

				fixed(uint* Tex	= &T)
					gl.GenTextures(1,Tex);

				Bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

				Bound	= new Rectangle(0,0,Bmp.Width,Bmp.Height);
				BitmapData BData	= Bmp.LockBits(Bound,ImageLockMode.ReadOnly,Bmp.PixelFormat);

				if(BData.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
				{
					gl.BindTexture(TEXTURE_2D,T);
					gl.TexImage2D(TEXTURE_2D,0,RGB,Bmp.Width,Bmp.Height
						,0,BGR,UNSIGNED_BYTE,(byte*)BData.Scan0);

					gl.TexParameteri(TEXTURE_2D,TEXTURE_MIN_FILTER,(int)NEAREST);
				}

				Bmp.UnlockBits(BData);
			}

			void IMustExit.Exit(GL gl)
			{
				fixed(uint* Tex	= &T)
					gl.DeleteTextures(1,Tex);
			}
		}
	}
}