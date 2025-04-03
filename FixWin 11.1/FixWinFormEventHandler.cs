using System;

namespace FixWin
{
	// Token: 0x02000011 RID: 17
	public class FixWinFormEventHandler : IObservable<bool>
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0001B1EB File Offset: 0x000193EB
		public IDisposable Subscribe(IObserver<bool> observer)
		{
			this.observer = observer;
			return (IDisposable)observer;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001B1FA File Offset: 0x000193FA
		public void FixWinClosing()
		{
			if (this.observer != null)
			{
				this.observer.OnNext(true);
			}
		}

		// Token: 0x0400019A RID: 410
		private IObserver<bool> observer;
	}
}
