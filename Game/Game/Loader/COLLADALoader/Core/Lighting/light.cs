using System;
using System.Xml;
using Game.Loader;

namespace COLLADALoader
{
	public abstract class LightSource : IHasChildNode
	{
		public ClampColor Color;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			if(Child.Name == "color")
			{
				char[] Splitter	= {' ','\n'};
				string[] C	= Child.InnerText.Split(Splitter,StringSplitOptions.RemoveEmptyEntries);

				Color	= new ClampColor(float.Parse(C[0]),float.Parse(C[1]),float.Parse(C[2]),1);
			}
			else InitChildNode(Doc,Child);
		}

		protected virtual void InitChildNode(COLLADA Doc,XmlNode Child)
		{
			throw new NotImplementedException();
		}
	}

	public class light : AssetResource,IHasTechChildNode
	{
		public LightSource LightSource;
		void IHasTechChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "ambient":
					LightSource	= Doc.Load<ambient>(this,Child);
					break;

				case "directional":
					LightSource	= Doc.Load<directional>(this,Child);
					break;

				case "point":
					LightSource	= Doc.Load<point>(this,Child);
					break;

				case "spot":
					LightSource	= Doc.Load<spot>(this,Child);
					break;

				default:
					throw new Exception("Invalid Child Node");
			}
		}
	}
}
