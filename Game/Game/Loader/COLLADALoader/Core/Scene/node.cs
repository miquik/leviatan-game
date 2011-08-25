using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public enum nodeType{Node,Joint}
	public class node : AssetResource,IHasScopedID,IHasAttribute,IHasChildNode
	{
		public nodeType Type;

		public string SID;
		string IHasScopedID.SID
		{
			set{SID	= value;}
		}

		public List<string> Layers;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			if(Attr.Name == "layer")
			{
				if(Layers == null)
					Layers	= new List<string>();
				Layers.Add(Attr.Value);
			}
		}

		public List<IElement> ChildNodes	= new List<IElement>();
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "lookat":
					ChildNodes.Add(Doc.Load<lookat>(this,Child));
					break;

				case "matrix":
					ChildNodes.Add(Doc.Load<matrix>(this,Child));
					break;

				case "rotate":
					ChildNodes.Add(Doc.Load<rotate>(this,Child));
					break;

				case "scale":
					ChildNodes.Add(Doc.Load<scale>(this,Child));
					break;

				case "skew":
					ChildNodes.Add(Doc.Load<skew>(this,Child));
					break;

				case "translate":
					ChildNodes.Add(Doc.Load<translate>(this,Child));
					break;

				case "instance_camera":
					ChildNodes.Add(Doc.Load<instance_camera>(this,Child));
					break;

				case "instance_controller":
					ChildNodes.Add(Doc.Load<instance_controller>(this,Child));
					break;

				case "instance_geometry":
					ChildNodes.Add(Doc.Load<instance_geometry>(this,Child));
					break;

				case "instance_light":
					ChildNodes.Add(Doc.Load<instance_light>(this,Child));
					break;

				case "instance_node":
					ChildNodes.Add(Doc.Load<instance_node>(this,Child));
					break;

				case "node":
					ChildNodes.Add(Doc.Load<node>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
