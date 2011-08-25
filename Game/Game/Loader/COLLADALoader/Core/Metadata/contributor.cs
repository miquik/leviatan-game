using System;
using System.Xml;						// XmlDocument

namespace COLLADALoader
{

	public class contributor : IHasChildNode
	{
		public string Author;
		public string Tool;
		public string Email;
		public string Comments;
		public string Copyright;
		public Uri Website;
		public Uri SourceData;
		void IHasChildNode.InitChildNode(COLLADA Doc,XmlNode Child)
		{
			switch(Child.Name)
			{
				case "author":
					Author	= Child.InnerText;
					break;

				case "author_email":
					Email	= Child.InnerText;
					break;

				case "author_website":
					Website	= new Uri(Child.InnerText);
					break;

				case "authoring_tool":
					Tool	= Child.InnerText;
					break;

				case "comments":
					Comments	= Child.InnerText;
					break;

				case "copyright":
					Copyright	= Child.InnerText;
					break;

				case "source_data":
					SourceData	= new Uri(Child.InnerText);
					break;

				default:
					throw new Exception("Invalid Child");
			}
		}
	}
}