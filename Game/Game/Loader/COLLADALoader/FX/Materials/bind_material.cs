using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class bind_material : Extensible,IHasChildNode,IHasTechChildNode
	{
		public List<param> Params;
		public List<technique> Technique;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "param":
					if(Params == null)
						Params	= new List<param>();
					Params.Add(Doc.Load<param>(this,Child));
					break;

				case "technique":
					if(Technique == null)
						Technique	= new List<technique>();
					Technique.Add(Doc.Load<technique>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}

		public List<instance_materials> Materials;
		void IHasTechChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "instance_material")
			{
				if(Materials == null)
					Materials	= new List<instance_materials>();
				Materials.Add(Doc.Load<instance_materials>(this,Child));
			}
			else throw new Exception("Invalid Child Node");
		}
	}
}
