
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
//using Meshomatic;


namespace Game
{
	public class Dummy
	{

		public Vector3 pos { get; set; }
		public float angle { get; set; }
		Meshomatic.MeshData m;
		int displayList=-1;

		public Dummy ()
		{
			pos = new Vector3 (10, 10, 10);
			
			m = new Meshomatic.ObjLoader ().LoadFile ("monk.obj");
			m = new Meshomatic.ObjLoader ().LoadFile ("cube.obj");
			m = new Meshomatic.ObjLoader ().LoadFile ("pyramid.obj");
			
		}

		public void KeyboardMove (KeyboardDevice Keyboard, FrameEventArgs e)
		{
			
			if (Keyboard[Key.Right])
				pos += new Vector3 (-0.1f, 0, 0);
			
			if (Keyboard[Key.Left])
				pos += new Vector3 (0.1f, 0, 0);
			
			if (Keyboard[Key.Up])
				pos += new Vector3 (0, 0, -0.1f);
			
			if (Keyboard[Key.Down])
				pos += new Vector3 (0, 0, 0.1f);
			
			if (Keyboard[Key.PageUp])
			{
				pos += new Vector3 (0, 0.1f,0.0f);
				angle += 10f;
			}
			
			if (Keyboard[Key.PageDown])
			{
				pos += new Vector3 (0, -0.1f, 0.0f);
				angle -= 10f;
			}
		}


		public void Draw ()
		{
			GL.PushMatrix ();
			GL.Translate (pos);
			GL.Rotate (angle, 0, 1, 0);									
			
			if (displayList == -1) {
				displayList = GL.GenLists (1);
				GL.NewList (displayList, ListMode.Compile);
				
				//Slow
				GL.Begin (BeginMode.Triangles);
				foreach (Meshomatic.Tri t in m.Tris) {
					foreach (Meshomatic.Point p in t.Points ()) {
						Meshomatic.Vector3 v = m.Vertices[p.Vertex];
						Meshomatic.Vector3 n = m.Normals[p.Normal];
						Meshomatic.Vector2 tc = m.TexCoords[p.TexCoord];
						GL.Normal3 (n.X, n.Y, n.Z);
						GL.TexCoord2 (tc.X, 1 - tc.Y);
						GL.Vertex3 (v.X, v.Y, v.Z);
					}
				}
				GL.End ();
				
				GL.EndList ();
				
			} else {
				GL.CallList (displayList);
			}										
			
			
			//Assi
			GL_Origin.Draw ();
			
			GL.PopMatrix ();
			
			
		}
		
		
		
		
	}
}
