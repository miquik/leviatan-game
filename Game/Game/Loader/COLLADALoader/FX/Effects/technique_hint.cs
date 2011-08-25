using System;
using System.Xml;

namespace COLLADALoader
{
	public class technique_hint : IHasAttribute
	{
		public string Platform;
		public string Ref;
		public string Profile;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "platform":
					Platform	= Attr.Value;
					break;

				case "ref":
					Ref	= Attr.Value;
					break;

				case "profile":
					Profile	= Attr.Value;
					break;

				default:
					throw new Exception("Invalid Attribute");
			}
		}
	}
}
