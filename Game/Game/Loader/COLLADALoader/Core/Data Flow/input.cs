using System;
using System.Xml;

namespace COLLADALoader
{
	public class input : IHasAttribute,IHasSource,IHasDocument
	{
		public string Source;
		string IHasSource.Source
		{
			get{return Source;}
			set{Source	= value;}
		}

		public uint Set;
		public uint Offset;
		public string Semantic;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "offset":
					Offset	= uint.Parse(Attr.Value);
					break;

				case "set":
					Set	= uint.Parse(Attr.Value);
					break;

				case "semantic":
					Semantic	= Attr.Value;
					break;

				default:
					throw new Exception("Invalid Attribute");
			}
		}
		
		COLLADA Doc;
		COLLADA IHasDocument.Doc
		{
			set{Doc	= value;}
		}

		public static implicit operator source(input Input)
		{
			IElement E	= Input;
			while(E is IHasSource)
			{
				E	= Input.Doc[(E as IHasSource).Source];
				if(E is vertices)
					E	= (E as vertices).Inputs[0];
			}

			return E as source;
		}
	}
}
