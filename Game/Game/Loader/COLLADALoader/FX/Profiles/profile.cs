using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class profile : Extensible,IHasID,IHasAsset,IHasAttribute
	{
		public string ID;
		string IHasID.ID
		{
			set{ID	= value;}
		}

		public asset Asset;
		void IHasAsset.Add(asset ChildAsset)
		{
			Asset	= ChildAsset;
		}

		public string Platform;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			if(Attr.Name == "platform")
				Platform	= Attr.Value;
			else throw new Exception("Invalid Atrribute");
		}
	}
}