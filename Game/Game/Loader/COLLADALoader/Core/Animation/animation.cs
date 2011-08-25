using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class animation : AssetResource,IHasChildNode
	{
		public List<animation> Childs;
		public List<source> Sources;
		public List<sampler> Samplers;
		public List<channel> Channels;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "animation":
					if(Childs == null)
						Childs	= new List<animation>();
					Childs.Add(Doc.Load<animation>(this,Child));
					break;

				case "source":
					if(Sources == null)
						Sources	= new List<source>();
					Sources.Add(Doc.Load<source>(this,Child));
					break;

				case "sampler":
					if(Samplers == null)
						Samplers	= new List<sampler>();
					Samplers.Add(Doc.Load<sampler>(this,Child));
					break;
						
				case "channel":
					if(Channels == null)
						Channels	= new List<channel>();
					Channels.Add(Doc.Load<channel>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
