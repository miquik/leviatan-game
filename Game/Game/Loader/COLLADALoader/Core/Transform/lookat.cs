using System;
using System.Xml;
using OpenTK;

namespace COLLADALoader
{
	public class lookat : transformation_element,IHasChildNode
	{
		public Vector3 Position,UpVector,Target;
		protected override void SetValue(float[] Values)
		{
			Position	= new Vector3(Values[0],Values[1],Values[2]);
			UpVector	= new Vector3(Values[3],Values[4],Values[5]);
			Target	= new Vector3(Values[6],Values[7],Values[8]);
		}
	}
}
