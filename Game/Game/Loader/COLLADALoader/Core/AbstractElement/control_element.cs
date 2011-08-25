using System;
using System.Xml;

namespace COLLADALoader
{
	public class control_element : Extensible,IHasAttribute,IHasChildNode
	{
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
		}

		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
		}
	}
}
