using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class camera : AssetResource,IHasChildNode
	{
		public optics Optics;
		public imager Imager;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "optics":
					Optics	= Doc.Load<optics>(this,Child);
					break;

				case "imager":
					Imager	= Doc.Load<imager>(this,Child);
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
