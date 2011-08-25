// Released to the public domain. Use, modify and relicense at will.

using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Input;
using Jitter;

namespace Game
{
	class Game : GameWindow
	{		
		Camera camera=new Camera(new Vector3 (10, 10, 10));
		Dummy dummy = new Dummy ();	
		Dummy dummy2 = new Dummy ();	
		
		Woods woods=new Woods();					
		Terrain terrain=new Terrain();		

		/// <summary>Creates a window with the specified title.</summary>
		public Game () : base(800, 600, GraphicsMode.Default, "OpenTK")
		{
			VSync = VSyncMode.On;
			//this.WindowState=WindowState.Maximized;
			//this.WindowBorder=WindowBorder.Hidden;
		}

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			
			GL.ClearColor (0.1f, 0.2f, 0.5f, 0f);
			GL.Enable (EnableCap.DepthTest);						
			GL.Enable (EnableCap.CullFace);
			
			
			//LIGHT
			float[] light_ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light_diffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
			//float[] light_specular = { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light_position = {0.0f,0.0f,0.0f,0.0f };

            GL.ShadeModel(ShadingModel.Smooth);
			//GL.ShadeModel(ShadingModel.Flat);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Light(LightName.Light1, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light1, LightParameter.Diffuse, light_diffuse);
			//GL.Light(LightName.Light1, LightParameter.Specular, light_specular);
            GL.Light(LightName.Light1, LightParameter.Position, light_position);

            GL.Enable(EnableCap.Light1);
            GL.Enable(EnableCap.ColorMaterial);
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also
		/// a good place to set up your projection matrix (which probably changes
		/// along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize (EventArgs e)
		{
			base.OnResize (e);
			
			GL.Viewport (ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
			
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView ((float)Math.PI / 4, Width / (float)Height, 1f, 640f);
			GL.MatrixMode (MatrixMode.Projection);
			GL.LoadMatrix (ref projection);
		}

		/// <summary>
		/// Called when it is time to setup the next frame. Add you game logic here.
		/// </summary>
		/// <param name="e">Contains timing information for framerate independent logic.</param>
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);
			
			//this.Title="Time: "+e.Time;					
			
			if (Keyboard[Key.Escape])
				Exit ();
			
			dummy.KeyboardMove (Keyboard,e);	
			camera.KeyboardMove(Keyboard,e);	
			
		}

		/// <summary>
		/// Called when it is time to render the next frame. Add your rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			
			//Light 1
			float[] light_position = { (float)dummy.pos.X,(float)dummy.pos.Y,(float)dummy.pos.Z,0.0f };
			GL.Light(LightName.Light1, LightParameter.Position, light_position);
			
			//Clear all
			GL.Clear (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			
			//Set Camera
			camera.Draw();							
			
			//Draw Objects
			GL_Origin.Draw ();	
			dummy.Draw();
			dummy2.Draw();
			
			woods.Draw();
			terrain.Draw();
			
							
			//swap
			SwapBuffers ();
		}
	}
}
