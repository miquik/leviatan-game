using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class fx_sampler : IHasChildNode
	{
		public List<annotate> Annotates;
		public string Semantic;
		public modifier Modifier;
		public object Value;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "annotate":
					if(Annotates == null)
						Annotates	= new List<annotate>();
					Annotates.Add(Doc.Load<annotate>(this,Child));
					break;
				case "semantic":
					Semantic	= Child.Value;
					break;
				case "modifier":
					Modifier	= (modifier)Enum.Parse(typeof(modifier),Child.Value);
					break;

				case "float":
					Value	= float.Parse(Child.Value);
					break;

				case "float2":
				case "float3":
				case "float4":
					char[] Splitter	= {' ','\n'};
					string[] V	= Child.Value.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

					Value	= new float[V.Length];

					int i	= 0;
					while(i < V.Length)
					{
						(Value as float[])[i]	= float.Parse(V[i]);
						i++;
					}

					break;

				case "sampler2D":
					break;
			}
		}
	}
}
