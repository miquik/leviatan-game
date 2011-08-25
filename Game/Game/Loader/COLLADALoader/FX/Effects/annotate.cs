using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public class annotate : IHasName,IHasChildNode
	{
		public string Name;
		string IHasName.Name
		{
			set{Name	= value;}
		}

		public Array Value;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			char[] Splitter	= {' ','\n'};
			string[] V	= Child.InnerText.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);
			switch(Child.Name)
			{
				case "bool":
				case "bool2":
				case "bool3":
				case "bool4":
					Value	= new bool[V.Length];

					int i	= 0;
					while(i < V.Length)
					{
						(Value as bool[])[i]	= bool.Parse(V[i]);
						i++;
					}
					break;

				case "int":
				case "int2":
				case "int3":
				case "int4":
					Value	= new int[V.Length];

					i	= 0;
					while(i < V.Length)
					{
						(Value as int[])[i]	= int.Parse(V[i]);
						i++;
					}
					break;

				case "float":
				case "float2":
				case "float3":
				case "float4":
					Value	= new bool[V.Length];

					i	= 0;
					while(i < V.Length)
					{
						(Value as bool[])[i]	= bool.Parse(V[i]);
						i++;
					}
					break;

				case "float2x2":
				case "float3x3":
				case "float4x4":
					int L	= (int)Math.Sqrt(V.Length);
					Value	= new bool[L,L];

					i	= 0;
					while(i < L)
					{
						int j	= 0;
						while(j < L)
						{
							(Value as bool[,])[i,j]	= bool.Parse(V[(i * L) + j]);
							j++;
						}
						i++;
					}
					break;

				case "string":
					Value	= new string[]{Child.InnerText};
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
