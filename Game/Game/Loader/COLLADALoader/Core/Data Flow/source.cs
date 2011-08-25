using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class source : IHasID,IHasName,IHasAsset,IHasChildNode,IHasTechChildNode
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

		public asset Asset;
		void IHasAsset.Add(asset ChildAsset)
		{
			Asset	= ChildAsset;
		}
		
		void IHasTechChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "accessor")
				Accessor	= Doc.Load<accessor>(this,Child);
			else throw new Exception("Invalid Child Node");
		}
		
		public accessor Accessor;
		public array_element Array;
		public List<technique> Techniques;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "bool_array":
					Array	= Doc.Load<bool_array>(this,Child);
					break;

				case "float_array":
					Array	= Doc.Load<float_array>(this,Child);
					break;

				case "IDREF_array":
					Array	= Doc.Load<IDREF_array>(this,Child);
					break;

				case "int_array":
					Array	= Doc.Load<int_array>(this,Child);
					break;

				case "Name_array":
					Array	= Doc.Load<Name_array>(this,Child);
					break;

				case "SIDREF_array":
					Array	= Doc.Load<SIDREF_array>(this,Child);
					break;

				case "token_array":
					break;
					
				case "technique":
					break;
			}
		}
	}
}
