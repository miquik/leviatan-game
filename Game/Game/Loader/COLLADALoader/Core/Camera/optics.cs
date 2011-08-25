using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class optics : Extensible,IHasChildNode,IHasTechChildNode
	{
		public MatrixProjection Projection;
		void IHasTechChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "orthographic":
					Projection	= Doc.Load<orthographic>(this,Child);
					break;

				case "perspective":
					Projection	= Doc.Load<perspective>(this,Child);
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}

		public List<technique> Techniques;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "technique")
				throw new NotImplementedException();
			else throw new Exception("Invalid Child Node");
		}
	}
}
