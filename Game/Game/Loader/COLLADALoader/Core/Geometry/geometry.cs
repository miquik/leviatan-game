using System;
using System.Xml;

namespace COLLADALoader
{
	public class geometry : AssetResource,IHasChildNode
	{
		public geometric_element Geometric;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "convex_mesh":
					break;

				case "mesh":
					Geometric	= Doc.Load<mesh>(this,Child);
					break;

				case "spline":
					break;

				case "brep":
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
