using System;
using System.Xml;

namespace COLLADALoader
{
	public class bool_array : array_element
	{
		protected override Array ParseValue(string[] Value)
		{
			bool []Values	= new bool[Value.Length];

			int i	= 0;
			while(i < Value.Length)
			{
				Values[i]	= bool.Parse(Value[i]);

				i++;
			}

			return Values;
		}
	}
}
