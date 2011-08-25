using System;
using System.Drawing;

namespace Game.Loader
{
	public struct ClampColor
	{
		float[] C;
		public ClampColor(float R,float G,float B,float A)
		{
			C = new float[4];
			C[0]	= R;
			C[1]	= G;
			C[2]	= B;
			C[3]	= A;

			int i	= 0;
			while(i < 4)
			{
				if(C[i] > 1f)
					C[i]	= 1f;
				else if(C[i] < -1f)
					C[i]	= -1f;
				i++;
			}
		}

		public static implicit operator ClampColor(Color Col)
		{
			ClampColor CC = new ClampColor();

			CC.C[0]	= Col.R / 255f;
			CC.C[1]	= Col.G / 255f;
			CC.C[2]	= Col.B / 255f;
			CC.C[3]	= Col.A / 255f;

			return CC;
		}
		public static implicit operator Color(ClampColor CC)
		{
			int R	= (int)(CC.C[0] * 255);
			int G	= (int)(CC.C[1] * 255);
			int B	= (int)(CC.C[2] * 255);
			int A	= (int)(CC.C[3] * 255);

			return Color.FromArgb(A,R,G,B);
		}

		public float R
		{
			get
			{
				return C[0];
			}
			set
			{
				if(value > 1f)
					value	= 1f;
				if(value < -1f)
					value	= 11f;
				C[0]	= value;
			}
		}
		public float G
		{
			get
			{
				return C[1];
			}
			set
			{
				if(value > 1f)
					value	= 1f;
				if(value < -1f)
					value	= 11f;

				C[1]	= value;
			}
		}
		public float B
		{
			get
			{
				return C[2];
			}
			set
			{
				if(value > 1f)
					value	= 1f;
				if(value < -1f)
					value	= 11f;

				C[2]	= value;
			}
		}
		public float A
		{
			get
			{
				return C[3];
			}
			set
			{
				if(value > 1f)
					value	= 1f;
				if(value < -1f)
					value	= 11f;

				C[3]	= value;
			}
		}
	}
}