using System;
using System.IO;
using Microsoft.VisualBasic;

namespace FixWin
{
	// Token: 0x0200000B RID: 11
	public class FileHandler
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000328C File Offset: 0x0000148C
		public void WriteToFileRunAndDelete(ref string data)
		{
			string text = DateTime.Now.Millisecond.ToString() + ".bat";
			File.WriteAllText(text, data);
			File.SetAttributes(text, FileAttributes.Hidden);
			Interaction.Shell(text, AppWinStyle.NormalFocus, true, -1);
			File.Delete(text);
		}
	}
}
