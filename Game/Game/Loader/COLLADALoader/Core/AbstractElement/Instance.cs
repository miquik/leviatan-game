using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public abstract class Instance<T> : Extensible,IHasDocument,IHasScopedID,IHasName,IHasURL where T : AssetResource
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
		
		public string URL;
		string IHasURL.URL
		{
			set{URL	= value;}
		}

		COLLADA Doc;
		COLLADA IHasDocument.Doc
		{
			set{Doc	= value;}
		}

		public static implicit operator T(Instance<T> InstanceObject)
		{
			return InstanceObject.Doc[InstanceObject.URL] as T;
		}
	}
}