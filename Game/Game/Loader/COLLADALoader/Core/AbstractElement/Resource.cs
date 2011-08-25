using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public abstract class Extensible : IHasExtra
	{
		public List<extra> Extras;
		void IHasExtra.Add(extra ChildExtra)
		{
			if(Extras == null)
				Extras	= new List<extra>();
			Extras.Add(ChildExtra);
		}
	}

	public abstract class Resource : Extensible,IHasID,IHasName
	{
		public string ID;
		string IHasID.ID
		{
			set{ID	= value;}
		}

		public string Name;
		string IHasName.Name
		{
			set{Name	= value;}
		}
	}

	public abstract class AssetResource : Resource,IHasAsset
	{
		public asset Asset;
		void IHasAsset.Add(asset ChildAsset)
		{
			Asset	= ChildAsset;
		}
	}
}