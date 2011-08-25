using System;
using System.Xml;
using OpenTK;

namespace COLLADALoader
{
	public class matrix : transformation_element
	{
		public Matrix4 M	= Matrix4.Identity;
		protected override void SetValue(float[] Values)
		{
			M = new Matrix4(Values[0], Values[1], Values[2] ,Values[3],
			                Values[4], Values[5], Values[6] ,Values[7],
			                Values[8], Values[9], Values[10] ,Values[11],
			                Values[12], Values[13], Values[14] ,Values[15]);
		}
	}
}
