using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class visual_scene : AssetResource,IHasChildNode
	{
		public List<node> Nodes;
		public List<evaluate_scene> EvaluateScenes;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "node":
					if(Nodes == null)
						Nodes	= new List<node>();
					Nodes.Add(Doc.Load<node>(this,Child));
					break;
					
				case "evaluate_scene":
					if(EvaluateScenes == null)
						EvaluateScenes	= new List<evaluate_scene>();
					EvaluateScenes.Add(Doc.Load<evaluate_scene>(this,Child));
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
