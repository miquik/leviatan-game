using System;
using System.Xml;

namespace COLLADALoader
{
	public class instance_geometry : Instance<geometry>,IHasChildNode
	{
		public bind_material Material;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "bind_material")
				Material	= Doc.Load<bind_material>(this,Child);
			else throw new Exception("Invalid Child Node");
		}
	}
}
