using System;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class image : AssetResource,IHasScopedID,IHasChildNode,IHasAttribute
	{
		public string SID;
		string IHasScopedID.SID
		{
			set{SID	= value;}
		}

		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
		}

		public bool MipGenerate;
		public init_from InitData;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "renderable":
					break;

				case "init_from":
					InitData	= Doc.Load<init_from>(this,Child);
					break;
			}
		}

		Bitmap Bmp;
		public static implicit operator Bitmap(image Img)
		{
			if(Img.Bmp == null)
			{
				Img.Bmp	= Bitmap.FromFile(Img.InitData.Ref) as Bitmap;
			}

			return Img.Bmp;
		}
	}
}