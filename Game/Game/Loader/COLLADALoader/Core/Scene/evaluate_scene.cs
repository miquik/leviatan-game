using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class evaluate_scene : AssetResource,IHasScopedID,IHasAttribute,IHasChildNode
	{
		public string SID;
		string IHasScopedID.SID
		{
			set{SID	= value;}
		}

		public bool Enable;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
		}

//		public List<render> Renders;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "render")
			{
			}
			else throw new Exception("Invalid Child Node");
		}
	}
}
