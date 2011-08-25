using System;
using System.Xml;
using OpenTK;

namespace COLLADALoader
{
	public class rotate : transformation_element,IHasChildNode
	{
		public float Degree;
		public Vector3 AngleVector;
		protected override void SetValue(float[] Values)
		{
			AngleVector	= new Vector3(Values[0],Values[1],Values[2]);
			Degree	= Values[3];
		}
	}
}
