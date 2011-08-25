using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class instance_image : Extensible,IHasURL,IHasScopedID,IHasName
	{
		public string URL;
		string IHasURL.URL
		{
			set{URL	= value;}
		}

		public string SID;
		string IHasScopedID.SID
		{
			set{SID	= value;}
		}

		public string Name;
		string IHasName.Name
		{
			set{Name	= value;}
		}
	}
}