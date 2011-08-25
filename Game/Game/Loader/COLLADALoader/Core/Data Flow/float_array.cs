using System;
using System.Xml;

namespace COLLADALoader
{
	public class float_array : array_element
	{
		protected override Array ParseValue(string[] Value)
		{
			float []Values	= new float[Value.Length];

			int i	= 0;
			while(i < Value.Length)
			{
				if(!string.IsNullOrEmpty(Value[i]))
					Values[i]	= float.Parse(Value[i]);

				i++;
			}

			return Values;
		}

		public byte Digits;
		public short Magnitude;
		protected override void InitAtrrib(XmlAttribute Attr)
		{
			if(Attr.Name == "digits")
				Digits	= byte.Parse(Attr.Value);
			else if(Attr.Name == "magnitude")
				Magnitude	= short.Parse(Attr.Value);
		}
	}
}
