using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class effect : AssetResource,IHasChildNode
	{
		public List<annotate> Annotates;
		public List<newparam> Params;
		public List<profile> Profiles;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "annotate":
					if(Annotates == null)
						Annotates	= new List<annotate>();
					Annotates.Add(Doc.Load<annotate>(this,Child));
					break;

				case "newparam":
					if(Params == null)
						Params	= new List<newparam>();
					Params.Add(Doc.Load<newparam>(this,Child));
					break;

				case "profile":
					if(Profiles == null)
						Profiles	= new List<profile>();
					Profiles.Add(Doc.Load<profile>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}