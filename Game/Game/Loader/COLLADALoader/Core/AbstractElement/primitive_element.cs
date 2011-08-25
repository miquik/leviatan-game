using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public abstract class primitive_element : Extensible,IHasName,IHasAttribute,IHasChildNode
	{
		public string Name;
		string IHasName.Name
		{
			set{Name	= value;}
		}

		public uint Count;
		public string Material;
		void IHasAttribute.InitAtrribute(XmlAttribute Attr)
		{
			switch(Attr.Name)
			{
				case "count":
					Count	= uint.Parse(Attr.Value);
					break;

				case "material":
					Material	= Attr.Value;
					break;
					
				default:
					throw new Exception("Invalid Attribute");
			}
		}

		public uint[,] P;
		public List<input> Inputs;
		uint MaxOffset	= 0;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "input":
					if(Inputs == null)
						Inputs	= new List<input>();

					input I	= Doc.Load<input>(this,Child);
					Inputs.Add(I);

					if(MaxOffset < I.Offset + 1)
						MaxOffset	= I.Offset + 1;
					break;

				case "p":
					char[] Splitter	= {' ','\n'};
					string[] V	= Child.InnerText.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

					P	= new uint[MaxOffset,V.Length / MaxOffset];

					int i	= 0;
					while(i < MaxOffset)
					{
						int  j	= 0;
						while(j < P.GetLength(1))
						{
							P[i,j]	= uint.Parse(V[i + (j * MaxOffset)]);
							j++;
						}

						i++;
					}
					break;
					
				default:
					InitChild(Doc,Child);
					break;
			}
		}

		protected virtual void InitChild(COLLADA Doc,XmlNode Child)
		{
			throw new Exception("Invalid Child Node");
		}
	}
}
