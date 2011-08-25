using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class accessor : IHasSource,IHasAttribute,IHasChildNode
	{
		public string Source;
		string IHasSource.Source
		{
			get{return Source;}
			set{Source	= value;}
		}

		public uint Count;
		public uint Offset	= 0;
		public uint Stride	= 1;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "count":
					Count	= uint.Parse(Attr.Value);
					break;
				
				case "offset":
					Offset	= uint.Parse(Attr.Value);
					break;

				case "stride":
					Stride	= uint.Parse(Attr.Value);
					break;

				default:
					throw new Exception("Invalid Attribute");
			}
		}
		
		public List<param> Params;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "param")
			{
				if(Params == null)
					Params	= new List<param>();
				Params.Add(Doc.Load<param>(this,Child));
			}
			else throw new Exception("Invalid Child Node");
		}
	}
}
