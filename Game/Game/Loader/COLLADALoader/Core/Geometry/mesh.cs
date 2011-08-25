using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class mesh : geometric_element
	{
		public vertices Vertices;
		public List<primitive_element> Primitives;
		protected override void InitChild(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "vertices")
				Vertices	= Doc.Load<vertices>(this,Child);
			else
			{
				primitive_element PrmElement	= null;
				switch(Child.Name)
				{
					case "lines":
						PrmElement	= Doc.Load<lines>(this,Child);
						break;

					case "linestrips":
						PrmElement	= Doc.Load<linestrips>(this,Child);
						break;

					case "polygons":
						PrmElement	= Doc.Load<polygons>(this,Child);
						break;

					case "polylist":
						PrmElement	= Doc.Load<polylist>(this,Child);
						break;

					case "triangles":
						PrmElement	= Doc.Load<triangles>(this,Child);
						break;

					case "trifans":
						PrmElement	= Doc.Load<trifans>(this,Child);
						break;

					case "tristrips":
						PrmElement	= Doc.Load<tristrips>(this,Child);
						break;

					default:
						throw new Exception("Invalid Child Node");
				}
				
				if(Primitives == null)
					Primitives	= new List<primitive_element>();
				if(PrmElement != null)
					Primitives.Add(PrmElement);
			}
		}
	}
}
