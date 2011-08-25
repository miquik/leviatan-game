using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public abstract class array_element : IHasID,IHasName,IHasAttribute,IHasChildNode
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

		public uint Count;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			if(ID == "mesh2-geometry-position-array")
			{
			}
			if(Attr.Name == "count")
				Count	= uint.Parse(Attr.Value);
			else InitAtrrib(Attr);
		}

		protected virtual void InitAtrrib(XmlAttribute Attr)
		{
			throw new Exception("Invalid Attribute");
		}

		public Array Values;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			char[] Splitter	= {' ','\n'};
			Values	= ParseValue(Child.InnerText.Split(Splitter,StringSplitOptions.RemoveEmptyEntries));
		}

		protected virtual Array ParseValue(string[] Value)
		{
			return Value;
		}
	}
}