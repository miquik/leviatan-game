using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class instance_controller : Instance<controller>,IHasChildNode
	{
		public bind_material Material;
		public List<skeleton> Skeletons;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "bind_material":
					Material	= Doc.Load<bind_material>(this,Child);
					break;
					
				case "skeleton":
					if(Skeletons == null)
						Skeletons	= new List<skeleton>();
					Skeletons.Add(Doc.Load<skeleton>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
