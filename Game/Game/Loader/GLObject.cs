using System;
using System.Threading;
using System.Collections.Generic;

using GLSpec;

namespace SharpGL
{
	public unsafe partial class CSGL
	{
		readonly Queue<IMustInit> InitQue	= new Queue<IMustInit>();
		readonly Queue<IMustExit> ExitQue	= new Queue<IMustExit>();

		public interface IMustInit
		{
			void Init(GL Control);
		}
		public interface IMustExit
		{
			void Exit(GL Control);
		}

		public abstract class GLObject : IDisposable
		{
			protected readonly CSGL Ctrl;
			public GLObject(CSGL Control)
			{
				Ctrl	= Control;
			}

			protected void Initial()
			{
				if(this is IMustInit)
				{
					if(Ctrl.GLThread == Thread.CurrentThread)
						(this as IMustInit).Init(Ctrl.gl);
					else Ctrl.InitQue.Enqueue(this as IMustInit);
				}
			}

			public void Dispose()
			{
				if(this is IMustExit)
				{
					if(Ctrl.GLThread == Thread.CurrentThread)
						(this as IMustExit).Exit(Ctrl.gl);
					else Ctrl.ExitQue.Enqueue(this as IMustExit);
				}
			}
		}
	}
}