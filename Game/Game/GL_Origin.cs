
using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game
{
	public static class GL_Origin
	{			
		public static void Draw()
		{
			GL.Begin(BeginMode.Lines);
			GL.Color3(1.0f, 0.0f, 0.0f);GL.Vertex3(Vector3.Zero);
			GL.Color3(1.0f, 0.0f, 0.0f);GL.Vertex3(Vector3.UnitX);				
			GL.End();
			
			GL.Begin(BeginMode.Lines);
			GL.Color3(0.0f, 1.0f, 0.0f);GL.Vertex3(Vector3.Zero);
			GL.Color3(0.0f, 1.0f, 0.0f);GL.Vertex3(Vector3.UnitY);				
			GL.End();
			
			GL.Begin(BeginMode.Lines);
			GL.Color3(0.0f, 0.0f, 1.0f);GL.Vertex3(Vector3.Zero);
			GL.Color3(0.0f, 0.0f, 1.0f);GL.Vertex3(Vector3.UnitZ);				
			GL.End();
		}
	}
}
