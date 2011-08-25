using System;
using System.Xml;

namespace COLLADALoader
{
	public class point : LightSource
	{
		public float ConstantAtten;
		public float LinearAtten;
		public float QuadraticAtten;
		protected override void InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "constant_attenuation":
					ConstantAtten	= float.Parse(Child.InnerText);
					break;
				case "linear_attenuation":
					LinearAtten	= float.Parse(Child.InnerText);
					break;
				case "quadratic_attenuation":
					QuadraticAtten	= float.Parse(Child.InnerText);
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
