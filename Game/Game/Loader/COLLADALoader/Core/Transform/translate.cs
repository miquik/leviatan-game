using System;
using System.Xml;
using OpenTK;


namespace COLLADALoader
{
	public class translate : transformation_element,IHasChildNode
	{
		public Vector3 Target;
		protected override void  SetValue(float[] Values)
		{
			Target	= new Vector3(Values[0],Values[1],Values[2]);
		}
	}
}
