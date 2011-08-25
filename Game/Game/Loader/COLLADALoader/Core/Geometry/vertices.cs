using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class vertices : Resource,IHasChildNode
	{
		public List<input> Inputs;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "input")
			{
				if(Inputs == null)
					Inputs	= new List<input>();
				Inputs.Add(Doc.Load<input>(this,Child));
			}
			else throw new Exception("Invalid Child Node");
		}
	}
}
