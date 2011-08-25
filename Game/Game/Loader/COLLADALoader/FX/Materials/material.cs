using System;
using System.Xml;

namespace COLLADALoader
{
	public class material : AssetResource,IHasChildNode
	{
		public instance_effect Effect;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "instance_effect")
				Effect	= Doc.Load<instance_effect>(this,Child);
			else throw new Exception("Invalid Child Node");
		}
	}
}
