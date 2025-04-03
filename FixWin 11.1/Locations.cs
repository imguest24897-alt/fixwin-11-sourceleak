using System;
using FixWin.My;
using Microsoft.Win32;

namespace FixWin
{
	// Token: 0x0200000D RID: 13
	public class Locations
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x000189E4 File Offset: 0x00016BE4
		public Locations()
		{
			this.ClassesRoot = MyProject.Computer.Registry.ClassesRoot;
			this.DesktopBackgroundShell = MyProject.Computer.Registry.ClassesRoot.OpenSubKey("DesktopBackground\\Shell", true);
			this.HKEYNamespace = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace", true);
			this.System = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows", true).CreateSubKey("System");
			this.Explorer = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", true);
			this.Services = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services", true);
			this.Edge = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft", true).CreateSubKey("MicrosoftEdge");
			this.FolderContextMenuHandlers = MyProject.Computer.Registry.ClassesRoot.OpenSubKey("Folder\\shellex\\ContextMenuHandlers", true);
			this.UWTSettings = MyProject.Computer.Registry.ClassesRoot.OpenSubKey("DesktopBackground\\Shell", true).CreateSubKey("UWTSettings").CreateSubKey("Shell");
			this.GiveAccessTo = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions", true).CreateSubKey("Blocked");
			this.HKCUExpAdv = MyProject.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true);
			this.HKCUExp = MyProject.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", true);
			this.MyConfig = MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Ultimate Windows Tweaker");
			this.DuplicateDrive = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", true).CreateSubKey("DelegateFolders");
			this.DesktopContextMenu = MyProject.Computer.Registry.ClassesRoot.OpenSubKey("DesktopBackground\\Shell", true);
			this.CLSID = MyProject.Computer.Registry.ClassesRoot.OpenSubKey("CLSID", true);
		}

		// Token: 0x0400017F RID: 383
		public RegistryKey ClassesRoot;

		// Token: 0x04000180 RID: 384
		public RegistryKey DesktopBackgroundShell;

		// Token: 0x04000181 RID: 385
		public RegistryKey HKEYNamespace;

		// Token: 0x04000182 RID: 386
		public RegistryKey System;

		// Token: 0x04000183 RID: 387
		public RegistryKey Explorer;

		// Token: 0x04000184 RID: 388
		public RegistryKey Services;

		// Token: 0x04000185 RID: 389
		public RegistryKey Edge;

		// Token: 0x04000186 RID: 390
		public RegistryKey FolderContextMenuHandlers;

		// Token: 0x04000187 RID: 391
		public RegistryKey UWTSettings;

		// Token: 0x04000188 RID: 392
		public RegistryKey GiveAccessTo;

		// Token: 0x04000189 RID: 393
		public RegistryKey HKCUExpAdv;

		// Token: 0x0400018A RID: 394
		public RegistryKey HKCUExp;

		// Token: 0x0400018B RID: 395
		public RegistryKey MyConfig;

		// Token: 0x0400018C RID: 396
		public RegistryKey DuplicateDrive;

		// Token: 0x0400018D RID: 397
		public RegistryKey DesktopContextMenu;

		// Token: 0x0400018E RID: 398
		public RegistryKey CLSID;
	}
}
