using System;
using System.Xml;

namespace COLLADALoader
{
	public class param : IHasName,IHasScopedID,IHasAttribute
	{
		public string Name;
		string IHasName.Name
		{
			set{Name	= value;}
		}

		public string SID;
		string IHasScopedID.SID
		{
			set{SID	= value;}
		}

		public string Type;
		public string Semantic;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "type":
					Type	= Attr.Value;
					break;

				case "semantic":
					Semantic	= Attr.Value;
					break;

				default:
					throw new Exception("Invalid Attribute");
			}
		}
	}
}
