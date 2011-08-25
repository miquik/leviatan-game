using System;
using System.Xml;
using System.Drawing;
using OpenTK;

namespace COLLADALoader
{
	public class perspective : MatrixProjection,IHasChildNode
	{
		public override Matrix4 FromScreenSize(Size ScrSize)
		{
			return Matrix4.CreatePerspectiveFieldOfView(YFOV, AspectRatio,ZNear,ZFar);
		}

		public float XFOV	= 0;
		public float YFOV	= 0;
		public float AspectRatio	= 0;
		public float ZNear,ZFar;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "xfov":
					XFOV	= float.Parse(Child.InnerText);
					break;
					
				case "yfov":
					YFOV	= float.Parse(Child.InnerText);
					break;
					
				case "aspect_ratio":
					AspectRatio	= float.Parse(Child.InnerText);
					break;
					
				case "znear":
					ZNear	= float.Parse(Child.InnerText);
					break;
					
				case "zfar":
					ZFar	= float.Parse(Child.InnerText);
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
			
			if(AspectRatio != 0)
			{
				if(YFOV != 0)
					XFOV	= YFOV * AspectRatio;
				else if(XFOV != 0)
					YFOV	= XFOV * AspectRatio;
			}
			else AspectRatio	= XFOV / YFOV;
		}
	}
}
