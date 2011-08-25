using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class instance_effect : Instance<effect>,IHasChildNode
	{
		public List<technique_hint> Hints;
		public List<setparam> Params;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "technique_hint":
					if(Hints == null)
						Hints	= new List<technique_hint>();
					Hints.Add(Doc.Load<technique_hint>(this,Child));
					break;

				case "param":
					if(Params == null)
						Params	= new List<setparam>();
					Params.Add(Doc.Load<setparam>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}