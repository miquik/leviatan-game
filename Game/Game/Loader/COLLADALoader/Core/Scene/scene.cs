using System;
using System.Xml;

namespace COLLADALoader
{
	public class scene : Extensible,IHasChildNode
	{
		public instance_visual_scene VisualScene;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "instance_physics_scene":
					break;
				
				case "instance_visual_scene":
					VisualScene	= Doc.Load<instance_visual_scene>(this,Child);
					break;
				
				case "instance_kinematics_scene":
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
