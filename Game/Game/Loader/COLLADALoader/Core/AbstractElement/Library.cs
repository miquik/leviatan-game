using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public abstract class library<T> : AssetResource,IHasChildNode where T : IElement,new()
	{
		public List<T> Elements	= new List<T>();
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == typeof(T).Name)
				Elements.Add(Doc.Load<T>(this,Child));
			else throw new Exception("Invalid Child Node");
		}
	}
}