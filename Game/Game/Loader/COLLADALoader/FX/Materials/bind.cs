using System;
using System.Xml;

namespace COLLADALoader
{
	public class bind : IHasTarget,IHasAttribute
	{
		public string Semantic;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			if(Attr.Name == "semantic")
				Semantic	= Attr.Value;
			else throw new Exception("Invalid Attribute");
		}

		public string Target;
		string IHasTarget.Target
		{
			set{Target	= value;}
		}
	}
}
