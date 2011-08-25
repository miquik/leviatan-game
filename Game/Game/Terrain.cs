
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game
{

	public class Terrain
	{
		Vector3[] positions = new Vector3[100];
		//Vector3[] heights = new Vector3[100];
		float[,] pos = new float[100, 100];
		int displayList = -1;
		Random r = new Random ();

		public Terrain ()
		{
			int i = 0;
			float rx;
			float ry;
			float rz;
			
			for (int x = 0; x < 100; x++) {
				for (int y = 0; y < 100; y++) {
					pos[x, y] =(float) (x*x*y*y)/2000000.0f;
				}			}
			
			
			for (int x = -5; x < 5; x++) {
				for (int y = -5; y < 5; y++) {
					rx = r.Next (-100, 100) / 200f;
					ry = r.Next (-100, 100) / 200f;
					rz = r.Next (-10, 10) / 20f;
					
					positions[i] = new Vector3 (x * 2 + rx, rz, y * 2 + ry);
					i++;
				}
			}
		}


		public void Draw ()
		{
			if (displayList == -1) {
				
				displayList = GL.GenLists (1);
				GL.NewList (displayList, ListMode.Compile);
				
				GL.PushMatrix();			
				GL.Translate (-50,-1,-50);							
				
				GL.Color3 (Color.DarkOliveGreen);
				Vector3 a,b,c,d;
				
				GL.Begin (BeginMode.Triangles);
				for (int y = 0; y < 99; y += 1) {
					for (int x = 0; x < 99; x += 1) {
						
						a=new Vector3(x, pos[x, y], y);
						b=new Vector3(x, pos[x, y + 1], y + 1);
						c=new Vector3(x+1, pos[x+1, y ], y );
						d=new Vector3(x+1, pos[x+1, y+1 ], y+1 );						
						
						GL.Vertex3 (a);
						GL.Vertex3 (b);
						GL.Vertex3 (c);
						GL.Normal3(Vector3.Normalize(Vector3.Cross(b-a,c-a)));											
						
						GL.Vertex3 (c);
						GL.Vertex3 (b);
						GL.Vertex3 (d);
						GL.Normal3(Vector3.Normalize(Vector3.Cross(b-c,d-c)));						
					}
				}
				GL.End ();	
				
				GL.LineWidth(3.0f);
				
				GL.Color3(Color.Black);
				GL.Begin (BeginMode.Lines);
				for (int y = 0; y < 99; y += 1) {
					for (int x = 0; x < 99; x += 1) {
						
						a=new Vector3(x, pos[x, y], y);
						b=new Vector3(x, pos[x, y + 1], y + 1);
						c=new Vector3(x+1, pos[x+1, y ], y );
						d=new Vector3(x+1, pos[x+1, y+1 ], y+1 );						
						
						GL.Vertex3 (a);
						GL.Vertex3 (b);
						GL.Vertex3 (c);
						GL.Normal3(Vector3.Normalize(Vector3.Cross(b-a,c-a)));											
						
						GL.Vertex3 (c);
						GL.Vertex3 (b);
						GL.Vertex3 (d);
						GL.Normal3(Vector3.Normalize(Vector3.Cross(b-c,d-c)));						
					}
				}
				GL.End ();		
				
				
				GL.PopMatrix();			
				
				GL.EndList ();
			} else {
				GL.CallList (displayList);
			}
			
		}
	}
}
