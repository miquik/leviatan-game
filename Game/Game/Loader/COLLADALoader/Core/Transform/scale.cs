using System;
using System.Xml;
using OpenTK;


namespace COLLADALoader
{
	public class scale : transformation_element,IHasChildNode
	{
		public Vector3 Size;
		protected override void SetValue(float[] Values)
		{
			Size	= new Vector3(Values[0],Values[1],Values[2]);
		}
	}
}
