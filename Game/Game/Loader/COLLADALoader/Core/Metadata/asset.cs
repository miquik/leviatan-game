using System;
using System.Xml;
using System.Collections.Generic;

namespace COLLADALoader
{
	public enum SixAxis
	{
		X_UP,
		Y_UP,
		Z_UP,
		X_DN,
		Y_DN,
		Z_DN,
	}
	public class asset : Extensible,IHasChildNode
	{
		public string Title;
		public string Subject;
		public string Revision;
		public string Keywords;
		public float Unit	= 1.0f;
		public string UnitType	= "meter";
		public SixAxis UpAxis	= SixAxis.Y_UP;
		public DateTime Created;
		public DateTime Modified;
		public List<contributor> Contributors;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "contributor":
					if(Contributors == null)
						Contributors	= new List<contributor>();
					Contributors.Add(Doc.Load<contributor>(this,Child));
					break;
					
				case "coverage":
					break;

				case "created":
					Created	= DateTime.Parse(Child.InnerText);
					break;

				case "keywords":
					Keywords	= Child.InnerText;
					break;

				case "modified":
					Modified	= DateTime.Parse(Child.InnerText);
					break;

				case "revision":
					Revision	= Child.InnerText;
					break;

				case "subject":
					Subject	= Child.InnerText;
					break;
					
				case "title":
					Title	= Child.InnerText;
					break;

				case "unit":
					Unit	= float.Parse(Child.Attributes["meter"].InnerText);
					UnitType	= Child.Attributes["name"].InnerText;
					break;

				case "up_axis":
					UpAxis	= (SixAxis)Enum.Parse(typeof(SixAxis),Child.InnerText);
					break;

				default:
					throw new Exception("Invalid Child");
			}
		}
	}
}