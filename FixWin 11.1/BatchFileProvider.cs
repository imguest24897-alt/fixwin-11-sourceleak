using System;

namespace FixWin
{
	// Token: 0x02000008 RID: 8
	public class BatchFileProvider
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000224B File Offset: 0x0000044B
		public BatchFileProvider()
		{
			this.RESET_DLL_SCRIPT = "\r\ncd C:/Windows/System32\r\nfor %%f in (*.dll) do regsvr32 /s %%f\r\n";
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000225E File Offset: 0x0000045E
		public string GetThumbnailClearFile()
		{
			return BatchFileProvider.RESET_THUMBNAIL_CACHE_SCRIPT;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002265 File Offset: 0x00000465
		public string GetResetDllFile()
		{
			return this.RESET_DLL_SCRIPT;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000226D File Offset: 0x0000046D
		public string GetRebuildTokensFile()
		{
			return "\r\nnet stop sppsvc\r\ncd %windir%\\ServiceProfiles\\LocalService\\AppData\\Local\\Microsoft\\WSLicense\r\nren tokens.dat tokens.bar\r\nnet start sppsvc\r\ncscript.exe %windir%\\system32\\slmgr.vbs /rilc\r\n";
		}

		// Token: 0x0400000B RID: 11
		private static string RESET_THUMBNAIL_CACHE_SCRIPT = "\r\n@echo off\r\necho.\r\ntaskkill /f /im explorer.exe\r\ntimeout 2 /nobreak>nul\r\necho.\r\n\r\nDEL /F /S /Q /A %LocalAppData%\\Microsoft\\Windows\\Explorer\\thumbcache_*.db\r\n\r\ntimeout 2 /nobreak>nul\r\nstart explorer.exe\r\n";

		// Token: 0x0400000C RID: 12
		private string RESET_DLL_SCRIPT;
	}
}
