using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class instance_materials : Instance<material>,IHasTarget,IHasSymbol,IHasChildNode
	{
		public string Symbol;
		string IHasSymbol.Symbol
		{
			set{Symbol	= value;}
		}

		public string Target;
		string IHasTarget.Target
		{
			set{Target	= value;}
		}

		public List<bind> Binds;
		public List<bind_vertex_input> BindVertexInputs;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "bind":
					if(Binds == null)
						Binds	= new List<bind>();
					Binds.Add(Doc.Load<bind>(this,Child));
					break;

				case "bind_vertex_input":
					if(BindVertexInputs == null)
						BindVertexInputs	= new List<bind_vertex_input>();
					BindVertexInputs.Add(Doc.Load<bind_vertex_input>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
