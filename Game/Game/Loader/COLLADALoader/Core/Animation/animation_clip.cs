using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class animation_clip : AssetResource,IHasAttribute,IHasChildNode
	{
		public float Start,End;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "start":
					Start	= float.Parse(Attr.Value);
					break;
				case "end":
					End	= float.Parse(Attr.Value);
					break;
					
				default:
					throw new Exception("Invalid Attribute");
			}
		}
		
		public List<instance_animation> InstanceAnimations;
		public List<instance_formula> InstanceFormulas;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "instance_animation":
					if(InstanceAnimations == null)
						InstanceAnimations	= new List<instance_animation>();
					InstanceAnimations.Add(Doc.Load<instance_animation>(this,Child));
					break;

				case "instance_formula":
					if(InstanceFormulas == null)
						InstanceFormulas	= new List<instance_formula>();
					InstanceFormulas.Add(Doc.Load<instance_formula>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
