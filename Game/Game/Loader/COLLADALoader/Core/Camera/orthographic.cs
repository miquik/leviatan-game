using System;
using System.Xml;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;


namespace COLLADALoader
{
	public class orthographic : MatrixProjection,IHasChildNode
	{
		public override Matrix4 FromScreenSize(System.Drawing.Size ScrSize)
		{
			Matrix4 M	= Matrix4.Identity;			
			M.M11	= XMag;
			M.M22	= YMag;

			return M;
		}

		public float XMag	= 0;
		public float YMag	= 0;
		public float AspectRatio	= 0;
		public float ZNear,ZFar;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "xmag":
					XMag	= float.Parse(Child.InnerText);
					break;
					
				case "ymag":
					YMag	= float.Parse(Child.InnerText);
					break;
					
				case "aspect_ratio>":
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
				if(YMag != 0)
					XMag	= YMag * AspectRatio;
				else if(XMag != 0)
					YMag	= XMag * AspectRatio;
			}
			else AspectRatio	= XMag / YMag;
		}
	}
}
