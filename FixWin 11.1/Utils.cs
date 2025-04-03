using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FixWin.My;

namespace FixWin
{
	// Token: 0x02000012 RID: 18
	public class Utils
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001B210 File Offset: 0x00019410
		public static void DeleteThumbnailAndIconCacheFiles()
		{
			ReadOnlyCollection<string> files = MyProject.Computer.FileSystem.GetFiles("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Microsoft\\Windows\\Explorer");
			try
			{
				foreach (string text in files)
				{
					if (text.Contains("iconcache") | text.Contains("thumbcache"))
					{
						MyProject.Computer.FileSystem.DeleteFile(text);
					}
				}
			}
			finally
			{
				IEnumerator<string> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}
	}
}
