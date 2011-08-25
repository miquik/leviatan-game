
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game
{
	public static class GL_Pyramid
	{						
		public static void Draw (Color baseColor,Color topColor)
		{
			float a_x = 0;
			float a_y = 0;
			float a_z = 1f;
			
			float b_x = 0.866f;
			float b_y = 0;
			float b_z = -0.5f;
			
			float c_x = -0.866f;
			float c_y = 0;
			float c_z = -0.5f;
			
			float d_x = 0;
			float d_y = 1;
			float d_z = 0;
			
			Vector3 a =new Vector3(a_x,a_y,a_z);
			Vector3 b =new Vector3(b_x,b_y,b_z);
			Vector3 c =new Vector3(c_x,c_y,c_z);
			Vector3 d =new Vector3(d_x,d_y,d_z);
			
			GL.Begin (BeginMode.Triangles);			
			
			//Base
			GL.Color3 (baseColor);			
			GL.Vertex3 (a);			
			GL.Normal3(a);
			GL.Vertex3 (c);
			GL.Normal3(c);
			GL.Vertex3 (b);				
			GL.Normal3(b);
			//GL.Normal3(Vector3.Normalize(Vector3.Cross(c-a,b-a)));																									
			
			//F1			
			GL.Color3 (baseColor);				
			GL.Vertex3 (a);
			GL.Normal3(a);
			GL.Vertex3 (b);
			GL.Normal3(b);
			GL.Color3 (topColor);
			GL.Vertex3 (d);	
			GL.Normal3(d);
			//GL.Normal3(Vector3.Normalize(Vector3.Cross(b-a,d-a)));											
			
			//F2			
			GL.Color3 (baseColor);			
			GL.Vertex3 (b);
			GL.Normal3(b);
			GL.Vertex3 (c);
			GL.Color3 (topColor);
			GL.Normal3(c);
			GL.Vertex3 (d);
			GL.Normal3(d);
			//GL.Normal3(Vector3.Normalize(Vector3.Cross(c-b,d-b)));											
			
			//F3			
			GL.Color3 (baseColor);			
			GL.Vertex3 (c);
			GL.Normal3(c);
			GL.Vertex3 (a);
			GL.Normal3(a);
			GL.Color3 (topColor);
			GL.Vertex3 (d);
			GL.Normal3(d);
			//GL.Normal3(Vector3.Normalize(Vector3.Cross(a-c,d-c)));											
			
			GL.End ();
			
		}
	}
}
