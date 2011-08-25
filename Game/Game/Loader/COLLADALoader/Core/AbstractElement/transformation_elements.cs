using System;
using System.Xml;

namespace COLLADALoader
{
	public abstract class transformation_element : IHasScopedID,IHasChildNode
	{
		public string SID;
		string IHasScopedID.SID
		{
			set{SID	= value;}
		}
		
		protected abstract void SetValue(float[] Values);
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Node)
		{
			char[] Splitter	= {' ','\n'};
			string[] Values	= Node.InnerText.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

			float[] V	= new float[Values.Length];

			int i	= 0;
			while(i < V.Length)
			{
				V[i]	= float.Parse(Values[i]);
				i++;
			}
			SetValue(V);
		}
	}
}
