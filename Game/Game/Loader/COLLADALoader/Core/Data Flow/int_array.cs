using System;
using System.Xml;

namespace COLLADALoader
{
	public class int_array : array_element
	{
		protected override Array ParseValue(string[] Value)
		{
			int []Values	= new int[Value.Length];

			int i	= 0;
			while(i < Value.Length)
			{
				Values[i]	= int.Parse(Value[i]);

				i++;
			}

			return Values;
		}
	}
}
