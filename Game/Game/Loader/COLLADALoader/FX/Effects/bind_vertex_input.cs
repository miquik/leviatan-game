using System;
using System.Xml;

namespace COLLADALoader
{
	public class bind_vertex_input : IHasAttribute
	{
		public uint Set;
		public string Semantic;
		public string InputSemantic;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "semantic":
					Semantic	= Attr.Value;
					break;

				case "input_semantic":
					InputSemantic	= Attr.Value;
					break;

				case "input_set":
					Set	= uint.Parse(Attr.Value);
					break;
			}
		}
	}
}