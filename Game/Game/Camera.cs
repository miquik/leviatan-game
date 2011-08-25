
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Game
{

	public class Camera
	{

		Matrix4 cameraMatrix;

	
		public Camera (Vector3 position)
		{				
			cameraMatrix = Matrix4.LookAt(new Vector3(10,10,10),new Vector3(),Vector3.UnitY);
		}



		public void KeyboardMove (KeyboardDevice Keyboard,FrameEventArgs e)
		{
			float speed = 1f;
			float time;
			time = (float)e.Time;
			speed *=time;
			
			
			if (Keyboard[Key.T]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateTranslation (0f, 0f, speed*10));
			}
			
			if (Keyboard[Key.G]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateTranslation (0f, 0f, -speed*10));
			}
			
			
			if (Keyboard[Key.W]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateRotationX (speed));
			}
			
			if (Keyboard[Key.S]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateRotationX (-speed));
			}
			
			if (Keyboard[Key.D]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateRotationY (speed));
			}
			
			if (Keyboard[Key.A]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateRotationY (-speed));
			}
			
			if (Keyboard[Key.E]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateRotationZ (speed));
			}
			
			if (Keyboard[Key.Q]) {
				cameraMatrix = Matrix4.Mult (cameraMatrix, Matrix4.CreateRotationZ (-speed));
			}												
		}

		public void Draw ()
		{													
			GL.MatrixMode (MatrixMode.Modelview);						
			GL.LoadMatrix (ref cameraMatrix);
		}
		
	}
}
