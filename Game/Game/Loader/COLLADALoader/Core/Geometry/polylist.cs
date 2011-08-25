using System;
using System.Xml;

namespace COLLADALoader
{
	public class polylist : primitive_element
	{
		public uint[] VCount;
		protected override void InitChild(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "vcount")
			{
				VCount	= new uint[Count];

				char[] Splitter	= {' ','\n'};
				string[] VC	= Child.InnerText.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

				int i	= 0;
				while(i < Count)
				{
					VCount[i]	= uint.Parse(VC[i]);
					i++;
				}
			}
		}
	}
}
