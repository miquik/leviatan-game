using System;
using System.Xml;

namespace COLLADALoader
{
	public class channel : IHasSource,IHasTarget
	{
		string Source,Target;
		string IHasSource.Source
		{
			get{return Source;}
			set
			{
				Source	= value;
			}
		}

		string IHasTarget.Target
		{
			set
			{
				Target	= value;
			}
		}
	}
}
