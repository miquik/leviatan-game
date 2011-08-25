using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public interface IElement
	{
	}

	interface IHasAttribute : IElement
	{
		void InitAtrribute(XmlAttribute Attr);
	}	
	interface IHasChildNode : IElement
	{
		void InitChildNode(COLLADA Doc,XmlNode Node);
	}
	
	interface IHasDocument : IElement
	{
		COLLADA Doc
		{
			set;
		}
	}

	interface IHasID : IElement
	{
		string ID{set;}
	}
	interface IHasName : IElement
	{
		string Name{set;}
	}
	interface IHasScopedID : IElement
	{
		string SID{set;}
	}

	interface IHasRef : IElement
	{
		string Ref{set;}
	}
	interface IHasURL : IElement
	{
		string URL
		{
			set;
		}
	}

	interface IHasSource : IElement
	{
		string Source
		{
			get;
			set;
		}
	}
	interface IHasTarget : IElement
	{
		string Target{set;}
	}
	interface IHasSymbol : IElement
	{
		string Symbol{set;}
	}

	interface IHasAsset : IElement
	{
		void Add(asset ChildAsset);
	}
	interface IHasExtra : IElement
	{
		void Add(extra ChildExtra);
	}

	interface IHasTechAttribute : IElement
	{
		void InitAttribute(XmlAttribute Attr);
	}	
	interface IHasTechChildNode : IElement
	{
		void InitChildNode(COLLADA Doc,XmlNode Node);
	}

}