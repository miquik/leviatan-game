using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using COLLADALoader;


namespace Game.Loader
{
	public interface IDrawable
	{
		void Draw();
	}

	public class Camera : IDrawable
	{
		public Camera(camera Cam)
		{
		}

		public void Draw()
		{
		}
	}
	
	public class Controller : IDrawable
	{
		public Controller(controller Con)
		{
		}
		public void Draw()
		{
		}
	}
	
	public class Geometry : IDrawable,IDisposable
	{
		public Mesh M;
		public Geometry(geometry G)
		{
			if(G.Geometric is mesh)
				M	= new Mesh(G.Geometric as COLLADALoader.mesh);
			else throw new NotImplementedException();
		}

		public void Draw()
		{
			M.Draw();
		}

		public void Dispose()
		{
			M.Dispose();
		}
	}
	
	public class Light : IDrawable
	{
		readonly LightSource light;
		public Light(light L)
		{
			light	= L.LightSource;
		}
		
		public void Draw()
		{		
			/*
			gl.Light0.Enabled	= true;
			if(light is ambient)
				gl.Light0.Ambient	= light.Color;
			
			else if(light is directional)
			{
				gl.Light0.IsDirectional	= true;
				gl.Light0.Direction	= new Vector3(0,0,1);
				gl.Light0.Diffuse	= light.Color;
			}
			
			else if(light is point)
			{
				gl.Light0.IsDirectional	= false;
				gl.Light0.Position	= new Vector3(0,0,1);
				gl.Light0.Diffuse	= light.Color;
				
				point LP	= light as point;
				gl.Light0.ConstantAttenuation	= LP.ConstantAtten;
				gl.Light0.LinearAttenuation	= LP.LinearAtten;
				gl.Light0.QuadraticAttenuation	= LP.QuadraticAtten;
			}
			
			else if(light is spot)
			{
				gl.Light0.IsDirectional	= true;
				gl.Light0.Direction	= new Vector3(0,0,1);
				gl.Light0.Diffuse	= light.Color;
				
				spot SP	= light as spot;
				gl.Light0.ConstantAttenuation	= SP.ConstantAtten;
				gl.Light0.LinearAttenuation	= SP.LinearAtten;
				gl.Light0.QuadraticAttenuation	= SP.QuadraticAtten;

				gl.Light0.SpotCutoff	= SP.FallOffAngle;
				gl.Light0.SpotExponent	= SP.FallOffExponent;
			}
			*/
		}
	}

	public class Transformation
	{
		public static void Transform(ref Matrix4 Mtrx,transformation_element T)
		{
			switch(T.GetType().Name)
			{
				case "lookat":
					lookat L	= T as lookat;
					Mtrx	= Matrix4.LookAt(L.Position, L.Target, L.UpVector);
					break;
					
				case "matrix":
					Mtrx	= (T as matrix).M;
					break;
					
				case "rotate":
					rotate R	= T as rotate;
					Mtrx = Matrix4.Rotate(R.AngleVector, R.Degree);
					break;
					
				case "scale":
					Mtrx = Matrix4.Scale((T as scale).Size);
					break;
					
				case "skew":
					throw new NotImplementedException();
					
				case "translate":
					Mtrx = Matrix4.CreateTranslation((T as translate).Target);
					break;

				default:
					throw new Exception("Invalid TransformationElement");
			}
		}
	}

	public class Node : List<IDrawable>,IDrawable,IDisposable
	{
		public Matrix4 Mtrx	= Matrix4.Identity;
		public Node(node Node)
		{
			foreach(IElement N in Node.ChildNodes)
			{
				if(N is transformation_element)
					Transformation.Transform(ref Mtrx,N as transformation_element);					

				else if(N is instance_camera)
					base.Add(new Camera(N as instance_camera));
				
				else if(N is instance_controller)
					base.Add(new Controller(N as instance_controller));
				
				else if(N is instance_light)
					base.Add(new Light(N as instance_light));
				
				else if(N is instance_geometry)
					base.Add(new Geometry(N as instance_geometry));
			
				////////////////////////////////////////////////////////////////

				else if(N is instance_node)
					base.Add(new Node(N as instance_node));

				else if(N is node)
					base.Add(new Node(N as node));

				else throw new Exception("Invalid ChildNode");
			}
		}
		
		public void Draw()
		{
			// gl.ModelView.Push();

			// gl.ModelView.Multiply(Mtrx);
			foreach(IDrawable I in this)
				I.Draw();
			
			// gl.ModelView.Pop();
		}

		public void Dispose()
		{
			foreach(IDrawable I in this)
			{
				if(I is IDisposable)
					(I as IDisposable).Dispose();
			}

			base.Clear();
		}
	}

	public class VisualScene : List<Node>,IDisposable
	{
		public VisualScene(visual_scene VScene)
		{
			foreach(node N in VScene.Nodes)
				base.Add(new Node(N));
		}

		public void Draw()
		{			
			foreach(Node N in this)
				N.Draw();
		}
		public void Dispose()
		{
			foreach(Node N in this)
				N.Dispose();

			base.Clear();
		}
	}
}