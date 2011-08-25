
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game
{
	public static class GL_Pine
	{
		public static void Draw(Vector3 pos,float angle)
		{
			GL.PushMatrix();
			
			GL.Translate (pos);
			GL.Rotate (angle, 0, 1, 0);
			
			//Assi
			//GL_Origin.Draw ();
			
			//Tronco			
			GL.Scale(0.5f,2,0.5f);
			GL_Pyramid.Draw(Color.Brown,Color.BurlyWood);			
			
			//Fronda
			GL.Translate(0,0.5f,0);
			GL.Scale(1,2,1);
			GL_Pyramid.Draw(Color.DarkGreen,Color.Green);
			
			GL.PopMatrix();
		}
	}
}
