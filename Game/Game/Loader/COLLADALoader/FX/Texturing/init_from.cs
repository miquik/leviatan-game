using System;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;

namespace COLLADALoader
{
	public enum CubeFace
	{
		POSITIVE_X,POSITIVE_Y,POSITIVE_Z,
		NEGATIVE_X,NEGATIVE_Y,NEGATIVE_Z,
	}
	public class init_from : IHasAttribute,IHasChildNode
	{
		public bool MipGenerate;

		public uint ArrayIndex;
		public uint MipIndex;
		public uint Depth;

		public CubeFace Face;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "mips_generate":
					MipGenerate	= bool.Parse(Attr.Value);
					break;

				case "array_index":
					ArrayIndex	= uint.Parse(Attr.Value);
					break;

				case "mip_index":
					MipIndex	= uint.Parse(Attr.Value);
					break;

				case "depth":
					Depth	= uint.Parse(Attr.Value);
					break;
		
				case "face":
					Face	= (CubeFace)Enum.Parse(typeof(CubeFace),Attr.Value);
					break;

				default:
					throw new Exception("Invalid Atrribute");
			}
		}
		
		public string Ref;
		public string Format;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Doc.Version < new Version(1,5))
				Ref	= Child.Value;
			else
			{
				switch(Child.Name)
				{
					case "ref":
						Ref	= Child.Value;
						break;

					case "hex":
						Format	= Child.ChildNodes[0].Value;
						break;
						
					default:
						throw new Exception("Invalid Child Node");
				}
			}
		}
	}
}