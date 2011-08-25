
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game
{


	public class Woods
	{
		Vector3[] positions = new Vector3[400];
		int displayList=-1;
		Random r = new Random ();
		
		public Woods ()
		{
			
			int i = 0;
			float rx;
			float ry;
			float rz;
			
			for (int x = -10; x < 10; x++) 
			{
				for (int y = -10; y < 10; y++) 
				{
					rx = r.Next (-100, 100) / 200.0f;
					ry = r.Next (-100, 100) / 200.0f;
					rz = r.Next (-10, 10) / 20.0f;
					
					positions[i] = new Vector3 (x*2 + rx, rz, y*2 + ry);
					i++;
				}
			}																		
		}


		public void Draw ()
		{
			if(displayList==-1)			
			{
				displayList = GL.GenLists(1);
				GL.NewList(displayList,ListMode.Compile);			
				
				
				foreach (Vector3 p in positions) 
				{										
					GL_Pine.Draw (p, p.X);							
				}							
				
				GL.EndList();
			}	
			else
			{			
				GL.CallList(displayList);										
			}
			
		}
		
	}
}
