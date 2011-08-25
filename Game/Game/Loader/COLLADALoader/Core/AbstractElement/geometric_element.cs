using System;
using System.Xml;

namespace COLLADALoader
{
	public abstract class geometric_element : Extensible,IHasChildNode
	{
		public source Source;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "source")
				Source	= Doc.Load<source>(this,Child);
			else InitChild(Doc,Child);
		}

		protected abstract void InitChild(COLLADA Doc,XmlNode Child);
	}
}
