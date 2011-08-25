using System;
using System.Xml;
using System.Drawing;
using OpenTK;

namespace COLLADALoader
{
	public abstract class MatrixProjection
	{
		public abstract Matrix4 FromScreenSize(Size ScrSize);
	}
}