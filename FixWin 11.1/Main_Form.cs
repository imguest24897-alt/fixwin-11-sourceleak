using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ButtonExtended;
using FixWin.My;
using FixWin.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace FixWin
{
	// Token: 0x0200000C RID: 12
	[DesignerGenerated]
	public partial class Main_Form : Form
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000032D8 File Offset: 0x000014D8
		public Main_Form()
		{
			base.Load += this.Main_Form_Load;
			base.FormClosing += this.Main_Form_FormClosing;
			this.eventHandler = new FixWinFormEventHandler();
			this.space = Environment.NewLine + Environment.NewLine;
			this.Locations = new Locations();
			this.batchFileProvider = new BatchFileProvider();
			this.fileHandler = new FileHandler();
			this.SupportedScrnSizes = new SupportedScreenSizes();
			this.InitializeComponent();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003361 File Offset: 0x00001561
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000336C File Offset: 0x0000156C
		private virtual ComputerHardwareInfo CHI
		{
			[CompilerGenerated]
			get
			{
				return this._CHI;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ComputerHardwareInfo.OperationCompletedEventHandler obj = new ComputerHardwareInfo.OperationCompletedEventHandler(this.CHI_OperationCompleted);
				ComputerHardwareInfo chi = this._CHI;
				if (chi != null)
				{
					chi.OperationCompleted -= obj;
				}
				this._CHI = value;
				chi = this._CHI;
				if (chi != null)
				{
					chi.OperationCompleted += obj;
				}
			}
		}

		// Token: 0x06000040 RID: 64
		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		// Token: 0x06000041 RID: 65
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("winbrand.dll", CharSet = CharSet.Unicode)]
		private static extern string BrandingFormatString(string format);

		// Token: 0x06000042 RID: 66 RVA: 0x000033B0 File Offset: 0x000015B0
		public void SaveToDisk(string resourceName, string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			foreach (string text in executingAssembly.GetManifestResourceNames())
			{
				if (text.ToLower().IndexOf(resourceName.ToLower()) != -1)
				{
					using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
					{
						if (manifestResourceStream != null)
						{
							using (BinaryReader binaryReader = new BinaryReader(manifestResourceStream))
							{
								byte[] buffer = binaryReader.ReadBytes(checked((int)manifestResourceStream.Length));
								using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
								{
									using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
									{
										binaryWriter.Write(buffer);
									}
								}
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000324C File Offset: 0x0000144C
		public void PaintGradient1()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000034A0 File Offset: 0x000016A0
		public void CreateSystemRestorePoint()
		{
			Process.Start("SystemPropertiesProtection");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000034B0 File Offset: 0x000016B0
		public void RestartExplorer()
		{
			foreach (Process process in Process.GetProcessesByName("EXPLORER"))
			{
				process.Kill();
				while (!process.HasExited)
				{
					Application.DoEvents();
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000034EF File Offset: 0x000016EF
		public void Balloon()
		{
			Interaction.MsgBox("You have successfully applied this fix! A reboot may be required to see changes.", MsgBoxStyle.Information, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003500 File Offset: 0x00001700
		public void CheckForUpdates()
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				if (!MyProject.Computer.Network.IsAvailable)
				{
					goto IL_10A;
				}
				IL_1D:
				num2 = 3;
				Interaction.MsgBox("You can continue using FixWin until we check for updates.", MsgBoxStyle.Information, null);
				IL_2D:
				num2 = 4;
				MyProject.Computer.Network.DownloadFile("http://www.thewindowsclub.com/downloads/Fixwin.txt", "C:\\fw.txt");
				IL_48:
				num2 = 5;
				if (Operators.CompareString(MyProject.Computer.FileSystem.ReadAllText("C:\\fw.txt"), MyProject.Application.Info.Version.ToString(), false) != 0)
				{
					goto IL_8C;
				}
				IL_7A:
				num2 = 6;
				Interaction.MsgBox("You're already on up-to-date version!", MsgBoxStyle.Information, null);
				goto IL_F1;
				IL_8C:
				num2 = 8;
				if (Interaction.MsgBox(string.Concat(new string[]
				{
					"FixWin ",
					MyProject.Computer.FileSystem.ReadAllText("C:\\fw.txt"),
					" is available for download!",
					Environment.NewLine,
					Environment.NewLine,
					"Do you want to update?"
				}), MsgBoxStyle.YesNo | MsgBoxStyle.Information, null) != MsgBoxResult.Yes)
				{
					goto IL_F1;
				}
				IL_E3:
				num2 = 9;
				Process.Start("http://www.thewindowsclub.com/fixwin-windows-8");
				IL_F1:
				num2 = 10;
				MyProject.Computer.FileSystem.DeleteFile("C:\\fw.txt");
				goto IL_11B;
				IL_10A:
				num2 = 12;
				Interaction.MsgBox("Internet connection is unavailable!", MsgBoxStyle.Critical, null);
				IL_11B:
				goto IL_1A1;
				IL_120:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_162:
				goto IL_196;
				IL_164:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_174:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_164;
			}
			IL_196:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_1A1:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000036D4 File Offset: 0x000018D4
		public void RemoveTab()
		{
			this.TabControlFixWin.TabPages.Remove(this.Welcome);
			this.TabControlFixWin.TabPages.Remove(this.FileExplorer);
			this.TabControlFixWin.TabPages.Remove(this.InternetConnectivity);
			this.TabControlFixWin.TabPages.Remove(this.Windows11);
			this.TabControlFixWin.TabPages.Remove(this.WindowsStore);
			this.TabControlFixWin.TabPages.Remove(this.SystemTools);
			this.TabControlFixWin.TabPages.Remove(this.QuickFixes);
			this.TabControlFixWin.TabPages.Remove(this.Troubleshooting);
			this.TabControlFixWin.TabPages.Remove(this.Additional);
			this.TabControlFixWin.TabPages.Remove(this.About);
			this.TabControlFixWin.TabPages.Remove(this.Evaluation);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000037D4 File Offset: 0x000019D4
		public ManagementObject GetMaximumResolution()
		{
			string text = "CIM_VideoControllerResolution";
			string str = ".";
			ManagementPath path = new ManagementPath("\\\\" + str + "\\root\\cimv2:" + text);
			ManagementScope scope = new ManagementScope(path);
			ManagementObjectCollection managementObjectCollection;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("select * from " + text))
			{
				managementObjectSearcher.Scope = scope;
				managementObjectCollection = managementObjectSearcher.Get();
			}
			List<ManagementObject> list = new List<ManagementObject>();
			try
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					list.Add((ManagementObject)managementBaseObject);
				}
			}
			finally
			{
				ManagementObjectCollection.ManagementObjectEnumerator enumerator;
				if (enumerator != null)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			ManagementObject managementObject = list.OrderBy((Main_Form._Closure$__.$I25-0 == null) ? (Main_Form._Closure$__.$I25-0 = ((ManagementObject vidSetting) => vidSetting["HorizontalResolution"])) : Main_Form._Closure$__.$I25-0).ThenBy((Main_Form._Closure$__.$I25-1 == null) ? (Main_Form._Closure$__.$I25-1 = ((ManagementObject vidSetting) => vidSetting["VerticalResolution"])) : Main_Form._Closure$__.$I25-1).ThenBy((Main_Form._Closure$__.$I25-2 == null) ? (Main_Form._Closure$__.$I25-2 = ((ManagementObject vidSetting) => vidSetting["NumberOfColors"])) : Main_Form._Closure$__.$I25-2).LastOrDefault<ManagementObject>();
			Interaction.MsgBox(managementObject, MsgBoxStyle.OkOnly, null);
			return managementObject;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003924 File Offset: 0x00001B24
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public void Main_Form_Load(object sender, EventArgs e)
		{
			int num2;
			int num4;
			object obj;
			try
			{
				IL_00:
				int num = 1;
				if (!(MyProject.Computer.Info.OSFullName.Contains("XP") | MyProject.Computer.Info.OSFullName.Contains("Vista") | MyProject.Computer.Info.OSFullName.Contains("7") | MyProject.Computer.Info.OSFullName.Contains("8")))
				{
					goto IL_85;
				}
				IL_6B:
				num = 2;
				Interaction.MsgBox("This program doesn't support this operating system! For Windows 7 and Vista, please use FixWin 1.2!", MsgBoxStyle.Critical, null);
				ProjectData.EndApp();
				goto IL_1C6;
				IL_85:
				ProjectData.ClearProjectError();
				num2 = 1;
				IL_8C:
				num = 5;
				this.processor.Text = Conversions.ToString(MyProject.Computer.Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0").GetValue("ProcessorNameString"));
				IL_C1:
				num = 6;
				this.VersionAbout.Text = "FixWin " + MyProject.Application.Info.Version.ToString();
				IL_EC:
				num = 7;
				if (!Environment.Is64BitOperatingSystem)
				{
					goto IL_109;
				}
				IL_F5:
				num = 8;
				this.bit1.Text = "64-bit";
				goto IL_11C;
				IL_109:
				num = 10;
				this.bit1.Text = "32-bit";
				IL_11C:
				num = 11;
				this.ram.Text = Conversions.ToString(Math.Round(MyProject.Computer.Info.TotalPhysicalMemory / 1048576.0, 0)) + " MB/" + Conversions.ToString(Math.Round(MyProject.Computer.Info.TotalPhysicalMemory / 1048576000.0, 0)) + " GB";
				IL_185:
				num = 12;
				this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
				IL_1A4:
				num = 13;
				this.RemoveTab();
				IL_1AD:
				num = 14;
				this.TabControlFixWin.TabPages.Add(this.Welcome);
				IL_1C6:
				num = 15;
				if (!Environment.Is64BitOperatingSystem)
				{
					goto IL_1D5;
				}
				IL_1D0:
				num = 16;
				goto IL_1D8;
				IL_1D5:
				num = 18;
				IL_1D8:
				num = 19;
				this.os.Text = Main_Form.BrandingFormatString("%WINDOWS_LONG%");
				IL_1F0:
				num = 20;
				this.username.Text = Environment.UserName;
				IL_203:
				num = 21;
				this.CHI = new ComputerHardwareInfo();
				IL_211:
				num = 22;
				this.CHI.GetHardwareInfo();
				IL_21F:
				num = 23;
				this.ListBox1.Items.AddRange(this.SupportedScrnSizes.GetSizesAsStrings(""));
				IL_242:
				num = 24;
				string[] sizesAsStrings = this.SupportedScrnSizes.GetSizesAsStrings("");
				IL_256:
				num = 25;
				this.display_resolution.Text = sizesAsStrings[checked(sizesAsStrings.Length - 1)];
				IL_26B:
				goto IL_325;
				IL_270:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_2E6:
				goto IL_31A;
				IL_2E8:
				num4 = num;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num2);
				IL_2F8:;
			}
			catch when (endfilter(obj is Exception & num2 != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_2E8;
			}
			IL_31A:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_325:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003C7C File Offset: 0x00001E7C
		private void CHI_OperationCompleted(object sender, ComputerHardwareInfo.OperationCompletedEventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				this.processor_name.Text = e.ProcessorName;
				IL_1A:
				num2 = 3;
				this.processor_cores.Text = e.ProcessorCores;
				IL_2D:
				num2 = 4;
				this.processor_Currentclockspeed.Text = Conversions.ToString(Math.Round(Conversions.ToDouble(e.ProcessorCurrentClockSpeed) / 1000.0, 1)) + " Ghz";
				IL_64:
				num2 = 5;
				this.processor_Maxclockspeed.Text = Conversions.ToString(Math.Round(Conversions.ToDouble(e.ProcessorMaxClockSpeed) / 1000.0, 1)) + " Ghz";
				IL_9B:
				num2 = 6;
				this.processor_Thread.Text = e.ProcessorThread;
				IL_AE:
				num2 = 7;
				this.processor_LogicalProcessors.Text = e.ProcessorNumberOfLog;
				IL_C1:
				num2 = 8;
				this.memory_totalram.Text = Conversions.ToString(Math.Round(MyProject.Computer.Info.TotalPhysicalMemory / 1048576.0, 0)) + " MB";
				IL_FE:
				num2 = 9;
				this.memory_availableRAM.Text = Conversions.ToString(Math.Round(MyProject.Computer.Info.AvailablePhysicalMemory / 1048576.0, 0)) + " MB";
				IL_13C:
				num2 = 10;
				this.memory_Speed.Text = e.SpeedOfRAM + " MHz";
				IL_15A:
				num2 = 11;
				this.MaxRefreshRate.Text = e.MAXREFRATE;
				IL_16E:
				num2 = 12;
				this.Display_monitortype.Text = e.MonitorType;
				IL_182:
				num2 = 13;
				this.Display_Graphicscard.Text = e.GraphicsCard1;
				IL_196:
				goto IL_220;
				IL_19B:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_1E1:
				goto IL_215;
				IL_1E3:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_1F3:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_1E3;
			}
			IL_215:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_220:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003ED0 File Offset: 0x000020D0
		public void Side_Welcome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.Welcome);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003FDC File Offset: 0x000021DC
		public void Side_FileExplorer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.FileExplorer);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000040E8 File Offset: 0x000022E8
		public void Side_Internet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.InternetConnectivity);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000041F4 File Offset: 0x000023F4
		public void Side_ModernUI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.Windows11);
			this.TabControlFixWin.TabPages.Add(this.WindowsStore);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004314 File Offset: 0x00002514
		public void Side_SystemTools_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.SystemTools);
			this.TabControlFixWin.TabPages.Add(this.Evaluation);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004434 File Offset: 0x00002634
		public void Side_Troubleshooting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.Troubleshooting);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004540 File Offset: 0x00002740
		public void Side_Additional_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.Additional);
			this.TabControlFixWin.TabPages.Add(this.QuickFixes);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004660 File Offset: 0x00002860
		public void Side_About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Side_Welcome.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_FileExplorer.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Internet.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_ModernUI.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_SystemTools.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Troubleshooting.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_Additional.Font = new Font(this.Side_Welcome.Font, FontStyle.Regular);
			this.Side_About.Font = new Font(this.Side_Welcome.Font, FontStyle.Bold);
			this.RemoveTab();
			this.TabControlFixWin.TabPages.Add(this.About);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004769 File Offset: 0x00002969
		public void TWC_Logo_Click(object sender, EventArgs e)
		{
			Process.Start("Http://www.thewindowsclub.com");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004776 File Offset: 0x00002976
		public void CreateRestorePoint_Click(object sender, EventArgs e)
		{
			this.CreateSystemRestorePoint();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000477E File Offset: 0x0000297E
		public void SystemFileChecker_Click(object sender, EventArgs e)
		{
			Interaction.MsgBox("The scan has started. It will take few minutes to complete. Until then you can continue using FixWin", MsgBoxStyle.Information, null);
			Interaction.Shell("sfc /scannow", AppWinStyle.NormalFocus, false, -1);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000479C File Offset: 0x0000299C
		public void RecycleBinIconMissing_Click(object sender, EventArgs e)
		{
			try
			{
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", true).CreateSubKey("Policies").CreateSubKey("NonEnum").DeleteValue("{645FF040-5081-101B-9F08-00AA002F954E}", false);
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true).CreateSubKey("HideDesktopIcons\\NewStartPanel").DeleteValue("{645FF040-5081-101B-9F08-00AA002F954E}", false);
				this.RestartExplorer();
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000488C File Offset: 0x00002A8C
		public void ResetFolderViewSettings_Click(object sender, EventArgs e)
		{
			Process.Start("mdsched");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000489C File Offset: 0x00002A9C
		public void FixFolderOptions_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteValue("DisallowCpl", false);
				IL_38:
				num2 = 3;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteSubKeyTree("DisallowCpl", false);
				IL_69:
				num2 = 4;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteValue("NoFolderOptions", false);
				IL_9A:
				num2 = 5;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteValue("NoFolderOptions", false);
				IL_CB:
				num2 = 6;
				this.RestartExplorer();
				IL_D3:
				num2 = 7;
				this.Balloon();
				IL_DB:
				goto IL_14A;
				IL_DD:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_10B:
				goto IL_13F;
				IL_10D:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_11D:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_10D;
			}
			IL_13F:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_14A:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004A18 File Offset: 0x00002C18
		public void FixRecycleBinIcon_Click(object sender, EventArgs e)
		{
			try
			{
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").SetValue("", "C:\\Windows\\System32\\imageres.dll,-54", RegistryValueKind.String);
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").SetValue("empty", "C:\\Windows\\System32\\imageres.dll,-55", RegistryValueKind.String);
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").SetValue("full", "C:\\Windows\\System32\\imageres.dll,-54", RegistryValueKind.String);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004B54 File Offset: 0x00002D54
		public void ExplorerDoesNTStart_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true).SetValue("Shell", "explorer.exe", RegistryValueKind.String);
				IL_33:
				num2 = 3;
				this.Balloon();
				IL_3B:
				goto IL_9A;
				IL_3D:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_5B:
				goto IL_8F;
				IL_5D:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_6D:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_5D;
			}
			IL_8F:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9A:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004C14 File Offset: 0x00002E14
		public void ThumbnailsNotShowing_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true).SetValue("IconsOnly", 0, RegistryValueKind.DWord);
				IL_34:
				num2 = 3;
				this.Balloon();
				IL_3C:
				goto IL_9B;
				IL_3E:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_5C:
				goto IL_90;
				IL_5E:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_6E:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_5E;
			}
			IL_90:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9B:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public void RightClickMenuIEDisabled_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft", true).CreateSubKey("Internet Explorer").CreateSubKey("Restrictions").DeleteValue("NoBrowserContextMenu", false);
				IL_42:
				num2 = 3;
				this.Balloon();
				IL_4A:
				goto IL_A9;
				IL_4C:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_6A:
				goto IL_9E;
				IL_6C:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_7C:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_6C;
			}
			IL_9E:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_A9:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public void ResetInternetProtocol_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("netsh int ip reset", AppWinStyle.Hide, true, -1);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004E34 File Offset: 0x00003034
		public void FixDNSResolverCache_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("ipconfig /flushdns", AppWinStyle.Hide, true, -1);
				Interaction.Shell("net stop dnscache", AppWinStyle.Hide, true, -1);
				Interaction.Shell("net start dnscache", AppWinStyle.Hide, true, -1);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004EE0 File Offset: 0x000030E0
		public void ClearWindowsUpdateHistory_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("net stop wuauserv", AppWinStyle.NormalFocus, true, -1);
				IL_17:
				num2 = 3;
				Interaction.Shell("net stop AeLookupSvc", AppWinStyle.NormalFocus, true, -1);
				IL_27:
				num2 = 4;
				MyProject.Computer.FileSystem.DeleteFile("C:\\Windows\\SoftwareDistribution\\DataStore\\Logs\\edb.log", UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
				IL_3F:
				num2 = 5;
				Interaction.Shell("net start wuauserv", AppWinStyle.NormalFocus, true, -1);
				IL_4F:
				num2 = 6;
				Interaction.Shell("net start AeLookupSvc", AppWinStyle.NormalFocus, true, -1);
				IL_5F:
				num2 = 7;
				this.Balloon();
				IL_67:
				goto IL_D6;
				IL_69:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_97:
				goto IL_CB;
				IL_99:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_A9:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_99;
			}
			IL_CB:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_D6:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004FDC File Offset: 0x000031DC
		public void ResetWindowsFirewall_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("netsh advfirewall reset", AppWinStyle.Hide, true, -1);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000506C File Offset: 0x0000326C
		public void ClearStoreCache_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Process.Start("WSReset.exe");
				IL_14:
				num2 = 3;
				this.Balloon();
				IL_1C:
				goto IL_7B;
				IL_1E:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_3C:
				goto IL_70;
				IL_3E:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_4E:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_3E;
			}
			IL_70:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_7B:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000510C File Offset: 0x0000330C
		public void TheApplicationsWasNotInstalled_Click(object sender, EventArgs e)
		{
			Interaction.Shell("net stop wuauserv", AppWinStyle.MinimizedFocus, false, -1);
			this.RunInCMD("cmd.exe /c ren C:\\Windows\\SoftwareDistribution SoftwareDistribution.bck", 0, true);
			Interaction.Shell("net start wuauserv", AppWinStyle.MinimizedFocus, false, -1);
			this.Balloon();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005140 File Offset: 0x00003340
		public void SomethingHappened_Click(object sender, EventArgs e)
		{
			try
			{
				MyProject.Computer.FileSystem.CreateDirectory("C:\\UWT");
				string text = "@ECHO OFF" + Environment.NewLine + "PowerShell.exe -NoProfile -Command \"& {Start-Process PowerShell.exe -ArgumentList '-NoProfile -ExecutionPolicy Bypass -File \"\"C:\\UWT\\Script1.ps1\"\"' -Verb RunAs}\"";
				string text2 = "Get-AppXPackage | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}";
				MyProject.Computer.FileSystem.WriteAllText("C:\\UWT\\script.bat", text, false);
				MyProject.Computer.FileSystem.WriteAllText("C:\\UWT\\script1.ps1", text2, false);
				Process.Start("C:\\UWT\\Script.bat");
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000051E4 File Offset: 0x000033E4
		public void WEI1()
		{
			foreach (string file in Directory.GetFiles("C:\\ProgramData\\Microsoft\\Windows\\AppRepository", "*.log", System.IO.SearchOption.TopDirectoryOnly))
			{
				try
				{
					MyProject.Computer.FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
				}
				catch (Exception ex)
				{
					Interaction.MsgBox(string.Concat(new string[]
					{
						ex.Message,
						Environment.NewLine,
						Environment.NewLine,
						ex.StackTrace,
						Environment.NewLine,
						Environment.NewLine,
						"Please report this to The Windows Club!"
					}), MsgBoxStyle.Critical, null);
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005294 File Offset: 0x00003494
		public void SomethingHappenedAndWindows_Click(object sender, EventArgs e)
		{
			int num;
			int num3;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				goto IL_5E;
				IL_09:
				int num2 = num3 + 1;
				num3 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num2);
				IL_1F:
				goto IL_53;
				IL_21:
				int num4;
				num3 = num4;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_31:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num3 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_21;
			}
			IL_53:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_5E:
			if (num3 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000324C File Offset: 0x0000144C
		public void ThisAppCouldntBeInstalled_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005318 File Offset: 0x00003518
		public void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.thewindowsclub.com");
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005325 File Offset: 0x00003525
		public void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.downloadinformer.com");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005332 File Offset: 0x00003532
		public void LinkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/fixwin-11-for-windows");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000533F File Offset: 0x0000353F
		public void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://forum.thewindowsclub.com/twc-windows-freeware/37455-feedback-support-fixwin-10-windows-10-a.html");
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000534C File Offset: 0x0000354C
		public void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.BackgroundWorker2.RunWorkerAsync();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000535C File Offset: 0x0000355C
		public void TaskManagerHasBeen_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("System").DeleteValue("DisableTaskMgr", false);
				IL_38:
				num2 = 3;
				this.Balloon();
				IL_40:
				goto IL_9F;
				IL_42:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_60:
				goto IL_94;
				IL_62:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_72:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_62;
			}
			IL_94:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9F:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005420 File Offset: 0x00003620
		public void CommandPromptHasBeen_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft\\Windows", true).CreateSubKey("System").DeleteValue("DisableCMD", false);
				IL_38:
				num2 = 3;
				this.Balloon();
				IL_40:
				goto IL_9F;
				IL_42:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_60:
				goto IL_94;
				IL_62:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_72:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_62;
			}
			IL_94:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9F:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000054E4 File Offset: 0x000036E4
		public void RegistryEditorHasBeen_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("System").DeleteValue("DisableRegistryTools", false);
				IL_38:
				num2 = 3;
				this.Balloon();
				IL_40:
				goto IL_9F;
				IL_42:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_60:
				goto IL_94;
				IL_62:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_72:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_62;
			}
			IL_94:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9F:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000055A8 File Offset: 0x000037A8
		public void EnableMMCSnap_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft", true).DeleteSubKeyTree("MMC", false);
				IL_2E:
				num2 = 3;
				this.Balloon();
				IL_36:
				goto IL_95;
				IL_38:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_56:
				goto IL_8A;
				IL_58:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_68:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_58;
			}
			IL_8A:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_95:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005664 File Offset: 0x00003864
		public void ResetWindowsSearch_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("net stop WSearch /Y", AppWinStyle.Hide, true, -1);
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\WSearch", true).SetValue("Start", 2, RegistryValueKind.DWord);
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows Search", true).SetValue("SetupCompletedSuccessfully", 0, RegistryValueKind.DWord);
				Interaction.Shell("net start WSearch", AppWinStyle.Hide, true, -1);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005758 File Offset: 0x00003958
		public void SystemRestoreHasBeen_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT", true).CreateSubKey("SystemRestore").DeleteValue("DisableConfig", false);
				IL_38:
				num2 = 3;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT", true).CreateSubKey("SystemRestore").DeleteValue("DisableSR", false);
				IL_69:
				num2 = 4;
				this.Balloon();
				IL_71:
				goto IL_D4;
				IL_73:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_95:
				goto IL_C9;
				IL_97:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_A7:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_97;
			}
			IL_C9:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_D4:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005854 File Offset: 0x00003A54
		public void DeviceManagerIsNot_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("net Start PlugPlay", AppWinStyle.Hide, true, -1);
				IL_17:
				num2 = 3;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\PlugPlay", true).SetValue("Start", 2, RegistryValueKind.DWord);
				IL_44:
				num2 = 4;
				this.Balloon();
				IL_4C:
				goto IL_AF;
				IL_4E:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_70:
				goto IL_A4;
				IL_72:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_82:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_72;
			}
			IL_A4:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_AF:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005928 File Offset: 0x00003B28
		public void RepairWindowsDefender_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("regsvr32 wuaueng.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 wucltui.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 softpub.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 wintrust.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 initpki.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 wups.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 wuweb.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 atl.dll", AppWinStyle.Hide, false, -1);
				Interaction.Shell("regsvr32 mssip32.dll", AppWinStyle.Hide, false, -1);
				File.WriteAllText(Application.StartupPath + "\\WinDef.reg", Resources.TextFile1);
				Process.Start(Application.StartupPath + "\\WinDef.reg");
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005A58 File Offset: 0x00003C58
		public void EnableHibernate_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("cmd.exe /c powercfg -h on", AppWinStyle.Hide, false, -1);
				IL_17:
				num2 = 3;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", true).CreateSubKey("FlyoutMenuSettings").SetValue("ShowHibernateOption", 1, RegistryValueKind.DWord);
				IL_4E:
				num2 = 4;
				this.Balloon();
				IL_56:
				goto IL_B9;
				IL_58:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_7A:
				goto IL_AE;
				IL_7C:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_8C:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_7C;
			}
			IL_AE:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_B9:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005B38 File Offset: 0x00003D38
		public void RestoreStickyNotes_Click(object sender, EventArgs e)
		{
			try
			{
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", true).CreateSubKey("Applets\\StickyNotes").SetValue("PROMPT_ON_DELETE", 1, RegistryValueKind.DWord);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public void AeroNotWorking_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true).SetValue("WindowArrangementActive", 1, RegistryValueKind.DWord);
				IL_34:
				num2 = 3;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true).DeleteValue("DisablePreviewDesktop", false);
				IL_5B:
				num2 = 4;
				this.Balloon();
				IL_63:
				goto IL_C6;
				IL_65:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_87:
				goto IL_BB;
				IL_89:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_99:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_89;
			}
			IL_BB:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_C6:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005CDC File Offset: 0x00003EDC
		public void FixCorruptedDesktopIcons_Click(object sender, EventArgs e)
		{
			try
			{
				Utils.DeleteThumbnailAndIconCacheFiles();
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000324C File Offset: 0x0000144C
		public void Timer1_Tick(object sender, EventArgs e)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005D64 File Offset: 0x00003F64
		public void ResetRecycleBin_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				if (Interaction.MsgBox("It will delete all your recycle bin items. Do you want to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Critical | MsgBoxStyle.Question, null) != MsgBoxResult.Yes)
				{
					goto IL_78;
				}
				IL_19:
				num2 = 3;
				IEnumerator<DriveInfo> enumerator = MyProject.Computer.FileSystem.Drives.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DriveInfo driveInfo = enumerator.Current;
					IL_3A:
					num2 = 4;
					Interaction.Shell("cmd.exe /c rd /s /q " + driveInfo.Name + "$RECYCLE.BIN", AppWinStyle.MinimizedFocus, false, -1);
					IL_5B:
					num2 = 5;
				}
				IL_65:
				num2 = 6;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
				IL_70:
				num2 = 7;
				this.Balloon();
				IL_78:
				goto IL_E7;
				IL_7A:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_A8:
				goto IL_DC;
				IL_AA:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_BA:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_AA;
			}
			IL_DC:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_E7:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005E70 File Offset: 0x00004070
		public void ResetPCSettings_Click(object sender, EventArgs e)
		{
			Interaction.Shell("powershell -ExecutionPolicy Unrestricted Add-AppxPackage -DisableDevelopmentMode -Register $Env:SystemRoot\\ImmersiveControlPanel\\AppxManifest.xml", AppWinStyle.Hide, true, -1);
			this.Balloon();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005E88 File Offset: 0x00004088
		public void DisableOnedrive_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows", true).CreateSubKey("OneDrive").SetValue("DisableFileSync", 1, RegistryValueKind.DWord);
				IL_3E:
				num2 = 3;
				this.Balloon();
				IL_46:
				goto IL_A5;
				IL_48:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_66:
				goto IL_9A;
				IL_68:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_78:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_68;
			}
			IL_9A:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_A5:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005F54 File Offset: 0x00004154
		public void Revert_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows", true).CreateSubKey("SkyDrive").DeleteValue("DisableFileSync", false);
				IL_38:
				num2 = 3;
				this.Balloon();
				IL_40:
				goto IL_9F;
				IL_42:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_60:
				goto IL_94;
				IL_62:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_72:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_62;
			}
			IL_94:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9F:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006018 File Offset: 0x00004218
		public void CDDriveOrDVD_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E965-E325-11CE-BFC1-08002BE10318}", true).DeleteValue("UpperFilters", false);
				IL_2E:
				num2 = 3;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E965-E325-11CE-BFC1-08002BE10318}", true).DeleteValue("LowerFilters", false);
				IL_55:
				num2 = 4;
				this.Balloon();
				IL_5D:
				goto IL_C0;
				IL_5F:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_81:
				goto IL_B5;
				IL_83:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_93:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_83;
			}
			IL_B5:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_C0:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006100 File Offset: 0x00004300
		public void ResetIE_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("rundll32.exe inetcpl.cpl,ResetIEtoDefaults", AppWinStyle.Hide, true, -1);
				IL_17:
				goto IL_72;
				IL_19:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_33:
				goto IL_67;
				IL_35:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_45:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_35;
			}
			IL_67:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_72:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006198 File Offset: 0x00004398
		public void RuntimeErrors_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).SetValue("Disable Script Debugger", "yes", RegistryValueKind.String);
				IL_33:
				num2 = 3;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).SetValue("DisableScriptDebuggerIE", "yes", RegistryValueKind.String);
				IL_5F:
				num2 = 4;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).SetValue("Error Dlg Displayed On Every Error", "no", RegistryValueKind.String);
				IL_8B:
				num2 = 5;
				this.Balloon();
				IL_93:
				goto IL_FA;
				IL_95:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_BB:
				goto IL_EF;
				IL_BD:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_CD:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_BD;
			}
			IL_EF:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_FA:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000062B8 File Offset: 0x000044B8
		public void OptimizeIE_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true).SetValue("MaxConnectionsPer1_0Server", 10, RegistryValueKind.DWord);
				IL_35:
				num2 = 3;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true).SetValue("MaxConnectionsPerServer", 10, RegistryValueKind.DWord);
				IL_63:
				num2 = 4;
				this.Balloon();
				IL_6B:
				goto IL_CE;
				IL_6D:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_8F:
				goto IL_C3;
				IL_91:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_A1:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_91;
			}
			IL_C3:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_CE:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000063AC File Offset: 0x000045AC
		public void PlayingAudio_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id AudioPlaybackDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006438 File Offset: 0x00004638
		public void RecordingAudio_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id AudioRecordingDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000064C4 File Offset: 0x000046C4
		public void HardwareAndDevices_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id DeviceDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006550 File Offset: 0x00004750
		public void InternetConnections_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id NetworkDiagnosticsWeb", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000065DC File Offset: 0x000047DC
		public void Power_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id PowerDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006668 File Offset: 0x00004868
		public void Printer_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id PrinterDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000066F4 File Offset: 0x000048F4
		public void SharedFolders_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id NetworkDiagnosticsFileShare", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006780 File Offset: 0x00004980
		public void Homegroup_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id HomeGroupDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000680C File Offset: 0x00004A0C
		public void IEPerformance_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id IEBrowseWebDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006898 File Offset: 0x00004A98
		public void IESafety_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id IESecurityDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006924 File Offset: 0x00004B24
		public void WMPSettings_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id WindowsMediaPlayerConfigurationDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000069B0 File Offset: 0x00004BB0
		public void WMPLibrary_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id WindowsMediaPlayerLibraryDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006A3C File Offset: 0x00004C3C
		public void WMPDVD_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id WindowsMediaPlayerDVDDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public void IncomingConnections_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id NetworkDiagnosticsInbound", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006B54 File Offset: 0x00004D54
		public void SystemMaintenence_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id MaintenanceDiagnostic", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public void NetworkAdapter_Click(object sender, EventArgs e)
		{
			try
			{
				Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id NetworkDiagnosticsNetworkAdapter", AppWinStyle.Hide, false, -1);
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006C6C File Offset: 0x00004E6C
		public void TaskbarJumplist_Click(object sender, EventArgs e)
		{
			try
			{
				MyProject.Computer.FileSystem.DeleteDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Recent\\AutomaticDestinations", DeleteDirectoryOption.DeleteAllContents);
				MyProject.Computer.FileSystem.DeleteDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Recent\\CustomDestinations", DeleteDirectoryOption.DeleteAllContents);
				this.Balloon();
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(string.Concat(new string[]
				{
					ex.Message,
					Environment.NewLine,
					Environment.NewLine,
					ex.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Please report this to The Windows Club!"
				}), MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006D38 File Offset: 0x00004F38
		public void ActionCenterAnd_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("net stop winmgmt", AppWinStyle.Hide, false, -1);
				IL_17:
				num2 = 3;
				Interaction.Shell("net stop wscsvc", AppWinStyle.Hide, false, -1);
				IL_27:
				num2 = 4;
				Interaction.Shell("Rd /s /q \"%windir%\\System32\\wbem\\Repository\"", AppWinStyle.Hide, false, -1);
				IL_37:
				num2 = 5;
				Interaction.Shell("net start wscsvc", AppWinStyle.Hide, false, -1);
				IL_47:
				num2 = 6;
				Interaction.Shell("net start winmgmt", AppWinStyle.Hide, false, -1);
				IL_57:
				num2 = 7;
				this.Balloon();
				IL_5F:
				goto IL_CE;
				IL_61:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_8F:
				goto IL_C3;
				IL_91:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_A1:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_91;
			}
			IL_C3:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_CE:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006E2C File Offset: 0x0000502C
		public void ClassNotRegistred_Click(object sender, EventArgs e)
		{
			Interaction.Shell("Regsvr32 /s ExplorerFrame.dll", AppWinStyle.NormalFocus, false, -1);
			this.Balloon();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006E44 File Offset: 0x00005044
		public void balloontips_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true).DeleteValue("TaskbarNoNotification", false);
				IL_2E:
				num2 = 3;
				this.Balloon();
				IL_36:
				goto IL_95;
				IL_38:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_56:
				goto IL_8A;
				IL_58:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_68:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_58;
			}
			IL_8A:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_95:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006F00 File Offset: 0x00005100
		public void WindowsScriptHost_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft", true).CreateSubKey("Windows Script Host").CreateSubKey("Settings").DeleteValue("Enabled", false);
				IL_42:
				num2 = 3;
				this.Balloon();
				IL_4A:
				goto IL_A9;
				IL_4C:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_6A:
				goto IL_9E;
				IL_6C:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_7C:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_6C;
			}
			IL_9E:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_A9:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006FD0 File Offset: 0x000051D0
		public void appswitcher_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("cmd.exe /c icacls \"%programfiles%\\Microsoft Office 15\" /grant *S-1-15-2-1:(OI)(CI)RX", AppWinStyle.Hide, false, -1);
				IL_17:
				num2 = 3;
				Interaction.Shell("cmd.exe /c icacls \"%programfiles%\\Microsoft Office\" /grant *S-1-15-2-1:(OI)(CI)RX", AppWinStyle.Hide, false, -1);
				IL_27:
				num2 = 4;
				this.Balloon();
				IL_2F:
				goto IL_92;
				IL_31:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_53:
				goto IL_87;
				IL_55:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_65:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_55;
			}
			IL_87:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_92:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007088 File Offset: 0x00005288
		public void recoveryimagecant_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\VSS", true).SetValue("Start", 2);
				IL_33:
				num2 = 3;
				Interaction.Shell("net start vss", AppWinStyle.Hide, false, -1);
				IL_43:
				num2 = 4;
				this.Balloon();
				IL_4B:
				goto IL_AE;
				IL_4D:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_6F:
				goto IL_A3;
				IL_71:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_81:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_71;
			}
			IL_A3:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_AE:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000715C File Offset: 0x0000535C
		public void showhiddenfiles_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\SHOWALL", true).SetValue("CheckedValue", 1);
				IL_33:
				num2 = 3;
				this.Balloon();
				IL_3B:
				goto IL_9A;
				IL_3D:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_5B:
				goto IL_8F;
				IL_5D:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_6D:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_5D;
			}
			IL_8F:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9A:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000721C File Offset: 0x0000541C
		public void internetoptionsmissing_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /c regsvr32 /n /i /s inetcpl.cpl", AppWinStyle.NormalFocus, true, -1);
			this.Balloon();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007232 File Offset: 0x00005432
		public void wmp_Click(object sender, EventArgs e)
		{
			Interaction.Shell("regsvr32 /s jscript.dll", AppWinStyle.Hide, true, -1);
			Interaction.Shell("regsvr32 /s vbscript.dll", AppWinStyle.Hide, true, -1);
			this.Balloon();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007256 File Offset: 0x00005456
		public void winsock_Click(object sender, EventArgs e)
		{
			Interaction.Shell("netsh winsock reset", AppWinStyle.NormalFocus, true, -1);
			this.Balloon();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000726C File Offset: 0x0000546C
		public void ResetWindowsSecurity_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /c secedit /configure /cfg %windir%\\inf\\defltbase.inf /db defltbase.sdb /verbose", AppWinStyle.NormalFocus, true, -1);
			this.Balloon();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007282 File Offset: 0x00005482
		public void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			this.CheckForUpdates();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000728A File Offset: 0x0000548A
		public void TWC_Logo_Click_1(object sender, EventArgs e)
		{
			Process.Start("Http://www.thewindowsclub.com");
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007297 File Offset: 0x00005497
		public void dism_Click(object sender, EventArgs e)
		{
			Interaction.MsgBox("This process has been started and a CMD window will show up now.", MsgBoxStyle.Information, null);
			Interaction.Shell("cmd.exe /k Dism /Online /Cleanup-Image /RestoreHealth", AppWinStyle.NormalFocus, false, -1);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000072B5 File Offset: 0x000054B5
		public void Button1_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Scan.Show();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000072C8 File Offset: 0x000054C8
		private void Button2_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This fix will restore Recycle Bin icon on desktop which was removed by some program or some virus. It will make some registry changes.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the value: {645FF040-5081-101B-9F08-00AA002F954E} from" + Environment.NewLine + "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\NonEnum";
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007328 File Offset: 0x00005528
		private void Button3_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This error may come due to problem with Windows Error Reporting. Fix 1 will start Windows Memory Diagnostics and Fix 2 will modify registry and disable WerSvc service.";
			MyProject.Forms.Description.CauseText.Text = "Fix 1: Starts \"Windows Memory Diagnostics\"." + Environment.NewLine + "Fix 2: Set the value \"Start\" to 4: HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\WerSvc.";
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007388 File Offset: 0x00005588
		private void Button4_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "File Explorer Options may not be available in Control Panel or you might have been receiving the message that it has been disabled by administrator.";
			MyProject.Forms.Description.CauseText.Text = "Delete the values \"DisallowCpl\" and \"NoFolderOptions\" from \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\"";
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000073D8 File Offset: 0x000055D8
		private void Button5_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Recycle Bin's icon changes on the condition if it is empty or has one or more items. If it is empty, its icon shows empty box. If it has any number of items, its icon is different. If this behaviour is not observed, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Go to HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID\\{645FF040-5081-101B-9F08-00AA002F954E}\\DefaultIcon",
				Environment.NewLine,
				"Set (Default) to C:\\Windows\\System32\\imageres.dll,-54",
				Environment.NewLine,
				"Set empty to C:\\Windows\\System32\\imageres.dll,-55",
				Environment.NewLine,
				"Set full to C:\\Windows\\System32\\imageres.dll,-54"
			});
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007464 File Offset: 0x00005664
		private void Button6_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This fix will help you in following condition: Whenever you start your computer, you see nothing except the wallpaper. Taskbar and other Windows components aren't visible.";
			MyProject.Forms.Description.CauseText.Text = "Set the value \"Shell\" to \"explorer.exe\": HKLM\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon";
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000074B4 File Offset: 0x000056B4
		private void Button7_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "In File Explorer, thumbnails of images are not shown and instead a predefined logo is shown.";
			MyProject.Forms.Description.CauseText.Text = "Set value \"Icons Only\" to 0: HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced";
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007504 File Offset: 0x00005704
		private void Button8_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This will fix all Recycle Bin related problems. However all of the deleted items will be deleted from Recycle Bin too.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Run the following CMD command:",
				Environment.NewLine,
				"rd /s /q C:\\$RECYCLE.BIN",
				Environment.NewLine,
				Environment.NewLine,
				"where C:\\ is the drive letter. FixWin performs this command for every drive."
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007588 File Offset: 0x00005788
		private void Button9_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Inserted your CD/DVD and isn't recognised? You can try using this fix after which Windows as well as 3rd party programs will also recognise them.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the values \"UpperFilters\" and \"LowerFilters\" from:" + Environment.NewLine + Environment.NewLine + "HKLM\\SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E965-E325-11CE-BFC1-08002BE10318}";
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000075EC File Offset: 0x000057EC
		private void Button10_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This kind of errors usually come when cache increases or integration with Windows breaks.";
			MyProject.Forms.Description.CauseText.Text = "Re-register ExplorerFrame.dll using CMD command:" + Environment.NewLine + Environment.NewLine + "Regsvr32 /s ExplorerFrame.dll";
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007650 File Offset: 0x00005850
		private void Button11_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If option to show/hide hidden files, folders is not available in Folder Options, there's chance that someone messed up with Windows Registry.";
			MyProject.Forms.Description.CauseText.Text = "Set the value 'CheckedValue' to 1:" + Environment.NewLine + Environment.NewLine + "HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\SHOWALL";
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000076B4 File Offset: 0x000058B4
		private void Button21_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Want to open a new tab in a window or save link as in Internet Explorer using Context Menu and it isn't opening? This fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the value \"NoBrowserContextMenu\" from:" + Environment.NewLine + Environment.NewLine + "HKLM\\SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Restrictions";
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007718 File Offset: 0x00005918
		private void Button20_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you have issues connecting to the internet, try to reset Internet Protocol. This fix will execute a cmd command which will modify registry to reset TCP/IP.";
			MyProject.Forms.Description.CauseText.Text = "Runs the following CMD command:" + Environment.NewLine + Environment.NewLine + "netsh int ip reset";
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000777C File Offset: 0x0000597C
		private void Button19_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "DNS cache stores the IP addresses of webservers that contain pages recently visited. If the location of web server changes before the entry in your DNS cache, you will be unable to access thee site. Hence this fix will help you by clearing the cache.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Runs the following CMD commands:",
				this.space,
				"ipconfig /flushdns",
				Environment.NewLine,
				"net stop dnscache",
				Environment.NewLine,
				"net start dnscache"
			});
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000780C File Offset: 0x00005A0C
		private void Button18_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Windows Update shows the history of installed and failed updates. If the list goes too long for you and you want to clear it, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"1. Run the following CMD commands:",
				Environment.NewLine,
				"net stop wuauserv",
				Environment.NewLine,
				"net stop AeLookupSvc",
				Environment.NewLine,
				"2. Delete the file C:\\Windows\\SoftwareDistribution\\DataStore\\Logs\\edb.log",
				Environment.NewLine,
				"3. Run the following commands:",
				Environment.NewLine,
				"net start wuauserv",
				Environment.NewLine,
				"net start AeLookupSvc"
			});
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000078D0 File Offset: 0x00005AD0
		private void Button17_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you've changed some settings and configuration of Windows Firewall and facing some problems, you may try to reset them. This fix will let you do so.";
			MyProject.Forms.Description.CauseText.Text = "Run the CMD command: netsh advfirewall reset.";
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007920 File Offset: 0x00005B20
		private void Button16_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you've changed some settings and configuration of Internet Explorer and facing some problems, you may try to reset them. This fix will let you do so.";
			MyProject.Forms.Description.CauseText.Text = "Run the CMD command: rundll32.exe inetcpl.cpl,ResetIEtoDefaults.";
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007970 File Offset: 0x00005B70
		private void Button15_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "A runtime error is a problem that prevents Internet Explorer from working correctly. These errors can be caused if a website uses an HTML code which isn't compatible with web browser.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Navigate to \"HKCU\\Software\\Microsoft\\Internet Explorer\\Main\" and set the values:",
				this.space,
				"\"Disable Script Debugger\" to \"yes\"",
				Environment.NewLine,
				"\"DisableScriptDebuggerIE\" to \"yes\"",
				Environment.NewLine,
				"\"Error Dlg Displayed On Every Error\" to \"no\""
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007A00 File Offset: 0x00005C00
		private void Button14_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "By default, Internet Explorer limits the number of downloads at a time. This fix will speed up IE by increasing the no. of downloads at a time.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Navigate to \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\" and set the values:",
				this.space,
				"MaxConnectionsPer1_0Server to 10",
				Environment.NewLine,
				"MaxConnectionsPerServer to 10"
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007A80 File Offset: 0x00005C80
		private void Button13_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Internet Options enables you to tweak Security level, Homepage, Connection and various other settings of Internet Explorer. If you find the option to launch Internet Options is missing, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Run the CMD command: regsvr32 /n /i /s inetcpl.cpl";
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007AD0 File Offset: 0x00005CD0
		private void Button12_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Winsock is a technical specification that defines how Windows network software should access network services, especially TCP/IP. Windows comes with a DLL that implements the API and coordinates Windows programs and TCP/IP connections. But sometimes it gets corrupted. As a consequence you mayn't be able to connect to internet. This fix will help you!";
			MyProject.Forms.Description.CauseText.Text = "Run the following CMD command: netsh winsock reset";
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007B20 File Offset: 0x00005D20
		private void Button49_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you're trying to download some app from Windows Store and it's not starting or you're facing any other problem, this fix will help you by resetting the cache.";
			MyProject.Forms.Description.CauseText.Text = "Run the following command:" + this.space + "WSReset.exe";
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007B80 File Offset: 0x00005D80
		private void Button48_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "In installation process of apps from Windows Store, you may face this error with code: \"0x8024001e\".";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"1. Run the CMD command: net stop wuauserv",
				this.space,
				"2. Rename the directory \"C:\\Windows\\SoftwareDistribution\" to \"SoftwareDistribution.bck\".",
				this.space,
				"3. Run the CMD command: net start wuauserv"
			});
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007C00 File Offset: 0x00005E00
		private void Button47_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "While starting download from Windows Store, if you're getting the error message \"Something happened and your purchase couldn't be completed \", this fix is for you!";
			MyProject.Forms.Description.CauseText.Text = "Run the following PowerShell command:" + this.space + "Get-AppXPackage | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}";
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007C60 File Offset: 0x00005E60
		private void Button45_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "The component store is feature of Windows which stores all of the system files. When OS is updated, this feature may get corrupted. To fix component store, DISM is used.";
			MyProject.Forms.Description.CauseText.Text = "Run the CMD command:" + this.space + "Dism /Online /Cleanup-Image /RestoreHealth";
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007CC0 File Offset: 0x00005EC0
		private void Button44_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If Settings app isn't opening, the application is broken. This fix will reinstall it and hence you will be able to open Settings again.";
			MyProject.Forms.Description.CauseText.Text = "Run the following PowerShell command:" + this.space + "-ExecutionPolicy Unrestricted Add-AppxPackage -DisableDevelopmentMode -Register $Env:SystemRoot\\ImmersiveControlPanel\\AppxManifest.xml";
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007D20 File Offset: 0x00005F20
		private void Button43_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "By default in Windows 11, OneDrive is enabled. It may sync your camera roll (if setting is enabled) time to time and may consume data and power.";
			MyProject.Forms.Description.CauseText.Text = "Set the registry value \"DisableFileSync\" to 1: HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\OneDrive";
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007D70 File Offset: 0x00005F70
		private void Button42_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "In Windows 11 due to corruption of system files, Start Menu may not open. If you're facing such problem with Start Menu, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Run the following PowerShell command:" + this.space + "Get-AppXPackage | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}";
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007DD0 File Offset: 0x00005FD0
		private void Button31_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you're receiving the message when you start Task Manager that Task Manager has been disabled by Administrator, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the value \"DisableTaskMgr\": HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System.";
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007E20 File Offset: 0x00006020
		private void Button30_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you're receiving the message when you start Command Prompt that it has been disabled by Administrator, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the value \"DisableCMD\": HKCU\\Software\\Microsoft\\Windows\\System.";
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007E70 File Offset: 0x00006070
		private void Button29_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you're receiving the message when you start Registry Editor that Registry Editor has been disabled by Administrator, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the value \"DisableRegistryTools\": HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System.";
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007EC0 File Offset: 0x000060C0
		private void Button28_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you're receiving the message when you start an MMC snap-in that MMC snap-ins have been disabled by Administrator, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Delete the key \"MMC\": HKCU\\Software\\Policies\\Microsoft.";
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007F10 File Offset: 0x00006110
		private void Button27_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If search isn't intialising or is taking time or any setting has been changed, this fix will revert Windows Search to default settings.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"1. Run the following CMD command: net stop WSearch /Y",
				Environment.NewLine,
				"2. Set the value \"Start\" to \"2\": HKLM\\SYSTEM\\CurrentControlSet\\Services\\WSearch",
				this.space,
				"3. Set the value \"SetupCompletedSuccessfully\" to \"0\": HKLM\\SOFTWARE\\Microsoft\\Windows Search",
				Environment.NewLine,
				"4. Run the following CMD command: net start WSearch"
			});
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007FA0 File Offset: 0x000061A0
		private void Button26_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you're receiving the message when you start System Restore that System Restore has been disabled by Administrator, this fix will help you.";
			MyProject.Forms.Description.CauseText.Text = "Delete the values \"DisableConfig\" and \"DisableSR\": HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows NT\\SystemRestore";
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007FF0 File Offset: 0x000061F0
		private void Button25_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Attaching any new hardware device to your computer and Device Manager isn't showing it? There could be some problem in Plug and Play Windows Service. This fix will help you, probably!";
			MyProject.Forms.Description.CauseText.Text = "1. Run the following CMD command: net Start PlugPlay" + this.space + "2. Set the value \"Start\" to 2: HKLM\\SYSTEM\\CurrentControlSet\\Services\\PlugPlay";
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00008050 File Offset: 0x00006250
		private void Button24_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This fix will reset the settings of Windows Defender which is the default antivirus of Windows.";
			MyProject.Forms.Description.CauseText.Text = "1. regsvr32 wuaueng.dll 2. regsvr32 wucltui.dll 3. regsvr32 softpub.dll 4. regsvr32 wintrust.dll 5. regsvr32 initpki.dll 6. regsvr32 wups.dll 7. regsvr32 wuweb.dll 8. regsvr32 atl.dll 9. regsvr32 mssip32.dll " + this.space + "Then it sets some registry keys.";
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000080B0 File Offset: 0x000062B0
		private void Button23_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Sometimes Windows may not detect the antivirus that currently has been installed. This is because after uninstallation of a previous antivirus, some traces leave. Due to them, Windows may fail to recognise the currently installed antivirus.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"1. net stop winmgmt",
				Environment.NewLine,
				"2. net stop wscsvc",
				Environment.NewLine,
				"3. Rd /s /q \"%windir%\\System32\\wbem\\Repository\"",
				Environment.NewLine,
				"4. net start wscsvc",
				Environment.NewLine,
				"5. net start winmgmt"
			});
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008150 File Offset: 0x00006350
		private void Button22_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This fix will completely reset your Windows Security Settings to defaults.";
			MyProject.Forms.Description.CauseText.Text = "Runs the command: secedit /configure /cfg %windir%\\inf\\defltbase.inf /db defltbase.sdb /verbose";
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000081A0 File Offset: 0x000063A0
		private void Button41_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "In list of options for Shutdown, Restart, Sleep, if Hibernate is missing, this fix will probably help you.";
			MyProject.Forms.Description.CauseText.Text = "1. Runs the CMD command: powercfg -h on" + Environment.NewLine + "2. Sets the value \"ShowHibernateOption\" to 1: HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FlyoutMenuSettings";
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00008200 File Offset: 0x00006400
		private void Button40_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Ever used Sticky Notes? When you click \"Close\" button, you get a confirmation. If you check a box there to never prompt you again but now wants that confirmation dialog back, this fix is for you.";
			MyProject.Forms.Description.CauseText.Text = "Sets the value \"PROMPT_ON_DELETE\" to 1: HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\StickyNotes";
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00008250 File Offset: 0x00006450
		private void Button39_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "There could be some modifications done in Registry to disable them.";
			MyProject.Forms.Description.CauseText.Text = "1. Sets the value \"WindowArrangementActive\" to 1: HKCU\\Control Panel\\Desktop" + Environment.NewLine + "2. Deletes the value \"DisablePreviewDesktop\": HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced";
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000082B0 File Offset: 0x000064B0
		private void Button38_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If some desktop icons are blank page icons instead Of what they should show, they might have been corrupted. Rebuild them now!";
			MyProject.Forms.Description.CauseText.Text = "Deletes the file \"C:\\Users\\<Username>\\AppData\\Local\\IconCache.db\".";
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008300 File Offset: 0x00006500
		private void Button37_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If taskbar jumplist is not shown or doesn't store \"Recent\" list, you should act now!";
			MyProject.Forms.Description.CauseText.Text = "Delete the folders \"AutomaticDestinations\" and \"CustomDestinations\" from \"C:\\Users\\<Username>\\AppData\\Roaming\\Microsoft\\Windows\\Recent\"";
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008350 File Offset: 0x00006550
		private void Button36_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Notifications are those little messages like \"Your antivirus definitions are outdated\" or \"New Windows Updates Available...\". If you have disabled them, this fix will enable them for you.";
			MyProject.Forms.Description.CauseText.Text = "Deletes the value \"TaskbarNoNotification\": HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000083A0 File Offset: 0x000065A0
		private void Button35_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Windows Script Host is responsible for running batch files with extension .bat. If you're getting the error message while running batch files, use this fix.";
			MyProject.Forms.Description.CauseText.Text = "Delete the value \"Enabled\": HKLM\\SOFTWARE\\Microsoft\\Windows Script Host\\Settings";
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000083F0 File Offset: 0x000065F0
		private void Button34_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = string.Concat(new string[]
			{
				"Office documents may give errors upon opening such as:",
				this.space,
				"- Word experienced an error trying to open the file",
				Environment.NewLine,
				"- This Excel file is corrupt and cannot be opened"
			});
			MyProject.Forms.Description.CauseText.Text = "Runs the following command:" + this.space + "icacls \"%programfiles%\\Microsoft Office 15\" /grant *S-1-15-2-1:(OI)(CI)RX";
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008480 File Offset: 0x00006680
		private void Button33_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you face this error while creating system image, there could be some problem in Volume Shadow Copy service. This fix comes to rescue!";
			MyProject.Forms.Description.CauseText.Text = "1. Set the value \"Start\" to \"2\": HKLM\\SYSTEM\\CurrentControlSet\\Services\\VSS." + this.space + "2. Run the following CMD command: net start vss.";
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000084E0 File Offset: 0x000066E0
		private void Button32_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Windows Media Player may be suffering from DLL problems. Re-register them now!";
			MyProject.Forms.Description.CauseText.Text = "1. Run the following CMD command: regsvr32 /s jscript.dll." + this.space + "2. Run the following CMD command: regsvr32 /s vbscript.dll.";
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000853E File Offset: 0x0000673E
		private void SearchTroubleshoo_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id SearchDiagnostic", AppWinStyle.Hide, false, -1);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000854E File Offset: 0x0000674E
		private void WinUpdTro_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /c %systemroot%\\system32\\msdt.exe -id WindowsUpdateDiagnostic", AppWinStyle.Hide, false, -1);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008560 File Offset: 0x00006760
		private void WerMgrorWerFault2_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\WerSvc", true).SetValue("Start", 4);
				IL_33:
				num2 = 3;
				this.Balloon();
				IL_3B:
				goto IL_9A;
				IL_3D:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_5B:
				goto IL_8F;
				IL_5D:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_6D:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_5D;
			}
			IL_8F:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_9A:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00008620 File Offset: 0x00006820
		private void Wifidoesntwork_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("cmd.exe /c reg delete HKCR\\CLSID\\{988248f3-a1ad-49bf-9170-676cbbc36ba3} /va /f", AppWinStyle.Hide, false, -1);
				IL_17:
				num2 = 3;
				Interaction.Shell("cmd.exe /c netcfg -v -u dni_dne", AppWinStyle.Hide, false, -1);
				IL_27:
				num2 = 4;
				this.Balloon();
				IL_2F:
				goto IL_92;
				IL_31:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_53:
				goto IL_87;
				IL_55:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_65:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_55;
			}
			IL_87:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_92:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000086D8 File Offset: 0x000068D8
		private void WinUpdatesStuck_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				Interaction.Shell("net stop wuauserv", AppWinStyle.Hide, true, -1);
				IL_17:
				num2 = 3;
				Interaction.Shell("net stop bits", AppWinStyle.Hide, true, -1);
				IL_27:
				num2 = 4;
				string text = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\SoftwareDistribution";
				IL_3B:
				num2 = 5;
				Interaction.Shell(string.Concat(new string[]
				{
					"cmd.exe /c takeown /f \"",
					text,
					"\" &&icacls \"",
					text,
					"\" /grant administrators :F"
				}), AppWinStyle.Hide, true, -1);
				IL_71:
				num2 = 6;
				MyProject.Computer.FileSystem.DeleteDirectory("C:\\Windows\\SoftwareDistribution", DeleteDirectoryOption.DeleteAllContents);
				IL_88:
				num2 = 7;
				Interaction.Shell("net start wuauserv", AppWinStyle.Hide, true, -1);
				IL_98:
				num2 = 8;
				Interaction.Shell("net start bits", AppWinStyle.Hide, true, -1);
				IL_A8:
				num2 = 9;
				this.Balloon();
				IL_B1:
				goto IL_128;
				IL_B3:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_E9:
				goto IL_11D;
				IL_EB:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_FB:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_EB;
			}
			IL_11D:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_128:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008828 File Offset: 0x00006A28
		private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.thewindowsclub.com/windows-10-mail-calendar-freezes-windows-10");
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008835 File Offset: 0x00006A35
		private void LinkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.thewindowsclub.com/windows-10-settings-app-does-not-launch");
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008842 File Offset: 0x00006A42
		private void LinkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.thewindowsclub.com/windows-10-printer-problems-troubleshooter");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000884F File Offset: 0x00006A4F
		private void LinkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.thewindowsclub.com/windows-store-apps-troubleshooter-for-windows-10");
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000885C File Offset: 0x00006A5C
		private void LinkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			EvaluationKind evaluationKind = new EvaluationKind();
			this.processor_Currentclockspeed.Text = Conversions.ToString(Conversions.ToDouble(evaluationKind.GetGraphicsCardName("Win32_Processor", "CurrentClockSpeed")) / 1000.0) + " GHz";
			this.memory_availableRAM.Text = Conversions.ToString(Math.Round(MyProject.Computer.Info.AvailablePhysicalMemory / 1048576.0, 0)) + " MB";
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000088E3 File Offset: 0x00006AE3
		private void CommandLink1_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /k Dism /Online /Cleanup-Image /RestoreHealth", AppWinStyle.NormalFocus, false, -1);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000324C File Offset: 0x0000144C
		public void Info()
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000088F4 File Offset: 0x00006AF4
		public string GetGraphicsCardName(string w32, string data)
		{
			string text = string.Empty;
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM " + w32);
				try
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						text = ((ManagementObject)managementBaseObject).GetPropertyValue(data).ToString();
						if (!string.IsNullOrEmpty(text))
						{
							break;
						}
					}
				}
				finally
				{
					ManagementObjectCollection.ManagementObjectEnumerator enumerator;
					if (enumerator != null)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (ManagementException ex)
			{
				MessageBox.Show(ex.Message);
			}
			return text;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008994 File Offset: 0x00006B94
		private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
		{
			this.processor_name.Text = this.GetGraphicsCardName("Win32_Processor", "Name");
			this.processor_cores.Text = this.GetGraphicsCardName("Win32_Processor", "NumberOfCores");
			this.processor_Currentclockspeed.Text = Conversions.ToString(Conversions.ToDouble(this.GetGraphicsCardName("Win32_Processor", "CurrentClockSpeed")) / 1000.0) + " GHz";
			this.processor_Maxclockspeed.Text = Conversions.ToString(Conversions.ToDouble(this.GetGraphicsCardName("Win32_Processor", "MaxClockSpeed")) / 1000.0) + " GHz";
			this.processor_Thread.Text = this.GetGraphicsCardName("Win32_Processor", "ThreadCount");
			this.processor_LogicalProcessors.Text = this.GetGraphicsCardName("Win32_Processor", "NumberOfLogicalProcessors");
			this.memory_totalram.Text = Conversions.ToString(Conversions.ToDouble(this.GetGraphicsCardName("Win32_PhysicalMemory", "Capacity")) / 1048576.0) + " MB";
			this.memory_Speed.Text = this.GetGraphicsCardName("Win32_PhysicalMemory", "Speed") + " MHz";
			this.memory_availableRAM.Text = Conversions.ToString(Math.Round(MyProject.Computer.Info.AvailablePhysicalMemory / 1048576.0, 0)) + " MB";
			this.Display_Graphicscard.Text = this.GetGraphicsCardName("Win32_DisplayControllerConfiguration", "Caption");
			this.Display_monitortype.Text = this.GetGraphicsCardName("Win32_DesktopMonitor", "Caption");
			this.MaxRefreshRate.Text = this.GetGraphicsCardName("Win32_VideoController", "MaxRefreshRate");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008B6C File Offset: 0x00006D6C
		private void Button52_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Upgrade to Windows 11 may mess up with some Wi-fi related drivers and system files. This fix will fix Wi-fi problems with Windows 10.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Run the following CMD commands:",
				this.space,
				"reg delete HKCR\\CLSID\\{988248f3-a1ad-49bf-9170-676cbbc36ba3} /va /f",
				this.space,
				"netcfg -v -u dni_dne"
			});
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008BEC File Offset: 0x00006DEC
		private void Button53_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If Windows Update is stuck on \"Checking for Updates\", this is the fix you should try.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"1. Run the following commands:",
				Environment.NewLine,
				"net stop wuauserv",
				Environment.NewLine,
				"net stop bits",
				Environment.NewLine,
				"2. Delete the directory: C:\\Windows\\SoftwareDistribution",
				Environment.NewLine,
				"3. Run following commands:",
				Environment.NewLine,
				"net start wuauserv",
				Environment.NewLine,
				"net start bits"
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008CAD File Offset: 0x00006EAD
		private void Button1_Click_1(object sender, EventArgs e)
		{
			Process.Start("http://www.thewindowsclub.com/the-windows-club-search-results");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008CBA File Offset: 0x00006EBA
		private void Changelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MyProject.Forms.WhatNew.Show();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008CCB File Offset: 0x00006ECB
		private void RunInCMD(string Command, int AppWinStyle, bool ShouldStop)
		{
			Interaction.Shell("cmd.exe /c " + Command, checked((AppWinStyle)AppWinStyle), ShouldStop, -1);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008CE2 File Offset: 0x00006EE2
		private void MultipleEntriesFix1_Click(object sender, EventArgs e)
		{
			this.RunInCMD("start /max /d \"%localappdata%\\Microsoft\\OneDrive\" onedrive.exe /reset", 0, true);
			this.Balloon();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008CF7 File Offset: 0x00006EF7
		private void MultipleEntriesFix2_Click(object sender, EventArgs e)
		{
			this.Locations.CLSID.CreateSubKey("{018D5C66-4533-4307-9B53-224DE2ED1FE6}").SetValue("System.IsPinnedToNameSpaceTree", 0);
			this.Balloon();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008D24 File Offset: 0x00006F24
		private void Button46_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Sometimes multiple OneDrive entries might become visible in the Navigation pane of File Explorer. This will fix this issue.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"Fix 1: Runs the CMD command:",
				this.space,
				"%localappdata%\\Microsoft\\OneDrive\\onedrive.exe /reset",
				this.space,
				"Fix 2: Sets the registry value \"System.IsPinnedToNameSpaceTree\" to 0",
				this.space,
				"HKEY_CLASSES_ROOT\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}"
			});
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008DB3 File Offset: 0x00006FB3
		private void ResetGroupPolicy_Click(object sender, EventArgs e)
		{
			this.RunInCMD("RD /S /Q \"%WinDir%\\System32\\GroupPolicyUsers\"", 1, true);
			this.RunInCMD("RD /S /Q \"%WinDir%\\System32\\GroupPolicy\"", 1, true);
			this.RunInCMD("gpupdate /force", 1, true);
			this.Balloon();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008DE2 File Offset: 0x00006FE2
		private void Button51_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-all-group-policy-settings-to-default");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008DEF File Offset: 0x00006FEF
		private void Button54_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/catroot-catroot2-folder-reset-windows");
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008DFC File Offset: 0x00006FFC
		private void ResetCatroot2Folder_Click(object sender, EventArgs e)
		{
			this.RunInCMD("net stop cryptsvc", 1, true);
			this.RunInCMD("ren %systemroot%\\System32\\Catroot2 oldcatroot2", 1, true);
			this.RunInCMD("net start cryptsvc", 1, true);
			this.Balloon();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008E2B File Offset: 0x0000702B
		private void ResetNotepad_Click(object sender, EventArgs e)
		{
			MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft", true).DeleteSubKeyTree("Notepad", false);
			this.Balloon();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008E58 File Offset: 0x00007058
		private void Button55_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-notepad-to-default-settings");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008E65 File Offset: 0x00007065
		private void Button50_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-touchpad-settings-default-windows-10");
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008E72 File Offset: 0x00007072
		private void Button56_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/how-to-recover-lost-or-forgotton-windows-login-password");
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008E7F File Offset: 0x0000707F
		private void Button57_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-keyboard-settings-default-windows");
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008E8C File Offset: 0x0000708C
		private void Button58_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-microsoft-edge-browser-to-default-settings-in-windows-10");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008E99 File Offset: 0x00007099
		private void Button59_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-winhttp-proxy-settings-windows");
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008EA6 File Offset: 0x000070A6
		private void Button60_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/clear-data-usage-in-windows-10");
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008EB3 File Offset: 0x000070B3
		private void ResetDataUsage_Click(object sender, EventArgs e)
		{
			this.RunInCMD("takeown /F C:\\Windows\\System32\\sru", 0, true);
			this.RunInCMD("ren C:\\Windows\\System32\\sru sru.old", 0, true);
			this.Balloon();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008ED5 File Offset: 0x000070D5
		private void ResetWMIRepository_Click(object sender, EventArgs e)
		{
			this.RunInCMD("winmgmt /resetrepository", 0, true);
			this.Balloon();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008EEA File Offset: 0x000070EA
		private void Button61_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/how-to-repair-or-rebuild-the-wmi-repository-on-windows-10");
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008EF7 File Offset: 0x000070F7
		private void Button62_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/rebuild-font-cache-in-windows");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008F04 File Offset: 0x00007104
		private void MyPanel_Paint(object sender, PaintEventArgs e)
		{
			Color color = ColorTranslator.FromHtml("#e8eff5");
			Color color2 = ColorTranslator.FromHtml("#e8eff5");
			LinearGradientBrush brush = new LinearGradientBrush(base.ClientRectangle, color, color2, LinearGradientMode.ForwardDiagonal);
			e.Graphics.FillRectangle(brush, base.ClientRectangle);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008F48 File Offset: 0x00007148
		private void ResetRBQF_Click(object sender, EventArgs e)
		{
			this.ResetRecycleBin.PerformClick();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008F55 File Offset: 0x00007155
		private void Button63_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/recycle-bin-is-corrupted-windows");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008F62 File Offset: 0x00007162
		private void ResetWinsockQF_Click(object sender, EventArgs e)
		{
			this.winsock.PerformClick();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008F6F File Offset: 0x0000716F
		private void Button67_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-winsock-in-windows-10");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008F7C File Offset: 0x0000717C
		private void ResetStoreCache_Click(object sender, EventArgs e)
		{
			this.ClearStoreCache.PerformClick();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008F89 File Offset: 0x00007189
		private void Button65_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-windows-store-apps-windows-10");
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008F96 File Offset: 0x00007196
		private void ResetDNSQF_Click(object sender, EventArgs e)
		{
			this.FixDNSResolverCache.PerformClick();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008FA3 File Offset: 0x000071A3
		private void ResetTCPQF_Click(object sender, EventArgs e)
		{
			this.ResetInternetProtocol.PerformClick();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008FB0 File Offset: 0x000071B0
		private void Button71_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/flush-windows-dns-cache");
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008FBD File Offset: 0x000071BD
		private void Button69_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-tcp-ip-internet-protocol");
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008FCA File Offset: 0x000071CA
		private void ResetDefenderQF_Click(object sender, EventArgs e)
		{
			this.RepairWindowsDefender.PerformClick();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008FD7 File Offset: 0x000071D7
		private void Button74_Click(object sender, EventArgs e)
		{
			this.Button24.PerformClick();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008FE4 File Offset: 0x000071E4
		private void ResetFirewallQF_Click(object sender, EventArgs e)
		{
			this.ResetWindowsFirewall.PerformClick();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008FF1 File Offset: 0x000071F1
		private void Button72_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/reset-windows-firewall-settings");
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008FFE File Offset: 0x000071FE
		private void ResetSettingsQF_Click(object sender, EventArgs e)
		{
			this.ResetPCSettings.PerformClick();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000900B File Offset: 0x0000720B
		private void Button68_Click(object sender, EventArgs e)
		{
			this.Button44.PerformClick();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009018 File Offset: 0x00007218
		private void ResetWinUpdateQF_Click(object sender, EventArgs e)
		{
			this.ClearWindowsUpdateHistory.PerformClick();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009025 File Offset: 0x00007225
		private void Button64_Click(object sender, EventArgs e)
		{
			this.Button18.PerformClick();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009034 File Offset: 0x00007234
		private void WindowsSandboxFailedToStart_Click(object sender, EventArgs e)
		{
			if (Interaction.MsgBox(string.Concat(new string[]
			{
				"To fix this, there are 4 options:",
				this.space,
				"- Make sure latest Windows Updates are installed",
				Environment.NewLine,
				"- Virtualization is enabled in BIOS",
				Environment.NewLine,
				"- Hyper-V is enabled",
				Environment.NewLine,
				"- CPU supports SLAT",
				this.space,
				"Out of these, only enabling Hyper-V is an automatic fix. Do you want to enable Hyper-V?"
			}), MsgBoxStyle.YesNo, null) == MsgBoxResult.Yes)
			{
				Interaction.Shell("cmd.exe /k DISM /online /enable-feature /featurename:Microsoft-Hyper-V-All", AppWinStyle.NormalFocus, true, -1);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000090C1 File Offset: 0x000072C1
		private void Button66_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/windows-sandbox-error-0x8007005");
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000090D0 File Offset: 0x000072D0
		private void WindowsUpdateSpecialError_Click(object sender, EventArgs e)
		{
			RegistryKey registryKey = MyProject.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\WindowsUpdate\\UX");
			registryKey.SetValue("IsConvergedUpdateStackEnabled", 0);
			registryKey.CreateSubKey("Settings").SetValue("UxOption", 0);
			this.TheApplicationsWasNotInstalled_Click(null, null);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000912C File Offset: 0x0000732C
		private void Button70_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "This is one of the most common Windows Update error 0x80070057.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"1. Sets the registry value \"IsConvergedUpdateStackEnabled\" to 0:",
				this.space,
				"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX",
				this.space,
				"2. Sets the registry value \"UxOption\" to 0:",
				this.space,
				"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\WindowsUpdate\\UX\\Settings",
				this.space,
				"3. Rename \"%SystemRoot%\\SoftwareDistribution\" to \"SoftwareDistribution.bck\""
			});
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000091CD File Offset: 0x000073CD
		private void TelnetIsNotRecognised_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /k DISM /online /enable-feature /featurename:TelnetClient", AppWinStyle.NormalFocus, false, -1);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000091E0 File Offset: 0x000073E0
		private void Button73_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Telnet (short for teletype network) is a protocol meant for internet or LAN communication. It is primarily used to control other computers easily.";
			MyProject.Forms.Description.CauseText.Text = "Enables Telnet Client from Windows Features option and is equivalent to running the CMD command:" + this.space + "dism /online /Enable-Feature /FeatureName:TelnetClient";
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000923E File Offset: 0x0000743E
		private void WslRegisterDistributionFailed_Click(object sender, EventArgs e)
		{
			Interaction.Shell("cmd.exe /k DISM /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux", AppWinStyle.NormalFocus, false, -1);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009250 File Offset: 0x00007450
		private void Button75_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "WSL or Windows Subsystem for Linux for Windows 11 is an excellent tool for the developers. But sometimes on the command line’s startup, it throws an error code 0x8007019e or 0x8000000d.";
			MyProject.Forms.Description.CauseText.Text = "Enables Windows Subsystem Linux from Windows Features option and is equivalent to running the CMD command:" + this.space + "dism /online /Enable-Feature /FeatureName:Microsoft-Windows-Subsystem-Linux";
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000092AE File Offset: 0x000074AE
		private void ResetSoftwareDistribution_Click(object sender, EventArgs e)
		{
			Interaction.Shell("net stop wuauserv", AppWinStyle.MinimizedFocus, false, -1);
			this.RunInCMD("cmd.exe /c ren C:\\Windows\\SoftwareDistribution SoftwareDistribution.bck", 0, true);
			Interaction.Shell("net start wuauserv", AppWinStyle.MinimizedFocus, false, -1);
			this.Balloon();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000092DF File Offset: 0x000074DF
		private void Button76_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.thewindowsclub.com/software-distribution-folder-in-windows");
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000092EC File Offset: 0x000074EC
		private void RecycleBinIsGreyedOut_Click(object sender, EventArgs e)
		{
			MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\NonEnum", true).SetValue("{645FF040-5081-101B-9F08-00AA002F954E}", 0);
			this.Balloon();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009320 File Offset: 0x00007520
		private void Button77_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If the Recycle Bin option is greyed out in the Desktop Icon Settings window in Windows 11, you can use this to fix the issue. It will help you get back the Recycle Bin option in the Desktop Icon Settings panel so that you can show or hide Recycle Bin on Desktop.";
			MyProject.Forms.Description.CauseText.Text = "1. Navigate to: \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\NonEnum\"" + Environment.NewLine + Environment.NewLine + "2. Set value {645FF040-5081-101B-9F08-00AA002F954E} to 0";
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009384 File Offset: 0x00007584
		private void BatteryRemainingTime_Click(object sender, EventArgs e)
		{
			RegistryKey registryKey = MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control", true).CreateSubKey("Power");
			registryKey.SetValue("EnergyEstimationEnabled", 1);
			registryKey.SetValue("EnergyEstimationDisabled", 0);
			registryKey.SetValue("UserBatteryDischargeEstimator", 0);
			this.Balloon();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000093F0 File Offset: 0x000075F0
		private void Button78_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you are using a Windows 11 laptop and you want to know how long you can continue to use your computer on battery backup, before you have to charge it, you can use FixWin and make Windows 10 show the remaining battery time";
			MyProject.Forms.Description.CauseText.Text = "1. Navigate to: \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Power\"" + Environment.NewLine + Environment.NewLine + "2. Set values: EnergyEstimationEnabled to 1, EnergyEstimationDisabled to 0, UserBatteryDischargeEstimator to 0.";
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009454 File Offset: 0x00007654
		private void Button80_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Sometimes due to loading a tons of images and random issues, the thumbnail cache gets corrupted. This can result in improper loading of thumbnails or not loading at all";
			MyProject.Forms.Description.CauseText.Text = "Runs the following batch command: " + Environment.NewLine + "DEL /F /S /Q /A %LocalAppData%\\Microsoft\\Windows\\Explorer\\thumbcache_*.db";
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000094B4 File Offset: 0x000076B4
		private void ResetThumbnailCache_Click(object sender, EventArgs e)
		{
			FileHandler fileHandler = this.fileHandler;
			string thumbnailClearFile = this.batchFileProvider.GetThumbnailClearFile();
			fileHandler.WriteToFileRunAndDelete(ref thumbnailClearFile);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000094DC File Offset: 0x000076DC
		private void Button81_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If some programs are giving errors like corrupted DLL files and can't open the application, re-registering the system DLL files can help.";
			MyProject.Forms.Description.CauseText.Text = "Runs the following batch command: " + Environment.NewLine + "for %%f in (*.dll) do regsvr32 /s %%f";
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000953C File Offset: 0x0000773C
		private void ReRegisterAllDLLFiles_Click(object sender, EventArgs e)
		{
			FileHandler fileHandler = this.fileHandler;
			string resetDllFile = this.batchFileProvider.GetResetDllFile();
			fileHandler.WriteToFileRunAndDelete(ref resetDllFile);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009564 File Offset: 0x00007764
		private void IssuesWithWindowsActivation_Click(object sender, EventArgs e)
		{
			FileHandler fileHandler = this.fileHandler;
			string rebuildTokensFile = this.batchFileProvider.GetRebuildTokensFile();
			fileHandler.WriteToFileRunAndDelete(ref rebuildTokensFile);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000958C File Offset: 0x0000778C
		private void Button82_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "Sometimes the Tokens.dat file may get corrupted as a result of which Windows Activation may not take place successfully.";
			MyProject.Forms.Description.CauseText.Text = "Removes tokens.dat and tokens.bar files. Then runs the following batch command: " + Environment.NewLine + "cscript.exe %windir%\\system32\\slmgr.vbs /rilc";
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000095E9 File Offset: 0x000077E9
		private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.eventHandler.FixWinClosing();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000095F6 File Offset: 0x000077F6
		private void WindowsSavingJPGsDownloadedAsJFIFs_Click(object sender, EventArgs e)
		{
			MyProject.Computer.Registry.ClassesRoot.OpenSubKey("MIME\\Database\\Content Type\\image/jpeg", true).SetValue("Extension", ".jpg");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009624 File Offset: 0x00007824
		private void Button83_Click(object sender, EventArgs e)
		{
			MyProject.Forms.Description.Show();
			MyProject.Forms.Description.DescriptionText.Text = "If you notice Windows is saving your JPG or JPEG files as JFIF files, it is because of incorrect file association.";
			MyProject.Forms.Description.CauseText.Text = string.Concat(new string[]
			{
				"In the following registry location:",
				Environment.NewLine,
				Environment.NewLine,
				"HKEY_CLASSES_ROOT\\MIME\\Database\\Content Type\\image/jpeg",
				Environment.NewLine,
				Environment.NewLine,
				"Set \"Extension\" key value to \".jpg\"."
			});
			this.Balloon();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000096F8 File Offset: 0x000078F8
		[DebuggerStepThrough]
		public void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Main_Form));
			this.MyPanel = new Panel();
			this.TWC_Logo = new PictureBox();
			this.Side_About = new LinkLabel();
			this.TWC = new LinkLabel();
			this.Side_Additional = new LinkLabel();
			this.Side_Troubleshooting = new LinkLabel();
			this.Side_SystemTools = new LinkLabel();
			this.Side_ModernUI = new LinkLabel();
			this.Side_Internet = new LinkLabel();
			this.Side_FileExplorer = new LinkLabel();
			this.Side_Welcome = new LinkLabel();
			this.TabControlFixWin = new TabControl();
			this.Welcome = new TabPage();
			this.PictureBox1 = new PictureBox();
			this.Button1 = new Button();
			this.CommandLink1 = new CommandLink();
			this.username = new Label();
			this.ram = new Label();
			this.bit1 = new Label();
			this.processor = new Label();
			this.os = new Label();
			this.CreateRestorePoint = new CommandLink();
			this.reregisterapps = new CommandLink();
			this.SystemFileChecker = new CommandLink();
			this.FileExplorer = new TabPage();
			this.RecycleBinIsGreyedOut = new Button();
			this.Label76 = new Label();
			this.Button77 = new Button();
			this.showhiddenfiles = new Button();
			this.ClassNotRegistred = new Button();
			this.Label55 = new Label();
			this.CDDriveOrDVD = new Button();
			this.ResetRecycleBin = new Button();
			this.ThumbnailsNotShowing = new Button();
			this.ExplorerDoesNTStart = new Button();
			this.FixRecycleBinIcon = new Button();
			this.FixFolderOptions = new Button();
			this.Label51 = new Label();
			this.WerMgrorWerFault2 = new Button();
			this.WerMgrorWerFault = new Button();
			this.Label40 = new Label();
			this.Button11 = new Button();
			this.Button10 = new Button();
			this.Button9 = new Button();
			this.Button8 = new Button();
			this.Button7 = new Button();
			this.Button6 = new Button();
			this.Button5 = new Button();
			this.Button4 = new Button();
			this.Button3 = new Button();
			this.Button2 = new Button();
			this.RecycleBinIconMissing = new Button();
			this.Label36 = new Label();
			this.Label4 = new Label();
			this.Label9 = new Label();
			this.Label8 = new Label();
			this.Label7 = new Label();
			this.Label6 = new Label();
			this.Label5 = new Label();
			this.InternetConnectivity = new TabPage();
			this.Button73 = new Button();
			this.TelnetIsNotRecognised = new Button();
			this.Label74 = new Label();
			this.Button12 = new Button();
			this.winsock = new Button();
			this.Button13 = new Button();
			this.internetoptionsmissing = new Button();
			this.Button14 = new Button();
			this.OptimizeIE = new Button();
			this.Button15 = new Button();
			this.RuntimeErrors = new Button();
			this.Button16 = new Button();
			this.ResetIE = new Button();
			this.Button17 = new Button();
			this.Label58 = new Label();
			this.Button18 = new Button();
			this.ResetWindowsFirewall = new Button();
			this.Button19 = new Button();
			this.Label56 = new Label();
			this.Button20 = new Button();
			this.ClearWindowsUpdateHistory = new Button();
			this.Button21 = new Button();
			this.Label45 = new Label();
			this.FixDNSResolverCache = new Button();
			this.Label44 = new Label();
			this.ResetInternetProtocol = new Button();
			this.Label41 = new Label();
			this.RightClickMenuIEDisabled = new Button();
			this.Label14 = new Label();
			this.Label13 = new Label();
			this.Label12 = new Label();
			this.Label11 = new Label();
			this.Label10 = new Label();
			this.Windows11 = new TabPage();
			this.Button78 = new Button();
			this.Button75 = new Button();
			this.BatteryRemainingTime = new Button();
			this.WslRegisterDistributionFailed = new Button();
			this.Label77 = new Label();
			this.Label75 = new Label();
			this.Button70 = new Button();
			this.WindowsUpdateSpecialError = new Button();
			this.Label73 = new Label();
			this.Button66 = new Button();
			this.WindowsSandboxFailedToStart = new Button();
			this.Label43 = new Label();
			this.Button46 = new Button();
			this.MultipleEntriesFix2 = new Button();
			this.MultipleEntriesFix1 = new Button();
			this.Label20 = new Label();
			this.Button53 = new Button();
			this.Button52 = new Button();
			this.Button42 = new Button();
			this.Button43 = new Button();
			this.dism = new Button();
			this.WinUpdatesStuck = new Button();
			this.Wifidoesntwork = new Button();
			this.Button44 = new Button();
			this.StartMenuDoesNTOpen = new Button();
			this.Label46 = new Label();
			this.Button45 = new Button();
			this.Label1 = new Label();
			this.Label60 = new Label();
			this.Label39 = new Label();
			this.Label37 = new Label();
			this.Revert = new Button();
			this.Label38 = new Label();
			this.DisableOnedrive = new Button();
			this.ResetPCSettings = new Button();
			this.WindowsStore = new TabPage();
			this.WindowsSavingJPGsDownloadedAsJFIFs = new Button();
			this.Button83 = new Button();
			this.Label48 = new Label();
			this.IssuesWithWindowsActivation = new Button();
			this.Button82 = new Button();
			this.Label79 = new Label();
			this.ReRegisterAllDLLFiles = new Button();
			this.Button81 = new Button();
			this.Label78 = new Label();
			this.ResetThumbnailCache = new Button();
			this.Button80 = new Button();
			this.Labelxyz = new Label();
			this.SomethingHappened = new Button();
			this.Label15 = new Label();
			this.Button47 = new Button();
			this.TheApplicationsWasNotInstalled = new Button();
			this.Button49 = new Button();
			this.Label16 = new Label();
			this.ClearStoreCache = new Button();
			this.Button48 = new Button();
			this.Label17 = new Label();
			this.SystemTools = new TabPage();
			this.Button22 = new Button();
			this.ResetWindowsSecurity = new Button();
			this.Button23 = new Button();
			this.ActionCenterAnd = new Button();
			this.Button24 = new Button();
			this.RepairWindowsDefender = new Button();
			this.Button25 = new Button();
			this.DeviceManagerIsNot = new Button();
			this.Button26 = new Button();
			this.SystemRestoreHasBeen = new Button();
			this.Button27 = new Button();
			this.ResetWindowsSearch = new Button();
			this.Button28 = new Button();
			this.EnableMMCSnap = new Button();
			this.Button29 = new Button();
			this.RegistryEditorHasBeen = new Button();
			this.Button30 = new Button();
			this.Label59 = new Label();
			this.Button31 = new Button();
			this.CommandPromptHasBeen = new Button();
			this.Label50 = new Label();
			this.TaskManagerHasBeen = new Button();
			this.Label30 = new Label();
			this.Label27 = new Label();
			this.Label26 = new Label();
			this.Label25 = new Label();
			this.Label24 = new Label();
			this.Label23 = new Label();
			this.Label22 = new Label();
			this.Label21 = new Label();
			this.QuickFixes = new TabPage();
			this.Button76 = new Button();
			this.ResetSoftwareDistribution = new Button();
			this.Button64 = new Button();
			this.ResetWinUpdateQF = new Button();
			this.Button68 = new Button();
			this.ResetSettingsQF = new Button();
			this.Button72 = new Button();
			this.ResetFirewallQF = new Button();
			this.Button74 = new Button();
			this.ResetDefenderQF = new Button();
			this.Button69 = new Button();
			this.ResetTCPQF = new Button();
			this.Button71 = new Button();
			this.ResetDNSQF = new Button();
			this.Button65 = new Button();
			this.ResetStoreCache = new Button();
			this.Button67 = new Button();
			this.ResetWinsockQF = new Button();
			this.Button63 = new Button();
			this.ResetRBQF = new Button();
			this.Button61 = new Button();
			this.ResetWMIRepository = new Button();
			this.Button60 = new Button();
			this.ResetDataUsage = new Button();
			this.GroupBox3 = new GroupBox();
			this.Button59 = new Button();
			this.Button62 = new Button();
			this.Button58 = new Button();
			this.Button57 = new Button();
			this.Button50 = new Button();
			this.Button56 = new Button();
			this.Button54 = new Button();
			this.Button55 = new Button();
			this.Button51 = new Button();
			this.ResetNotepad = new Button();
			this.ResetCatroot2Folder = new Button();
			this.ResetGroupPolicy = new Button();
			this.Evaluation = new TabPage();
			this.GroupBox2 = new GroupBox();
			this.Label72 = new Label();
			this.Display_monitortype = new TextBox();
			this.Label71 = new Label();
			this.MaxRefreshRate = new TextBox();
			this.display_resolution = new TextBox();
			this.Label68 = new Label();
			this.Display_Graphicscard = new TextBox();
			this.Label70 = new Label();
			this.GroupBox1 = new GroupBox();
			this.processor_Maxclockspeed = new TextBox();
			this.Label63 = new Label();
			this.processor_LogicalProcessors = new TextBox();
			this.Label69 = new Label();
			this.processor_Currentclockspeed = new TextBox();
			this.Label62 = new Label();
			this.memory_Speed = new TextBox();
			this.Label66 = new Label();
			this.memory_availableRAM = new TextBox();
			this.Label67 = new Label();
			this.memory_totalram = new TextBox();
			this.Label65 = new Label();
			this.processor_Thread = new TextBox();
			this.Label64 = new Label();
			this.processor_cores = new TextBox();
			this.Label61 = new Label();
			this.processor_name = new TextBox();
			this.Label49 = new Label();
			this.Troubleshooting = new TabPage();
			this.LinkLabel9 = new LinkLabel();
			this.LinkLabel8 = new LinkLabel();
			this.LinkLabel7 = new LinkLabel();
			this.Label19 = new Label();
			this.LinkLabel5 = new LinkLabel();
			this.Label18 = new Label();
			this.Label3 = new Label();
			this.Label2 = new Label();
			this.WinUpdTro = new Button();
			this.SearchTroubleshoo = new Button();
			this.WMPDVD = new Button();
			this.WMPLibrary = new Button();
			this.WMPSettings = new Button();
			this.Printer = new Button();
			this.Power = new Button();
			this.SystemMaintenence = new Button();
			this.IESafety = new Button();
			this.IEPerformance = new Button();
			this.IncomingConnections = new Button();
			this.NetworkAdapter = new Button();
			this.Homegroup = new Button();
			this.SharedFolders = new Button();
			this.InternetConnections = new Button();
			this.HardwareAndDevices = new Button();
			this.RecordingAudio = new Button();
			this.PlayingAudio = new Button();
			this.Additional = new TabPage();
			this.Button32 = new Button();
			this.wmp = new Button();
			this.Button33 = new Button();
			this.recoveryimagecant = new Button();
			this.Button34 = new Button();
			this.officedocsdonotopen = new Button();
			this.Button35 = new Button();
			this.WindowsScriptHost = new Button();
			this.Button36 = new Button();
			this.Label57 = new Label();
			this.Button37 = new Button();
			this.balloontips = new Button();
			this.Button38 = new Button();
			this.Label54 = new Label();
			this.Button39 = new Button();
			this.TaskbarJumplist = new Button();
			this.Button40 = new Button();
			this.Label53 = new Label();
			this.Button41 = new Button();
			this.FixCorruptedDesktopIcons = new Button();
			this.Label52 = new Label();
			this.AeroNotWorking = new Button();
			this.Label47 = new Label();
			this.Label35 = new Label();
			this.RestoreStickyNotes = new Button();
			this.Label34 = new Label();
			this.EnableHibernate = new Button();
			this.Label33 = new Label();
			this.Label32 = new Label();
			this.Label31 = new Label();
			this.About = new TabPage();
			this.Changelog = new LinkLabel();
			this.VersionAbout = new Label();
			this.RichTextBox1 = new RichTextBox();
			this.Label28 = new Label();
			this.LinkLabel6 = new LinkLabel();
			this.Label29 = new Label();
			this.LinkLabel2 = new LinkLabel();
			this.Label42 = new Label();
			this.LinkLabel1 = new LinkLabel();
			this.ListBox1 = new ListBox();
			this.NotifyIcon1 = new NotifyIcon(this.components);
			this.Timer1 = new Timer(this.components);
			this.BackgroundWorker2 = new BackgroundWorker();
			this.BackgroundWorker3 = new BackgroundWorker();
			this.MyPanel.SuspendLayout();
			((ISupportInitialize)this.TWC_Logo).BeginInit();
			this.TabControlFixWin.SuspendLayout();
			this.Welcome.SuspendLayout();
			((ISupportInitialize)this.PictureBox1).BeginInit();
			this.FileExplorer.SuspendLayout();
			this.InternetConnectivity.SuspendLayout();
			this.Windows11.SuspendLayout();
			this.WindowsStore.SuspendLayout();
			this.SystemTools.SuspendLayout();
			this.QuickFixes.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			this.Evaluation.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.Troubleshooting.SuspendLayout();
			this.Additional.SuspendLayout();
			this.About.SuspendLayout();
			base.SuspendLayout();
			this.MyPanel.BackColor = Color.Gainsboro;
			this.MyPanel.Controls.Add(this.TWC_Logo);
			this.MyPanel.Controls.Add(this.Side_About);
			this.MyPanel.Controls.Add(this.TWC);
			this.MyPanel.Controls.Add(this.Side_Additional);
			this.MyPanel.Controls.Add(this.Side_Troubleshooting);
			this.MyPanel.Controls.Add(this.Side_SystemTools);
			this.MyPanel.Controls.Add(this.Side_ModernUI);
			this.MyPanel.Controls.Add(this.Side_Internet);
			this.MyPanel.Controls.Add(this.Side_FileExplorer);
			this.MyPanel.Controls.Add(this.Side_Welcome);
			this.MyPanel.Location = new Point(-18, -1);
			this.MyPanel.Name = "MyPanel";
			this.MyPanel.Size = new Size(213, 597);
			this.MyPanel.TabIndex = 0;
			this.TWC_Logo.BackColor = Color.Transparent;
			this.TWC_Logo.Cursor = Cursors.Hand;
			this.TWC_Logo.Image = (Image)componentResourceManager.GetObject("TWC_Logo.Image");
			this.TWC_Logo.Location = new Point(46, 361);
			this.TWC_Logo.Name = "TWC_Logo";
			this.TWC_Logo.Size = new Size(138, 136);
			this.TWC_Logo.SizeMode = PictureBoxSizeMode.StretchImage;
			this.TWC_Logo.TabIndex = 4;
			this.TWC_Logo.TabStop = false;
			this.Side_About.ActiveLinkColor = Color.Black;
			this.Side_About.AutoSize = true;
			this.Side_About.BackColor = Color.Transparent;
			this.Side_About.Font = new Font("Segoe UI", 11.8f);
			this.Side_About.ForeColor = Color.Black;
			this.Side_About.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_About.LinkColor = Color.Black;
			this.Side_About.Location = new Point(30, 307);
			this.Side_About.Name = "Side_About";
			this.Side_About.Size = new Size(52, 21);
			this.Side_About.TabIndex = 1;
			this.Side_About.TabStop = true;
			this.Side_About.Text = "About";
			this.TWC.ActiveLinkColor = Color.Black;
			this.TWC.AutoSize = true;
			this.TWC.BackColor = Color.Transparent;
			this.TWC.Font = new Font("Segoe UI", 17f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.TWC.ForeColor = Color.Black;
			this.TWC.LinkBehavior = LinkBehavior.HoverUnderline;
			this.TWC.LinkColor = Color.Black;
			this.TWC.Location = new Point(39, 519);
			this.TWC.Name = "TWC";
			this.TWC.Size = new Size(152, 23);
			this.TWC.TabIndex = 1;
			this.TWC.TabStop = true;
			this.TWC.Text = "The Windows Club";
			this.Side_Additional.ActiveLinkColor = Color.Black;
			this.Side_Additional.AutoSize = true;
			this.Side_Additional.BackColor = Color.Transparent;
			this.Side_Additional.Font = new Font("Segoe UI", 11.8f);
			this.Side_Additional.ForeColor = Color.Black;
			this.Side_Additional.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_Additional.LinkColor = Color.Black;
			this.Side_Additional.Location = new Point(30, 266);
			this.Side_Additional.Name = "Side_Additional";
			this.Side_Additional.Size = new Size(119, 21);
			this.Side_Additional.TabIndex = 1;
			this.Side_Additional.TabStop = true;
			this.Side_Additional.Text = "Additional Fixes";
			this.Side_Troubleshooting.ActiveLinkColor = Color.Black;
			this.Side_Troubleshooting.AutoSize = true;
			this.Side_Troubleshooting.BackColor = Color.Transparent;
			this.Side_Troubleshooting.Font = new Font("Segoe UI", 11.8f);
			this.Side_Troubleshooting.ForeColor = Color.Black;
			this.Side_Troubleshooting.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_Troubleshooting.LinkColor = Color.Black;
			this.Side_Troubleshooting.Location = new Point(30, 225);
			this.Side_Troubleshooting.Name = "Side_Troubleshooting";
			this.Side_Troubleshooting.Size = new Size(122, 21);
			this.Side_Troubleshooting.TabIndex = 1;
			this.Side_Troubleshooting.TabStop = true;
			this.Side_Troubleshooting.Text = "Troubleshooters";
			this.Side_SystemTools.ActiveLinkColor = Color.Black;
			this.Side_SystemTools.AutoSize = true;
			this.Side_SystemTools.BackColor = Color.Transparent;
			this.Side_SystemTools.Font = new Font("Segoe UI", 11.8f);
			this.Side_SystemTools.ForeColor = Color.Black;
			this.Side_SystemTools.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_SystemTools.LinkColor = Color.Black;
			this.Side_SystemTools.Location = new Point(30, 184);
			this.Side_SystemTools.Name = "Side_SystemTools";
			this.Side_SystemTools.Size = new Size(100, 21);
			this.Side_SystemTools.TabIndex = 1;
			this.Side_SystemTools.TabStop = true;
			this.Side_SystemTools.Text = "System Tools";
			this.Side_ModernUI.ActiveLinkColor = Color.Black;
			this.Side_ModernUI.AutoSize = true;
			this.Side_ModernUI.BackColor = Color.Transparent;
			this.Side_ModernUI.Font = new Font("Segoe UI", 11.8f);
			this.Side_ModernUI.ForeColor = Color.Black;
			this.Side_ModernUI.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_ModernUI.LinkColor = Color.Black;
			this.Side_ModernUI.Location = new Point(30, 143);
			this.Side_ModernUI.Name = "Side_ModernUI";
			this.Side_ModernUI.Size = new Size(99, 21);
			this.Side_ModernUI.TabIndex = 1;
			this.Side_ModernUI.TabStop = true;
			this.Side_ModernUI.Text = "System Fixes";
			this.Side_Internet.ActiveLinkColor = Color.Black;
			this.Side_Internet.AutoSize = true;
			this.Side_Internet.BackColor = Color.Transparent;
			this.Side_Internet.Font = new Font("Segoe UI", 11.8f);
			this.Side_Internet.ForeColor = Color.Black;
			this.Side_Internet.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_Internet.LinkColor = Color.Black;
			this.Side_Internet.Location = new Point(30, 102);
			this.Side_Internet.Name = "Side_Internet";
			this.Side_Internet.Size = new Size(171, 21);
			this.Side_Internet.TabIndex = 1;
			this.Side_Internet.TabStop = true;
			this.Side_Internet.Text = "Internet && Connectivity";
			this.Side_FileExplorer.ActiveLinkColor = Color.Black;
			this.Side_FileExplorer.AutoSize = true;
			this.Side_FileExplorer.BackColor = Color.Transparent;
			this.Side_FileExplorer.Font = new Font("Segoe UI", 11.8f);
			this.Side_FileExplorer.ForeColor = Color.Black;
			this.Side_FileExplorer.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_FileExplorer.LinkColor = Color.Black;
			this.Side_FileExplorer.Location = new Point(30, 61);
			this.Side_FileExplorer.Name = "Side_FileExplorer";
			this.Side_FileExplorer.Size = new Size(95, 21);
			this.Side_FileExplorer.TabIndex = 1;
			this.Side_FileExplorer.TabStop = true;
			this.Side_FileExplorer.Text = "File Explorer";
			this.Side_Welcome.ActiveLinkColor = Color.Black;
			this.Side_Welcome.AutoSize = true;
			this.Side_Welcome.BackColor = Color.Transparent;
			this.Side_Welcome.Font = new Font("Segoe UI", 11.8f);
			this.Side_Welcome.ForeColor = Color.Black;
			this.Side_Welcome.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Side_Welcome.LinkColor = Color.Black;
			this.Side_Welcome.Location = new Point(30, 20);
			this.Side_Welcome.Name = "Side_Welcome";
			this.Side_Welcome.Size = new Size(74, 21);
			this.Side_Welcome.TabIndex = 1;
			this.Side_Welcome.TabStop = true;
			this.Side_Welcome.Text = "Welcome";
			this.TabControlFixWin.Controls.Add(this.Welcome);
			this.TabControlFixWin.Controls.Add(this.FileExplorer);
			this.TabControlFixWin.Controls.Add(this.InternetConnectivity);
			this.TabControlFixWin.Controls.Add(this.Windows11);
			this.TabControlFixWin.Controls.Add(this.WindowsStore);
			this.TabControlFixWin.Controls.Add(this.SystemTools);
			this.TabControlFixWin.Controls.Add(this.QuickFixes);
			this.TabControlFixWin.Controls.Add(this.Evaluation);
			this.TabControlFixWin.Controls.Add(this.Troubleshooting);
			this.TabControlFixWin.Controls.Add(this.Additional);
			this.TabControlFixWin.Controls.Add(this.About);
			this.TabControlFixWin.Location = new Point(199, 5);
			this.TabControlFixWin.Name = "TabControlFixWin";
			this.TabControlFixWin.SelectedIndex = 0;
			this.TabControlFixWin.Size = new Size(613, 553);
			this.TabControlFixWin.TabIndex = 3;
			this.Welcome.BackColor = Color.White;
			this.Welcome.Controls.Add(this.PictureBox1);
			this.Welcome.Controls.Add(this.Button1);
			this.Welcome.Controls.Add(this.CommandLink1);
			this.Welcome.Controls.Add(this.username);
			this.Welcome.Controls.Add(this.ram);
			this.Welcome.Controls.Add(this.bit1);
			this.Welcome.Controls.Add(this.processor);
			this.Welcome.Controls.Add(this.os);
			this.Welcome.Controls.Add(this.CreateRestorePoint);
			this.Welcome.Controls.Add(this.reregisterapps);
			this.Welcome.Controls.Add(this.SystemFileChecker);
			this.Welcome.Location = new Point(4, 26);
			this.Welcome.Name = "Welcome";
			this.Welcome.Padding = new Padding(3);
			this.Welcome.Size = new Size(605, 523);
			this.Welcome.TabIndex = 0;
			this.Welcome.Text = "Welcome";
			this.PictureBox1.Image = (Image)componentResourceManager.GetObject("PictureBox1.Image");
			this.PictureBox1.Location = new Point(410, 81);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new Size(181, 181);
			this.PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			this.PictureBox1.TabIndex = 15;
			this.PictureBox1.TabStop = false;
			this.Button1.Location = new Point(226, 476);
			this.Button1.Name = "Button1";
			this.Button1.Size = new Size(172, 32);
			this.Button1.TabIndex = 15;
			this.Button1.Text = "Search For More Fixes";
			this.Button1.UseVisualStyleBackColor = true;
			this.CommandLink1.FlatStyle = FlatStyle.System;
			this.CommandLink1.image = null;
			this.CommandLink1.Location = new Point(313, 395);
			this.CommandLink1.Name = "CommandLink1";
			this.CommandLink1.Size = new Size(286, 74);
			this.CommandLink1.SupplementalExplaination = "Using DISM, repair corrupt Windows component store.";
			this.CommandLink1.TabIndex = 13;
			this.CommandLink1.Text = "Repair Windows System Image";
			this.CommandLink1.UseVisualStyleBackColor = true;
			this.username.AutoSize = true;
			this.username.Font = new Font("Segoe UI Light", 16.25f);
			this.username.Location = new Point(9, 185);
			this.username.Name = "username";
			this.username.Size = new Size(106, 30);
			this.username.TabIndex = 11;
			this.username.Text = "Username";
			this.ram.AutoSize = true;
			this.ram.Font = new Font("Segoe UI Light", 16.25f);
			this.ram.Location = new Point(9, 142);
			this.ram.Name = "ram";
			this.ram.Size = new Size(41, 30);
			this.ram.TabIndex = 11;
			this.ram.Text = "OS";
			this.bit1.AutoSize = true;
			this.bit1.Font = new Font("Segoe UI Light", 16.25f);
			this.bit1.Location = new Point(9, 99);
			this.bit1.Name = "bit1";
			this.bit1.Size = new Size(41, 30);
			this.bit1.TabIndex = 11;
			this.bit1.Text = "OS";
			this.processor.AutoSize = true;
			this.processor.Font = new Font("Segoe UI Light", 16.25f);
			this.processor.Location = new Point(9, 56);
			this.processor.Name = "processor";
			this.processor.Size = new Size(41, 30);
			this.processor.TabIndex = 11;
			this.processor.Text = "OS";
			this.os.AutoSize = true;
			this.os.Font = new Font("Segoe UI Light", 16.25f);
			this.os.Location = new Point(9, 13);
			this.os.Name = "os";
			this.os.Size = new Size(41, 30);
			this.os.TabIndex = 11;
			this.os.Text = "OS";
			this.CreateRestorePoint.FlatStyle = FlatStyle.System;
			this.CreateRestorePoint.image = null;
			this.CreateRestorePoint.Location = new Point(15, 395);
			this.CreateRestorePoint.Name = "CreateRestorePoint";
			this.CreateRestorePoint.Size = new Size(286, 74);
			this.CreateRestorePoint.SupplementalExplaination = "Before you move on, we strongly recommend you to create a System Restore point.";
			this.CreateRestorePoint.TabIndex = 10;
			this.CreateRestorePoint.Text = "Create Restore Point";
			this.CreateRestorePoint.UseVisualStyleBackColor = true;
			this.reregisterapps.FlatStyle = FlatStyle.System;
			this.reregisterapps.image = null;
			this.reregisterapps.Location = new Point(313, 295);
			this.reregisterapps.Name = "reregisterapps";
			this.reregisterapps.Size = new Size(286, 74);
			this.reregisterapps.SupplementalExplaination = "If some store apps corrupt and are unable to start, re-register them through PowerShell.";
			this.reregisterapps.TabIndex = 9;
			this.reregisterapps.Text = "Re-Register Store Apps";
			this.reregisterapps.UseVisualStyleBackColor = true;
			this.SystemFileChecker.FlatStyle = FlatStyle.System;
			this.SystemFileChecker.image = null;
			this.SystemFileChecker.Location = new Point(15, 295);
			this.SystemFileChecker.Name = "SystemFileChecker";
			this.SystemFileChecker.Size = new Size(286, 74);
			this.SystemFileChecker.SupplementalExplaination = "We suggest you first run System File Checker to fix corrupted system files, if any";
			this.SystemFileChecker.TabIndex = 9;
			this.SystemFileChecker.Text = "System File Checker Utility";
			this.SystemFileChecker.UseVisualStyleBackColor = true;
			this.FileExplorer.BackColor = Color.White;
			this.FileExplorer.Controls.Add(this.RecycleBinIsGreyedOut);
			this.FileExplorer.Controls.Add(this.Label76);
			this.FileExplorer.Controls.Add(this.Button77);
			this.FileExplorer.Controls.Add(this.showhiddenfiles);
			this.FileExplorer.Controls.Add(this.ClassNotRegistred);
			this.FileExplorer.Controls.Add(this.Label55);
			this.FileExplorer.Controls.Add(this.CDDriveOrDVD);
			this.FileExplorer.Controls.Add(this.ResetRecycleBin);
			this.FileExplorer.Controls.Add(this.ThumbnailsNotShowing);
			this.FileExplorer.Controls.Add(this.ExplorerDoesNTStart);
			this.FileExplorer.Controls.Add(this.FixRecycleBinIcon);
			this.FileExplorer.Controls.Add(this.FixFolderOptions);
			this.FileExplorer.Controls.Add(this.Label51);
			this.FileExplorer.Controls.Add(this.WerMgrorWerFault2);
			this.FileExplorer.Controls.Add(this.WerMgrorWerFault);
			this.FileExplorer.Controls.Add(this.Label40);
			this.FileExplorer.Controls.Add(this.Button11);
			this.FileExplorer.Controls.Add(this.Button10);
			this.FileExplorer.Controls.Add(this.Button9);
			this.FileExplorer.Controls.Add(this.Button8);
			this.FileExplorer.Controls.Add(this.Button7);
			this.FileExplorer.Controls.Add(this.Button6);
			this.FileExplorer.Controls.Add(this.Button5);
			this.FileExplorer.Controls.Add(this.Button4);
			this.FileExplorer.Controls.Add(this.Button3);
			this.FileExplorer.Controls.Add(this.Button2);
			this.FileExplorer.Controls.Add(this.RecycleBinIconMissing);
			this.FileExplorer.Controls.Add(this.Label36);
			this.FileExplorer.Controls.Add(this.Label4);
			this.FileExplorer.Controls.Add(this.Label9);
			this.FileExplorer.Controls.Add(this.Label8);
			this.FileExplorer.Controls.Add(this.Label7);
			this.FileExplorer.Controls.Add(this.Label6);
			this.FileExplorer.Controls.Add(this.Label5);
			this.FileExplorer.Location = new Point(4, 22);
			this.FileExplorer.Name = "FileExplorer";
			this.FileExplorer.Size = new Size(605, 527);
			this.FileExplorer.TabIndex = 1;
			this.FileExplorer.Text = "File Explorer";
			this.RecycleBinIsGreyedOut.Location = new Point(509, 485);
			this.RecycleBinIsGreyedOut.Name = "RecycleBinIsGreyedOut";
			this.RecycleBinIsGreyedOut.Size = new Size(60, 26);
			this.RecycleBinIsGreyedOut.TabIndex = 21;
			this.RecycleBinIsGreyedOut.Text = "Fix";
			this.RecycleBinIsGreyedOut.UseVisualStyleBackColor = true;
			this.Label76.AutoSize = true;
			this.Label76.Location = new Point(20, 489);
			this.Label76.Name = "Label76";
			this.Label76.Size = new Size(391, 17);
			this.Label76.TabIndex = 20;
			this.Label76.Text = "Recycle Bin is greyed out in Desktop Icon Settings on Windows 11";
			this.Button77.Location = new Point(574, 485);
			this.Button77.Name = "Button77";
			this.Button77.Size = new Size(22, 26);
			this.Button77.TabIndex = 19;
			this.Button77.Text = "?";
			this.Button77.UseVisualStyleBackColor = true;
			this.showhiddenfiles.Location = new Point(509, 444);
			this.showhiddenfiles.Name = "showhiddenfiles";
			this.showhiddenfiles.Size = new Size(60, 26);
			this.showhiddenfiles.TabIndex = 15;
			this.showhiddenfiles.Text = "Fix";
			this.showhiddenfiles.UseVisualStyleBackColor = true;
			this.ClassNotRegistred.Location = new Point(508, 399);
			this.ClassNotRegistred.Name = "ClassNotRegistred";
			this.ClassNotRegistred.Size = new Size(60, 26);
			this.ClassNotRegistred.TabIndex = 5;
			this.ClassNotRegistred.Text = "Fix";
			this.ClassNotRegistred.UseVisualStyleBackColor = true;
			this.Label55.AutoSize = true;
			this.Label55.Location = new Point(20, 448);
			this.Label55.Name = "Label55";
			this.Label55.Size = new Size(448, 17);
			this.Label55.TabIndex = 14;
			this.Label55.Text = "\"Show hidden files, folders and drives\" option isn't shown in Folder Options";
			this.CDDriveOrDVD.Location = new Point(508, 356);
			this.CDDriveOrDVD.Name = "CDDriveOrDVD";
			this.CDDriveOrDVD.Size = new Size(60, 26);
			this.CDDriveOrDVD.TabIndex = 5;
			this.CDDriveOrDVD.Text = "Fix";
			this.CDDriveOrDVD.UseVisualStyleBackColor = true;
			this.ResetRecycleBin.Location = new Point(508, 312);
			this.ResetRecycleBin.Name = "ResetRecycleBin";
			this.ResetRecycleBin.Size = new Size(60, 26);
			this.ResetRecycleBin.TabIndex = 5;
			this.ResetRecycleBin.Text = "Fix";
			this.ResetRecycleBin.UseVisualStyleBackColor = true;
			this.ThumbnailsNotShowing.Location = new Point(508, 267);
			this.ThumbnailsNotShowing.Name = "ThumbnailsNotShowing";
			this.ThumbnailsNotShowing.Size = new Size(60, 26);
			this.ThumbnailsNotShowing.TabIndex = 5;
			this.ThumbnailsNotShowing.Text = "Fix";
			this.ThumbnailsNotShowing.UseVisualStyleBackColor = true;
			this.ExplorerDoesNTStart.Location = new Point(508, 220);
			this.ExplorerDoesNTStart.Name = "ExplorerDoesNTStart";
			this.ExplorerDoesNTStart.Size = new Size(60, 26);
			this.ExplorerDoesNTStart.TabIndex = 5;
			this.ExplorerDoesNTStart.Text = "Fix";
			this.ExplorerDoesNTStart.UseVisualStyleBackColor = true;
			this.FixRecycleBinIcon.Location = new Point(508, 174);
			this.FixRecycleBinIcon.Name = "FixRecycleBinIcon";
			this.FixRecycleBinIcon.Size = new Size(60, 26);
			this.FixRecycleBinIcon.TabIndex = 5;
			this.FixRecycleBinIcon.Text = "Fix";
			this.FixRecycleBinIcon.UseVisualStyleBackColor = true;
			this.FixFolderOptions.Location = new Point(508, 126);
			this.FixFolderOptions.Name = "FixFolderOptions";
			this.FixFolderOptions.Size = new Size(60, 26);
			this.FixFolderOptions.TabIndex = 5;
			this.FixFolderOptions.Text = "Fix";
			this.FixFolderOptions.UseVisualStyleBackColor = true;
			this.Label51.Location = new Point(20, 404);
			this.Label51.Name = "Label51";
			this.Label51.Size = new Size(427, 22);
			this.Label51.TabIndex = 0;
			this.Label51.Text = "\"Class not registered\" error in File Explorer or Internet Explorer";
			this.WerMgrorWerFault2.Location = new Point(509, 83);
			this.WerMgrorWerFault2.Name = "WerMgrorWerFault2";
			this.WerMgrorWerFault2.Size = new Size(60, 26);
			this.WerMgrorWerFault2.TabIndex = 5;
			this.WerMgrorWerFault2.Text = "Fix 2";
			this.WerMgrorWerFault2.UseVisualStyleBackColor = true;
			this.WerMgrorWerFault.Location = new Point(508, 53);
			this.WerMgrorWerFault.Name = "WerMgrorWerFault";
			this.WerMgrorWerFault.Size = new Size(60, 26);
			this.WerMgrorWerFault.TabIndex = 5;
			this.WerMgrorWerFault.Text = "Fix 1";
			this.WerMgrorWerFault.UseVisualStyleBackColor = true;
			this.Label40.Location = new Point(20, 361);
			this.Label40.Name = "Label40";
			this.Label40.Size = new Size(427, 22);
			this.Label40.TabIndex = 0;
			this.Label40.Text = "CD drive or DVD drive isn't recognised by Windows or other programs";
			this.Button11.Location = new Point(574, 444);
			this.Button11.Name = "Button11";
			this.Button11.Size = new Size(22, 26);
			this.Button11.TabIndex = 5;
			this.Button11.Text = "?";
			this.Button11.UseVisualStyleBackColor = true;
			this.Button10.Location = new Point(574, 399);
			this.Button10.Name = "Button10";
			this.Button10.Size = new Size(22, 26);
			this.Button10.TabIndex = 5;
			this.Button10.Text = "?";
			this.Button10.UseVisualStyleBackColor = true;
			this.Button9.Location = new Point(574, 356);
			this.Button9.Name = "Button9";
			this.Button9.Size = new Size(22, 26);
			this.Button9.TabIndex = 5;
			this.Button9.Text = "?";
			this.Button9.UseVisualStyleBackColor = true;
			this.Button8.Location = new Point(574, 312);
			this.Button8.Name = "Button8";
			this.Button8.Size = new Size(22, 26);
			this.Button8.TabIndex = 5;
			this.Button8.Text = "?";
			this.Button8.UseVisualStyleBackColor = true;
			this.Button7.Location = new Point(574, 267);
			this.Button7.Name = "Button7";
			this.Button7.Size = new Size(22, 26);
			this.Button7.TabIndex = 5;
			this.Button7.Text = "?";
			this.Button7.UseVisualStyleBackColor = true;
			this.Button6.Location = new Point(574, 220);
			this.Button6.Name = "Button6";
			this.Button6.Size = new Size(22, 26);
			this.Button6.TabIndex = 5;
			this.Button6.Text = "?";
			this.Button6.UseVisualStyleBackColor = true;
			this.Button5.Location = new Point(574, 174);
			this.Button5.Name = "Button5";
			this.Button5.Size = new Size(22, 26);
			this.Button5.TabIndex = 5;
			this.Button5.Text = "?";
			this.Button5.UseVisualStyleBackColor = true;
			this.Button4.Location = new Point(574, 126);
			this.Button4.Name = "Button4";
			this.Button4.Size = new Size(22, 26);
			this.Button4.TabIndex = 5;
			this.Button4.Text = "?";
			this.Button4.UseVisualStyleBackColor = true;
			this.Button3.Location = new Point(574, 68);
			this.Button3.Name = "Button3";
			this.Button3.Size = new Size(22, 26);
			this.Button3.TabIndex = 5;
			this.Button3.Text = "?";
			this.Button3.UseVisualStyleBackColor = true;
			this.Button2.Location = new Point(574, 17);
			this.Button2.Name = "Button2";
			this.Button2.Size = new Size(22, 26);
			this.Button2.TabIndex = 5;
			this.Button2.Text = "?";
			this.Button2.UseVisualStyleBackColor = true;
			this.RecycleBinIconMissing.Location = new Point(508, 17);
			this.RecycleBinIconMissing.Name = "RecycleBinIconMissing";
			this.RecycleBinIconMissing.Size = new Size(60, 26);
			this.RecycleBinIconMissing.TabIndex = 5;
			this.RecycleBinIconMissing.Text = "Fix";
			this.RecycleBinIconMissing.UseVisualStyleBackColor = true;
			this.Label36.Location = new Point(20, 315);
			this.Label36.Name = "Label36";
			this.Label36.Size = new Size(427, 22);
			this.Label36.TabIndex = 0;
			this.Label36.Text = "Reset Recycle Bin. Recycle Bin is corrupted.";
			this.Label4.AutoSize = true;
			this.Label4.Location = new Point(20, 19);
			this.Label4.Name = "Label4";
			this.Label4.Size = new Size(245, 17);
			this.Label4.TabIndex = 0;
			this.Label4.Text = "Recycle Bin icon is missing from Desktop";
			this.Label9.Location = new Point(20, 270);
			this.Label9.Name = "Label9";
			this.Label9.Size = new Size(427, 23);
			this.Label9.TabIndex = 0;
			this.Label9.Text = "Thumbnails not showing in File Explorer";
			this.Label8.Location = new Point(20, 224);
			this.Label8.Name = "Label8";
			this.Label8.Size = new Size(427, 23);
			this.Label8.TabIndex = 0;
			this.Label8.Text = "Explorer doesn't start on startup in Windows";
			this.Label7.Location = new Point(20, 179);
			this.Label7.Name = "Label7";
			this.Label7.Size = new Size(427, 23);
			this.Label7.TabIndex = 0;
			this.Label7.Text = "Fix Recycle Bin when its icon doesn't refresh automatically";
			this.Label6.Location = new Point(20, 127);
			this.Label6.Name = "Label6";
			this.Label6.Size = new Size(465, 40);
			this.Label6.TabIndex = 0;
			this.Label6.Text = "File Explorer Options is missing from Control Panel or has been disabled by administrator or malware";
			this.Label5.Location = new Point(20, 63);
			this.Label5.Name = "Label5";
			this.Label5.Size = new Size(465, 40);
			this.Label5.TabIndex = 0;
			this.Label5.Text = "WerMgr.exe or WerFault.exe Application Error. The instruction at the referenced memory could not read. Click on OK to terminate the program";
			this.InternetConnectivity.BackColor = Color.White;
			this.InternetConnectivity.Controls.Add(this.Button73);
			this.InternetConnectivity.Controls.Add(this.TelnetIsNotRecognised);
			this.InternetConnectivity.Controls.Add(this.Label74);
			this.InternetConnectivity.Controls.Add(this.Button12);
			this.InternetConnectivity.Controls.Add(this.winsock);
			this.InternetConnectivity.Controls.Add(this.Button13);
			this.InternetConnectivity.Controls.Add(this.internetoptionsmissing);
			this.InternetConnectivity.Controls.Add(this.Button14);
			this.InternetConnectivity.Controls.Add(this.OptimizeIE);
			this.InternetConnectivity.Controls.Add(this.Button15);
			this.InternetConnectivity.Controls.Add(this.RuntimeErrors);
			this.InternetConnectivity.Controls.Add(this.Button16);
			this.InternetConnectivity.Controls.Add(this.ResetIE);
			this.InternetConnectivity.Controls.Add(this.Button17);
			this.InternetConnectivity.Controls.Add(this.Label58);
			this.InternetConnectivity.Controls.Add(this.Button18);
			this.InternetConnectivity.Controls.Add(this.ResetWindowsFirewall);
			this.InternetConnectivity.Controls.Add(this.Button19);
			this.InternetConnectivity.Controls.Add(this.Label56);
			this.InternetConnectivity.Controls.Add(this.Button20);
			this.InternetConnectivity.Controls.Add(this.ClearWindowsUpdateHistory);
			this.InternetConnectivity.Controls.Add(this.Button21);
			this.InternetConnectivity.Controls.Add(this.Label45);
			this.InternetConnectivity.Controls.Add(this.FixDNSResolverCache);
			this.InternetConnectivity.Controls.Add(this.Label44);
			this.InternetConnectivity.Controls.Add(this.ResetInternetProtocol);
			this.InternetConnectivity.Controls.Add(this.Label41);
			this.InternetConnectivity.Controls.Add(this.RightClickMenuIEDisabled);
			this.InternetConnectivity.Controls.Add(this.Label14);
			this.InternetConnectivity.Controls.Add(this.Label13);
			this.InternetConnectivity.Controls.Add(this.Label12);
			this.InternetConnectivity.Controls.Add(this.Label11);
			this.InternetConnectivity.Controls.Add(this.Label10);
			this.InternetConnectivity.Location = new Point(4, 22);
			this.InternetConnectivity.Name = "InternetConnectivity";
			this.InternetConnectivity.Size = new Size(605, 527);
			this.InternetConnectivity.TabIndex = 2;
			this.InternetConnectivity.Text = "Internet && Connectivity";
			this.Button73.Location = new Point(573, 485);
			this.Button73.Name = "Button73";
			this.Button73.Size = new Size(22, 26);
			this.Button73.TabIndex = 18;
			this.Button73.Text = "?";
			this.Button73.UseVisualStyleBackColor = true;
			this.TelnetIsNotRecognised.Location = new Point(507, 485);
			this.TelnetIsNotRecognised.Name = "TelnetIsNotRecognised";
			this.TelnetIsNotRecognised.Size = new Size(60, 26);
			this.TelnetIsNotRecognised.TabIndex = 17;
			this.TelnetIsNotRecognised.Text = "Fix";
			this.TelnetIsNotRecognised.UseVisualStyleBackColor = true;
			this.Label74.Location = new Point(20, 489);
			this.Label74.Name = "Label74";
			this.Label74.Size = new Size(454, 22);
			this.Label74.TabIndex = 16;
			this.Label74.Text = "Telnet is not recognized as an internal or external command";
			this.Button12.Location = new Point(573, 447);
			this.Button12.Name = "Button12";
			this.Button12.Size = new Size(22, 26);
			this.Button12.TabIndex = 6;
			this.Button12.Text = "?";
			this.Button12.UseVisualStyleBackColor = true;
			this.winsock.Location = new Point(507, 447);
			this.winsock.Name = "winsock";
			this.winsock.Size = new Size(60, 26);
			this.winsock.TabIndex = 6;
			this.winsock.Text = "Fix";
			this.winsock.UseVisualStyleBackColor = true;
			this.Button13.Location = new Point(573, 401);
			this.Button13.Name = "Button13";
			this.Button13.Size = new Size(22, 26);
			this.Button13.TabIndex = 7;
			this.Button13.Text = "?";
			this.Button13.UseVisualStyleBackColor = true;
			this.internetoptionsmissing.Location = new Point(507, 401);
			this.internetoptionsmissing.Name = "internetoptionsmissing";
			this.internetoptionsmissing.Size = new Size(60, 26);
			this.internetoptionsmissing.TabIndex = 6;
			this.internetoptionsmissing.Text = "Fix";
			this.internetoptionsmissing.UseVisualStyleBackColor = true;
			this.Button14.Location = new Point(573, 353);
			this.Button14.Name = "Button14";
			this.Button14.Size = new Size(22, 26);
			this.Button14.TabIndex = 8;
			this.Button14.Text = "?";
			this.Button14.UseVisualStyleBackColor = true;
			this.OptimizeIE.Location = new Point(507, 353);
			this.OptimizeIE.Name = "OptimizeIE";
			this.OptimizeIE.Size = new Size(60, 26);
			this.OptimizeIE.TabIndex = 6;
			this.OptimizeIE.Text = "Fix";
			this.OptimizeIE.UseVisualStyleBackColor = true;
			this.Button15.Location = new Point(573, 305);
			this.Button15.Name = "Button15";
			this.Button15.Size = new Size(22, 26);
			this.Button15.TabIndex = 9;
			this.Button15.Text = "?";
			this.Button15.UseVisualStyleBackColor = true;
			this.RuntimeErrors.Location = new Point(507, 305);
			this.RuntimeErrors.Name = "RuntimeErrors";
			this.RuntimeErrors.Size = new Size(60, 26);
			this.RuntimeErrors.TabIndex = 6;
			this.RuntimeErrors.Text = "Fix";
			this.RuntimeErrors.UseVisualStyleBackColor = true;
			this.Button16.Location = new Point(573, 257);
			this.Button16.Name = "Button16";
			this.Button16.Size = new Size(22, 26);
			this.Button16.TabIndex = 10;
			this.Button16.Text = "?";
			this.Button16.UseVisualStyleBackColor = true;
			this.ResetIE.Location = new Point(507, 257);
			this.ResetIE.Name = "ResetIE";
			this.ResetIE.Size = new Size(60, 26);
			this.ResetIE.TabIndex = 6;
			this.ResetIE.Text = "Fix";
			this.ResetIE.UseVisualStyleBackColor = true;
			this.Button17.Location = new Point(573, 208);
			this.Button17.Name = "Button17";
			this.Button17.Size = new Size(22, 26);
			this.Button17.TabIndex = 11;
			this.Button17.Text = "?";
			this.Button17.UseVisualStyleBackColor = true;
			this.Label58.Location = new Point(20, 451);
			this.Label58.Name = "Label58";
			this.Label58.Size = new Size(427, 22);
			this.Label58.TabIndex = 5;
			this.Label58.Text = "Difficulty connecting internet? Repair Winsock (Reset Catalog)";
			this.Button18.Location = new Point(573, 161);
			this.Button18.Name = "Button18";
			this.Button18.Size = new Size(22, 26);
			this.Button18.TabIndex = 12;
			this.Button18.Text = "?";
			this.Button18.UseVisualStyleBackColor = true;
			this.ResetWindowsFirewall.Location = new Point(507, 209);
			this.ResetWindowsFirewall.Name = "ResetWindowsFirewall";
			this.ResetWindowsFirewall.Size = new Size(60, 26);
			this.ResetWindowsFirewall.TabIndex = 6;
			this.ResetWindowsFirewall.Text = "Fix";
			this.ResetWindowsFirewall.UseVisualStyleBackColor = true;
			this.Button19.Location = new Point(573, 113);
			this.Button19.Name = "Button19";
			this.Button19.Size = new Size(22, 26);
			this.Button19.TabIndex = 13;
			this.Button19.Text = "?";
			this.Button19.UseVisualStyleBackColor = true;
			this.Label56.Location = new Point(20, 396);
			this.Label56.Name = "Label56";
			this.Label56.Size = new Size(427, 37);
			this.Label56.TabIndex = 5;
			this.Label56.Text = "Internet Options are missing in Settings under \"Advanced\" tab of \"Internet Options\" dialog box";
			this.Button20.Location = new Point(573, 64);
			this.Button20.Name = "Button20";
			this.Button20.Size = new Size(22, 26);
			this.Button20.TabIndex = 14;
			this.Button20.Text = "?";
			this.Button20.UseVisualStyleBackColor = true;
			this.ClearWindowsUpdateHistory.Location = new Point(507, 161);
			this.ClearWindowsUpdateHistory.Name = "ClearWindowsUpdateHistory";
			this.ClearWindowsUpdateHistory.Size = new Size(60, 26);
			this.ClearWindowsUpdateHistory.TabIndex = 6;
			this.ClearWindowsUpdateHistory.Text = "Fix";
			this.ClearWindowsUpdateHistory.UseVisualStyleBackColor = true;
			this.Button21.Location = new Point(573, 17);
			this.Button21.Name = "Button21";
			this.Button21.Size = new Size(22, 26);
			this.Button21.TabIndex = 15;
			this.Button21.Text = "?";
			this.Button21.UseVisualStyleBackColor = true;
			this.Label45.Location = new Point(20, 350);
			this.Label45.Name = "Label45";
			this.Label45.Size = new Size(427, 34);
			this.Label45.TabIndex = 5;
			this.Label45.Text = "Optimize Internet Explorer maximum connections per server to download more than two files at the same time";
			this.FixDNSResolverCache.Location = new Point(507, 113);
			this.FixDNSResolverCache.Name = "FixDNSResolverCache";
			this.FixDNSResolverCache.Size = new Size(60, 26);
			this.FixDNSResolverCache.TabIndex = 6;
			this.FixDNSResolverCache.Text = "Fix";
			this.FixDNSResolverCache.UseVisualStyleBackColor = true;
			this.Label44.Location = new Point(20, 305);
			this.Label44.Name = "Label44";
			this.Label44.Size = new Size(427, 34);
			this.Label44.TabIndex = 5;
			this.Label44.Text = "Runtime errors are appearing in Internet Explorer while surfing";
			this.ResetInternetProtocol.Location = new Point(507, 65);
			this.ResetInternetProtocol.Name = "ResetInternetProtocol";
			this.ResetInternetProtocol.Size = new Size(60, 26);
			this.ResetInternetProtocol.TabIndex = 6;
			this.ResetInternetProtocol.Text = "Fix";
			this.ResetInternetProtocol.UseVisualStyleBackColor = true;
			this.Label41.Location = new Point(20, 258);
			this.Label41.Name = "Label41";
			this.Label41.Size = new Size(427, 34);
			this.Label41.TabIndex = 5;
			this.Label41.Text = "Reset Internet Explorer To Default Configuration.";
			this.RightClickMenuIEDisabled.Location = new Point(507, 17);
			this.RightClickMenuIEDisabled.Name = "RightClickMenuIEDisabled";
			this.RightClickMenuIEDisabled.Size = new Size(60, 26);
			this.RightClickMenuIEDisabled.TabIndex = 6;
			this.RightClickMenuIEDisabled.Text = "Fix";
			this.RightClickMenuIEDisabled.UseVisualStyleBackColor = true;
			this.Label14.Location = new Point(20, 199);
			this.Label14.Name = "Label14";
			this.Label14.Size = new Size(427, 35);
			this.Label14.TabIndex = 5;
			this.Label14.Text = "Problem with Windows Firewall settings. Reset Windows Firewall Configuration";
			this.Label13.Location = new Point(20, 163);
			this.Label13.Name = "Label13";
			this.Label13.Size = new Size(427, 35);
			this.Label13.TabIndex = 5;
			this.Label13.Text = "Long list of failed and installed updates. Clear Windows Update History";
			this.Label12.Location = new Point(20, 110);
			this.Label12.Name = "Label12";
			this.Label12.Size = new Size(427, 36);
			this.Label12.TabIndex = 5;
			this.Label12.Text = "Problem regarding DNS resolution. Fix it by clearing DNS resolver cache";
			this.Label11.Location = new Point(20, 55);
			this.Label11.Name = "Label11";
			this.Label11.Size = new Size(427, 42);
			this.Label11.TabIndex = 5;
			this.Label11.Text = "Cannot connect to internet. There's some problem in Internet Protocol (TCP/IP)";
			this.Label10.AutoSize = true;
			this.Label10.Location = new Point(20, 19);
			this.Label10.Name = "Label10";
			this.Label10.Size = new Size(337, 17);
			this.Label10.TabIndex = 5;
			this.Label10.Text = "Right Click Context Menu of Internet Explorer is disabled";
			this.Windows11.BackColor = Color.White;
			this.Windows11.Controls.Add(this.Button78);
			this.Windows11.Controls.Add(this.Button75);
			this.Windows11.Controls.Add(this.BatteryRemainingTime);
			this.Windows11.Controls.Add(this.WslRegisterDistributionFailed);
			this.Windows11.Controls.Add(this.Label77);
			this.Windows11.Controls.Add(this.Label75);
			this.Windows11.Controls.Add(this.Button70);
			this.Windows11.Controls.Add(this.WindowsUpdateSpecialError);
			this.Windows11.Controls.Add(this.Label73);
			this.Windows11.Controls.Add(this.Button66);
			this.Windows11.Controls.Add(this.WindowsSandboxFailedToStart);
			this.Windows11.Controls.Add(this.Label43);
			this.Windows11.Controls.Add(this.Button46);
			this.Windows11.Controls.Add(this.MultipleEntriesFix2);
			this.Windows11.Controls.Add(this.MultipleEntriesFix1);
			this.Windows11.Controls.Add(this.Label20);
			this.Windows11.Controls.Add(this.Button53);
			this.Windows11.Controls.Add(this.Button52);
			this.Windows11.Controls.Add(this.Button42);
			this.Windows11.Controls.Add(this.Button43);
			this.Windows11.Controls.Add(this.dism);
			this.Windows11.Controls.Add(this.WinUpdatesStuck);
			this.Windows11.Controls.Add(this.Wifidoesntwork);
			this.Windows11.Controls.Add(this.Button44);
			this.Windows11.Controls.Add(this.StartMenuDoesNTOpen);
			this.Windows11.Controls.Add(this.Label46);
			this.Windows11.Controls.Add(this.Button45);
			this.Windows11.Controls.Add(this.Label1);
			this.Windows11.Controls.Add(this.Label60);
			this.Windows11.Controls.Add(this.Label39);
			this.Windows11.Controls.Add(this.Label37);
			this.Windows11.Controls.Add(this.Revert);
			this.Windows11.Controls.Add(this.Label38);
			this.Windows11.Controls.Add(this.DisableOnedrive);
			this.Windows11.Controls.Add(this.ResetPCSettings);
			this.Windows11.Location = new Point(4, 22);
			this.Windows11.Name = "Windows11";
			this.Windows11.Size = new Size(605, 527);
			this.Windows11.TabIndex = 8;
			this.Windows11.Text = "Page 1";
			this.Button78.Location = new Point(575, 483);
			this.Button78.Name = "Button78";
			this.Button78.Size = new Size(22, 26);
			this.Button78.TabIndex = 35;
			this.Button78.Text = "?";
			this.Button78.UseVisualStyleBackColor = true;
			this.Button75.Location = new Point(574, 446);
			this.Button75.Name = "Button75";
			this.Button75.Size = new Size(22, 26);
			this.Button75.TabIndex = 41;
			this.Button75.Text = "?";
			this.Button75.UseVisualStyleBackColor = true;
			this.BatteryRemainingTime.Location = new Point(508, 483);
			this.BatteryRemainingTime.Name = "BatteryRemainingTime";
			this.BatteryRemainingTime.Size = new Size(60, 26);
			this.BatteryRemainingTime.TabIndex = 34;
			this.BatteryRemainingTime.Text = "Fix";
			this.BatteryRemainingTime.UseVisualStyleBackColor = true;
			this.WslRegisterDistributionFailed.Location = new Point(507, 446);
			this.WslRegisterDistributionFailed.Name = "WslRegisterDistributionFailed";
			this.WslRegisterDistributionFailed.Size = new Size(60, 26);
			this.WslRegisterDistributionFailed.TabIndex = 40;
			this.WslRegisterDistributionFailed.Text = "Fix";
			this.WslRegisterDistributionFailed.UseVisualStyleBackColor = true;
			this.Label77.Location = new Point(21, 487);
			this.Label77.Name = "Label77";
			this.Label77.Size = new Size(432, 22);
			this.Label77.TabIndex = 33;
			this.Label77.Text = "Battery Remaining Time Not Visible in Battery Layout";
			this.Label75.Location = new Point(20, 450);
			this.Label75.Name = "Label75";
			this.Label75.Size = new Size(432, 22);
			this.Label75.TabIndex = 39;
			this.Label75.Text = "WslRegisterDistribution failed with error: 0x8007019e && 0x8000000d";
			this.Button70.Location = new Point(574, 404);
			this.Button70.Name = "Button70";
			this.Button70.Size = new Size(22, 26);
			this.Button70.TabIndex = 38;
			this.Button70.Text = "?";
			this.Button70.UseVisualStyleBackColor = true;
			this.WindowsUpdateSpecialError.Location = new Point(507, 404);
			this.WindowsUpdateSpecialError.Name = "WindowsUpdateSpecialError";
			this.WindowsUpdateSpecialError.Size = new Size(60, 26);
			this.WindowsUpdateSpecialError.TabIndex = 37;
			this.WindowsUpdateSpecialError.Text = "Fix";
			this.WindowsUpdateSpecialError.UseVisualStyleBackColor = true;
			this.Label73.Location = new Point(20, 408);
			this.Label73.Name = "Label73";
			this.Label73.Size = new Size(432, 22);
			this.Label73.TabIndex = 36;
			this.Label73.Text = "Windows Update Error 0x80070057";
			this.Button66.Location = new Point(574, 362);
			this.Button66.Name = "Button66";
			this.Button66.Size = new Size(22, 26);
			this.Button66.TabIndex = 35;
			this.Button66.Text = "?";
			this.Button66.UseVisualStyleBackColor = true;
			this.WindowsSandboxFailedToStart.Location = new Point(507, 362);
			this.WindowsSandboxFailedToStart.Name = "WindowsSandboxFailedToStart";
			this.WindowsSandboxFailedToStart.Size = new Size(60, 26);
			this.WindowsSandboxFailedToStart.TabIndex = 34;
			this.WindowsSandboxFailedToStart.Text = "Fix";
			this.WindowsSandboxFailedToStart.UseVisualStyleBackColor = true;
			this.Label43.Location = new Point(20, 355);
			this.Label43.Name = "Label43";
			this.Label43.Size = new Size(432, 36);
			this.Label43.TabIndex = 33;
			this.Label43.Text = "Windows Sandbox failed to start, Error 0x80070057, The parameter is incorrect";
			this.Button46.Location = new Point(574, 311);
			this.Button46.Name = "Button46";
			this.Button46.Size = new Size(22, 26);
			this.Button46.TabIndex = 32;
			this.Button46.Text = "?";
			this.Button46.UseVisualStyleBackColor = true;
			this.MultipleEntriesFix2.Location = new Point(507, 310);
			this.MultipleEntriesFix2.Name = "MultipleEntriesFix2";
			this.MultipleEntriesFix2.Size = new Size(60, 26);
			this.MultipleEntriesFix2.TabIndex = 31;
			this.MultipleEntriesFix2.Text = "Fix 2";
			this.MultipleEntriesFix2.UseVisualStyleBackColor = true;
			this.MultipleEntriesFix1.Location = new Point(431, 310);
			this.MultipleEntriesFix1.Name = "MultipleEntriesFix1";
			this.MultipleEntriesFix1.Size = new Size(60, 26);
			this.MultipleEntriesFix1.TabIndex = 31;
			this.MultipleEntriesFix1.Text = "Fix 1";
			this.MultipleEntriesFix1.UseVisualStyleBackColor = true;
			this.Label20.Location = new Point(20, 315);
			this.Label20.Name = "Label20";
			this.Label20.Size = new Size(405, 22);
			this.Label20.TabIndex = 30;
			this.Label20.Text = "There are multiple entries of OneDrive in File Explorer";
			this.Button53.Location = new Point(574, 266);
			this.Button53.Name = "Button53";
			this.Button53.Size = new Size(22, 26);
			this.Button53.TabIndex = 26;
			this.Button53.Text = "?";
			this.Button53.UseVisualStyleBackColor = true;
			this.Button52.Location = new Point(574, 219);
			this.Button52.Name = "Button52";
			this.Button52.Size = new Size(22, 26);
			this.Button52.TabIndex = 26;
			this.Button52.Text = "?";
			this.Button52.UseVisualStyleBackColor = true;
			this.Button42.Location = new Point(574, 176);
			this.Button42.Name = "Button42";
			this.Button42.Size = new Size(22, 26);
			this.Button42.TabIndex = 26;
			this.Button42.Text = "?";
			this.Button42.UseVisualStyleBackColor = true;
			this.Button43.Location = new Point(574, 108);
			this.Button43.Name = "Button43";
			this.Button43.Size = new Size(22, 26);
			this.Button43.TabIndex = 27;
			this.Button43.Text = "?";
			this.Button43.UseVisualStyleBackColor = true;
			this.dism.Location = new Point(508, 17);
			this.dism.Name = "dism";
			this.dism.Size = new Size(60, 26);
			this.dism.TabIndex = 11;
			this.dism.Text = "Fix";
			this.dism.UseVisualStyleBackColor = true;
			this.WinUpdatesStuck.Location = new Point(507, 266);
			this.WinUpdatesStuck.Name = "WinUpdatesStuck";
			this.WinUpdatesStuck.Size = new Size(60, 26);
			this.WinUpdatesStuck.TabIndex = 19;
			this.WinUpdatesStuck.Text = "Fix";
			this.WinUpdatesStuck.UseVisualStyleBackColor = true;
			this.Wifidoesntwork.Location = new Point(507, 219);
			this.Wifidoesntwork.Name = "Wifidoesntwork";
			this.Wifidoesntwork.Size = new Size(60, 26);
			this.Wifidoesntwork.TabIndex = 19;
			this.Wifidoesntwork.Text = "Fix";
			this.Wifidoesntwork.UseVisualStyleBackColor = true;
			this.Button44.Location = new Point(574, 63);
			this.Button44.Name = "Button44";
			this.Button44.Size = new Size(22, 26);
			this.Button44.TabIndex = 28;
			this.Button44.Text = "?";
			this.Button44.UseVisualStyleBackColor = true;
			this.StartMenuDoesNTOpen.Location = new Point(507, 176);
			this.StartMenuDoesNTOpen.Name = "StartMenuDoesNTOpen";
			this.StartMenuDoesNTOpen.Size = new Size(60, 26);
			this.StartMenuDoesNTOpen.TabIndex = 19;
			this.StartMenuDoesNTOpen.Text = "Fix";
			this.StartMenuDoesNTOpen.UseVisualStyleBackColor = true;
			this.Label46.Location = new Point(20, 270);
			this.Label46.Name = "Label46";
			this.Label46.Size = new Size(432, 22);
			this.Label46.TabIndex = 18;
			this.Label46.Text = "Windows Updates stuck downloading updates after upgrading.";
			this.Button45.Location = new Point(574, 17);
			this.Button45.Name = "Button45";
			this.Button45.Size = new Size(22, 26);
			this.Button45.TabIndex = 29;
			this.Button45.Text = "?";
			this.Button45.UseVisualStyleBackColor = true;
			this.Label1.Location = new Point(20, 223);
			this.Label1.Name = "Label1";
			this.Label1.Size = new Size(432, 22);
			this.Label1.TabIndex = 18;
			this.Label1.Text = "Wi-fi doesn't work after upgrading to Windows 11.";
			this.Label60.Location = new Point(20, 16);
			this.Label60.Name = "Label60";
			this.Label60.Size = new Size(432, 40);
			this.Label60.TabIndex = 10;
			this.Label60.Text = "Windows Component Store Is Corrupt. Repair it using Deployment Imaging and Servicing Management (DISM)";
			this.Label39.Location = new Point(20, 180);
			this.Label39.Name = "Label39";
			this.Label39.Size = new Size(432, 22);
			this.Label39.TabIndex = 18;
			this.Label39.Text = "Start Menu doesn't open or doesn't work in Windows 11.";
			this.Label37.AutoSize = true;
			this.Label37.Location = new Point(20, 68);
			this.Label37.Name = "Label37";
			this.Label37.Size = new Size(363, 17);
			this.Label37.TabIndex = 14;
			this.Label37.Text = "Reset Settings app. Settings doesn't launch or exit with error.";
			this.Revert.Location = new Point(508, 141);
			this.Revert.Name = "Revert";
			this.Revert.Size = new Size(60, 26);
			this.Revert.TabIndex = 15;
			this.Revert.Text = "Revert";
			this.Revert.UseVisualStyleBackColor = true;
			this.Label38.Location = new Point(20, 112);
			this.Label38.Name = "Label38";
			this.Label38.Size = new Size(432, 39);
			this.Label38.TabIndex = 13;
			this.Label38.Text = "Disable OneDrive. OneDrive runs in background and may sync large amount of files.";
			this.DisableOnedrive.Location = new Point(508, 109);
			this.DisableOnedrive.Name = "DisableOnedrive";
			this.DisableOnedrive.Size = new Size(60, 26);
			this.DisableOnedrive.TabIndex = 16;
			this.DisableOnedrive.Text = "Fix";
			this.DisableOnedrive.UseVisualStyleBackColor = true;
			this.ResetPCSettings.Location = new Point(508, 62);
			this.ResetPCSettings.Name = "ResetPCSettings";
			this.ResetPCSettings.Size = new Size(60, 26);
			this.ResetPCSettings.TabIndex = 17;
			this.ResetPCSettings.Text = "Fix";
			this.ResetPCSettings.UseVisualStyleBackColor = true;
			this.WindowsStore.BackColor = Color.White;
			this.WindowsStore.Controls.Add(this.WindowsSavingJPGsDownloadedAsJFIFs);
			this.WindowsStore.Controls.Add(this.Button83);
			this.WindowsStore.Controls.Add(this.Label48);
			this.WindowsStore.Controls.Add(this.IssuesWithWindowsActivation);
			this.WindowsStore.Controls.Add(this.Button82);
			this.WindowsStore.Controls.Add(this.Label79);
			this.WindowsStore.Controls.Add(this.ReRegisterAllDLLFiles);
			this.WindowsStore.Controls.Add(this.Button81);
			this.WindowsStore.Controls.Add(this.Label78);
			this.WindowsStore.Controls.Add(this.ResetThumbnailCache);
			this.WindowsStore.Controls.Add(this.Button80);
			this.WindowsStore.Controls.Add(this.Labelxyz);
			this.WindowsStore.Controls.Add(this.SomethingHappened);
			this.WindowsStore.Controls.Add(this.Label15);
			this.WindowsStore.Controls.Add(this.Button47);
			this.WindowsStore.Controls.Add(this.TheApplicationsWasNotInstalled);
			this.WindowsStore.Controls.Add(this.Button49);
			this.WindowsStore.Controls.Add(this.Label16);
			this.WindowsStore.Controls.Add(this.ClearStoreCache);
			this.WindowsStore.Controls.Add(this.Button48);
			this.WindowsStore.Controls.Add(this.Label17);
			this.WindowsStore.Location = new Point(4, 26);
			this.WindowsStore.Name = "WindowsStore";
			this.WindowsStore.Size = new Size(605, 523);
			this.WindowsStore.TabIndex = 10;
			this.WindowsStore.Text = "Page 2";
			this.WindowsSavingJPGsDownloadedAsJFIFs.Location = new Point(508, 311);
			this.WindowsSavingJPGsDownloadedAsJFIFs.Name = "WindowsSavingJPGsDownloadedAsJFIFs";
			this.WindowsSavingJPGsDownloadedAsJFIFs.Size = new Size(60, 26);
			this.WindowsSavingJPGsDownloadedAsJFIFs.TabIndex = 49;
			this.WindowsSavingJPGsDownloadedAsJFIFs.Text = "Fix";
			this.WindowsSavingJPGsDownloadedAsJFIFs.UseVisualStyleBackColor = true;
			this.Button83.Location = new Point(574, 311);
			this.Button83.Name = "Button83";
			this.Button83.Size = new Size(22, 26);
			this.Button83.TabIndex = 50;
			this.Button83.Text = "?";
			this.Button83.UseVisualStyleBackColor = true;
			this.Label48.Location = new Point(19, 313);
			this.Label48.Name = "Label48";
			this.Label48.Size = new Size(433, 38);
			this.Label48.TabIndex = 48;
			this.Label48.Text = "Windows saving JPGs downloaded from the internet as JFIFs";
			this.IssuesWithWindowsActivation.Location = new Point(508, 262);
			this.IssuesWithWindowsActivation.Name = "IssuesWithWindowsActivation";
			this.IssuesWithWindowsActivation.Size = new Size(60, 26);
			this.IssuesWithWindowsActivation.TabIndex = 46;
			this.IssuesWithWindowsActivation.Text = "Fix";
			this.IssuesWithWindowsActivation.UseVisualStyleBackColor = true;
			this.Button82.Location = new Point(574, 262);
			this.Button82.Name = "Button82";
			this.Button82.Size = new Size(22, 26);
			this.Button82.TabIndex = 47;
			this.Button82.Text = "?";
			this.Button82.UseVisualStyleBackColor = true;
			this.Label79.Location = new Point(19, 265);
			this.Label79.Name = "Label79";
			this.Label79.Size = new Size(433, 38);
			this.Label79.TabIndex = 45;
			this.Label79.Text = "Issues with Windows Activation";
			this.ReRegisterAllDLLFiles.Location = new Point(508, 213);
			this.ReRegisterAllDLLFiles.Name = "ReRegisterAllDLLFiles";
			this.ReRegisterAllDLLFiles.Size = new Size(60, 26);
			this.ReRegisterAllDLLFiles.TabIndex = 43;
			this.ReRegisterAllDLLFiles.Text = "Fix";
			this.ReRegisterAllDLLFiles.UseVisualStyleBackColor = true;
			this.Button81.Location = new Point(574, 213);
			this.Button81.Name = "Button81";
			this.Button81.Size = new Size(22, 26);
			this.Button81.TabIndex = 44;
			this.Button81.Text = "?";
			this.Button81.UseVisualStyleBackColor = true;
			this.Label78.Location = new Point(19, 217);
			this.Label78.Name = "Label78";
			this.Label78.Size = new Size(433, 38);
			this.Label78.TabIndex = 42;
			this.Label78.Text = "Re-register all system DLL files";
			this.ResetThumbnailCache.Location = new Point(508, 164);
			this.ResetThumbnailCache.Name = "ResetThumbnailCache";
			this.ResetThumbnailCache.Size = new Size(60, 26);
			this.ResetThumbnailCache.TabIndex = 40;
			this.ResetThumbnailCache.Text = "Fix";
			this.ResetThumbnailCache.UseVisualStyleBackColor = true;
			this.Button80.Location = new Point(574, 164);
			this.Button80.Name = "Button80";
			this.Button80.Size = new Size(22, 26);
			this.Button80.TabIndex = 41;
			this.Button80.Text = "?";
			this.Button80.UseVisualStyleBackColor = true;
			this.Labelxyz.Location = new Point(19, 169);
			this.Labelxyz.Name = "Labelxyz";
			this.Labelxyz.Size = new Size(433, 38);
			this.Labelxyz.TabIndex = 39;
			this.Labelxyz.Text = "Thumbnails do not load or not working. Reset Thumbnail cache.";
			this.SomethingHappened.Location = new Point(508, 115);
			this.SomethingHappened.Name = "SomethingHappened";
			this.SomethingHappened.Size = new Size(60, 26);
			this.SomethingHappened.TabIndex = 33;
			this.SomethingHappened.Text = "Fix";
			this.SomethingHappened.UseVisualStyleBackColor = true;
			this.Label15.Location = new Point(19, 20);
			this.Label15.Name = "Label15";
			this.Label15.Size = new Size(433, 40);
			this.Label15.TabIndex = 30;
			this.Label15.Text = "Having problem downloading Apps from Store. Clear and reset Store cache";
			this.Button47.Location = new Point(574, 115);
			this.Button47.Name = "Button47";
			this.Button47.Size = new Size(22, 26);
			this.Button47.TabIndex = 36;
			this.Button47.Text = "?";
			this.Button47.UseVisualStyleBackColor = true;
			this.TheApplicationsWasNotInstalled.Location = new Point(508, 66);
			this.TheApplicationsWasNotInstalled.Name = "TheApplicationsWasNotInstalled";
			this.TheApplicationsWasNotInstalled.Size = new Size(60, 26);
			this.TheApplicationsWasNotInstalled.TabIndex = 35;
			this.TheApplicationsWasNotInstalled.Text = "Fix";
			this.TheApplicationsWasNotInstalled.UseVisualStyleBackColor = true;
			this.Button49.Location = new Point(574, 17);
			this.Button49.Name = "Button49";
			this.Button49.Size = new Size(22, 26);
			this.Button49.TabIndex = 38;
			this.Button49.Text = "?";
			this.Button49.UseVisualStyleBackColor = true;
			this.Label16.Location = new Point(19, 70);
			this.Label16.Name = "Label16";
			this.Label16.Size = new Size(433, 41);
			this.Label16.TabIndex = 31;
			this.Label16.Text = "The Application wasn't installed from Windows Store. Error Code: 0x8024001e";
			this.ClearStoreCache.Location = new Point(508, 17);
			this.ClearStoreCache.Name = "ClearStoreCache";
			this.ClearStoreCache.Size = new Size(60, 26);
			this.ClearStoreCache.TabIndex = 34;
			this.ClearStoreCache.Text = "Fix";
			this.ClearStoreCache.UseVisualStyleBackColor = true;
			this.Button48.Location = new Point(574, 66);
			this.Button48.Name = "Button48";
			this.Button48.Size = new Size(22, 26);
			this.Button48.TabIndex = 37;
			this.Button48.Text = "?";
			this.Button48.UseVisualStyleBackColor = true;
			this.Label17.Location = new Point(19, 121);
			this.Label17.Name = "Label17";
			this.Label17.Size = new Size(433, 38);
			this.Label17.TabIndex = 32;
			this.Label17.Text = "Windows Store apps not opening. Re-register all apps.";
			this.SystemTools.BackColor = Color.White;
			this.SystemTools.Controls.Add(this.Button22);
			this.SystemTools.Controls.Add(this.ResetWindowsSecurity);
			this.SystemTools.Controls.Add(this.Button23);
			this.SystemTools.Controls.Add(this.ActionCenterAnd);
			this.SystemTools.Controls.Add(this.Button24);
			this.SystemTools.Controls.Add(this.RepairWindowsDefender);
			this.SystemTools.Controls.Add(this.Button25);
			this.SystemTools.Controls.Add(this.DeviceManagerIsNot);
			this.SystemTools.Controls.Add(this.Button26);
			this.SystemTools.Controls.Add(this.SystemRestoreHasBeen);
			this.SystemTools.Controls.Add(this.Button27);
			this.SystemTools.Controls.Add(this.ResetWindowsSearch);
			this.SystemTools.Controls.Add(this.Button28);
			this.SystemTools.Controls.Add(this.EnableMMCSnap);
			this.SystemTools.Controls.Add(this.Button29);
			this.SystemTools.Controls.Add(this.RegistryEditorHasBeen);
			this.SystemTools.Controls.Add(this.Button30);
			this.SystemTools.Controls.Add(this.Label59);
			this.SystemTools.Controls.Add(this.Button31);
			this.SystemTools.Controls.Add(this.CommandPromptHasBeen);
			this.SystemTools.Controls.Add(this.Label50);
			this.SystemTools.Controls.Add(this.TaskManagerHasBeen);
			this.SystemTools.Controls.Add(this.Label30);
			this.SystemTools.Controls.Add(this.Label27);
			this.SystemTools.Controls.Add(this.Label26);
			this.SystemTools.Controls.Add(this.Label25);
			this.SystemTools.Controls.Add(this.Label24);
			this.SystemTools.Controls.Add(this.Label23);
			this.SystemTools.Controls.Add(this.Label22);
			this.SystemTools.Controls.Add(this.Label21);
			this.SystemTools.Location = new Point(4, 22);
			this.SystemTools.Name = "SystemTools";
			this.SystemTools.Size = new Size(605, 527);
			this.SystemTools.TabIndex = 4;
			this.SystemTools.Text = "System Tools";
			this.Button22.Location = new Point(574, 457);
			this.Button22.Name = "Button22";
			this.Button22.Size = new Size(22, 26);
			this.Button22.TabIndex = 16;
			this.Button22.Text = "?";
			this.Button22.UseVisualStyleBackColor = true;
			this.ResetWindowsSecurity.Location = new Point(508, 458);
			this.ResetWindowsSecurity.Name = "ResetWindowsSecurity";
			this.ResetWindowsSecurity.Size = new Size(60, 26);
			this.ResetWindowsSecurity.TabIndex = 11;
			this.ResetWindowsSecurity.Text = "Fix";
			this.ResetWindowsSecurity.UseVisualStyleBackColor = true;
			this.Button23.Location = new Point(574, 410);
			this.Button23.Name = "Button23";
			this.Button23.Size = new Size(22, 26);
			this.Button23.TabIndex = 17;
			this.Button23.Text = "?";
			this.Button23.UseVisualStyleBackColor = true;
			this.ActionCenterAnd.Location = new Point(508, 409);
			this.ActionCenterAnd.Name = "ActionCenterAnd";
			this.ActionCenterAnd.Size = new Size(60, 26);
			this.ActionCenterAnd.TabIndex = 11;
			this.ActionCenterAnd.Text = "Fix";
			this.ActionCenterAnd.UseVisualStyleBackColor = true;
			this.Button24.Location = new Point(574, 360);
			this.Button24.Name = "Button24";
			this.Button24.Size = new Size(22, 26);
			this.Button24.TabIndex = 18;
			this.Button24.Text = "?";
			this.Button24.UseVisualStyleBackColor = true;
			this.RepairWindowsDefender.Location = new Point(508, 360);
			this.RepairWindowsDefender.Name = "RepairWindowsDefender";
			this.RepairWindowsDefender.Size = new Size(60, 26);
			this.RepairWindowsDefender.TabIndex = 11;
			this.RepairWindowsDefender.Text = "Fix";
			this.RepairWindowsDefender.UseVisualStyleBackColor = true;
			this.Button25.Location = new Point(574, 311);
			this.Button25.Name = "Button25";
			this.Button25.Size = new Size(22, 26);
			this.Button25.TabIndex = 19;
			this.Button25.Text = "?";
			this.Button25.UseVisualStyleBackColor = true;
			this.DeviceManagerIsNot.Location = new Point(508, 311);
			this.DeviceManagerIsNot.Name = "DeviceManagerIsNot";
			this.DeviceManagerIsNot.Size = new Size(60, 26);
			this.DeviceManagerIsNot.TabIndex = 11;
			this.DeviceManagerIsNot.Text = "Fix";
			this.DeviceManagerIsNot.UseVisualStyleBackColor = true;
			this.Button26.Location = new Point(574, 262);
			this.Button26.Name = "Button26";
			this.Button26.Size = new Size(22, 26);
			this.Button26.TabIndex = 20;
			this.Button26.Text = "?";
			this.Button26.UseVisualStyleBackColor = true;
			this.SystemRestoreHasBeen.Location = new Point(508, 262);
			this.SystemRestoreHasBeen.Name = "SystemRestoreHasBeen";
			this.SystemRestoreHasBeen.Size = new Size(60, 26);
			this.SystemRestoreHasBeen.TabIndex = 11;
			this.SystemRestoreHasBeen.Text = "Fix";
			this.SystemRestoreHasBeen.UseVisualStyleBackColor = true;
			this.Button27.Location = new Point(574, 211);
			this.Button27.Name = "Button27";
			this.Button27.Size = new Size(22, 26);
			this.Button27.TabIndex = 21;
			this.Button27.Text = "?";
			this.Button27.UseVisualStyleBackColor = true;
			this.ResetWindowsSearch.Location = new Point(508, 213);
			this.ResetWindowsSearch.Name = "ResetWindowsSearch";
			this.ResetWindowsSearch.Size = new Size(60, 26);
			this.ResetWindowsSearch.TabIndex = 11;
			this.ResetWindowsSearch.Text = "Fix";
			this.ResetWindowsSearch.UseVisualStyleBackColor = true;
			this.Button28.Location = new Point(574, 164);
			this.Button28.Name = "Button28";
			this.Button28.Size = new Size(22, 26);
			this.Button28.TabIndex = 22;
			this.Button28.Text = "?";
			this.Button28.UseVisualStyleBackColor = true;
			this.EnableMMCSnap.Location = new Point(508, 164);
			this.EnableMMCSnap.Name = "EnableMMCSnap";
			this.EnableMMCSnap.Size = new Size(60, 26);
			this.EnableMMCSnap.TabIndex = 11;
			this.EnableMMCSnap.Text = "Fix";
			this.EnableMMCSnap.UseVisualStyleBackColor = true;
			this.Button29.Location = new Point(574, 115);
			this.Button29.Name = "Button29";
			this.Button29.Size = new Size(22, 26);
			this.Button29.TabIndex = 23;
			this.Button29.Text = "?";
			this.Button29.UseVisualStyleBackColor = true;
			this.RegistryEditorHasBeen.Location = new Point(508, 115);
			this.RegistryEditorHasBeen.Name = "RegistryEditorHasBeen";
			this.RegistryEditorHasBeen.Size = new Size(60, 26);
			this.RegistryEditorHasBeen.TabIndex = 11;
			this.RegistryEditorHasBeen.Text = "Fix";
			this.RegistryEditorHasBeen.UseVisualStyleBackColor = true;
			this.Button30.Location = new Point(574, 66);
			this.Button30.Name = "Button30";
			this.Button30.Size = new Size(22, 26);
			this.Button30.TabIndex = 24;
			this.Button30.Text = "?";
			this.Button30.UseVisualStyleBackColor = true;
			this.Label59.Location = new Point(19, 460);
			this.Label59.Name = "Label59";
			this.Label59.Size = new Size(429, 21);
			this.Label59.TabIndex = 9;
			this.Label59.Text = "Reset Windows Security Settings to defaults";
			this.Button31.Location = new Point(574, 17);
			this.Button31.Name = "Button31";
			this.Button31.Size = new Size(22, 26);
			this.Button31.TabIndex = 25;
			this.Button31.Text = "?";
			this.Button31.UseVisualStyleBackColor = true;
			this.CommandPromptHasBeen.Location = new Point(508, 66);
			this.CommandPromptHasBeen.Name = "CommandPromptHasBeen";
			this.CommandPromptHasBeen.Size = new Size(60, 26);
			this.CommandPromptHasBeen.TabIndex = 11;
			this.CommandPromptHasBeen.Text = "Fix";
			this.CommandPromptHasBeen.UseVisualStyleBackColor = true;
			this.Label50.Location = new Point(19, 406);
			this.Label50.Name = "Label50";
			this.Label50.Size = new Size(429, 35);
			this.Label50.TabIndex = 9;
			this.Label50.Text = "Action Center and Windows Security Center don't recognise installed AntiVirus or Firewall or still identifies old AV as installed.";
			this.TaskManagerHasBeen.Location = new Point(508, 17);
			this.TaskManagerHasBeen.Name = "TaskManagerHasBeen";
			this.TaskManagerHasBeen.Size = new Size(60, 26);
			this.TaskManagerHasBeen.TabIndex = 11;
			this.TaskManagerHasBeen.Text = "Fix";
			this.TaskManagerHasBeen.UseVisualStyleBackColor = true;
			this.Label30.Location = new Point(19, 357);
			this.Label30.Name = "Label30";
			this.Label30.Size = new Size(429, 35);
			this.Label30.TabIndex = 9;
			this.Label30.Text = "Repair Windows Defender. Reset all Windows Defender settings to default.";
			this.Label27.Location = new Point(20, 311);
			this.Label27.Name = "Label27";
			this.Label27.Size = new Size(429, 35);
			this.Label27.TabIndex = 9;
			this.Label27.Text = "Device Manager isn't working properly and not showing any devices.";
			this.Label26.Location = new Point(19, 260);
			this.Label26.Name = "Label26";
			this.Label26.Size = new Size(429, 35);
			this.Label26.TabIndex = 9;
			this.Label26.Text = "\"System Restore has been disabled by your administrator. Please contact your system administrator.\"";
			this.Label25.Location = new Point(19, 210);
			this.Label25.Name = "Label25";
			this.Label25.Size = new Size(429, 35);
			this.Label25.TabIndex = 9;
			this.Label25.Text = "Reset Windows Search to defaults. This will fix issues related to Windows Search";
			this.Label24.Location = new Point(19, 159);
			this.Label24.Name = "Label24";
			this.Label24.Size = new Size(429, 35);
			this.Label24.TabIndex = 9;
			this.Label24.Text = "Enable MMC Snap-ins. Some viruses disable Snap-ins which prevents Group Policy (gpedit.msc) and similar services to run";
			this.Label23.Location = new Point(19, 115);
			this.Label23.Name = "Label23";
			this.Label23.Size = new Size(429, 30);
			this.Label23.TabIndex = 9;
			this.Label23.Text = "\"Registry Editor has been disabled by your administrator\"";
			this.Label22.Location = new Point(19, 63);
			this.Label22.Name = "Label22";
			this.Label22.Size = new Size(429, 48);
			this.Label22.TabIndex = 9;
			this.Label22.Text = "\"Command Prompt has been disabled by your administrator\" and can't run any cmd or batch file";
			this.Label21.Location = new Point(19, 16);
			this.Label21.Name = "Label21";
			this.Label21.Size = new Size(429, 43);
			this.Label21.TabIndex = 9;
			this.Label21.Text = "\"Task Manager has been disabled by your administrator\" or Task Manager option is disabled";
			this.QuickFixes.BackColor = Color.White;
			this.QuickFixes.Controls.Add(this.Button76);
			this.QuickFixes.Controls.Add(this.ResetSoftwareDistribution);
			this.QuickFixes.Controls.Add(this.Button64);
			this.QuickFixes.Controls.Add(this.ResetWinUpdateQF);
			this.QuickFixes.Controls.Add(this.Button68);
			this.QuickFixes.Controls.Add(this.ResetSettingsQF);
			this.QuickFixes.Controls.Add(this.Button72);
			this.QuickFixes.Controls.Add(this.ResetFirewallQF);
			this.QuickFixes.Controls.Add(this.Button74);
			this.QuickFixes.Controls.Add(this.ResetDefenderQF);
			this.QuickFixes.Controls.Add(this.Button69);
			this.QuickFixes.Controls.Add(this.ResetTCPQF);
			this.QuickFixes.Controls.Add(this.Button71);
			this.QuickFixes.Controls.Add(this.ResetDNSQF);
			this.QuickFixes.Controls.Add(this.Button65);
			this.QuickFixes.Controls.Add(this.ResetStoreCache);
			this.QuickFixes.Controls.Add(this.Button67);
			this.QuickFixes.Controls.Add(this.ResetWinsockQF);
			this.QuickFixes.Controls.Add(this.Button63);
			this.QuickFixes.Controls.Add(this.ResetRBQF);
			this.QuickFixes.Controls.Add(this.Button61);
			this.QuickFixes.Controls.Add(this.ResetWMIRepository);
			this.QuickFixes.Controls.Add(this.Button60);
			this.QuickFixes.Controls.Add(this.ResetDataUsage);
			this.QuickFixes.Controls.Add(this.GroupBox3);
			this.QuickFixes.Controls.Add(this.Button54);
			this.QuickFixes.Controls.Add(this.Button55);
			this.QuickFixes.Controls.Add(this.Button51);
			this.QuickFixes.Controls.Add(this.ResetNotepad);
			this.QuickFixes.Controls.Add(this.ResetCatroot2Folder);
			this.QuickFixes.Controls.Add(this.ResetGroupPolicy);
			this.QuickFixes.Location = new Point(4, 22);
			this.QuickFixes.Name = "QuickFixes";
			this.QuickFixes.Size = new Size(605, 527);
			this.QuickFixes.TabIndex = 11;
			this.QuickFixes.Text = "Quick Fixes";
			this.Button76.Location = new Point(257, 306);
			this.Button76.Name = "Button76";
			this.Button76.Size = new Size(22, 31);
			this.Button76.TabIndex = 53;
			this.Button76.Text = "?";
			this.Button76.UseVisualStyleBackColor = true;
			this.ResetSoftwareDistribution.Location = new Point(10, 306);
			this.ResetSoftwareDistribution.Name = "ResetSoftwareDistribution";
			this.ResetSoftwareDistribution.Size = new Size(241, 31);
			this.ResetSoftwareDistribution.TabIndex = 52;
			this.ResetSoftwareDistribution.Text = "Reset Software Distribution Folder";
			this.ResetSoftwareDistribution.UseVisualStyleBackColor = true;
			this.Button64.Location = new Point(573, 266);
			this.Button64.Name = "Button64";
			this.Button64.Size = new Size(22, 31);
			this.Button64.TabIndex = 51;
			this.Button64.Text = "?";
			this.Button64.UseVisualStyleBackColor = true;
			this.ResetWinUpdateQF.Location = new Point(326, 266);
			this.ResetWinUpdateQF.Name = "ResetWinUpdateQF";
			this.ResetWinUpdateQF.Size = new Size(241, 31);
			this.ResetWinUpdateQF.TabIndex = 50;
			this.ResetWinUpdateQF.Text = "Reset Windows Update History";
			this.ResetWinUpdateQF.UseVisualStyleBackColor = true;
			this.Button68.Location = new Point(256, 266);
			this.Button68.Name = "Button68";
			this.Button68.Size = new Size(22, 31);
			this.Button68.TabIndex = 49;
			this.Button68.Text = "?";
			this.Button68.UseVisualStyleBackColor = true;
			this.ResetSettingsQF.Location = new Point(9, 266);
			this.ResetSettingsQF.Name = "ResetSettingsQF";
			this.ResetSettingsQF.Size = new Size(241, 31);
			this.ResetSettingsQF.TabIndex = 48;
			this.ResetSettingsQF.Text = "Reset Settings App";
			this.ResetSettingsQF.UseVisualStyleBackColor = true;
			this.Button72.Location = new Point(573, 225);
			this.Button72.Name = "Button72";
			this.Button72.Size = new Size(22, 31);
			this.Button72.TabIndex = 47;
			this.Button72.Text = "?";
			this.Button72.UseVisualStyleBackColor = true;
			this.ResetFirewallQF.Location = new Point(326, 225);
			this.ResetFirewallQF.Name = "ResetFirewallQF";
			this.ResetFirewallQF.Size = new Size(241, 31);
			this.ResetFirewallQF.TabIndex = 46;
			this.ResetFirewallQF.Text = "Reset Windows Firewall Settings";
			this.ResetFirewallQF.UseVisualStyleBackColor = true;
			this.Button74.Location = new Point(256, 225);
			this.Button74.Name = "Button74";
			this.Button74.Size = new Size(22, 31);
			this.Button74.TabIndex = 45;
			this.Button74.Text = "?";
			this.Button74.UseVisualStyleBackColor = true;
			this.ResetDefenderQF.Location = new Point(9, 225);
			this.ResetDefenderQF.Name = "ResetDefenderQF";
			this.ResetDefenderQF.Size = new Size(241, 31);
			this.ResetDefenderQF.TabIndex = 44;
			this.ResetDefenderQF.Text = "Reset Windows Defender Settings";
			this.ResetDefenderQF.UseVisualStyleBackColor = true;
			this.Button69.Location = new Point(573, 183);
			this.Button69.Name = "Button69";
			this.Button69.Size = new Size(22, 31);
			this.Button69.TabIndex = 43;
			this.Button69.Text = "?";
			this.Button69.UseVisualStyleBackColor = true;
			this.ResetTCPQF.Location = new Point(326, 183);
			this.ResetTCPQF.Name = "ResetTCPQF";
			this.ResetTCPQF.Size = new Size(241, 31);
			this.ResetTCPQF.TabIndex = 42;
			this.ResetTCPQF.Text = "Reset TCP/IP";
			this.ResetTCPQF.UseVisualStyleBackColor = true;
			this.Button71.Location = new Point(256, 183);
			this.Button71.Name = "Button71";
			this.Button71.Size = new Size(22, 31);
			this.Button71.TabIndex = 41;
			this.Button71.Text = "?";
			this.Button71.UseVisualStyleBackColor = true;
			this.ResetDNSQF.Location = new Point(9, 183);
			this.ResetDNSQF.Name = "ResetDNSQF";
			this.ResetDNSQF.Size = new Size(241, 31);
			this.ResetDNSQF.TabIndex = 40;
			this.ResetDNSQF.Text = "Reset DNS Cache";
			this.ResetDNSQF.UseVisualStyleBackColor = true;
			this.Button65.Location = new Point(573, 142);
			this.Button65.Name = "Button65";
			this.Button65.Size = new Size(22, 31);
			this.Button65.TabIndex = 39;
			this.Button65.Text = "?";
			this.Button65.UseVisualStyleBackColor = true;
			this.ResetStoreCache.Location = new Point(326, 142);
			this.ResetStoreCache.Name = "ResetStoreCache";
			this.ResetStoreCache.Size = new Size(241, 31);
			this.ResetStoreCache.TabIndex = 38;
			this.ResetStoreCache.Text = "Reset Windows Store Cache";
			this.ResetStoreCache.UseVisualStyleBackColor = true;
			this.Button67.Location = new Point(256, 142);
			this.Button67.Name = "Button67";
			this.Button67.Size = new Size(22, 31);
			this.Button67.TabIndex = 37;
			this.Button67.Text = "?";
			this.Button67.UseVisualStyleBackColor = true;
			this.ResetWinsockQF.Location = new Point(9, 142);
			this.ResetWinsockQF.Name = "ResetWinsockQF";
			this.ResetWinsockQF.Size = new Size(241, 31);
			this.ResetWinsockQF.TabIndex = 36;
			this.ResetWinsockQF.Text = "Reset Winsock";
			this.ResetWinsockQF.UseVisualStyleBackColor = true;
			this.Button63.Location = new Point(574, 101);
			this.Button63.Name = "Button63";
			this.Button63.Size = new Size(22, 31);
			this.Button63.TabIndex = 35;
			this.Button63.Text = "?";
			this.Button63.UseVisualStyleBackColor = true;
			this.ResetRBQF.Location = new Point(327, 101);
			this.ResetRBQF.Name = "ResetRBQF";
			this.ResetRBQF.Size = new Size(241, 31);
			this.ResetRBQF.TabIndex = 34;
			this.ResetRBQF.Text = "Reset Recycle Bin";
			this.ResetRBQF.UseVisualStyleBackColor = true;
			this.Button61.Location = new Point(257, 101);
			this.Button61.Name = "Button61";
			this.Button61.Size = new Size(22, 31);
			this.Button61.TabIndex = 33;
			this.Button61.Text = "?";
			this.Button61.UseVisualStyleBackColor = true;
			this.ResetWMIRepository.Location = new Point(10, 101);
			this.ResetWMIRepository.Name = "ResetWMIRepository";
			this.ResetWMIRepository.Size = new Size(241, 31);
			this.ResetWMIRepository.TabIndex = 32;
			this.ResetWMIRepository.Text = "Reset WMI Repository";
			this.ResetWMIRepository.UseVisualStyleBackColor = true;
			this.Button60.Location = new Point(574, 60);
			this.Button60.Name = "Button60";
			this.Button60.Size = new Size(22, 31);
			this.Button60.TabIndex = 31;
			this.Button60.Text = "?";
			this.Button60.UseVisualStyleBackColor = true;
			this.ResetDataUsage.Location = new Point(329, 60);
			this.ResetDataUsage.Name = "ResetDataUsage";
			this.ResetDataUsage.Size = new Size(241, 31);
			this.ResetDataUsage.TabIndex = 30;
			this.ResetDataUsage.Text = "Reset Data Usage";
			this.ResetDataUsage.UseVisualStyleBackColor = true;
			this.GroupBox3.Controls.Add(this.Button59);
			this.GroupBox3.Controls.Add(this.Button62);
			this.GroupBox3.Controls.Add(this.Button58);
			this.GroupBox3.Controls.Add(this.Button57);
			this.GroupBox3.Controls.Add(this.Button50);
			this.GroupBox3.Controls.Add(this.Button56);
			this.GroupBox3.Location = new Point(10, 367);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new Size(586, 146);
			this.GroupBox3.TabIndex = 28;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Manual Fixes";
			this.Button59.Location = new Point(12, 99);
			this.Button59.Name = "Button59";
			this.Button59.Size = new Size(241, 31);
			this.Button59.TabIndex = 29;
			this.Button59.Text = "Reset WinHTTP Proxy Server Settings";
			this.Button59.UseVisualStyleBackColor = true;
			this.Button62.Location = new Point(319, 99);
			this.Button62.Name = "Button62";
			this.Button62.Size = new Size(241, 31);
			this.Button62.TabIndex = 29;
			this.Button62.Text = "Reset and Rebuild Font Cache";
			this.Button62.UseVisualStyleBackColor = true;
			this.Button58.Location = new Point(319, 61);
			this.Button58.Name = "Button58";
			this.Button58.Size = new Size(241, 31);
			this.Button58.TabIndex = 29;
			this.Button58.Text = "Reset Edge Browser Settings";
			this.Button58.UseVisualStyleBackColor = true;
			this.Button57.Location = new Point(12, 61);
			this.Button57.Name = "Button57";
			this.Button57.Size = new Size(241, 31);
			this.Button57.TabIndex = 29;
			this.Button57.Text = "Reset Keyboard Settings";
			this.Button57.UseVisualStyleBackColor = true;
			this.Button50.Location = new Point(12, 24);
			this.Button50.Name = "Button50";
			this.Button50.Size = new Size(241, 31);
			this.Button50.TabIndex = 29;
			this.Button50.Text = "Reset Touchpad Settings";
			this.Button50.UseVisualStyleBackColor = true;
			this.Button56.Location = new Point(319, 24);
			this.Button56.Name = "Button56";
			this.Button56.Size = new Size(241, 31);
			this.Button56.TabIndex = 29;
			this.Button56.Text = "Reset Windows Password Settings";
			this.Button56.UseVisualStyleBackColor = true;
			this.Button54.Location = new Point(574, 16);
			this.Button54.Name = "Button54";
			this.Button54.Size = new Size(22, 31);
			this.Button54.TabIndex = 27;
			this.Button54.Text = "?";
			this.Button54.UseVisualStyleBackColor = true;
			this.Button55.Location = new Point(257, 60);
			this.Button55.Name = "Button55";
			this.Button55.Size = new Size(22, 31);
			this.Button55.TabIndex = 26;
			this.Button55.Text = "?";
			this.Button55.UseVisualStyleBackColor = true;
			this.Button51.Location = new Point(257, 16);
			this.Button51.Name = "Button51";
			this.Button51.Size = new Size(22, 31);
			this.Button51.TabIndex = 26;
			this.Button51.Text = "?";
			this.Button51.UseVisualStyleBackColor = true;
			this.ResetNotepad.Location = new Point(10, 60);
			this.ResetNotepad.Name = "ResetNotepad";
			this.ResetNotepad.Size = new Size(241, 31);
			this.ResetNotepad.TabIndex = 5;
			this.ResetNotepad.Text = "Reset Notepad To Defaults";
			this.ResetNotepad.UseVisualStyleBackColor = true;
			this.ResetCatroot2Folder.Location = new Point(329, 16);
			this.ResetCatroot2Folder.Name = "ResetCatroot2Folder";
			this.ResetCatroot2Folder.Size = new Size(241, 31);
			this.ResetCatroot2Folder.TabIndex = 4;
			this.ResetCatroot2Folder.Text = "Reset catroot2 Folder";
			this.ResetCatroot2Folder.UseVisualStyleBackColor = true;
			this.ResetGroupPolicy.Location = new Point(10, 16);
			this.ResetGroupPolicy.Name = "ResetGroupPolicy";
			this.ResetGroupPolicy.Size = new Size(241, 31);
			this.ResetGroupPolicy.TabIndex = 5;
			this.ResetGroupPolicy.Text = "Reset Group Policy Settings";
			this.ResetGroupPolicy.UseVisualStyleBackColor = true;
			this.Evaluation.BackColor = Color.White;
			this.Evaluation.Controls.Add(this.GroupBox2);
			this.Evaluation.Controls.Add(this.GroupBox1);
			this.Evaluation.Font = new Font("Segoe UI", 11.5f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.Evaluation.Location = new Point(4, 22);
			this.Evaluation.Name = "Evaluation";
			this.Evaluation.Size = new Size(605, 527);
			this.Evaluation.TabIndex = 9;
			this.Evaluation.Text = "Advanced System Information";
			this.GroupBox2.Controls.Add(this.Label72);
			this.GroupBox2.Controls.Add(this.Display_monitortype);
			this.GroupBox2.Controls.Add(this.Label71);
			this.GroupBox2.Controls.Add(this.MaxRefreshRate);
			this.GroupBox2.Controls.Add(this.display_resolution);
			this.GroupBox2.Controls.Add(this.Label68);
			this.GroupBox2.Controls.Add(this.Display_Graphicscard);
			this.GroupBox2.Controls.Add(this.Label70);
			this.GroupBox2.Location = new Point(10, 193);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new Size(580, 137);
			this.GroupBox2.TabIndex = 1;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Display";
			this.Label72.AutoSize = true;
			this.Label72.Location = new Point(336, 87);
			this.Label72.Name = "Label72";
			this.Label72.Size = new Size(130, 15);
			this.Label72.TabIndex = 2;
			this.Label72.Text = "Maximum Refresh Rate";
			this.Display_monitortype.Location = new Point(112, 53);
			this.Display_monitortype.Name = "Display_monitortype";
			this.Display_monitortype.ReadOnly = true;
			this.Display_monitortype.Size = new Size(303, 23);
			this.Display_monitortype.TabIndex = 1;
			this.Display_monitortype.TextAlign = HorizontalAlignment.Center;
			this.Label71.AutoSize = true;
			this.Label71.Location = new Point(18, 59);
			this.Label71.Name = "Label71";
			this.Label71.Size = new Size(77, 15);
			this.Label71.TabIndex = 0;
			this.Label71.Text = "Monitor Type";
			this.MaxRefreshRate.Location = new Point(476, 82);
			this.MaxRefreshRate.Name = "MaxRefreshRate";
			this.MaxRefreshRate.ReadOnly = true;
			this.MaxRefreshRate.Size = new Size(67, 23);
			this.MaxRefreshRate.TabIndex = 1;
			this.MaxRefreshRate.TextAlign = HorizontalAlignment.Center;
			this.display_resolution.Location = new Point(144, 82);
			this.display_resolution.Name = "display_resolution";
			this.display_resolution.ReadOnly = true;
			this.display_resolution.Size = new Size(132, 23);
			this.display_resolution.TabIndex = 1;
			this.display_resolution.TextAlign = HorizontalAlignment.Center;
			this.Label68.AutoSize = true;
			this.Label68.Location = new Point(18, 87);
			this.Label68.Name = "Label68";
			this.Label68.Size = new Size(121, 15);
			this.Label68.TabIndex = 0;
			this.Label68.Text = "Maximum Resolution";
			this.Display_Graphicscard.Location = new Point(112, 24);
			this.Display_Graphicscard.Name = "Display_Graphicscard";
			this.Display_Graphicscard.ReadOnly = true;
			this.Display_Graphicscard.Size = new Size(431, 23);
			this.Display_Graphicscard.TabIndex = 1;
			this.Display_Graphicscard.TextAlign = HorizontalAlignment.Center;
			this.Label70.AutoSize = true;
			this.Label70.Location = new Point(16, 29);
			this.Label70.Name = "Label70";
			this.Label70.Size = new Size(81, 15);
			this.Label70.TabIndex = 0;
			this.Label70.Text = "Graphics Card";
			this.GroupBox1.Controls.Add(this.processor_Maxclockspeed);
			this.GroupBox1.Controls.Add(this.Label63);
			this.GroupBox1.Controls.Add(this.processor_LogicalProcessors);
			this.GroupBox1.Controls.Add(this.Label69);
			this.GroupBox1.Controls.Add(this.processor_Currentclockspeed);
			this.GroupBox1.Controls.Add(this.Label62);
			this.GroupBox1.Controls.Add(this.memory_Speed);
			this.GroupBox1.Controls.Add(this.Label66);
			this.GroupBox1.Controls.Add(this.memory_availableRAM);
			this.GroupBox1.Controls.Add(this.Label67);
			this.GroupBox1.Controls.Add(this.memory_totalram);
			this.GroupBox1.Controls.Add(this.Label65);
			this.GroupBox1.Controls.Add(this.processor_Thread);
			this.GroupBox1.Controls.Add(this.Label64);
			this.GroupBox1.Controls.Add(this.processor_cores);
			this.GroupBox1.Controls.Add(this.Label61);
			this.GroupBox1.Controls.Add(this.processor_name);
			this.GroupBox1.Controls.Add(this.Label49);
			this.GroupBox1.Location = new Point(10, 6);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new Size(580, 167);
			this.GroupBox1.TabIndex = 0;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Processor && Memory";
			this.processor_Maxclockspeed.Location = new Point(496, 53);
			this.processor_Maxclockspeed.Name = "processor_Maxclockspeed";
			this.processor_Maxclockspeed.ReadOnly = true;
			this.processor_Maxclockspeed.Size = new Size(65, 23);
			this.processor_Maxclockspeed.TabIndex = 1;
			this.processor_Maxclockspeed.TextAlign = HorizontalAlignment.Center;
			this.Label63.AutoSize = true;
			this.Label63.Location = new Point(368, 58);
			this.Label63.Name = "Label63";
			this.Label63.Size = new Size(98, 15);
			this.Label63.TabIndex = 0;
			this.Label63.Text = "Max Clock Speed";
			this.processor_LogicalProcessors.Location = new Point(289, 82);
			this.processor_LogicalProcessors.Name = "processor_LogicalProcessors";
			this.processor_LogicalProcessors.ReadOnly = true;
			this.processor_LogicalProcessors.Size = new Size(65, 23);
			this.processor_LogicalProcessors.TabIndex = 1;
			this.processor_LogicalProcessors.TextAlign = HorizontalAlignment.Center;
			this.Label69.AutoSize = true;
			this.Label69.Location = new Point(161, 87);
			this.Label69.Name = "Label69";
			this.Label69.Size = new Size(104, 15);
			this.Label69.TabIndex = 0;
			this.Label69.Text = "Logical Processors";
			this.processor_Currentclockspeed.Location = new Point(289, 53);
			this.processor_Currentclockspeed.Name = "processor_Currentclockspeed";
			this.processor_Currentclockspeed.ReadOnly = true;
			this.processor_Currentclockspeed.Size = new Size(65, 23);
			this.processor_Currentclockspeed.TabIndex = 1;
			this.processor_Currentclockspeed.TextAlign = HorizontalAlignment.Center;
			this.Label62.AutoSize = true;
			this.Label62.Location = new Point(161, 58);
			this.Label62.Name = "Label62";
			this.Label62.Size = new Size(115, 15);
			this.Label62.TabIndex = 0;
			this.Label62.Text = "Current Clock Speed";
			this.memory_Speed.Location = new Point(289, 122);
			this.memory_Speed.Name = "memory_Speed";
			this.memory_Speed.ReadOnly = true;
			this.memory_Speed.Size = new Size(67, 23);
			this.memory_Speed.TabIndex = 1;
			this.memory_Speed.TextAlign = HorizontalAlignment.Center;
			this.Label66.AutoSize = true;
			this.Label66.Location = new Point(237, 127);
			this.Label66.Name = "Label66";
			this.Label66.Size = new Size(39, 15);
			this.Label66.TabIndex = 0;
			this.Label66.Text = "Speed";
			this.memory_availableRAM.Location = new Point(485, 122);
			this.memory_availableRAM.Name = "memory_availableRAM";
			this.memory_availableRAM.ReadOnly = true;
			this.memory_availableRAM.Size = new Size(75, 23);
			this.memory_availableRAM.TabIndex = 1;
			this.memory_availableRAM.TextAlign = HorizontalAlignment.Center;
			this.Label67.AutoSize = true;
			this.Label67.Location = new Point(407, 127);
			this.Label67.Name = "Label67";
			this.Label67.Size = new Size(58, 15);
			this.Label67.TabIndex = 0;
			this.Label67.Text = "Free RAM";
			this.memory_totalram.Location = new Point(94, 122);
			this.memory_totalram.Name = "memory_totalram";
			this.memory_totalram.ReadOnly = true;
			this.memory_totalram.Size = new Size(75, 23);
			this.memory_totalram.TabIndex = 1;
			this.memory_totalram.TextAlign = HorizontalAlignment.Center;
			this.Label65.AutoSize = true;
			this.Label65.Location = new Point(16, 127);
			this.Label65.Name = "Label65";
			this.Label65.Size = new Size(61, 15);
			this.Label65.TabIndex = 0;
			this.Label65.Text = "Total RAM";
			this.processor_Thread.Location = new Point(94, 82);
			this.processor_Thread.Name = "processor_Thread";
			this.processor_Thread.ReadOnly = true;
			this.processor_Thread.Size = new Size(50, 23);
			this.processor_Thread.TabIndex = 1;
			this.processor_Thread.TextAlign = HorizontalAlignment.Center;
			this.Label64.AutoSize = true;
			this.Label64.Location = new Point(16, 87);
			this.Label64.Name = "Label64";
			this.Label64.Size = new Size(48, 15);
			this.Label64.TabIndex = 0;
			this.Label64.Text = "Threads";
			this.processor_cores.Location = new Point(94, 53);
			this.processor_cores.Name = "processor_cores";
			this.processor_cores.ReadOnly = true;
			this.processor_cores.Size = new Size(50, 23);
			this.processor_cores.TabIndex = 1;
			this.processor_cores.TextAlign = HorizontalAlignment.Center;
			this.Label61.AutoSize = true;
			this.Label61.Location = new Point(16, 58);
			this.Label61.Name = "Label61";
			this.Label61.Size = new Size(37, 15);
			this.Label61.TabIndex = 0;
			this.Label61.Text = "Cores";
			this.processor_name.Location = new Point(94, 24);
			this.processor_name.Name = "processor_name";
			this.processor_name.ReadOnly = true;
			this.processor_name.Size = new Size(467, 23);
			this.processor_name.TabIndex = 1;
			this.processor_name.TextAlign = HorizontalAlignment.Center;
			this.Label49.AutoSize = true;
			this.Label49.Location = new Point(16, 29);
			this.Label49.Name = "Label49";
			this.Label49.Size = new Size(39, 15);
			this.Label49.TabIndex = 0;
			this.Label49.Text = "Name";
			this.Troubleshooting.BackColor = Color.White;
			this.Troubleshooting.Controls.Add(this.LinkLabel9);
			this.Troubleshooting.Controls.Add(this.LinkLabel8);
			this.Troubleshooting.Controls.Add(this.LinkLabel7);
			this.Troubleshooting.Controls.Add(this.Label19);
			this.Troubleshooting.Controls.Add(this.LinkLabel5);
			this.Troubleshooting.Controls.Add(this.Label18);
			this.Troubleshooting.Controls.Add(this.Label3);
			this.Troubleshooting.Controls.Add(this.Label2);
			this.Troubleshooting.Controls.Add(this.WinUpdTro);
			this.Troubleshooting.Controls.Add(this.SearchTroubleshoo);
			this.Troubleshooting.Controls.Add(this.WMPDVD);
			this.Troubleshooting.Controls.Add(this.WMPLibrary);
			this.Troubleshooting.Controls.Add(this.WMPSettings);
			this.Troubleshooting.Controls.Add(this.Printer);
			this.Troubleshooting.Controls.Add(this.Power);
			this.Troubleshooting.Controls.Add(this.SystemMaintenence);
			this.Troubleshooting.Controls.Add(this.IESafety);
			this.Troubleshooting.Controls.Add(this.IEPerformance);
			this.Troubleshooting.Controls.Add(this.IncomingConnections);
			this.Troubleshooting.Controls.Add(this.NetworkAdapter);
			this.Troubleshooting.Controls.Add(this.Homegroup);
			this.Troubleshooting.Controls.Add(this.SharedFolders);
			this.Troubleshooting.Controls.Add(this.InternetConnections);
			this.Troubleshooting.Controls.Add(this.HardwareAndDevices);
			this.Troubleshooting.Controls.Add(this.RecordingAudio);
			this.Troubleshooting.Controls.Add(this.PlayingAudio);
			this.Troubleshooting.Location = new Point(4, 22);
			this.Troubleshooting.Name = "Troubleshooting";
			this.Troubleshooting.Size = new Size(605, 527);
			this.Troubleshooting.TabIndex = 7;
			this.Troubleshooting.Text = "Troubleshooters";
			this.LinkLabel9.AutoSize = true;
			this.LinkLabel9.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel9.Location = new Point(431, 472);
			this.LinkLabel9.Name = "LinkLabel9";
			this.LinkLabel9.Size = new Size(160, 17);
			this.LinkLabel9.TabIndex = 3;
			this.LinkLabel9.TabStop = true;
			this.LinkLabel9.Text = "Download Troubleshooter";
			this.LinkLabel8.AutoSize = true;
			this.LinkLabel8.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel8.Location = new Point(431, 445);
			this.LinkLabel8.Name = "LinkLabel8";
			this.LinkLabel8.Size = new Size(160, 17);
			this.LinkLabel8.TabIndex = 3;
			this.LinkLabel8.TabStop = true;
			this.LinkLabel8.Text = "Download Troubleshooter";
			this.LinkLabel7.AutoSize = true;
			this.LinkLabel7.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel7.Location = new Point(431, 418);
			this.LinkLabel7.Name = "LinkLabel7";
			this.LinkLabel7.Size = new Size(160, 17);
			this.LinkLabel7.TabIndex = 3;
			this.LinkLabel7.TabStop = true;
			this.LinkLabel7.Text = "Download Troubleshooter";
			this.Label19.AutoSize = true;
			this.Label19.Location = new Point(18, 472);
			this.Label19.Name = "Label19";
			this.Label19.Size = new Size(373, 17);
			this.Label19.TabIndex = 2;
			this.Label19.Text = "Windows Store Apps Troubleshooter for those who upgraded.";
			this.LinkLabel5.AutoSize = true;
			this.LinkLabel5.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel5.Location = new Point(431, 391);
			this.LinkLabel5.Name = "LinkLabel5";
			this.LinkLabel5.Size = new Size(160, 17);
			this.LinkLabel5.TabIndex = 3;
			this.LinkLabel5.TabStop = true;
			this.LinkLabel5.Text = "Download Troubleshooter";
			this.Label18.AutoSize = true;
			this.Label18.Location = new Point(18, 445);
			this.Label18.Name = "Label18";
			this.Label18.Size = new Size(325, 17);
			this.Label18.TabIndex = 2;
			this.Label18.Text = "Printer Troubleshooter to fix specific printer problems.";
			this.Label3.AutoSize = true;
			this.Label3.Location = new Point(18, 418);
			this.Label3.Name = "Label3";
			this.Label3.Size = new Size(337, 17);
			this.Label3.TabIndex = 2;
			this.Label3.Text = "Settings app doesn't launch or opens Store app instead.";
			this.Label2.AutoSize = true;
			this.Label2.Location = new Point(18, 391);
			this.Label2.Name = "Label2";
			this.Label2.Size = new Size(226, 17);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Mail or Calender app(s) are crashing.";
			this.WinUpdTro.Location = new Point(18, 335);
			this.WinUpdTro.Name = "WinUpdTro";
			this.WinUpdTro.Size = new Size(261, 31);
			this.WinUpdTro.TabIndex = 1;
			this.WinUpdTro.Text = "Windows Update Troubleshooter";
			this.WinUpdTro.UseVisualStyleBackColor = true;
			this.SearchTroubleshoo.Location = new Point(337, 335);
			this.SearchTroubleshoo.Name = "SearchTroubleshoo";
			this.SearchTroubleshoo.Size = new Size(254, 31);
			this.SearchTroubleshoo.TabIndex = 1;
			this.SearchTroubleshoo.Text = "Search && Indexing Troubleshooter";
			this.SearchTroubleshoo.UseVisualStyleBackColor = true;
			this.WMPDVD.Location = new Point(337, 295);
			this.WMPDVD.Name = "WMPDVD";
			this.WMPDVD.Size = new Size(254, 31);
			this.WMPDVD.TabIndex = 0;
			this.WMPDVD.Text = "WMP DVD Troubleshooter";
			this.WMPDVD.UseVisualStyleBackColor = true;
			this.WMPLibrary.Location = new Point(337, 255);
			this.WMPLibrary.Name = "WMPLibrary";
			this.WMPLibrary.Size = new Size(254, 31);
			this.WMPLibrary.TabIndex = 0;
			this.WMPLibrary.Text = "WMP Library Troubleshooter";
			this.WMPLibrary.UseVisualStyleBackColor = true;
			this.WMPSettings.Location = new Point(337, 215);
			this.WMPSettings.Name = "WMPSettings";
			this.WMPSettings.Size = new Size(254, 31);
			this.WMPSettings.TabIndex = 0;
			this.WMPSettings.Text = "WMP Settings Troubleshooter";
			this.WMPSettings.UseVisualStyleBackColor = true;
			this.Printer.Location = new Point(18, 134);
			this.Printer.Name = "Printer";
			this.Printer.Size = new Size(261, 31);
			this.Printer.TabIndex = 0;
			this.Printer.Text = "Printer Troubleshooter";
			this.Printer.UseVisualStyleBackColor = true;
			this.Power.Location = new Point(18, 94);
			this.Power.Name = "Power";
			this.Power.Size = new Size(261, 31);
			this.Power.TabIndex = 0;
			this.Power.Text = "Power Troubleshooter";
			this.Power.UseVisualStyleBackColor = true;
			this.SystemMaintenence.Location = new Point(337, 135);
			this.SystemMaintenence.Name = "SystemMaintenence";
			this.SystemMaintenence.Size = new Size(254, 31);
			this.SystemMaintenence.TabIndex = 0;
			this.SystemMaintenence.Text = "System Maintenence Troubleshooter";
			this.SystemMaintenence.UseVisualStyleBackColor = true;
			this.IESafety.Location = new Point(18, 294);
			this.IESafety.Name = "IESafety";
			this.IESafety.Size = new Size(261, 31);
			this.IESafety.TabIndex = 0;
			this.IESafety.Text = "IE Safety Troubleshooter";
			this.IESafety.UseVisualStyleBackColor = true;
			this.IEPerformance.Location = new Point(18, 254);
			this.IEPerformance.Name = "IEPerformance";
			this.IEPerformance.Size = new Size(261, 31);
			this.IEPerformance.TabIndex = 0;
			this.IEPerformance.Text = "IE Performance Troubleshooter";
			this.IEPerformance.UseVisualStyleBackColor = true;
			this.IncomingConnections.Location = new Point(337, 95);
			this.IncomingConnections.Name = "IncomingConnections";
			this.IncomingConnections.Size = new Size(254, 31);
			this.IncomingConnections.TabIndex = 0;
			this.IncomingConnections.Text = "Incoming Connections Troubleshooter";
			this.IncomingConnections.UseVisualStyleBackColor = true;
			this.NetworkAdapter.Location = new Point(337, 175);
			this.NetworkAdapter.Name = "NetworkAdapter";
			this.NetworkAdapter.Size = new Size(254, 31);
			this.NetworkAdapter.TabIndex = 0;
			this.NetworkAdapter.Text = "Network Adapter Troubleshooter";
			this.NetworkAdapter.UseVisualStyleBackColor = true;
			this.Homegroup.Location = new Point(18, 214);
			this.Homegroup.Name = "Homegroup";
			this.Homegroup.Size = new Size(261, 31);
			this.Homegroup.TabIndex = 0;
			this.Homegroup.Text = "Homegroup Troubleshooter";
			this.Homegroup.UseVisualStyleBackColor = true;
			this.SharedFolders.Location = new Point(18, 174);
			this.SharedFolders.Name = "SharedFolders";
			this.SharedFolders.Size = new Size(261, 31);
			this.SharedFolders.TabIndex = 0;
			this.SharedFolders.Text = "Shared Folders Troubleshooter";
			this.SharedFolders.UseVisualStyleBackColor = true;
			this.InternetConnections.Location = new Point(337, 15);
			this.InternetConnections.Name = "InternetConnections";
			this.InternetConnections.Size = new Size(254, 31);
			this.InternetConnections.TabIndex = 0;
			this.InternetConnections.Text = "Internet Connections Troubleshooter";
			this.InternetConnections.UseVisualStyleBackColor = true;
			this.HardwareAndDevices.Location = new Point(337, 55);
			this.HardwareAndDevices.Name = "HardwareAndDevices";
			this.HardwareAndDevices.Size = new Size(254, 31);
			this.HardwareAndDevices.TabIndex = 0;
			this.HardwareAndDevices.Text = "Hardware And Devices Troubleshooter";
			this.HardwareAndDevices.UseVisualStyleBackColor = true;
			this.RecordingAudio.Location = new Point(18, 54);
			this.RecordingAudio.Name = "RecordingAudio";
			this.RecordingAudio.Size = new Size(261, 31);
			this.RecordingAudio.TabIndex = 0;
			this.RecordingAudio.Text = "Recording Audio Troubleshooter";
			this.RecordingAudio.UseVisualStyleBackColor = true;
			this.PlayingAudio.Location = new Point(18, 14);
			this.PlayingAudio.Name = "PlayingAudio";
			this.PlayingAudio.Size = new Size(261, 31);
			this.PlayingAudio.TabIndex = 0;
			this.PlayingAudio.Text = "Playing Audio Troubleshooter";
			this.PlayingAudio.UseVisualStyleBackColor = true;
			this.Additional.BackColor = Color.White;
			this.Additional.Controls.Add(this.Button32);
			this.Additional.Controls.Add(this.wmp);
			this.Additional.Controls.Add(this.Button33);
			this.Additional.Controls.Add(this.recoveryimagecant);
			this.Additional.Controls.Add(this.Button34);
			this.Additional.Controls.Add(this.officedocsdonotopen);
			this.Additional.Controls.Add(this.Button35);
			this.Additional.Controls.Add(this.WindowsScriptHost);
			this.Additional.Controls.Add(this.Button36);
			this.Additional.Controls.Add(this.Label57);
			this.Additional.Controls.Add(this.Button37);
			this.Additional.Controls.Add(this.balloontips);
			this.Additional.Controls.Add(this.Button38);
			this.Additional.Controls.Add(this.Label54);
			this.Additional.Controls.Add(this.Button39);
			this.Additional.Controls.Add(this.TaskbarJumplist);
			this.Additional.Controls.Add(this.Button40);
			this.Additional.Controls.Add(this.Label53);
			this.Additional.Controls.Add(this.Button41);
			this.Additional.Controls.Add(this.FixCorruptedDesktopIcons);
			this.Additional.Controls.Add(this.Label52);
			this.Additional.Controls.Add(this.AeroNotWorking);
			this.Additional.Controls.Add(this.Label47);
			this.Additional.Controls.Add(this.Label35);
			this.Additional.Controls.Add(this.RestoreStickyNotes);
			this.Additional.Controls.Add(this.Label34);
			this.Additional.Controls.Add(this.EnableHibernate);
			this.Additional.Controls.Add(this.Label33);
			this.Additional.Controls.Add(this.Label32);
			this.Additional.Controls.Add(this.Label31);
			this.Additional.Location = new Point(4, 22);
			this.Additional.Name = "Additional";
			this.Additional.Size = new Size(605, 527);
			this.Additional.TabIndex = 5;
			this.Additional.Text = "Additional";
			this.Button32.Location = new Point(574, 451);
			this.Button32.Name = "Button32";
			this.Button32.Size = new Size(22, 26);
			this.Button32.TabIndex = 16;
			this.Button32.Text = "?";
			this.Button32.UseVisualStyleBackColor = true;
			this.wmp.Location = new Point(508, 452);
			this.wmp.Name = "wmp";
			this.wmp.Size = new Size(60, 26);
			this.wmp.TabIndex = 13;
			this.wmp.Text = "Fix";
			this.wmp.UseVisualStyleBackColor = true;
			this.Button33.Location = new Point(574, 405);
			this.Button33.Name = "Button33";
			this.Button33.Size = new Size(22, 26);
			this.Button33.TabIndex = 17;
			this.Button33.Text = "?";
			this.Button33.UseVisualStyleBackColor = true;
			this.recoveryimagecant.Location = new Point(508, 406);
			this.recoveryimagecant.Name = "recoveryimagecant";
			this.recoveryimagecant.Size = new Size(60, 26);
			this.recoveryimagecant.TabIndex = 13;
			this.recoveryimagecant.Text = "Fix";
			this.recoveryimagecant.UseVisualStyleBackColor = true;
			this.Button34.Location = new Point(574, 358);
			this.Button34.Name = "Button34";
			this.Button34.Size = new Size(22, 26);
			this.Button34.TabIndex = 18;
			this.Button34.Text = "?";
			this.Button34.UseVisualStyleBackColor = true;
			this.officedocsdonotopen.Location = new Point(508, 359);
			this.officedocsdonotopen.Name = "officedocsdonotopen";
			this.officedocsdonotopen.Size = new Size(60, 26);
			this.officedocsdonotopen.TabIndex = 13;
			this.officedocsdonotopen.Text = "Fix";
			this.officedocsdonotopen.UseVisualStyleBackColor = true;
			this.Button35.Location = new Point(574, 311);
			this.Button35.Name = "Button35";
			this.Button35.Size = new Size(22, 26);
			this.Button35.TabIndex = 19;
			this.Button35.Text = "?";
			this.Button35.UseVisualStyleBackColor = true;
			this.WindowsScriptHost.Location = new Point(508, 312);
			this.WindowsScriptHost.Name = "WindowsScriptHost";
			this.WindowsScriptHost.Size = new Size(60, 26);
			this.WindowsScriptHost.TabIndex = 13;
			this.WindowsScriptHost.Text = "Fix";
			this.WindowsScriptHost.UseVisualStyleBackColor = true;
			this.Button36.Location = new Point(574, 263);
			this.Button36.Name = "Button36";
			this.Button36.Size = new Size(22, 26);
			this.Button36.TabIndex = 20;
			this.Button36.Text = "?";
			this.Button36.UseVisualStyleBackColor = true;
			this.Label57.Location = new Point(19, 444);
			this.Label57.Name = "Label57";
			this.Label57.Size = new Size(437, 37);
			this.Label57.TabIndex = 11;
			this.Label57.Text = "Windows Media Player shows following error: \"An internal application error has occured.\"";
			this.Button37.Location = new Point(574, 214);
			this.Button37.Name = "Button37";
			this.Button37.Size = new Size(22, 26);
			this.Button37.TabIndex = 21;
			this.Button37.Text = "?";
			this.Button37.UseVisualStyleBackColor = true;
			this.balloontips.Location = new Point(508, 262);
			this.balloontips.Name = "balloontips";
			this.balloontips.Size = new Size(60, 26);
			this.balloontips.TabIndex = 13;
			this.balloontips.Text = "Fix";
			this.balloontips.UseVisualStyleBackColor = true;
			this.Button38.Location = new Point(574, 162);
			this.Button38.Name = "Button38";
			this.Button38.Size = new Size(22, 26);
			this.Button38.TabIndex = 22;
			this.Button38.Text = "?";
			this.Button38.UseVisualStyleBackColor = true;
			this.Label54.AutoSize = true;
			this.Label54.Location = new Point(19, 406);
			this.Label54.Name = "Label54";
			this.Label54.Size = new Size(381, 17);
			this.Label54.TabIndex = 11;
			this.Label54.Text = "The recovery image cannot be written. Error code – 0x8004230c";
			this.Button39.Location = new Point(574, 113);
			this.Button39.Name = "Button39";
			this.Button39.Size = new Size(22, 26);
			this.Button39.TabIndex = 23;
			this.Button39.Text = "?";
			this.Button39.UseVisualStyleBackColor = true;
			this.TaskbarJumplist.Location = new Point(508, 214);
			this.TaskbarJumplist.Name = "TaskbarJumplist";
			this.TaskbarJumplist.Size = new Size(60, 26);
			this.TaskbarJumplist.TabIndex = 13;
			this.TaskbarJumplist.Text = "Fix";
			this.TaskbarJumplist.UseVisualStyleBackColor = true;
			this.Button40.Location = new Point(574, 64);
			this.Button40.Name = "Button40";
			this.Button40.Size = new Size(22, 26);
			this.Button40.TabIndex = 24;
			this.Button40.Text = "?";
			this.Button40.UseVisualStyleBackColor = true;
			this.Label53.AutoSize = true;
			this.Label53.Location = new Point(19, 361);
			this.Label53.Name = "Label53";
			this.Label53.Size = new Size(375, 17);
			this.Label53.TabIndex = 11;
			this.Label53.Text = "Office Documents do not open after upgrading to Windows 11";
			this.Button41.Location = new Point(574, 17);
			this.Button41.Name = "Button41";
			this.Button41.Size = new Size(22, 26);
			this.Button41.TabIndex = 25;
			this.Button41.Text = "?";
			this.Button41.UseVisualStyleBackColor = true;
			this.FixCorruptedDesktopIcons.Location = new Point(508, 162);
			this.FixCorruptedDesktopIcons.Name = "FixCorruptedDesktopIcons";
			this.FixCorruptedDesktopIcons.Size = new Size(60, 26);
			this.FixCorruptedDesktopIcons.TabIndex = 13;
			this.FixCorruptedDesktopIcons.Text = "Fix";
			this.FixCorruptedDesktopIcons.UseVisualStyleBackColor = true;
			this.Label52.AutoSize = true;
			this.Label52.Location = new Point(19, 314);
			this.Label52.Name = "Label52";
			this.Label52.Size = new Size(333, 17);
			this.Label52.TabIndex = 11;
			this.Label52.Text = "Windows Script Host access is disabled on this machine";
			this.AeroNotWorking.Location = new Point(508, 112);
			this.AeroNotWorking.Name = "AeroNotWorking";
			this.AeroNotWorking.Size = new Size(60, 26);
			this.AeroNotWorking.TabIndex = 13;
			this.AeroNotWorking.Text = "Fix";
			this.AeroNotWorking.UseVisualStyleBackColor = true;
			this.Label47.AutoSize = true;
			this.Label47.Location = new Point(19, 264);
			this.Label47.Name = "Label47";
			this.Label47.Size = new Size(199, 17);
			this.Label47.TabIndex = 11;
			this.Label47.Text = "Notifications have been disabled";
			this.Label35.AutoSize = true;
			this.Label35.Location = new Point(19, 219);
			this.Label35.Name = "Label35";
			this.Label35.Size = new Size(335, 17);
			this.Label35.TabIndex = 11;
			this.Label35.Text = "Taskbar jumplist is missing or doesn't store MRU file list";
			this.RestoreStickyNotes.Location = new Point(508, 65);
			this.RestoreStickyNotes.Name = "RestoreStickyNotes";
			this.RestoreStickyNotes.Size = new Size(60, 26);
			this.RestoreStickyNotes.TabIndex = 13;
			this.RestoreStickyNotes.Text = "Fix";
			this.RestoreStickyNotes.UseVisualStyleBackColor = true;
			this.Label34.AutoSize = true;
			this.Label34.Location = new Point(19, 169);
			this.Label34.Name = "Label34";
			this.Label34.Size = new Size(346, 17);
			this.Label34.TabIndex = 11;
			this.Label34.Text = "Icons are corrupted. Fix and rebuild corrupted icon cache.";
			this.EnableHibernate.Location = new Point(508, 17);
			this.EnableHibernate.Name = "EnableHibernate";
			this.EnableHibernate.Size = new Size(60, 26);
			this.EnableHibernate.TabIndex = 13;
			this.EnableHibernate.Text = "Fix";
			this.EnableHibernate.UseVisualStyleBackColor = true;
			this.Label33.AutoSize = true;
			this.Label33.Location = new Point(19, 119);
			this.Label33.Name = "Label33";
			this.Label33.Size = new Size(299, 17);
			this.Label33.TabIndex = 11;
			this.Label33.Text = "Aero Snap, Aero Peek or Aero Shake isn't working";
			this.Label32.AutoSize = true;
			this.Label32.Location = new Point(19, 70);
			this.Label32.Name = "Label32";
			this.Label32.Size = new Size(307, 17);
			this.Label32.TabIndex = 11;
			this.Label32.Text = "Restore the Sticky Notes delete warning dialog box";
			this.Label31.AutoSize = true;
			this.Label31.Location = new Point(19, 23);
			this.Label31.Name = "Label31";
			this.Label31.Size = new Size(416, 17);
			this.Label31.TabIndex = 11;
			this.Label31.Text = "Enable Hibernate. Hibernate option is missing from Shutdown options";
			this.About.BackColor = Color.White;
			this.About.Controls.Add(this.Changelog);
			this.About.Controls.Add(this.VersionAbout);
			this.About.Controls.Add(this.RichTextBox1);
			this.About.Controls.Add(this.Label28);
			this.About.Controls.Add(this.LinkLabel6);
			this.About.Controls.Add(this.Label29);
			this.About.Controls.Add(this.LinkLabel2);
			this.About.Controls.Add(this.Label42);
			this.About.Controls.Add(this.LinkLabel1);
			this.About.Location = new Point(4, 22);
			this.About.Name = "About";
			this.About.Size = new Size(605, 527);
			this.About.TabIndex = 6;
			this.About.Text = "About";
			this.Changelog.AutoSize = true;
			this.Changelog.Font = new Font("Segoe UI Semilight", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Changelog.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Changelog.Location = new Point(508, 8);
			this.Changelog.Name = "Changelog";
			this.Changelog.Size = new Size(85, 21);
			this.Changelog.TabIndex = 4;
			this.Changelog.TabStop = true;
			this.Changelog.Text = "Changelog";
			this.VersionAbout.AutoSize = true;
			this.VersionAbout.Font = new Font("Segoe UI Semilight", 12.11215f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.VersionAbout.Location = new Point(13, 8);
			this.VersionAbout.Name = "VersionAbout";
			this.VersionAbout.Size = new Size(88, 23);
			this.VersionAbout.TabIndex = 1;
			this.VersionAbout.Text = "FixWin 11.0";
			this.RichTextBox1.BackColor = SystemColors.ButtonHighlight;
			this.RichTextBox1.Cursor = Cursors.Default;
			this.RichTextBox1.Location = new Point(13, 39);
			this.RichTextBox1.Name = "RichTextBox1";
			this.RichTextBox1.ReadOnly = true;
			this.RichTextBox1.Size = new Size(580, 372);
			this.RichTextBox1.TabIndex = 0;
			this.RichTextBox1.Text = componentResourceManager.GetString("RichTextBox1.Text");
			this.Label28.AutoSize = true;
			this.Label28.Location = new Point(14, 426);
			this.Label28.Name = "Label28";
			this.Label28.Size = new Size(64, 17);
			this.Label28.TabIndex = 9;
			this.Label28.Text = "Publisher:";
			this.LinkLabel6.AutoSize = true;
			this.LinkLabel6.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel6.Location = new Point(150, 483);
			this.LinkLabel6.Name = "LinkLabel6";
			this.LinkLabel6.Size = new Size(76, 17);
			this.LinkLabel6.TabIndex = 13;
			this.LinkLabel6.TabStop = true;
			this.LinkLabel6.Text = "Home Page";
			this.Label29.AutoSize = true;
			this.Label29.Location = new Point(14, 455);
			this.Label29.Name = "Label29";
			this.Label29.Size = new Size(71, 17);
			this.Label29.TabIndex = 8;
			this.Label29.Text = "Developer:";
			this.LinkLabel2.AutoSize = true;
			this.LinkLabel2.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel2.Location = new Point(83, 455);
			this.LinkLabel2.Name = "LinkLabel2";
			this.LinkLabel2.Size = new Size(76, 17);
			this.LinkLabel2.TabIndex = 14;
			this.LinkLabel2.TabStop = true;
			this.LinkLabel2.Text = "Paras Sidhu";
			this.Label42.AutoSize = true;
			this.Label42.Location = new Point(13, 483);
			this.Label42.Name = "Label42";
			this.Label42.Size = new Size(131, 17);
			this.Label42.TabIndex = 7;
			this.Label42.Text = "Feedback && Support:";
			this.LinkLabel1.AutoSize = true;
			this.LinkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
			this.LinkLabel1.Location = new Point(83, 426);
			this.LinkLabel1.Name = "LinkLabel1";
			this.LinkLabel1.Size = new Size(116, 17);
			this.LinkLabel1.TabIndex = 15;
			this.LinkLabel1.TabStop = true;
			this.LinkLabel1.Text = "The Windows Club";
			this.ListBox1.FormattingEnabled = true;
			this.ListBox1.ItemHeight = 17;
			this.ListBox1.Location = new Point(654, 560);
			this.ListBox1.Name = "ListBox1";
			this.ListBox1.Size = new Size(120, 4);
			this.ListBox1.TabIndex = 3;
			this.ListBox1.Visible = false;
			this.NotifyIcon1.Icon = (Icon)componentResourceManager.GetObject("NotifyIcon1.Icon");
			this.NotifyIcon1.Text = "FixWin 10";
			this.NotifyIcon1.Visible = true;
			this.Timer1.Interval = 1500;
			base.AutoScaleMode = AutoScaleMode.None;
			this.BackColor = Color.White;
			base.ClientSize = new Size(819, 570);
			base.Controls.Add(this.ListBox1);
			base.Controls.Add(this.TabControlFixWin);
			base.Controls.Add(this.MyPanel);
			this.Font = new Font("Segoe UI", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			base.Name = "Main_Form";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "FixWin 11.1";
			this.MyPanel.ResumeLayout(false);
			this.MyPanel.PerformLayout();
			((ISupportInitialize)this.TWC_Logo).EndInit();
			this.TabControlFixWin.ResumeLayout(false);
			this.Welcome.ResumeLayout(false);
			this.Welcome.PerformLayout();
			((ISupportInitialize)this.PictureBox1).EndInit();
			this.FileExplorer.ResumeLayout(false);
			this.FileExplorer.PerformLayout();
			this.InternetConnectivity.ResumeLayout(false);
			this.InternetConnectivity.PerformLayout();
			this.Windows11.ResumeLayout(false);
			this.Windows11.PerformLayout();
			this.WindowsStore.ResumeLayout(false);
			this.SystemTools.ResumeLayout(false);
			this.QuickFixes.ResumeLayout(false);
			this.GroupBox3.ResumeLayout(false);
			this.Evaluation.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox2.PerformLayout();
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.Troubleshooting.ResumeLayout(false);
			this.Troubleshooting.PerformLayout();
			this.Additional.ResumeLayout(false);
			this.Additional.PerformLayout();
			this.About.ResumeLayout(false);
			this.About.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000145D1 File Offset: 0x000127D1
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000145DC File Offset: 0x000127DC
		internal virtual Panel MyPanel
		{
			[CompilerGenerated]
			get
			{
				return this._MyPanel;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				PaintEventHandler value2 = new PaintEventHandler(this.MyPanel_Paint);
				Panel myPanel = this._MyPanel;
				if (myPanel != null)
				{
					myPanel.Paint -= value2;
				}
				this._MyPanel = value;
				myPanel = this._MyPanel;
				if (myPanel != null)
				{
					myPanel.Paint += value2;
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0001461F File Offset: 0x0001281F
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00014628 File Offset: 0x00012828
		internal virtual LinkLabel Side_About
		{
			[CompilerGenerated]
			get
			{
				return this._Side_About;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_About_LinkClicked);
				LinkLabel side_About = this._Side_About;
				if (side_About != null)
				{
					side_About.LinkClicked -= value2;
				}
				this._Side_About = value;
				side_About = this._Side_About;
				if (side_About != null)
				{
					side_About.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0001466B File Offset: 0x0001286B
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00014674 File Offset: 0x00012874
		internal virtual LinkLabel Side_Additional
		{
			[CompilerGenerated]
			get
			{
				return this._Side_Additional;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_Additional_LinkClicked);
				LinkLabel side_Additional = this._Side_Additional;
				if (side_Additional != null)
				{
					side_Additional.LinkClicked -= value2;
				}
				this._Side_Additional = value;
				side_Additional = this._Side_Additional;
				if (side_Additional != null)
				{
					side_Additional.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000146B7 File Offset: 0x000128B7
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000146C0 File Offset: 0x000128C0
		internal virtual LinkLabel Side_SystemTools
		{
			[CompilerGenerated]
			get
			{
				return this._Side_SystemTools;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_SystemTools_LinkClicked);
				LinkLabel side_SystemTools = this._Side_SystemTools;
				if (side_SystemTools != null)
				{
					side_SystemTools.LinkClicked -= value2;
				}
				this._Side_SystemTools = value;
				side_SystemTools = this._Side_SystemTools;
				if (side_SystemTools != null)
				{
					side_SystemTools.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00014703 File Offset: 0x00012903
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0001470C File Offset: 0x0001290C
		internal virtual LinkLabel Side_ModernUI
		{
			[CompilerGenerated]
			get
			{
				return this._Side_ModernUI;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_ModernUI_LinkClicked);
				LinkLabel side_ModernUI = this._Side_ModernUI;
				if (side_ModernUI != null)
				{
					side_ModernUI.LinkClicked -= value2;
				}
				this._Side_ModernUI = value;
				side_ModernUI = this._Side_ModernUI;
				if (side_ModernUI != null)
				{
					side_ModernUI.LinkClicked += value2;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0001474F File Offset: 0x0001294F
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00014758 File Offset: 0x00012958
		internal virtual LinkLabel Side_Internet
		{
			[CompilerGenerated]
			get
			{
				return this._Side_Internet;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_Internet_LinkClicked);
				LinkLabel side_Internet = this._Side_Internet;
				if (side_Internet != null)
				{
					side_Internet.LinkClicked -= value2;
				}
				this._Side_Internet = value;
				side_Internet = this._Side_Internet;
				if (side_Internet != null)
				{
					side_Internet.LinkClicked += value2;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0001479B File Offset: 0x0001299B
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000147A4 File Offset: 0x000129A4
		internal virtual LinkLabel Side_FileExplorer
		{
			[CompilerGenerated]
			get
			{
				return this._Side_FileExplorer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_FileExplorer_LinkClicked);
				LinkLabel side_FileExplorer = this._Side_FileExplorer;
				if (side_FileExplorer != null)
				{
					side_FileExplorer.LinkClicked -= value2;
				}
				this._Side_FileExplorer = value;
				side_FileExplorer = this._Side_FileExplorer;
				if (side_FileExplorer != null)
				{
					side_FileExplorer.LinkClicked += value2;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000147E7 File Offset: 0x000129E7
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000147F0 File Offset: 0x000129F0
		internal virtual LinkLabel Side_Welcome
		{
			[CompilerGenerated]
			get
			{
				return this._Side_Welcome;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_Welcome_LinkClicked);
				LinkLabel side_Welcome = this._Side_Welcome;
				if (side_Welcome != null)
				{
					side_Welcome.LinkClicked -= value2;
				}
				this._Side_Welcome = value;
				side_Welcome = this._Side_Welcome;
				if (side_Welcome != null)
				{
					side_Welcome.LinkClicked += value2;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00014833 File Offset: 0x00012A33
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0001483C File Offset: 0x00012A3C
		internal virtual LinkLabel TWC
		{
			[CompilerGenerated]
			get
			{
				return this._TWC;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TWC_Logo_Click);
				LinkLabel twc = this._TWC;
				if (twc != null)
				{
					twc.Click -= value2;
				}
				this._TWC = value;
				twc = this._TWC;
				if (twc != null)
				{
					twc.Click += value2;
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0001487F File Offset: 0x00012A7F
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00014887 File Offset: 0x00012A87
		internal virtual TabControl TabControlFixWin { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00014890 File Offset: 0x00012A90
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00014898 File Offset: 0x00012A98
		internal virtual TabPage Welcome { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000148A1 File Offset: 0x00012AA1
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000148A9 File Offset: 0x00012AA9
		internal virtual NotifyIcon NotifyIcon1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000148B2 File Offset: 0x00012AB2
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000148BA File Offset: 0x00012ABA
		internal virtual TabPage FileExplorer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000148C3 File Offset: 0x00012AC3
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000148CB File Offset: 0x00012ACB
		internal virtual TabPage InternetConnectivity { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000148D4 File Offset: 0x00012AD4
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000148DC File Offset: 0x00012ADC
		internal virtual TabPage SystemTools { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000148E5 File Offset: 0x00012AE5
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000148ED File Offset: 0x00012AED
		internal virtual TabPage Additional { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000148F6 File Offset: 0x00012AF6
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000148FE File Offset: 0x00012AFE
		internal virtual TabPage About { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00014907 File Offset: 0x00012B07
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0001490F File Offset: 0x00012B0F
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00014918 File Offset: 0x00012B18
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00014920 File Offset: 0x00012B20
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00014929 File Offset: 0x00012B29
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00014931 File Offset: 0x00012B31
		internal virtual Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0001493A File Offset: 0x00012B3A
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00014942 File Offset: 0x00012B42
		internal virtual Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0001494B File Offset: 0x00012B4B
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00014953 File Offset: 0x00012B53
		internal virtual Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0001495C File Offset: 0x00012B5C
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00014964 File Offset: 0x00012B64
		internal virtual Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0001496D File Offset: 0x00012B6D
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00014975 File Offset: 0x00012B75
		internal virtual Label Label10 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0001497E File Offset: 0x00012B7E
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00014986 File Offset: 0x00012B86
		internal virtual Label Label11 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0001498F File Offset: 0x00012B8F
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00014997 File Offset: 0x00012B97
		internal virtual Label Label12 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000149A0 File Offset: 0x00012BA0
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000149A8 File Offset: 0x00012BA8
		internal virtual Label Label13 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000149B1 File Offset: 0x00012BB1
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000149B9 File Offset: 0x00012BB9
		internal virtual Label Label14 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000149C2 File Offset: 0x00012BC2
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000149CA File Offset: 0x00012BCA
		internal virtual RichTextBox RichTextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000149D3 File Offset: 0x00012BD3
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000149DB File Offset: 0x00012BDB
		internal virtual Label VersionAbout { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000149E4 File Offset: 0x00012BE4
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000149EC File Offset: 0x00012BEC
		internal virtual Label Label28 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000149F5 File Offset: 0x00012BF5
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00014A00 File Offset: 0x00012C00
		internal virtual LinkLabel LinkLabel6
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel6;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel6_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel6;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel6 = value;
				linkLabel = this._LinkLabel6;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00014A43 File Offset: 0x00012C43
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00014A4B File Offset: 0x00012C4B
		internal virtual Label Label29 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00014A54 File Offset: 0x00012C54
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00014A5C File Offset: 0x00012C5C
		internal virtual LinkLabel LinkLabel2
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel2;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel2_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel2;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel2 = value;
				linkLabel = this._LinkLabel2;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00014A9F File Offset: 0x00012C9F
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00014AA7 File Offset: 0x00012CA7
		internal virtual Label Label42 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00014AB0 File Offset: 0x00012CB0
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00014AB8 File Offset: 0x00012CB8
		internal virtual LinkLabel LinkLabel1
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel1;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel1 = value;
				linkLabel = this._LinkLabel1;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00014AFB File Offset: 0x00012CFB
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00014B03 File Offset: 0x00012D03
		internal virtual Label Label21 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00014B0C File Offset: 0x00012D0C
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00014B14 File Offset: 0x00012D14
		internal virtual Label Label22 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00014B1D File Offset: 0x00012D1D
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00014B25 File Offset: 0x00012D25
		internal virtual Label Label23 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00014B2E File Offset: 0x00012D2E
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00014B36 File Offset: 0x00012D36
		internal virtual Label Label24 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00014B3F File Offset: 0x00012D3F
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00014B47 File Offset: 0x00012D47
		internal virtual Label Label25 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00014B50 File Offset: 0x00012D50
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00014B58 File Offset: 0x00012D58
		internal virtual Label Label26 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00014B61 File Offset: 0x00012D61
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00014B69 File Offset: 0x00012D69
		internal virtual Label Label27 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00014B72 File Offset: 0x00012D72
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00014B7A File Offset: 0x00012D7A
		internal virtual Label Label30 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00014B83 File Offset: 0x00012D83
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00014B8B File Offset: 0x00012D8B
		internal virtual Label Label31 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00014B94 File Offset: 0x00012D94
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00014B9C File Offset: 0x00012D9C
		internal virtual Label Label32 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00014BA5 File Offset: 0x00012DA5
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00014BAD File Offset: 0x00012DAD
		internal virtual Label Label33 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00014BB6 File Offset: 0x00012DB6
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00014BC0 File Offset: 0x00012DC0
		internal virtual Button ThumbnailsNotShowing
		{
			[CompilerGenerated]
			get
			{
				return this._ThumbnailsNotShowing;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ThumbnailsNotShowing_Click);
				Button thumbnailsNotShowing = this._ThumbnailsNotShowing;
				if (thumbnailsNotShowing != null)
				{
					thumbnailsNotShowing.Click -= value2;
				}
				this._ThumbnailsNotShowing = value;
				thumbnailsNotShowing = this._ThumbnailsNotShowing;
				if (thumbnailsNotShowing != null)
				{
					thumbnailsNotShowing.Click += value2;
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00014C03 File Offset: 0x00012E03
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00014C0C File Offset: 0x00012E0C
		internal virtual Button ExplorerDoesNTStart
		{
			[CompilerGenerated]
			get
			{
				return this._ExplorerDoesNTStart;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ExplorerDoesNTStart_Click);
				Button explorerDoesNTStart = this._ExplorerDoesNTStart;
				if (explorerDoesNTStart != null)
				{
					explorerDoesNTStart.Click -= value2;
				}
				this._ExplorerDoesNTStart = value;
				explorerDoesNTStart = this._ExplorerDoesNTStart;
				if (explorerDoesNTStart != null)
				{
					explorerDoesNTStart.Click += value2;
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00014C4F File Offset: 0x00012E4F
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00014C58 File Offset: 0x00012E58
		internal virtual Button FixRecycleBinIcon
		{
			[CompilerGenerated]
			get
			{
				return this._FixRecycleBinIcon;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.FixRecycleBinIcon_Click);
				Button fixRecycleBinIcon = this._FixRecycleBinIcon;
				if (fixRecycleBinIcon != null)
				{
					fixRecycleBinIcon.Click -= value2;
				}
				this._FixRecycleBinIcon = value;
				fixRecycleBinIcon = this._FixRecycleBinIcon;
				if (fixRecycleBinIcon != null)
				{
					fixRecycleBinIcon.Click += value2;
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00014C9B File Offset: 0x00012E9B
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00014CA4 File Offset: 0x00012EA4
		internal virtual Button FixFolderOptions
		{
			[CompilerGenerated]
			get
			{
				return this._FixFolderOptions;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.FixFolderOptions_Click);
				Button fixFolderOptions = this._FixFolderOptions;
				if (fixFolderOptions != null)
				{
					fixFolderOptions.Click -= value2;
				}
				this._FixFolderOptions = value;
				fixFolderOptions = this._FixFolderOptions;
				if (fixFolderOptions != null)
				{
					fixFolderOptions.Click += value2;
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00014CE7 File Offset: 0x00012EE7
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00014CF0 File Offset: 0x00012EF0
		internal virtual Button WerMgrorWerFault
		{
			[CompilerGenerated]
			get
			{
				return this._WerMgrorWerFault;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetFolderViewSettings_Click);
				Button werMgrorWerFault = this._WerMgrorWerFault;
				if (werMgrorWerFault != null)
				{
					werMgrorWerFault.Click -= value2;
				}
				this._WerMgrorWerFault = value;
				werMgrorWerFault = this._WerMgrorWerFault;
				if (werMgrorWerFault != null)
				{
					werMgrorWerFault.Click += value2;
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00014D33 File Offset: 0x00012F33
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00014D3C File Offset: 0x00012F3C
		internal virtual Button RecycleBinIconMissing
		{
			[CompilerGenerated]
			get
			{
				return this._RecycleBinIconMissing;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RecycleBinIconMissing_Click);
				Button recycleBinIconMissing = this._RecycleBinIconMissing;
				if (recycleBinIconMissing != null)
				{
					recycleBinIconMissing.Click -= value2;
				}
				this._RecycleBinIconMissing = value;
				recycleBinIconMissing = this._RecycleBinIconMissing;
				if (recycleBinIconMissing != null)
				{
					recycleBinIconMissing.Click += value2;
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00014D7F File Offset: 0x00012F7F
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00014D88 File Offset: 0x00012F88
		internal virtual Button ResetWindowsFirewall
		{
			[CompilerGenerated]
			get
			{
				return this._ResetWindowsFirewall;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetWindowsFirewall_Click);
				Button resetWindowsFirewall = this._ResetWindowsFirewall;
				if (resetWindowsFirewall != null)
				{
					resetWindowsFirewall.Click -= value2;
				}
				this._ResetWindowsFirewall = value;
				resetWindowsFirewall = this._ResetWindowsFirewall;
				if (resetWindowsFirewall != null)
				{
					resetWindowsFirewall.Click += value2;
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00014DCB File Offset: 0x00012FCB
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00014DD4 File Offset: 0x00012FD4
		internal virtual Button ClearWindowsUpdateHistory
		{
			[CompilerGenerated]
			get
			{
				return this._ClearWindowsUpdateHistory;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ClearWindowsUpdateHistory_Click);
				Button clearWindowsUpdateHistory = this._ClearWindowsUpdateHistory;
				if (clearWindowsUpdateHistory != null)
				{
					clearWindowsUpdateHistory.Click -= value2;
				}
				this._ClearWindowsUpdateHistory = value;
				clearWindowsUpdateHistory = this._ClearWindowsUpdateHistory;
				if (clearWindowsUpdateHistory != null)
				{
					clearWindowsUpdateHistory.Click += value2;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00014E17 File Offset: 0x00013017
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00014E20 File Offset: 0x00013020
		internal virtual Button FixDNSResolverCache
		{
			[CompilerGenerated]
			get
			{
				return this._FixDNSResolverCache;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.FixDNSResolverCache_Click);
				Button fixDNSResolverCache = this._FixDNSResolverCache;
				if (fixDNSResolverCache != null)
				{
					fixDNSResolverCache.Click -= value2;
				}
				this._FixDNSResolverCache = value;
				fixDNSResolverCache = this._FixDNSResolverCache;
				if (fixDNSResolverCache != null)
				{
					fixDNSResolverCache.Click += value2;
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00014E63 File Offset: 0x00013063
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00014E6C File Offset: 0x0001306C
		internal virtual Button ResetInternetProtocol
		{
			[CompilerGenerated]
			get
			{
				return this._ResetInternetProtocol;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetInternetProtocol_Click);
				Button resetInternetProtocol = this._ResetInternetProtocol;
				if (resetInternetProtocol != null)
				{
					resetInternetProtocol.Click -= value2;
				}
				this._ResetInternetProtocol = value;
				resetInternetProtocol = this._ResetInternetProtocol;
				if (resetInternetProtocol != null)
				{
					resetInternetProtocol.Click += value2;
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00014EAF File Offset: 0x000130AF
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00014EB8 File Offset: 0x000130B8
		internal virtual Button RightClickMenuIEDisabled
		{
			[CompilerGenerated]
			get
			{
				return this._RightClickMenuIEDisabled;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RightClickMenuIEDisabled_Click);
				Button rightClickMenuIEDisabled = this._RightClickMenuIEDisabled;
				if (rightClickMenuIEDisabled != null)
				{
					rightClickMenuIEDisabled.Click -= value2;
				}
				this._RightClickMenuIEDisabled = value;
				rightClickMenuIEDisabled = this._RightClickMenuIEDisabled;
				if (rightClickMenuIEDisabled != null)
				{
					rightClickMenuIEDisabled.Click += value2;
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00014EFB File Offset: 0x000130FB
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00014F04 File Offset: 0x00013104
		internal virtual Button RepairWindowsDefender
		{
			[CompilerGenerated]
			get
			{
				return this._RepairWindowsDefender;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RepairWindowsDefender_Click);
				Button repairWindowsDefender = this._RepairWindowsDefender;
				if (repairWindowsDefender != null)
				{
					repairWindowsDefender.Click -= value2;
				}
				this._RepairWindowsDefender = value;
				repairWindowsDefender = this._RepairWindowsDefender;
				if (repairWindowsDefender != null)
				{
					repairWindowsDefender.Click += value2;
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00014F47 File Offset: 0x00013147
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00014F50 File Offset: 0x00013150
		internal virtual Button DeviceManagerIsNot
		{
			[CompilerGenerated]
			get
			{
				return this._DeviceManagerIsNot;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.DeviceManagerIsNot_Click);
				Button deviceManagerIsNot = this._DeviceManagerIsNot;
				if (deviceManagerIsNot != null)
				{
					deviceManagerIsNot.Click -= value2;
				}
				this._DeviceManagerIsNot = value;
				deviceManagerIsNot = this._DeviceManagerIsNot;
				if (deviceManagerIsNot != null)
				{
					deviceManagerIsNot.Click += value2;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00014F93 File Offset: 0x00013193
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00014F9C File Offset: 0x0001319C
		internal virtual Button SystemRestoreHasBeen
		{
			[CompilerGenerated]
			get
			{
				return this._SystemRestoreHasBeen;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SystemRestoreHasBeen_Click);
				Button systemRestoreHasBeen = this._SystemRestoreHasBeen;
				if (systemRestoreHasBeen != null)
				{
					systemRestoreHasBeen.Click -= value2;
				}
				this._SystemRestoreHasBeen = value;
				systemRestoreHasBeen = this._SystemRestoreHasBeen;
				if (systemRestoreHasBeen != null)
				{
					systemRestoreHasBeen.Click += value2;
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00014FDF File Offset: 0x000131DF
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00014FE8 File Offset: 0x000131E8
		internal virtual Button ResetWindowsSearch
		{
			[CompilerGenerated]
			get
			{
				return this._ResetWindowsSearch;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetWindowsSearch_Click);
				Button resetWindowsSearch = this._ResetWindowsSearch;
				if (resetWindowsSearch != null)
				{
					resetWindowsSearch.Click -= value2;
				}
				this._ResetWindowsSearch = value;
				resetWindowsSearch = this._ResetWindowsSearch;
				if (resetWindowsSearch != null)
				{
					resetWindowsSearch.Click += value2;
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0001502B File Offset: 0x0001322B
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00015034 File Offset: 0x00013234
		internal virtual Button EnableMMCSnap
		{
			[CompilerGenerated]
			get
			{
				return this._EnableMMCSnap;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.EnableMMCSnap_Click);
				Button enableMMCSnap = this._EnableMMCSnap;
				if (enableMMCSnap != null)
				{
					enableMMCSnap.Click -= value2;
				}
				this._EnableMMCSnap = value;
				enableMMCSnap = this._EnableMMCSnap;
				if (enableMMCSnap != null)
				{
					enableMMCSnap.Click += value2;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00015077 File Offset: 0x00013277
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00015080 File Offset: 0x00013280
		internal virtual Button RegistryEditorHasBeen
		{
			[CompilerGenerated]
			get
			{
				return this._RegistryEditorHasBeen;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RegistryEditorHasBeen_Click);
				Button registryEditorHasBeen = this._RegistryEditorHasBeen;
				if (registryEditorHasBeen != null)
				{
					registryEditorHasBeen.Click -= value2;
				}
				this._RegistryEditorHasBeen = value;
				registryEditorHasBeen = this._RegistryEditorHasBeen;
				if (registryEditorHasBeen != null)
				{
					registryEditorHasBeen.Click += value2;
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000150C3 File Offset: 0x000132C3
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000150CC File Offset: 0x000132CC
		internal virtual Button CommandPromptHasBeen
		{
			[CompilerGenerated]
			get
			{
				return this._CommandPromptHasBeen;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CommandPromptHasBeen_Click);
				Button commandPromptHasBeen = this._CommandPromptHasBeen;
				if (commandPromptHasBeen != null)
				{
					commandPromptHasBeen.Click -= value2;
				}
				this._CommandPromptHasBeen = value;
				commandPromptHasBeen = this._CommandPromptHasBeen;
				if (commandPromptHasBeen != null)
				{
					commandPromptHasBeen.Click += value2;
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0001510F File Offset: 0x0001330F
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00015118 File Offset: 0x00013318
		internal virtual Button TaskManagerHasBeen
		{
			[CompilerGenerated]
			get
			{
				return this._TaskManagerHasBeen;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TaskManagerHasBeen_Click);
				Button taskManagerHasBeen = this._TaskManagerHasBeen;
				if (taskManagerHasBeen != null)
				{
					taskManagerHasBeen.Click -= value2;
				}
				this._TaskManagerHasBeen = value;
				taskManagerHasBeen = this._TaskManagerHasBeen;
				if (taskManagerHasBeen != null)
				{
					taskManagerHasBeen.Click += value2;
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0001515B File Offset: 0x0001335B
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00015164 File Offset: 0x00013364
		internal virtual Button AeroNotWorking
		{
			[CompilerGenerated]
			get
			{
				return this._AeroNotWorking;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.AeroNotWorking_Click);
				Button aeroNotWorking = this._AeroNotWorking;
				if (aeroNotWorking != null)
				{
					aeroNotWorking.Click -= value2;
				}
				this._AeroNotWorking = value;
				aeroNotWorking = this._AeroNotWorking;
				if (aeroNotWorking != null)
				{
					aeroNotWorking.Click += value2;
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000151A7 File Offset: 0x000133A7
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000151B0 File Offset: 0x000133B0
		internal virtual Button RestoreStickyNotes
		{
			[CompilerGenerated]
			get
			{
				return this._RestoreStickyNotes;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RestoreStickyNotes_Click);
				Button restoreStickyNotes = this._RestoreStickyNotes;
				if (restoreStickyNotes != null)
				{
					restoreStickyNotes.Click -= value2;
				}
				this._RestoreStickyNotes = value;
				restoreStickyNotes = this._RestoreStickyNotes;
				if (restoreStickyNotes != null)
				{
					restoreStickyNotes.Click += value2;
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000151F3 File Offset: 0x000133F3
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000151FC File Offset: 0x000133FC
		internal virtual Button EnableHibernate
		{
			[CompilerGenerated]
			get
			{
				return this._EnableHibernate;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.EnableHibernate_Click);
				Button enableHibernate = this._EnableHibernate;
				if (enableHibernate != null)
				{
					enableHibernate.Click -= value2;
				}
				this._EnableHibernate = value;
				enableHibernate = this._EnableHibernate;
				if (enableHibernate != null)
				{
					enableHibernate.Click += value2;
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0001523F File Offset: 0x0001343F
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00015248 File Offset: 0x00013448
		internal virtual Button FixCorruptedDesktopIcons
		{
			[CompilerGenerated]
			get
			{
				return this._FixCorruptedDesktopIcons;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.FixCorruptedDesktopIcons_Click);
				Button fixCorruptedDesktopIcons = this._FixCorruptedDesktopIcons;
				if (fixCorruptedDesktopIcons != null)
				{
					fixCorruptedDesktopIcons.Click -= value2;
				}
				this._FixCorruptedDesktopIcons = value;
				fixCorruptedDesktopIcons = this._FixCorruptedDesktopIcons;
				if (fixCorruptedDesktopIcons != null)
				{
					fixCorruptedDesktopIcons.Click += value2;
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0001528B File Offset: 0x0001348B
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00015293 File Offset: 0x00013493
		internal virtual Label Label34 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0001529C File Offset: 0x0001349C
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000152A4 File Offset: 0x000134A4
		internal virtual Timer Timer1
		{
			[CompilerGenerated]
			get
			{
				return this._Timer1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer1_Tick);
				Timer timer = this._Timer1;
				if (timer != null)
				{
					timer.Tick -= value2;
				}
				this._Timer1 = value;
				timer = this._Timer1;
				if (timer != null)
				{
					timer.Tick += value2;
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000152E7 File Offset: 0x000134E7
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000152F0 File Offset: 0x000134F0
		internal virtual Button TaskbarJumplist
		{
			[CompilerGenerated]
			get
			{
				return this._TaskbarJumplist;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TaskbarJumplist_Click);
				Button taskbarJumplist = this._TaskbarJumplist;
				if (taskbarJumplist != null)
				{
					taskbarJumplist.Click -= value2;
				}
				this._TaskbarJumplist = value;
				taskbarJumplist = this._TaskbarJumplist;
				if (taskbarJumplist != null)
				{
					taskbarJumplist.Click += value2;
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00015333 File Offset: 0x00013533
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0001533B File Offset: 0x0001353B
		internal virtual Label Label35 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00015344 File Offset: 0x00013544
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0001534C File Offset: 0x0001354C
		internal virtual Button ResetRecycleBin
		{
			[CompilerGenerated]
			get
			{
				return this._ResetRecycleBin;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetRecycleBin_Click);
				Button resetRecycleBin = this._ResetRecycleBin;
				if (resetRecycleBin != null)
				{
					resetRecycleBin.Click -= value2;
				}
				this._ResetRecycleBin = value;
				resetRecycleBin = this._ResetRecycleBin;
				if (resetRecycleBin != null)
				{
					resetRecycleBin.Click += value2;
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0001538F File Offset: 0x0001358F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00015397 File Offset: 0x00013597
		internal virtual Label Label36 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000153A0 File Offset: 0x000135A0
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000153A8 File Offset: 0x000135A8
		internal virtual Button CDDriveOrDVD
		{
			[CompilerGenerated]
			get
			{
				return this._CDDriveOrDVD;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CDDriveOrDVD_Click);
				Button cddriveOrDVD = this._CDDriveOrDVD;
				if (cddriveOrDVD != null)
				{
					cddriveOrDVD.Click -= value2;
				}
				this._CDDriveOrDVD = value;
				cddriveOrDVD = this._CDDriveOrDVD;
				if (cddriveOrDVD != null)
				{
					cddriveOrDVD.Click += value2;
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000153EB File Offset: 0x000135EB
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000153F3 File Offset: 0x000135F3
		internal virtual Label Label40 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000153FC File Offset: 0x000135FC
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00015404 File Offset: 0x00013604
		internal virtual Button ResetIE
		{
			[CompilerGenerated]
			get
			{
				return this._ResetIE;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetIE_Click);
				Button resetIE = this._ResetIE;
				if (resetIE != null)
				{
					resetIE.Click -= value2;
				}
				this._ResetIE = value;
				resetIE = this._ResetIE;
				if (resetIE != null)
				{
					resetIE.Click += value2;
				}
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00015447 File Offset: 0x00013647
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0001544F File Offset: 0x0001364F
		internal virtual Label Label41 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00015458 File Offset: 0x00013658
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00015460 File Offset: 0x00013660
		internal virtual Button RuntimeErrors
		{
			[CompilerGenerated]
			get
			{
				return this._RuntimeErrors;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RuntimeErrors_Click);
				Button runtimeErrors = this._RuntimeErrors;
				if (runtimeErrors != null)
				{
					runtimeErrors.Click -= value2;
				}
				this._RuntimeErrors = value;
				runtimeErrors = this._RuntimeErrors;
				if (runtimeErrors != null)
				{
					runtimeErrors.Click += value2;
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000154A3 File Offset: 0x000136A3
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000154AB File Offset: 0x000136AB
		internal virtual Label Label44 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000154B4 File Offset: 0x000136B4
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000154BC File Offset: 0x000136BC
		internal virtual Button OptimizeIE
		{
			[CompilerGenerated]
			get
			{
				return this._OptimizeIE;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.OptimizeIE_Click);
				Button optimizeIE = this._OptimizeIE;
				if (optimizeIE != null)
				{
					optimizeIE.Click -= value2;
				}
				this._OptimizeIE = value;
				optimizeIE = this._OptimizeIE;
				if (optimizeIE != null)
				{
					optimizeIE.Click += value2;
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000154FF File Offset: 0x000136FF
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00015507 File Offset: 0x00013707
		internal virtual Label Label45 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00015510 File Offset: 0x00013710
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00015518 File Offset: 0x00013718
		internal virtual LinkLabel Side_Troubleshooting
		{
			[CompilerGenerated]
			get
			{
				return this._Side_Troubleshooting;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Side_Troubleshooting_LinkClicked);
				LinkLabel side_Troubleshooting = this._Side_Troubleshooting;
				if (side_Troubleshooting != null)
				{
					side_Troubleshooting.LinkClicked -= value2;
				}
				this._Side_Troubleshooting = value;
				side_Troubleshooting = this._Side_Troubleshooting;
				if (side_Troubleshooting != null)
				{
					side_Troubleshooting.LinkClicked += value2;
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0001555B File Offset: 0x0001375B
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00015563 File Offset: 0x00013763
		internal virtual TabPage Troubleshooting { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0001556C File Offset: 0x0001376C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00015574 File Offset: 0x00013774
		internal virtual Button PlayingAudio
		{
			[CompilerGenerated]
			get
			{
				return this._PlayingAudio;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.PlayingAudio_Click);
				Button playingAudio = this._PlayingAudio;
				if (playingAudio != null)
				{
					playingAudio.Click -= value2;
				}
				this._PlayingAudio = value;
				playingAudio = this._PlayingAudio;
				if (playingAudio != null)
				{
					playingAudio.Click += value2;
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000155B7 File Offset: 0x000137B7
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000155C0 File Offset: 0x000137C0
		internal virtual Button RecordingAudio
		{
			[CompilerGenerated]
			get
			{
				return this._RecordingAudio;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RecordingAudio_Click);
				Button recordingAudio = this._RecordingAudio;
				if (recordingAudio != null)
				{
					recordingAudio.Click -= value2;
				}
				this._RecordingAudio = value;
				recordingAudio = this._RecordingAudio;
				if (recordingAudio != null)
				{
					recordingAudio.Click += value2;
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00015603 File Offset: 0x00013803
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0001560C File Offset: 0x0001380C
		internal virtual Button HardwareAndDevices
		{
			[CompilerGenerated]
			get
			{
				return this._HardwareAndDevices;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.HardwareAndDevices_Click);
				Button hardwareAndDevices = this._HardwareAndDevices;
				if (hardwareAndDevices != null)
				{
					hardwareAndDevices.Click -= value2;
				}
				this._HardwareAndDevices = value;
				hardwareAndDevices = this._HardwareAndDevices;
				if (hardwareAndDevices != null)
				{
					hardwareAndDevices.Click += value2;
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0001564F File Offset: 0x0001384F
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00015658 File Offset: 0x00013858
		internal virtual Button InternetConnections
		{
			[CompilerGenerated]
			get
			{
				return this._InternetConnections;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.InternetConnections_Click);
				Button internetConnections = this._InternetConnections;
				if (internetConnections != null)
				{
					internetConnections.Click -= value2;
				}
				this._InternetConnections = value;
				internetConnections = this._InternetConnections;
				if (internetConnections != null)
				{
					internetConnections.Click += value2;
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0001569B File Offset: 0x0001389B
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000156A4 File Offset: 0x000138A4
		internal virtual Button NetworkAdapter
		{
			[CompilerGenerated]
			get
			{
				return this._NetworkAdapter;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.NetworkAdapter_Click);
				Button networkAdapter = this._NetworkAdapter;
				if (networkAdapter != null)
				{
					networkAdapter.Click -= value2;
				}
				this._NetworkAdapter = value;
				networkAdapter = this._NetworkAdapter;
				if (networkAdapter != null)
				{
					networkAdapter.Click += value2;
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000156E7 File Offset: 0x000138E7
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000156F0 File Offset: 0x000138F0
		internal virtual Button Homegroup
		{
			[CompilerGenerated]
			get
			{
				return this._Homegroup;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Homegroup_Click);
				Button homegroup = this._Homegroup;
				if (homegroup != null)
				{
					homegroup.Click -= value2;
				}
				this._Homegroup = value;
				homegroup = this._Homegroup;
				if (homegroup != null)
				{
					homegroup.Click += value2;
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00015733 File Offset: 0x00013933
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0001573C File Offset: 0x0001393C
		internal virtual Button SharedFolders
		{
			[CompilerGenerated]
			get
			{
				return this._SharedFolders;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SharedFolders_Click);
				Button sharedFolders = this._SharedFolders;
				if (sharedFolders != null)
				{
					sharedFolders.Click -= value2;
				}
				this._SharedFolders = value;
				sharedFolders = this._SharedFolders;
				if (sharedFolders != null)
				{
					sharedFolders.Click += value2;
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0001577F File Offset: 0x0001397F
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00015788 File Offset: 0x00013988
		internal virtual Button WMPDVD
		{
			[CompilerGenerated]
			get
			{
				return this._WMPDVD;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WMPDVD_Click);
				Button wmpdvd = this._WMPDVD;
				if (wmpdvd != null)
				{
					wmpdvd.Click -= value2;
				}
				this._WMPDVD = value;
				wmpdvd = this._WMPDVD;
				if (wmpdvd != null)
				{
					wmpdvd.Click += value2;
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000157CB File Offset: 0x000139CB
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000157D4 File Offset: 0x000139D4
		internal virtual Button WMPLibrary
		{
			[CompilerGenerated]
			get
			{
				return this._WMPLibrary;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WMPLibrary_Click);
				Button wmplibrary = this._WMPLibrary;
				if (wmplibrary != null)
				{
					wmplibrary.Click -= value2;
				}
				this._WMPLibrary = value;
				wmplibrary = this._WMPLibrary;
				if (wmplibrary != null)
				{
					wmplibrary.Click += value2;
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00015817 File Offset: 0x00013A17
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00015820 File Offset: 0x00013A20
		internal virtual Button WMPSettings
		{
			[CompilerGenerated]
			get
			{
				return this._WMPSettings;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WMPSettings_Click);
				Button wmpsettings = this._WMPSettings;
				if (wmpsettings != null)
				{
					wmpsettings.Click -= value2;
				}
				this._WMPSettings = value;
				wmpsettings = this._WMPSettings;
				if (wmpsettings != null)
				{
					wmpsettings.Click += value2;
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00015863 File Offset: 0x00013A63
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0001586C File Offset: 0x00013A6C
		internal virtual Button Printer
		{
			[CompilerGenerated]
			get
			{
				return this._Printer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Printer_Click);
				Button printer = this._Printer;
				if (printer != null)
				{
					printer.Click -= value2;
				}
				this._Printer = value;
				printer = this._Printer;
				if (printer != null)
				{
					printer.Click += value2;
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000158AF File Offset: 0x00013AAF
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000158B8 File Offset: 0x00013AB8
		internal virtual Button Power
		{
			[CompilerGenerated]
			get
			{
				return this._Power;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Power_Click);
				Button power = this._Power;
				if (power != null)
				{
					power.Click -= value2;
				}
				this._Power = value;
				power = this._Power;
				if (power != null)
				{
					power.Click += value2;
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000158FB File Offset: 0x00013AFB
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00015904 File Offset: 0x00013B04
		internal virtual Button SystemMaintenence
		{
			[CompilerGenerated]
			get
			{
				return this._SystemMaintenence;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SystemMaintenence_Click);
				Button systemMaintenence = this._SystemMaintenence;
				if (systemMaintenence != null)
				{
					systemMaintenence.Click -= value2;
				}
				this._SystemMaintenence = value;
				systemMaintenence = this._SystemMaintenence;
				if (systemMaintenence != null)
				{
					systemMaintenence.Click += value2;
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00015947 File Offset: 0x00013B47
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00015950 File Offset: 0x00013B50
		internal virtual Button IESafety
		{
			[CompilerGenerated]
			get
			{
				return this._IESafety;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.IESafety_Click);
				Button iesafety = this._IESafety;
				if (iesafety != null)
				{
					iesafety.Click -= value2;
				}
				this._IESafety = value;
				iesafety = this._IESafety;
				if (iesafety != null)
				{
					iesafety.Click += value2;
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00015993 File Offset: 0x00013B93
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0001599C File Offset: 0x00013B9C
		internal virtual Button IEPerformance
		{
			[CompilerGenerated]
			get
			{
				return this._IEPerformance;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.IEPerformance_Click);
				Button ieperformance = this._IEPerformance;
				if (ieperformance != null)
				{
					ieperformance.Click -= value2;
				}
				this._IEPerformance = value;
				ieperformance = this._IEPerformance;
				if (ieperformance != null)
				{
					ieperformance.Click += value2;
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000159DF File Offset: 0x00013BDF
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000159E8 File Offset: 0x00013BE8
		internal virtual Button IncomingConnections
		{
			[CompilerGenerated]
			get
			{
				return this._IncomingConnections;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.IncomingConnections_Click);
				Button incomingConnections = this._IncomingConnections;
				if (incomingConnections != null)
				{
					incomingConnections.Click -= value2;
				}
				this._IncomingConnections = value;
				incomingConnections = this._IncomingConnections;
				if (incomingConnections != null)
				{
					incomingConnections.Click += value2;
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00015A2B File Offset: 0x00013C2B
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00015A34 File Offset: 0x00013C34
		internal virtual Button ActionCenterAnd
		{
			[CompilerGenerated]
			get
			{
				return this._ActionCenterAnd;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ActionCenterAnd_Click);
				Button actionCenterAnd = this._ActionCenterAnd;
				if (actionCenterAnd != null)
				{
					actionCenterAnd.Click -= value2;
				}
				this._ActionCenterAnd = value;
				actionCenterAnd = this._ActionCenterAnd;
				if (actionCenterAnd != null)
				{
					actionCenterAnd.Click += value2;
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00015A77 File Offset: 0x00013C77
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00015A7F File Offset: 0x00013C7F
		internal virtual Label Label50 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00015A88 File Offset: 0x00013C88
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00015A90 File Offset: 0x00013C90
		internal virtual Button ClassNotRegistred
		{
			[CompilerGenerated]
			get
			{
				return this._ClassNotRegistred;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ClassNotRegistred_Click);
				Button classNotRegistred = this._ClassNotRegistred;
				if (classNotRegistred != null)
				{
					classNotRegistred.Click -= value2;
				}
				this._ClassNotRegistred = value;
				classNotRegistred = this._ClassNotRegistred;
				if (classNotRegistred != null)
				{
					classNotRegistred.Click += value2;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00015AD3 File Offset: 0x00013CD3
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00015ADB File Offset: 0x00013CDB
		internal virtual Label Label51 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00015AE4 File Offset: 0x00013CE4
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00015AEC File Offset: 0x00013CEC
		internal virtual Button balloontips
		{
			[CompilerGenerated]
			get
			{
				return this._balloontips;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.balloontips_Click);
				Button balloontips = this._balloontips;
				if (balloontips != null)
				{
					balloontips.Click -= value2;
				}
				this._balloontips = value;
				balloontips = this._balloontips;
				if (balloontips != null)
				{
					balloontips.Click += value2;
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00015B2F File Offset: 0x00013D2F
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00015B37 File Offset: 0x00013D37
		internal virtual Label Label47 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00015B40 File Offset: 0x00013D40
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00015B48 File Offset: 0x00013D48
		internal virtual Button WindowsScriptHost
		{
			[CompilerGenerated]
			get
			{
				return this._WindowsScriptHost;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WindowsScriptHost_Click);
				Button windowsScriptHost = this._WindowsScriptHost;
				if (windowsScriptHost != null)
				{
					windowsScriptHost.Click -= value2;
				}
				this._WindowsScriptHost = value;
				windowsScriptHost = this._WindowsScriptHost;
				if (windowsScriptHost != null)
				{
					windowsScriptHost.Click += value2;
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00015B8B File Offset: 0x00013D8B
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00015B93 File Offset: 0x00013D93
		internal virtual Label Label52 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00015B9C File Offset: 0x00013D9C
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00015BA4 File Offset: 0x00013DA4
		internal virtual Button officedocsdonotopen
		{
			[CompilerGenerated]
			get
			{
				return this._officedocsdonotopen;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.appswitcher_Click);
				Button officedocsdonotopen = this._officedocsdonotopen;
				if (officedocsdonotopen != null)
				{
					officedocsdonotopen.Click -= value2;
				}
				this._officedocsdonotopen = value;
				officedocsdonotopen = this._officedocsdonotopen;
				if (officedocsdonotopen != null)
				{
					officedocsdonotopen.Click += value2;
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00015BE7 File Offset: 0x00013DE7
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00015BEF File Offset: 0x00013DEF
		internal virtual Label Label53 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00015BF8 File Offset: 0x00013DF8
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00015C00 File Offset: 0x00013E00
		internal virtual Button recoveryimagecant
		{
			[CompilerGenerated]
			get
			{
				return this._recoveryimagecant;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.recoveryimagecant_Click);
				Button recoveryimagecant = this._recoveryimagecant;
				if (recoveryimagecant != null)
				{
					recoveryimagecant.Click -= value2;
				}
				this._recoveryimagecant = value;
				recoveryimagecant = this._recoveryimagecant;
				if (recoveryimagecant != null)
				{
					recoveryimagecant.Click += value2;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00015C43 File Offset: 0x00013E43
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00015C4B File Offset: 0x00013E4B
		internal virtual Label Label54 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00015C54 File Offset: 0x00013E54
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00015C5C File Offset: 0x00013E5C
		internal virtual Button showhiddenfiles
		{
			[CompilerGenerated]
			get
			{
				return this._showhiddenfiles;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.showhiddenfiles_Click);
				Button showhiddenfiles = this._showhiddenfiles;
				if (showhiddenfiles != null)
				{
					showhiddenfiles.Click -= value2;
				}
				this._showhiddenfiles = value;
				showhiddenfiles = this._showhiddenfiles;
				if (showhiddenfiles != null)
				{
					showhiddenfiles.Click += value2;
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00015C9F File Offset: 0x00013E9F
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00015CA7 File Offset: 0x00013EA7
		internal virtual Label Label55 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00015CB0 File Offset: 0x00013EB0
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00015CB8 File Offset: 0x00013EB8
		internal virtual Button internetoptionsmissing
		{
			[CompilerGenerated]
			get
			{
				return this._internetoptionsmissing;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.internetoptionsmissing_Click);
				Button internetoptionsmissing = this._internetoptionsmissing;
				if (internetoptionsmissing != null)
				{
					internetoptionsmissing.Click -= value2;
				}
				this._internetoptionsmissing = value;
				internetoptionsmissing = this._internetoptionsmissing;
				if (internetoptionsmissing != null)
				{
					internetoptionsmissing.Click += value2;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00015CFB File Offset: 0x00013EFB
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00015D03 File Offset: 0x00013F03
		internal virtual Label Label56 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00015D0C File Offset: 0x00013F0C
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00015D14 File Offset: 0x00013F14
		internal virtual Button wmp
		{
			[CompilerGenerated]
			get
			{
				return this._wmp;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.wmp_Click);
				Button wmp = this._wmp;
				if (wmp != null)
				{
					wmp.Click -= value2;
				}
				this._wmp = value;
				wmp = this._wmp;
				if (wmp != null)
				{
					wmp.Click += value2;
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00015D57 File Offset: 0x00013F57
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00015D5F File Offset: 0x00013F5F
		internal virtual Label Label57 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00015D68 File Offset: 0x00013F68
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00015D70 File Offset: 0x00013F70
		internal virtual Button winsock
		{
			[CompilerGenerated]
			get
			{
				return this._winsock;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.winsock_Click);
				Button winsock = this._winsock;
				if (winsock != null)
				{
					winsock.Click -= value2;
				}
				this._winsock = value;
				winsock = this._winsock;
				if (winsock != null)
				{
					winsock.Click += value2;
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00015DB3 File Offset: 0x00013FB3
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00015DBB File Offset: 0x00013FBB
		internal virtual Label Label58 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00015DC4 File Offset: 0x00013FC4
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00015DCC File Offset: 0x00013FCC
		internal virtual Button ResetWindowsSecurity
		{
			[CompilerGenerated]
			get
			{
				return this._ResetWindowsSecurity;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetWindowsSecurity_Click);
				Button resetWindowsSecurity = this._ResetWindowsSecurity;
				if (resetWindowsSecurity != null)
				{
					resetWindowsSecurity.Click -= value2;
				}
				this._ResetWindowsSecurity = value;
				resetWindowsSecurity = this._ResetWindowsSecurity;
				if (resetWindowsSecurity != null)
				{
					resetWindowsSecurity.Click += value2;
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00015E0F File Offset: 0x0001400F
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00015E17 File Offset: 0x00014017
		internal virtual Label Label59 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00015E20 File Offset: 0x00014020
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00015E28 File Offset: 0x00014028
		internal virtual BackgroundWorker BackgroundWorker2
		{
			[CompilerGenerated]
			get
			{
				return this._BackgroundWorker2;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				DoWorkEventHandler value2 = new DoWorkEventHandler(this.BackgroundWorker2_DoWork);
				BackgroundWorker backgroundWorker = this._BackgroundWorker2;
				if (backgroundWorker != null)
				{
					backgroundWorker.DoWork -= value2;
				}
				this._BackgroundWorker2 = value;
				backgroundWorker = this._BackgroundWorker2;
				if (backgroundWorker != null)
				{
					backgroundWorker.DoWork += value2;
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00015E6B File Offset: 0x0001406B
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00015E73 File Offset: 0x00014073
		internal virtual TabPage Windows11 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00015E7C File Offset: 0x0001407C
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00015E84 File Offset: 0x00014084
		internal virtual Button dism
		{
			[CompilerGenerated]
			get
			{
				return this._dism;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.dism_Click);
				Button dism = this._dism;
				if (dism != null)
				{
					dism.Click -= value2;
				}
				this._dism = value;
				dism = this._dism;
				if (dism != null)
				{
					dism.Click += value2;
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00015EC7 File Offset: 0x000140C7
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00015ECF File Offset: 0x000140CF
		internal virtual Label Label60 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00015ED8 File Offset: 0x000140D8
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00015EE0 File Offset: 0x000140E0
		internal virtual Button StartMenuDoesNTOpen
		{
			[CompilerGenerated]
			get
			{
				return this._StartMenuDoesNTOpen;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SomethingHappened_Click);
				Button startMenuDoesNTOpen = this._StartMenuDoesNTOpen;
				if (startMenuDoesNTOpen != null)
				{
					startMenuDoesNTOpen.Click -= value2;
				}
				this._StartMenuDoesNTOpen = value;
				startMenuDoesNTOpen = this._StartMenuDoesNTOpen;
				if (startMenuDoesNTOpen != null)
				{
					startMenuDoesNTOpen.Click += value2;
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00015F23 File Offset: 0x00014123
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00015F2B File Offset: 0x0001412B
		internal virtual Label Label39 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00015F34 File Offset: 0x00014134
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00015F3C File Offset: 0x0001413C
		internal virtual Label Label37 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00015F45 File Offset: 0x00014145
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00015F50 File Offset: 0x00014150
		internal virtual Button Revert
		{
			[CompilerGenerated]
			get
			{
				return this._Revert;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Revert_Click);
				Button revert = this._Revert;
				if (revert != null)
				{
					revert.Click -= value2;
				}
				this._Revert = value;
				revert = this._Revert;
				if (revert != null)
				{
					revert.Click += value2;
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00015F93 File Offset: 0x00014193
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00015F9B File Offset: 0x0001419B
		internal virtual Label Label38 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00015FA4 File Offset: 0x000141A4
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00015FAC File Offset: 0x000141AC
		internal virtual Button DisableOnedrive
		{
			[CompilerGenerated]
			get
			{
				return this._DisableOnedrive;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.DisableOnedrive_Click);
				Button disableOnedrive = this._DisableOnedrive;
				if (disableOnedrive != null)
				{
					disableOnedrive.Click -= value2;
				}
				this._DisableOnedrive = value;
				disableOnedrive = this._DisableOnedrive;
				if (disableOnedrive != null)
				{
					disableOnedrive.Click += value2;
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00015FEF File Offset: 0x000141EF
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00015FF8 File Offset: 0x000141F8
		internal virtual Button ResetPCSettings
		{
			[CompilerGenerated]
			get
			{
				return this._ResetPCSettings;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetPCSettings_Click);
				Button resetPCSettings = this._ResetPCSettings;
				if (resetPCSettings != null)
				{
					resetPCSettings.Click -= value2;
				}
				this._ResetPCSettings = value;
				resetPCSettings = this._ResetPCSettings;
				if (resetPCSettings != null)
				{
					resetPCSettings.Click += value2;
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0001603B File Offset: 0x0001423B
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00016044 File Offset: 0x00014244
		internal virtual Button Button2
		{
			[CompilerGenerated]
			get
			{
				return this._Button2;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button2_Click);
				Button button = this._Button2;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button2 = value;
				button = this._Button2;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00016087 File Offset: 0x00014287
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00016090 File Offset: 0x00014290
		internal virtual Button Button3
		{
			[CompilerGenerated]
			get
			{
				return this._Button3;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button3_Click);
				Button button = this._Button3;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button3 = value;
				button = this._Button3;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000160D3 File Offset: 0x000142D3
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000160DC File Offset: 0x000142DC
		internal virtual Button Button4
		{
			[CompilerGenerated]
			get
			{
				return this._Button4;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button4_Click);
				Button button = this._Button4;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button4 = value;
				button = this._Button4;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0001611F File Offset: 0x0001431F
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00016128 File Offset: 0x00014328
		internal virtual Button Button5
		{
			[CompilerGenerated]
			get
			{
				return this._Button5;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button5_Click);
				Button button = this._Button5;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button5 = value;
				button = this._Button5;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0001616B File Offset: 0x0001436B
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00016174 File Offset: 0x00014374
		internal virtual Button Button6
		{
			[CompilerGenerated]
			get
			{
				return this._Button6;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button6_Click);
				Button button = this._Button6;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button6 = value;
				button = this._Button6;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000161B7 File Offset: 0x000143B7
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000161C0 File Offset: 0x000143C0
		internal virtual Button Button11
		{
			[CompilerGenerated]
			get
			{
				return this._Button11;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button11_Click);
				Button button = this._Button11;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button11 = value;
				button = this._Button11;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00016203 File Offset: 0x00014403
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0001620C File Offset: 0x0001440C
		internal virtual Button Button10
		{
			[CompilerGenerated]
			get
			{
				return this._Button10;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button10_Click);
				Button button = this._Button10;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button10 = value;
				button = this._Button10;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0001624F File Offset: 0x0001444F
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00016258 File Offset: 0x00014458
		internal virtual Button Button9
		{
			[CompilerGenerated]
			get
			{
				return this._Button9;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button9_Click);
				Button button = this._Button9;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button9 = value;
				button = this._Button9;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0001629B File Offset: 0x0001449B
		// (set) Token: 0x06000242 RID: 578 RVA: 0x000162A4 File Offset: 0x000144A4
		internal virtual Button Button8
		{
			[CompilerGenerated]
			get
			{
				return this._Button8;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button8_Click);
				Button button = this._Button8;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button8 = value;
				button = this._Button8;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000162E7 File Offset: 0x000144E7
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000162F0 File Offset: 0x000144F0
		internal virtual Button Button7
		{
			[CompilerGenerated]
			get
			{
				return this._Button7;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button7_Click);
				Button button = this._Button7;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button7 = value;
				button = this._Button7;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00016333 File Offset: 0x00014533
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0001633C File Offset: 0x0001453C
		internal virtual Button Button12
		{
			[CompilerGenerated]
			get
			{
				return this._Button12;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button12_Click);
				Button button = this._Button12;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button12 = value;
				button = this._Button12;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0001637F File Offset: 0x0001457F
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00016388 File Offset: 0x00014588
		internal virtual Button Button13
		{
			[CompilerGenerated]
			get
			{
				return this._Button13;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button13_Click);
				Button button = this._Button13;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button13 = value;
				button = this._Button13;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000163CB File Offset: 0x000145CB
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000163D4 File Offset: 0x000145D4
		internal virtual Button Button14
		{
			[CompilerGenerated]
			get
			{
				return this._Button14;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button14_Click);
				Button button = this._Button14;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button14 = value;
				button = this._Button14;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00016417 File Offset: 0x00014617
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00016420 File Offset: 0x00014620
		internal virtual Button Button15
		{
			[CompilerGenerated]
			get
			{
				return this._Button15;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button15_Click);
				Button button = this._Button15;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button15 = value;
				button = this._Button15;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00016463 File Offset: 0x00014663
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0001646C File Offset: 0x0001466C
		internal virtual Button Button16
		{
			[CompilerGenerated]
			get
			{
				return this._Button16;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button16_Click);
				Button button = this._Button16;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button16 = value;
				button = this._Button16;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000164AF File Offset: 0x000146AF
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000164B8 File Offset: 0x000146B8
		internal virtual Button Button17
		{
			[CompilerGenerated]
			get
			{
				return this._Button17;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button17_Click);
				Button button = this._Button17;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button17 = value;
				button = this._Button17;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000164FB File Offset: 0x000146FB
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00016504 File Offset: 0x00014704
		internal virtual Button Button18
		{
			[CompilerGenerated]
			get
			{
				return this._Button18;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button18_Click);
				Button button = this._Button18;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button18 = value;
				button = this._Button18;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00016547 File Offset: 0x00014747
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00016550 File Offset: 0x00014750
		internal virtual Button Button19
		{
			[CompilerGenerated]
			get
			{
				return this._Button19;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button19_Click);
				Button button = this._Button19;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button19 = value;
				button = this._Button19;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00016593 File Offset: 0x00014793
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0001659C File Offset: 0x0001479C
		internal virtual Button Button20
		{
			[CompilerGenerated]
			get
			{
				return this._Button20;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button20_Click);
				Button button = this._Button20;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button20 = value;
				button = this._Button20;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000165DF File Offset: 0x000147DF
		// (set) Token: 0x06000258 RID: 600 RVA: 0x000165E8 File Offset: 0x000147E8
		internal virtual Button Button21
		{
			[CompilerGenerated]
			get
			{
				return this._Button21;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button21_Click);
				Button button = this._Button21;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button21 = value;
				button = this._Button21;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0001662B File Offset: 0x0001482B
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00016634 File Offset: 0x00014834
		internal virtual Button Button42
		{
			[CompilerGenerated]
			get
			{
				return this._Button42;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button42_Click);
				Button button = this._Button42;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button42 = value;
				button = this._Button42;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00016677 File Offset: 0x00014877
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00016680 File Offset: 0x00014880
		internal virtual Button Button43
		{
			[CompilerGenerated]
			get
			{
				return this._Button43;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button43_Click);
				Button button = this._Button43;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button43 = value;
				button = this._Button43;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000166C3 File Offset: 0x000148C3
		// (set) Token: 0x0600025E RID: 606 RVA: 0x000166CC File Offset: 0x000148CC
		internal virtual Button Button44
		{
			[CompilerGenerated]
			get
			{
				return this._Button44;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button44_Click);
				Button button = this._Button44;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button44 = value;
				button = this._Button44;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0001670F File Offset: 0x0001490F
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00016718 File Offset: 0x00014918
		internal virtual Button Button45
		{
			[CompilerGenerated]
			get
			{
				return this._Button45;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button45_Click);
				Button button = this._Button45;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button45 = value;
				button = this._Button45;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0001675B File Offset: 0x0001495B
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00016764 File Offset: 0x00014964
		internal virtual Button Button22
		{
			[CompilerGenerated]
			get
			{
				return this._Button22;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button22_Click);
				Button button = this._Button22;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button22 = value;
				button = this._Button22;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000167A7 File Offset: 0x000149A7
		// (set) Token: 0x06000264 RID: 612 RVA: 0x000167B0 File Offset: 0x000149B0
		internal virtual Button Button23
		{
			[CompilerGenerated]
			get
			{
				return this._Button23;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button23_Click);
				Button button = this._Button23;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button23 = value;
				button = this._Button23;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000167F3 File Offset: 0x000149F3
		// (set) Token: 0x06000266 RID: 614 RVA: 0x000167FC File Offset: 0x000149FC
		internal virtual Button Button24
		{
			[CompilerGenerated]
			get
			{
				return this._Button24;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button24_Click);
				Button button = this._Button24;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button24 = value;
				button = this._Button24;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0001683F File Offset: 0x00014A3F
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00016848 File Offset: 0x00014A48
		internal virtual Button Button25
		{
			[CompilerGenerated]
			get
			{
				return this._Button25;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button25_Click);
				Button button = this._Button25;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button25 = value;
				button = this._Button25;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0001688B File Offset: 0x00014A8B
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00016894 File Offset: 0x00014A94
		internal virtual Button Button26
		{
			[CompilerGenerated]
			get
			{
				return this._Button26;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button26_Click);
				Button button = this._Button26;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button26 = value;
				button = this._Button26;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000168D7 File Offset: 0x00014AD7
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000168E0 File Offset: 0x00014AE0
		internal virtual Button Button27
		{
			[CompilerGenerated]
			get
			{
				return this._Button27;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button27_Click);
				Button button = this._Button27;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button27 = value;
				button = this._Button27;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00016923 File Offset: 0x00014B23
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0001692C File Offset: 0x00014B2C
		internal virtual Button Button28
		{
			[CompilerGenerated]
			get
			{
				return this._Button28;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button28_Click);
				Button button = this._Button28;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button28 = value;
				button = this._Button28;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0001696F File Offset: 0x00014B6F
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00016978 File Offset: 0x00014B78
		internal virtual Button Button29
		{
			[CompilerGenerated]
			get
			{
				return this._Button29;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button29_Click);
				Button button = this._Button29;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button29 = value;
				button = this._Button29;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000169BB File Offset: 0x00014BBB
		// (set) Token: 0x06000272 RID: 626 RVA: 0x000169C4 File Offset: 0x00014BC4
		internal virtual Button Button30
		{
			[CompilerGenerated]
			get
			{
				return this._Button30;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button30_Click);
				Button button = this._Button30;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button30 = value;
				button = this._Button30;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00016A07 File Offset: 0x00014C07
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00016A10 File Offset: 0x00014C10
		internal virtual Button Button31
		{
			[CompilerGenerated]
			get
			{
				return this._Button31;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button31_Click);
				Button button = this._Button31;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button31 = value;
				button = this._Button31;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00016A53 File Offset: 0x00014C53
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00016A5C File Offset: 0x00014C5C
		internal virtual Button Button32
		{
			[CompilerGenerated]
			get
			{
				return this._Button32;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button32_Click);
				Button button = this._Button32;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button32 = value;
				button = this._Button32;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00016A9F File Offset: 0x00014C9F
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00016AA8 File Offset: 0x00014CA8
		internal virtual Button Button33
		{
			[CompilerGenerated]
			get
			{
				return this._Button33;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button33_Click);
				Button button = this._Button33;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button33 = value;
				button = this._Button33;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00016AEB File Offset: 0x00014CEB
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00016AF4 File Offset: 0x00014CF4
		internal virtual Button Button34
		{
			[CompilerGenerated]
			get
			{
				return this._Button34;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button34_Click);
				Button button = this._Button34;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button34 = value;
				button = this._Button34;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00016B37 File Offset: 0x00014D37
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00016B40 File Offset: 0x00014D40
		internal virtual Button Button35
		{
			[CompilerGenerated]
			get
			{
				return this._Button35;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button35_Click);
				Button button = this._Button35;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button35 = value;
				button = this._Button35;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00016B83 File Offset: 0x00014D83
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00016B8C File Offset: 0x00014D8C
		internal virtual Button Button36
		{
			[CompilerGenerated]
			get
			{
				return this._Button36;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button36_Click);
				Button button = this._Button36;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button36 = value;
				button = this._Button36;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00016BCF File Offset: 0x00014DCF
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00016BD8 File Offset: 0x00014DD8
		internal virtual Button Button37
		{
			[CompilerGenerated]
			get
			{
				return this._Button37;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button37_Click);
				Button button = this._Button37;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button37 = value;
				button = this._Button37;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00016C1B File Offset: 0x00014E1B
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00016C24 File Offset: 0x00014E24
		internal virtual Button Button38
		{
			[CompilerGenerated]
			get
			{
				return this._Button38;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button38_Click);
				Button button = this._Button38;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button38 = value;
				button = this._Button38;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00016C67 File Offset: 0x00014E67
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00016C70 File Offset: 0x00014E70
		internal virtual Button Button39
		{
			[CompilerGenerated]
			get
			{
				return this._Button39;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button39_Click);
				Button button = this._Button39;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button39 = value;
				button = this._Button39;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00016CB3 File Offset: 0x00014EB3
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00016CBC File Offset: 0x00014EBC
		internal virtual Button Button40
		{
			[CompilerGenerated]
			get
			{
				return this._Button40;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button40_Click);
				Button button = this._Button40;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button40 = value;
				button = this._Button40;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00016CFF File Offset: 0x00014EFF
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00016D08 File Offset: 0x00014F08
		internal virtual Button Button41
		{
			[CompilerGenerated]
			get
			{
				return this._Button41;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button41_Click);
				Button button = this._Button41;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button41 = value;
				button = this._Button41;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00016D4B File Offset: 0x00014F4B
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00016D54 File Offset: 0x00014F54
		internal virtual CommandLink SystemFileChecker
		{
			[CompilerGenerated]
			get
			{
				return this._SystemFileChecker;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SystemFileChecker_Click);
				CommandLink systemFileChecker = this._SystemFileChecker;
				if (systemFileChecker != null)
				{
					systemFileChecker.Click -= value2;
				}
				this._SystemFileChecker = value;
				systemFileChecker = this._SystemFileChecker;
				if (systemFileChecker != null)
				{
					systemFileChecker.Click += value2;
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00016D97 File Offset: 0x00014F97
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00016DA0 File Offset: 0x00014FA0
		internal virtual CommandLink CreateRestorePoint
		{
			[CompilerGenerated]
			get
			{
				return this._CreateRestorePoint;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CreateRestorePoint_Click);
				CommandLink createRestorePoint = this._CreateRestorePoint;
				if (createRestorePoint != null)
				{
					createRestorePoint.Click -= value2;
				}
				this._CreateRestorePoint = value;
				createRestorePoint = this._CreateRestorePoint;
				if (createRestorePoint != null)
				{
					createRestorePoint.Click += value2;
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00016DE3 File Offset: 0x00014FE3
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00016DEB File Offset: 0x00014FEB
		internal virtual Label os { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00016DF4 File Offset: 0x00014FF4
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00016DFC File Offset: 0x00014FFC
		internal virtual Label username { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00016E05 File Offset: 0x00015005
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00016E10 File Offset: 0x00015010
		internal virtual Button SearchTroubleshoo
		{
			[CompilerGenerated]
			get
			{
				return this._SearchTroubleshoo;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SearchTroubleshoo_Click);
				Button searchTroubleshoo = this._SearchTroubleshoo;
				if (searchTroubleshoo != null)
				{
					searchTroubleshoo.Click -= value2;
				}
				this._SearchTroubleshoo = value;
				searchTroubleshoo = this._SearchTroubleshoo;
				if (searchTroubleshoo != null)
				{
					searchTroubleshoo.Click += value2;
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00016E53 File Offset: 0x00015053
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00016E5C File Offset: 0x0001505C
		internal virtual Button WinUpdTro
		{
			[CompilerGenerated]
			get
			{
				return this._WinUpdTro;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WinUpdTro_Click);
				Button winUpdTro = this._WinUpdTro;
				if (winUpdTro != null)
				{
					winUpdTro.Click -= value2;
				}
				this._WinUpdTro = value;
				winUpdTro = this._WinUpdTro;
				if (winUpdTro != null)
				{
					winUpdTro.Click += value2;
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00016E9F File Offset: 0x0001509F
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00016EA8 File Offset: 0x000150A8
		internal virtual Button WerMgrorWerFault2
		{
			[CompilerGenerated]
			get
			{
				return this._WerMgrorWerFault2;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WerMgrorWerFault2_Click);
				Button werMgrorWerFault = this._WerMgrorWerFault2;
				if (werMgrorWerFault != null)
				{
					werMgrorWerFault.Click -= value2;
				}
				this._WerMgrorWerFault2 = value;
				werMgrorWerFault = this._WerMgrorWerFault2;
				if (werMgrorWerFault != null)
				{
					werMgrorWerFault.Click += value2;
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00016EEB File Offset: 0x000150EB
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00016EF4 File Offset: 0x000150F4
		internal virtual Button Button52
		{
			[CompilerGenerated]
			get
			{
				return this._Button52;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button52_Click);
				Button button = this._Button52;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button52 = value;
				button = this._Button52;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00016F37 File Offset: 0x00015137
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00016F40 File Offset: 0x00015140
		internal virtual Button Wifidoesntwork
		{
			[CompilerGenerated]
			get
			{
				return this._Wifidoesntwork;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Wifidoesntwork_Click);
				Button wifidoesntwork = this._Wifidoesntwork;
				if (wifidoesntwork != null)
				{
					wifidoesntwork.Click -= value2;
				}
				this._Wifidoesntwork = value;
				wifidoesntwork = this._Wifidoesntwork;
				if (wifidoesntwork != null)
				{
					wifidoesntwork.Click += value2;
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00016F83 File Offset: 0x00015183
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00016F8B File Offset: 0x0001518B
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00016F94 File Offset: 0x00015194
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00016F9C File Offset: 0x0001519C
		internal virtual Button Button53
		{
			[CompilerGenerated]
			get
			{
				return this._Button53;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button53_Click);
				Button button = this._Button53;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button53 = value;
				button = this._Button53;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00016FDF File Offset: 0x000151DF
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00016FE8 File Offset: 0x000151E8
		internal virtual Button WinUpdatesStuck
		{
			[CompilerGenerated]
			get
			{
				return this._WinUpdatesStuck;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WinUpdatesStuck_Click);
				Button winUpdatesStuck = this._WinUpdatesStuck;
				if (winUpdatesStuck != null)
				{
					winUpdatesStuck.Click -= value2;
				}
				this._WinUpdatesStuck = value;
				winUpdatesStuck = this._WinUpdatesStuck;
				if (winUpdatesStuck != null)
				{
					winUpdatesStuck.Click += value2;
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0001702B File Offset: 0x0001522B
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00017033 File Offset: 0x00015233
		internal virtual Label Label46 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0001703C File Offset: 0x0001523C
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00017044 File Offset: 0x00015244
		internal virtual LinkLabel LinkLabel9
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel9;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel9_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel9;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel9 = value;
				linkLabel = this._LinkLabel9;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00017087 File Offset: 0x00015287
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00017090 File Offset: 0x00015290
		internal virtual LinkLabel LinkLabel8
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel8;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel8_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel8;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel8 = value;
				linkLabel = this._LinkLabel8;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000170D3 File Offset: 0x000152D3
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000170DC File Offset: 0x000152DC
		internal virtual LinkLabel LinkLabel7
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel7;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel7_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel7;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel7 = value;
				linkLabel = this._LinkLabel7;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001711F File Offset: 0x0001531F
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00017127 File Offset: 0x00015327
		internal virtual Label Label19 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00017130 File Offset: 0x00015330
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00017138 File Offset: 0x00015338
		internal virtual LinkLabel LinkLabel5
		{
			[CompilerGenerated]
			get
			{
				return this._LinkLabel5;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.LinkLabel5_LinkClicked);
				LinkLabel linkLabel = this._LinkLabel5;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked -= value2;
				}
				this._LinkLabel5 = value;
				linkLabel = this._LinkLabel5;
				if (linkLabel != null)
				{
					linkLabel.LinkClicked += value2;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0001717B File Offset: 0x0001537B
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00017183 File Offset: 0x00015383
		internal virtual Label Label18 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001718C File Offset: 0x0001538C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00017194 File Offset: 0x00015394
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0001719D File Offset: 0x0001539D
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x000171A5 File Offset: 0x000153A5
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000171AE File Offset: 0x000153AE
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000171B6 File Offset: 0x000153B6
		internal virtual TabPage Evaluation { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000171BF File Offset: 0x000153BF
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000171C7 File Offset: 0x000153C7
		internal virtual GroupBox GroupBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000171D0 File Offset: 0x000153D0
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000171D8 File Offset: 0x000153D8
		internal virtual TextBox processor_name { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000171E1 File Offset: 0x000153E1
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000171E9 File Offset: 0x000153E9
		internal virtual Label Label49 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000171F2 File Offset: 0x000153F2
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000171FA File Offset: 0x000153FA
		internal virtual TextBox processor_cores { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00017203 File Offset: 0x00015403
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0001720B File Offset: 0x0001540B
		internal virtual Label Label61 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00017214 File Offset: 0x00015414
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0001721C File Offset: 0x0001541C
		internal virtual TextBox processor_Currentclockspeed { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00017225 File Offset: 0x00015425
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0001722D File Offset: 0x0001542D
		internal virtual Label Label62 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00017236 File Offset: 0x00015436
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0001723E File Offset: 0x0001543E
		internal virtual TextBox processor_Maxclockspeed { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00017247 File Offset: 0x00015447
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0001724F File Offset: 0x0001544F
		internal virtual Label Label63 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00017258 File Offset: 0x00015458
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00017260 File Offset: 0x00015460
		internal virtual TextBox processor_Thread { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00017269 File Offset: 0x00015469
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00017271 File Offset: 0x00015471
		internal virtual Label Label64 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001727A File Offset: 0x0001547A
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00017282 File Offset: 0x00015482
		internal virtual TextBox memory_totalram { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0001728B File Offset: 0x0001548B
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00017293 File Offset: 0x00015493
		internal virtual Label Label65 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0001729C File Offset: 0x0001549C
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000172A4 File Offset: 0x000154A4
		internal virtual TextBox memory_Speed { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000172AD File Offset: 0x000154AD
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x000172B5 File Offset: 0x000154B5
		internal virtual Label Label66 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000172BE File Offset: 0x000154BE
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x000172C6 File Offset: 0x000154C6
		internal virtual TextBox memory_availableRAM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000172CF File Offset: 0x000154CF
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x000172D7 File Offset: 0x000154D7
		internal virtual Label Label67 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000172E0 File Offset: 0x000154E0
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x000172E8 File Offset: 0x000154E8
		internal virtual GroupBox GroupBox2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000172F1 File Offset: 0x000154F1
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000172F9 File Offset: 0x000154F9
		internal virtual TextBox display_resolution { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00017302 File Offset: 0x00015502
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0001730A File Offset: 0x0001550A
		internal virtual Label Label68 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00017313 File Offset: 0x00015513
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0001731B File Offset: 0x0001551B
		internal virtual TextBox processor_LogicalProcessors { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00017324 File Offset: 0x00015524
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0001732C File Offset: 0x0001552C
		internal virtual Label Label69 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00017335 File Offset: 0x00015535
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0001733D File Offset: 0x0001553D
		internal virtual TextBox Display_Graphicscard { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00017346 File Offset: 0x00015546
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0001734E File Offset: 0x0001554E
		internal virtual Label Label70 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00017357 File Offset: 0x00015557
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0001735F File Offset: 0x0001555F
		internal virtual TextBox Display_monitortype { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00017368 File Offset: 0x00015568
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00017370 File Offset: 0x00015570
		internal virtual Label Label71 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00017379 File Offset: 0x00015579
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00017381 File Offset: 0x00015581
		internal virtual ListBox ListBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0001738A File Offset: 0x0001558A
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00017394 File Offset: 0x00015594
		internal virtual CommandLink reregisterapps
		{
			[CompilerGenerated]
			get
			{
				return this._reregisterapps;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SomethingHappened_Click);
				CommandLink reregisterapps = this._reregisterapps;
				if (reregisterapps != null)
				{
					reregisterapps.Click -= value2;
				}
				this._reregisterapps = value;
				reregisterapps = this._reregisterapps;
				if (reregisterapps != null)
				{
					reregisterapps.Click += value2;
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000173D7 File Offset: 0x000155D7
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000173DF File Offset: 0x000155DF
		internal virtual Label ram { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000173E8 File Offset: 0x000155E8
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x000173F0 File Offset: 0x000155F0
		internal virtual Label bit1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000173F9 File Offset: 0x000155F9
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00017401 File Offset: 0x00015601
		internal virtual Label processor { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001740A File Offset: 0x0001560A
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00017414 File Offset: 0x00015614
		internal virtual CommandLink CommandLink1
		{
			[CompilerGenerated]
			get
			{
				return this._CommandLink1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CommandLink1_Click);
				CommandLink commandLink = this._CommandLink1;
				if (commandLink != null)
				{
					commandLink.Click -= value2;
				}
				this._CommandLink1 = value;
				commandLink = this._CommandLink1;
				if (commandLink != null)
				{
					commandLink.Click += value2;
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00017457 File Offset: 0x00015657
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0001745F File Offset: 0x0001565F
		internal virtual Label Label72 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00017468 File Offset: 0x00015668
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00017470 File Offset: 0x00015670
		internal virtual TextBox MaxRefreshRate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00017479 File Offset: 0x00015679
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00017484 File Offset: 0x00015684
		internal virtual BackgroundWorker BackgroundWorker3
		{
			[CompilerGenerated]
			get
			{
				return this._BackgroundWorker3;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				DoWorkEventHandler value2 = new DoWorkEventHandler(this.BackgroundWorker3_DoWork);
				BackgroundWorker backgroundWorker = this._BackgroundWorker3;
				if (backgroundWorker != null)
				{
					backgroundWorker.DoWork -= value2;
				}
				this._BackgroundWorker3 = value;
				backgroundWorker = this._BackgroundWorker3;
				if (backgroundWorker != null)
				{
					backgroundWorker.DoWork += value2;
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000174C7 File Offset: 0x000156C7
		// (set) Token: 0x060002FC RID: 764 RVA: 0x000174D0 File Offset: 0x000156D0
		internal virtual Button Button1
		{
			[CompilerGenerated]
			get
			{
				return this._Button1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button1_Click_1);
				Button button = this._Button1;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button1 = value;
				button = this._Button1;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00017513 File Offset: 0x00015713
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0001751C File Offset: 0x0001571C
		internal virtual LinkLabel Changelog
		{
			[CompilerGenerated]
			get
			{
				return this._Changelog;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				LinkLabelLinkClickedEventHandler value2 = new LinkLabelLinkClickedEventHandler(this.Changelog_LinkClicked);
				LinkLabel changelog = this._Changelog;
				if (changelog != null)
				{
					changelog.LinkClicked -= value2;
				}
				this._Changelog = value;
				changelog = this._Changelog;
				if (changelog != null)
				{
					changelog.LinkClicked += value2;
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0001755F File Offset: 0x0001575F
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00017567 File Offset: 0x00015767
		internal virtual TabPage WindowsStore { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00017570 File Offset: 0x00015770
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00017578 File Offset: 0x00015778
		internal virtual Button SomethingHappened
		{
			[CompilerGenerated]
			get
			{
				return this._SomethingHappened;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SomethingHappened_Click);
				Button somethingHappened = this._SomethingHappened;
				if (somethingHappened != null)
				{
					somethingHappened.Click -= value2;
				}
				this._SomethingHappened = value;
				somethingHappened = this._SomethingHappened;
				if (somethingHappened != null)
				{
					somethingHappened.Click += value2;
				}
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000175BB File Offset: 0x000157BB
		// (set) Token: 0x06000304 RID: 772 RVA: 0x000175C3 File Offset: 0x000157C3
		internal virtual Label Label15 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000175CC File Offset: 0x000157CC
		// (set) Token: 0x06000306 RID: 774 RVA: 0x000175D4 File Offset: 0x000157D4
		internal virtual Button Button47
		{
			[CompilerGenerated]
			get
			{
				return this._Button47;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button47_Click);
				Button button = this._Button47;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button47 = value;
				button = this._Button47;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00017617 File Offset: 0x00015817
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00017620 File Offset: 0x00015820
		internal virtual Button TheApplicationsWasNotInstalled
		{
			[CompilerGenerated]
			get
			{
				return this._TheApplicationsWasNotInstalled;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TheApplicationsWasNotInstalled_Click);
				Button theApplicationsWasNotInstalled = this._TheApplicationsWasNotInstalled;
				if (theApplicationsWasNotInstalled != null)
				{
					theApplicationsWasNotInstalled.Click -= value2;
				}
				this._TheApplicationsWasNotInstalled = value;
				theApplicationsWasNotInstalled = this._TheApplicationsWasNotInstalled;
				if (theApplicationsWasNotInstalled != null)
				{
					theApplicationsWasNotInstalled.Click += value2;
				}
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00017663 File Offset: 0x00015863
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0001766C File Offset: 0x0001586C
		internal virtual Button Button49
		{
			[CompilerGenerated]
			get
			{
				return this._Button49;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button49_Click);
				Button button = this._Button49;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button49 = value;
				button = this._Button49;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000176AF File Offset: 0x000158AF
		// (set) Token: 0x0600030C RID: 780 RVA: 0x000176B7 File Offset: 0x000158B7
		internal virtual Label Label16 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000176C0 File Offset: 0x000158C0
		// (set) Token: 0x0600030E RID: 782 RVA: 0x000176C8 File Offset: 0x000158C8
		internal virtual Button ClearStoreCache
		{
			[CompilerGenerated]
			get
			{
				return this._ClearStoreCache;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ClearStoreCache_Click);
				Button clearStoreCache = this._ClearStoreCache;
				if (clearStoreCache != null)
				{
					clearStoreCache.Click -= value2;
				}
				this._ClearStoreCache = value;
				clearStoreCache = this._ClearStoreCache;
				if (clearStoreCache != null)
				{
					clearStoreCache.Click += value2;
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0001770B File Offset: 0x0001590B
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00017714 File Offset: 0x00015914
		internal virtual Button Button48
		{
			[CompilerGenerated]
			get
			{
				return this._Button48;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button48_Click);
				Button button = this._Button48;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button48 = value;
				button = this._Button48;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00017757 File Offset: 0x00015957
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0001775F File Offset: 0x0001595F
		internal virtual Label Label17 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00017768 File Offset: 0x00015968
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00017770 File Offset: 0x00015970
		internal virtual Button Button46
		{
			[CompilerGenerated]
			get
			{
				return this._Button46;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button46_Click);
				Button button = this._Button46;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button46 = value;
				button = this._Button46;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000177B3 File Offset: 0x000159B3
		// (set) Token: 0x06000316 RID: 790 RVA: 0x000177BC File Offset: 0x000159BC
		internal virtual Button MultipleEntriesFix2
		{
			[CompilerGenerated]
			get
			{
				return this._MultipleEntriesFix2;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.MultipleEntriesFix2_Click);
				Button multipleEntriesFix = this._MultipleEntriesFix2;
				if (multipleEntriesFix != null)
				{
					multipleEntriesFix.Click -= value2;
				}
				this._MultipleEntriesFix2 = value;
				multipleEntriesFix = this._MultipleEntriesFix2;
				if (multipleEntriesFix != null)
				{
					multipleEntriesFix.Click += value2;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000177FF File Offset: 0x000159FF
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00017808 File Offset: 0x00015A08
		internal virtual Button MultipleEntriesFix1
		{
			[CompilerGenerated]
			get
			{
				return this._MultipleEntriesFix1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.MultipleEntriesFix1_Click);
				Button multipleEntriesFix = this._MultipleEntriesFix1;
				if (multipleEntriesFix != null)
				{
					multipleEntriesFix.Click -= value2;
				}
				this._MultipleEntriesFix1 = value;
				multipleEntriesFix = this._MultipleEntriesFix1;
				if (multipleEntriesFix != null)
				{
					multipleEntriesFix.Click += value2;
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0001784B File Offset: 0x00015A4B
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00017853 File Offset: 0x00015A53
		internal virtual Label Label20 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0001785C File Offset: 0x00015A5C
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00017864 File Offset: 0x00015A64
		internal virtual TabPage QuickFixes { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0001786D File Offset: 0x00015A6D
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00017878 File Offset: 0x00015A78
		internal virtual Button ResetCatroot2Folder
		{
			[CompilerGenerated]
			get
			{
				return this._ResetCatroot2Folder;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetCatroot2Folder_Click);
				Button resetCatroot2Folder = this._ResetCatroot2Folder;
				if (resetCatroot2Folder != null)
				{
					resetCatroot2Folder.Click -= value2;
				}
				this._ResetCatroot2Folder = value;
				resetCatroot2Folder = this._ResetCatroot2Folder;
				if (resetCatroot2Folder != null)
				{
					resetCatroot2Folder.Click += value2;
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000178BB File Offset: 0x00015ABB
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000178C4 File Offset: 0x00015AC4
		internal virtual Button ResetGroupPolicy
		{
			[CompilerGenerated]
			get
			{
				return this._ResetGroupPolicy;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetGroupPolicy_Click);
				Button resetGroupPolicy = this._ResetGroupPolicy;
				if (resetGroupPolicy != null)
				{
					resetGroupPolicy.Click -= value2;
				}
				this._ResetGroupPolicy = value;
				resetGroupPolicy = this._ResetGroupPolicy;
				if (resetGroupPolicy != null)
				{
					resetGroupPolicy.Click += value2;
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00017907 File Offset: 0x00015B07
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00017910 File Offset: 0x00015B10
		internal virtual Button Button51
		{
			[CompilerGenerated]
			get
			{
				return this._Button51;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button51_Click);
				Button button = this._Button51;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button51 = value;
				button = this._Button51;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00017953 File Offset: 0x00015B53
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0001795C File Offset: 0x00015B5C
		internal virtual Button Button54
		{
			[CompilerGenerated]
			get
			{
				return this._Button54;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button54_Click);
				Button button = this._Button54;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button54 = value;
				button = this._Button54;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0001799F File Offset: 0x00015B9F
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000179A8 File Offset: 0x00015BA8
		internal virtual Button Button55
		{
			[CompilerGenerated]
			get
			{
				return this._Button55;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button55_Click);
				Button button = this._Button55;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button55 = value;
				button = this._Button55;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000179EB File Offset: 0x00015BEB
		// (set) Token: 0x06000328 RID: 808 RVA: 0x000179F4 File Offset: 0x00015BF4
		internal virtual Button ResetNotepad
		{
			[CompilerGenerated]
			get
			{
				return this._ResetNotepad;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetNotepad_Click);
				Button resetNotepad = this._ResetNotepad;
				if (resetNotepad != null)
				{
					resetNotepad.Click -= value2;
				}
				this._ResetNotepad = value;
				resetNotepad = this._ResetNotepad;
				if (resetNotepad != null)
				{
					resetNotepad.Click += value2;
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00017A37 File Offset: 0x00015C37
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00017A40 File Offset: 0x00015C40
		internal virtual Button Button50
		{
			[CompilerGenerated]
			get
			{
				return this._Button50;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button50_Click);
				Button button = this._Button50;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button50 = value;
				button = this._Button50;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00017A83 File Offset: 0x00015C83
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00017A8B File Offset: 0x00015C8B
		internal virtual GroupBox GroupBox3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00017A94 File Offset: 0x00015C94
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00017A9C File Offset: 0x00015C9C
		internal virtual Button Button56
		{
			[CompilerGenerated]
			get
			{
				return this._Button56;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button56_Click);
				Button button = this._Button56;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button56 = value;
				button = this._Button56;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00017ADF File Offset: 0x00015CDF
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00017AE8 File Offset: 0x00015CE8
		internal virtual Button Button57
		{
			[CompilerGenerated]
			get
			{
				return this._Button57;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button57_Click);
				Button button = this._Button57;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button57 = value;
				button = this._Button57;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00017B2B File Offset: 0x00015D2B
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00017B34 File Offset: 0x00015D34
		internal virtual Button Button58
		{
			[CompilerGenerated]
			get
			{
				return this._Button58;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button58_Click);
				Button button = this._Button58;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button58 = value;
				button = this._Button58;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00017B77 File Offset: 0x00015D77
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00017B80 File Offset: 0x00015D80
		internal virtual Button Button59
		{
			[CompilerGenerated]
			get
			{
				return this._Button59;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button59_Click);
				Button button = this._Button59;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button59 = value;
				button = this._Button59;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00017BC3 File Offset: 0x00015DC3
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00017BCC File Offset: 0x00015DCC
		internal virtual Button Button60
		{
			[CompilerGenerated]
			get
			{
				return this._Button60;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button60_Click);
				Button button = this._Button60;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button60 = value;
				button = this._Button60;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00017C0F File Offset: 0x00015E0F
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00017C18 File Offset: 0x00015E18
		internal virtual Button ResetDataUsage
		{
			[CompilerGenerated]
			get
			{
				return this._ResetDataUsage;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetDataUsage_Click);
				Button resetDataUsage = this._ResetDataUsage;
				if (resetDataUsage != null)
				{
					resetDataUsage.Click -= value2;
				}
				this._ResetDataUsage = value;
				resetDataUsage = this._ResetDataUsage;
				if (resetDataUsage != null)
				{
					resetDataUsage.Click += value2;
				}
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00017C5B File Offset: 0x00015E5B
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00017C64 File Offset: 0x00015E64
		internal virtual Button Button61
		{
			[CompilerGenerated]
			get
			{
				return this._Button61;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button61_Click);
				Button button = this._Button61;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button61 = value;
				button = this._Button61;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00017CA7 File Offset: 0x00015EA7
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00017CB0 File Offset: 0x00015EB0
		internal virtual Button ResetWMIRepository
		{
			[CompilerGenerated]
			get
			{
				return this._ResetWMIRepository;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetWMIRepository_Click);
				Button resetWMIRepository = this._ResetWMIRepository;
				if (resetWMIRepository != null)
				{
					resetWMIRepository.Click -= value2;
				}
				this._ResetWMIRepository = value;
				resetWMIRepository = this._ResetWMIRepository;
				if (resetWMIRepository != null)
				{
					resetWMIRepository.Click += value2;
				}
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00017CF3 File Offset: 0x00015EF3
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00017CFC File Offset: 0x00015EFC
		internal virtual Button Button62
		{
			[CompilerGenerated]
			get
			{
				return this._Button62;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button62_Click);
				Button button = this._Button62;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button62 = value;
				button = this._Button62;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00017D3F File Offset: 0x00015F3F
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00017D48 File Offset: 0x00015F48
		internal virtual Button Button69
		{
			[CompilerGenerated]
			get
			{
				return this._Button69;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button69_Click);
				Button button = this._Button69;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button69 = value;
				button = this._Button69;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00017D8B File Offset: 0x00015F8B
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00017D94 File Offset: 0x00015F94
		internal virtual Button ResetTCPQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetTCPQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetTCPQF_Click);
				Button resetTCPQF = this._ResetTCPQF;
				if (resetTCPQF != null)
				{
					resetTCPQF.Click -= value2;
				}
				this._ResetTCPQF = value;
				resetTCPQF = this._ResetTCPQF;
				if (resetTCPQF != null)
				{
					resetTCPQF.Click += value2;
				}
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00017DD7 File Offset: 0x00015FD7
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00017DE0 File Offset: 0x00015FE0
		internal virtual Button Button71
		{
			[CompilerGenerated]
			get
			{
				return this._Button71;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button71_Click);
				Button button = this._Button71;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button71 = value;
				button = this._Button71;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00017E23 File Offset: 0x00016023
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00017E2C File Offset: 0x0001602C
		internal virtual Button ResetDNSQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetDNSQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetDNSQF_Click);
				Button resetDNSQF = this._ResetDNSQF;
				if (resetDNSQF != null)
				{
					resetDNSQF.Click -= value2;
				}
				this._ResetDNSQF = value;
				resetDNSQF = this._ResetDNSQF;
				if (resetDNSQF != null)
				{
					resetDNSQF.Click += value2;
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00017E6F File Offset: 0x0001606F
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00017E78 File Offset: 0x00016078
		internal virtual Button Button65
		{
			[CompilerGenerated]
			get
			{
				return this._Button65;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button65_Click);
				Button button = this._Button65;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button65 = value;
				button = this._Button65;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00017EBB File Offset: 0x000160BB
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00017EC4 File Offset: 0x000160C4
		internal virtual Button ResetStoreCache
		{
			[CompilerGenerated]
			get
			{
				return this._ResetStoreCache;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetStoreCache_Click);
				Button resetStoreCache = this._ResetStoreCache;
				if (resetStoreCache != null)
				{
					resetStoreCache.Click -= value2;
				}
				this._ResetStoreCache = value;
				resetStoreCache = this._ResetStoreCache;
				if (resetStoreCache != null)
				{
					resetStoreCache.Click += value2;
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00017F07 File Offset: 0x00016107
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00017F10 File Offset: 0x00016110
		internal virtual Button Button67
		{
			[CompilerGenerated]
			get
			{
				return this._Button67;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button67_Click);
				Button button = this._Button67;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button67 = value;
				button = this._Button67;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00017F53 File Offset: 0x00016153
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00017F5C File Offset: 0x0001615C
		internal virtual Button ResetWinsockQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetWinsockQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetWinsockQF_Click);
				Button resetWinsockQF = this._ResetWinsockQF;
				if (resetWinsockQF != null)
				{
					resetWinsockQF.Click -= value2;
				}
				this._ResetWinsockQF = value;
				resetWinsockQF = this._ResetWinsockQF;
				if (resetWinsockQF != null)
				{
					resetWinsockQF.Click += value2;
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00017F9F File Offset: 0x0001619F
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00017FA8 File Offset: 0x000161A8
		internal virtual Button Button63
		{
			[CompilerGenerated]
			get
			{
				return this._Button63;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button63_Click);
				Button button = this._Button63;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button63 = value;
				button = this._Button63;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00017FEB File Offset: 0x000161EB
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00017FF4 File Offset: 0x000161F4
		internal virtual Button ResetRBQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetRBQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetRBQF_Click);
				Button resetRBQF = this._ResetRBQF;
				if (resetRBQF != null)
				{
					resetRBQF.Click -= value2;
				}
				this._ResetRBQF = value;
				resetRBQF = this._ResetRBQF;
				if (resetRBQF != null)
				{
					resetRBQF.Click += value2;
				}
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00018037 File Offset: 0x00016237
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00018040 File Offset: 0x00016240
		internal virtual Button Button64
		{
			[CompilerGenerated]
			get
			{
				return this._Button64;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button64_Click);
				Button button = this._Button64;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button64 = value;
				button = this._Button64;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00018083 File Offset: 0x00016283
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0001808C File Offset: 0x0001628C
		internal virtual Button ResetWinUpdateQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetWinUpdateQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetWinUpdateQF_Click);
				Button resetWinUpdateQF = this._ResetWinUpdateQF;
				if (resetWinUpdateQF != null)
				{
					resetWinUpdateQF.Click -= value2;
				}
				this._ResetWinUpdateQF = value;
				resetWinUpdateQF = this._ResetWinUpdateQF;
				if (resetWinUpdateQF != null)
				{
					resetWinUpdateQF.Click += value2;
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000180CF File Offset: 0x000162CF
		// (set) Token: 0x06000358 RID: 856 RVA: 0x000180D8 File Offset: 0x000162D8
		internal virtual Button Button68
		{
			[CompilerGenerated]
			get
			{
				return this._Button68;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button68_Click);
				Button button = this._Button68;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button68 = value;
				button = this._Button68;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001811B File Offset: 0x0001631B
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00018124 File Offset: 0x00016324
		internal virtual Button ResetSettingsQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetSettingsQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetSettingsQF_Click);
				Button resetSettingsQF = this._ResetSettingsQF;
				if (resetSettingsQF != null)
				{
					resetSettingsQF.Click -= value2;
				}
				this._ResetSettingsQF = value;
				resetSettingsQF = this._ResetSettingsQF;
				if (resetSettingsQF != null)
				{
					resetSettingsQF.Click += value2;
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00018167 File Offset: 0x00016367
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00018170 File Offset: 0x00016370
		internal virtual Button Button72
		{
			[CompilerGenerated]
			get
			{
				return this._Button72;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button72_Click);
				Button button = this._Button72;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button72 = value;
				button = this._Button72;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000181B3 File Offset: 0x000163B3
		// (set) Token: 0x0600035E RID: 862 RVA: 0x000181BC File Offset: 0x000163BC
		internal virtual Button ResetFirewallQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetFirewallQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetFirewallQF_Click);
				Button resetFirewallQF = this._ResetFirewallQF;
				if (resetFirewallQF != null)
				{
					resetFirewallQF.Click -= value2;
				}
				this._ResetFirewallQF = value;
				resetFirewallQF = this._ResetFirewallQF;
				if (resetFirewallQF != null)
				{
					resetFirewallQF.Click += value2;
				}
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000181FF File Offset: 0x000163FF
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00018208 File Offset: 0x00016408
		internal virtual Button Button74
		{
			[CompilerGenerated]
			get
			{
				return this._Button74;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button74_Click);
				Button button = this._Button74;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button74 = value;
				button = this._Button74;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0001824B File Offset: 0x0001644B
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00018254 File Offset: 0x00016454
		internal virtual Button ResetDefenderQF
		{
			[CompilerGenerated]
			get
			{
				return this._ResetDefenderQF;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetDefenderQF_Click);
				Button resetDefenderQF = this._ResetDefenderQF;
				if (resetDefenderQF != null)
				{
					resetDefenderQF.Click -= value2;
				}
				this._ResetDefenderQF = value;
				resetDefenderQF = this._ResetDefenderQF;
				if (resetDefenderQF != null)
				{
					resetDefenderQF.Click += value2;
				}
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00018297 File Offset: 0x00016497
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000182A0 File Offset: 0x000164A0
		internal virtual Button Button66
		{
			[CompilerGenerated]
			get
			{
				return this._Button66;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button66_Click);
				Button button = this._Button66;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button66 = value;
				button = this._Button66;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000182E3 File Offset: 0x000164E3
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000182EC File Offset: 0x000164EC
		internal virtual Button WindowsSandboxFailedToStart
		{
			[CompilerGenerated]
			get
			{
				return this._WindowsSandboxFailedToStart;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WindowsSandboxFailedToStart_Click);
				Button windowsSandboxFailedToStart = this._WindowsSandboxFailedToStart;
				if (windowsSandboxFailedToStart != null)
				{
					windowsSandboxFailedToStart.Click -= value2;
				}
				this._WindowsSandboxFailedToStart = value;
				windowsSandboxFailedToStart = this._WindowsSandboxFailedToStart;
				if (windowsSandboxFailedToStart != null)
				{
					windowsSandboxFailedToStart.Click += value2;
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001832F File Offset: 0x0001652F
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00018337 File Offset: 0x00016537
		internal virtual Label Label43 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00018340 File Offset: 0x00016540
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00018348 File Offset: 0x00016548
		internal virtual Button Button70
		{
			[CompilerGenerated]
			get
			{
				return this._Button70;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button70_Click);
				Button button = this._Button70;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button70 = value;
				button = this._Button70;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001838B File Offset: 0x0001658B
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00018394 File Offset: 0x00016594
		internal virtual Button WindowsUpdateSpecialError
		{
			[CompilerGenerated]
			get
			{
				return this._WindowsUpdateSpecialError;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WindowsUpdateSpecialError_Click);
				Button windowsUpdateSpecialError = this._WindowsUpdateSpecialError;
				if (windowsUpdateSpecialError != null)
				{
					windowsUpdateSpecialError.Click -= value2;
				}
				this._WindowsUpdateSpecialError = value;
				windowsUpdateSpecialError = this._WindowsUpdateSpecialError;
				if (windowsUpdateSpecialError != null)
				{
					windowsUpdateSpecialError.Click += value2;
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000183D7 File Offset: 0x000165D7
		// (set) Token: 0x0600036E RID: 878 RVA: 0x000183DF File Offset: 0x000165DF
		internal virtual Label Label73 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000183E8 File Offset: 0x000165E8
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000183F0 File Offset: 0x000165F0
		internal virtual Button Button73
		{
			[CompilerGenerated]
			get
			{
				return this._Button73;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button73_Click);
				Button button = this._Button73;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button73 = value;
				button = this._Button73;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00018433 File Offset: 0x00016633
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0001843C File Offset: 0x0001663C
		internal virtual Button TelnetIsNotRecognised
		{
			[CompilerGenerated]
			get
			{
				return this._TelnetIsNotRecognised;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TelnetIsNotRecognised_Click);
				Button telnetIsNotRecognised = this._TelnetIsNotRecognised;
				if (telnetIsNotRecognised != null)
				{
					telnetIsNotRecognised.Click -= value2;
				}
				this._TelnetIsNotRecognised = value;
				telnetIsNotRecognised = this._TelnetIsNotRecognised;
				if (telnetIsNotRecognised != null)
				{
					telnetIsNotRecognised.Click += value2;
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0001847F File Offset: 0x0001667F
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00018487 File Offset: 0x00016687
		internal virtual Label Label74 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00018490 File Offset: 0x00016690
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00018498 File Offset: 0x00016698
		internal virtual Button Button75
		{
			[CompilerGenerated]
			get
			{
				return this._Button75;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button75_Click);
				Button button = this._Button75;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button75 = value;
				button = this._Button75;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000184DB File Offset: 0x000166DB
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000184E4 File Offset: 0x000166E4
		internal virtual Button WslRegisterDistributionFailed
		{
			[CompilerGenerated]
			get
			{
				return this._WslRegisterDistributionFailed;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WslRegisterDistributionFailed_Click);
				Button wslRegisterDistributionFailed = this._WslRegisterDistributionFailed;
				if (wslRegisterDistributionFailed != null)
				{
					wslRegisterDistributionFailed.Click -= value2;
				}
				this._WslRegisterDistributionFailed = value;
				wslRegisterDistributionFailed = this._WslRegisterDistributionFailed;
				if (wslRegisterDistributionFailed != null)
				{
					wslRegisterDistributionFailed.Click += value2;
				}
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00018527 File Offset: 0x00016727
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0001852F File Offset: 0x0001672F
		internal virtual Label Label75 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00018538 File Offset: 0x00016738
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00018540 File Offset: 0x00016740
		internal virtual Button Button76
		{
			[CompilerGenerated]
			get
			{
				return this._Button76;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button76_Click);
				Button button = this._Button76;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button76 = value;
				button = this._Button76;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00018583 File Offset: 0x00016783
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0001858C File Offset: 0x0001678C
		internal virtual Button ResetSoftwareDistribution
		{
			[CompilerGenerated]
			get
			{
				return this._ResetSoftwareDistribution;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetSoftwareDistribution_Click);
				Button resetSoftwareDistribution = this._ResetSoftwareDistribution;
				if (resetSoftwareDistribution != null)
				{
					resetSoftwareDistribution.Click -= value2;
				}
				this._ResetSoftwareDistribution = value;
				resetSoftwareDistribution = this._ResetSoftwareDistribution;
				if (resetSoftwareDistribution != null)
				{
					resetSoftwareDistribution.Click += value2;
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000185CF File Offset: 0x000167CF
		// (set) Token: 0x06000380 RID: 896 RVA: 0x000185D7 File Offset: 0x000167D7
		internal virtual PictureBox TWC_Logo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000185E0 File Offset: 0x000167E0
		// (set) Token: 0x06000382 RID: 898 RVA: 0x000185E8 File Offset: 0x000167E8
		internal virtual PictureBox PictureBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000383 RID: 899 RVA: 0x000185F1 File Offset: 0x000167F1
		// (set) Token: 0x06000384 RID: 900 RVA: 0x000185FC File Offset: 0x000167FC
		internal virtual Button RecycleBinIsGreyedOut
		{
			[CompilerGenerated]
			get
			{
				return this._RecycleBinIsGreyedOut;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RecycleBinIsGreyedOut_Click);
				Button recycleBinIsGreyedOut = this._RecycleBinIsGreyedOut;
				if (recycleBinIsGreyedOut != null)
				{
					recycleBinIsGreyedOut.Click -= value2;
				}
				this._RecycleBinIsGreyedOut = value;
				recycleBinIsGreyedOut = this._RecycleBinIsGreyedOut;
				if (recycleBinIsGreyedOut != null)
				{
					recycleBinIsGreyedOut.Click += value2;
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001863F File Offset: 0x0001683F
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00018647 File Offset: 0x00016847
		internal virtual Label Label76 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00018650 File Offset: 0x00016850
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00018658 File Offset: 0x00016858
		internal virtual Button Button77
		{
			[CompilerGenerated]
			get
			{
				return this._Button77;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button77_Click);
				Button button = this._Button77;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button77 = value;
				button = this._Button77;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001869B File Offset: 0x0001689B
		// (set) Token: 0x0600038A RID: 906 RVA: 0x000186A4 File Offset: 0x000168A4
		internal virtual Button Button78
		{
			[CompilerGenerated]
			get
			{
				return this._Button78;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button78_Click);
				Button button = this._Button78;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button78 = value;
				button = this._Button78;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000186E7 File Offset: 0x000168E7
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000186F0 File Offset: 0x000168F0
		internal virtual Button BatteryRemainingTime
		{
			[CompilerGenerated]
			get
			{
				return this._BatteryRemainingTime;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.BatteryRemainingTime_Click);
				Button batteryRemainingTime = this._BatteryRemainingTime;
				if (batteryRemainingTime != null)
				{
					batteryRemainingTime.Click -= value2;
				}
				this._BatteryRemainingTime = value;
				batteryRemainingTime = this._BatteryRemainingTime;
				if (batteryRemainingTime != null)
				{
					batteryRemainingTime.Click += value2;
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00018733 File Offset: 0x00016933
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0001873B File Offset: 0x0001693B
		internal virtual Label Label77 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00018744 File Offset: 0x00016944
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0001874C File Offset: 0x0001694C
		internal virtual Button ResetThumbnailCache
		{
			[CompilerGenerated]
			get
			{
				return this._ResetThumbnailCache;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ResetThumbnailCache_Click);
				Button resetThumbnailCache = this._ResetThumbnailCache;
				if (resetThumbnailCache != null)
				{
					resetThumbnailCache.Click -= value2;
				}
				this._ResetThumbnailCache = value;
				resetThumbnailCache = this._ResetThumbnailCache;
				if (resetThumbnailCache != null)
				{
					resetThumbnailCache.Click += value2;
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001878F File Offset: 0x0001698F
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00018798 File Offset: 0x00016998
		internal virtual Button Button80
		{
			[CompilerGenerated]
			get
			{
				return this._Button80;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button80_Click);
				Button button = this._Button80;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button80 = value;
				button = this._Button80;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000187DB File Offset: 0x000169DB
		// (set) Token: 0x06000394 RID: 916 RVA: 0x000187E3 File Offset: 0x000169E3
		internal virtual Label Labelxyz { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000187EC File Offset: 0x000169EC
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000187F4 File Offset: 0x000169F4
		internal virtual Button ReRegisterAllDLLFiles
		{
			[CompilerGenerated]
			get
			{
				return this._ReRegisterAllDLLFiles;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ReRegisterAllDLLFiles_Click);
				Button reRegisterAllDLLFiles = this._ReRegisterAllDLLFiles;
				if (reRegisterAllDLLFiles != null)
				{
					reRegisterAllDLLFiles.Click -= value2;
				}
				this._ReRegisterAllDLLFiles = value;
				reRegisterAllDLLFiles = this._ReRegisterAllDLLFiles;
				if (reRegisterAllDLLFiles != null)
				{
					reRegisterAllDLLFiles.Click += value2;
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00018837 File Offset: 0x00016A37
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00018840 File Offset: 0x00016A40
		internal virtual Button Button81
		{
			[CompilerGenerated]
			get
			{
				return this._Button81;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button81_Click);
				Button button = this._Button81;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button81 = value;
				button = this._Button81;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00018883 File Offset: 0x00016A83
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0001888B File Offset: 0x00016A8B
		internal virtual Label Label78 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00018894 File Offset: 0x00016A94
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0001889C File Offset: 0x00016A9C
		internal virtual Button IssuesWithWindowsActivation
		{
			[CompilerGenerated]
			get
			{
				return this._IssuesWithWindowsActivation;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.IssuesWithWindowsActivation_Click);
				Button issuesWithWindowsActivation = this._IssuesWithWindowsActivation;
				if (issuesWithWindowsActivation != null)
				{
					issuesWithWindowsActivation.Click -= value2;
				}
				this._IssuesWithWindowsActivation = value;
				issuesWithWindowsActivation = this._IssuesWithWindowsActivation;
				if (issuesWithWindowsActivation != null)
				{
					issuesWithWindowsActivation.Click += value2;
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000188DF File Offset: 0x00016ADF
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000188E8 File Offset: 0x00016AE8
		internal virtual Button Button82
		{
			[CompilerGenerated]
			get
			{
				return this._Button82;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button82_Click);
				Button button = this._Button82;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button82 = value;
				button = this._Button82;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001892B File Offset: 0x00016B2B
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00018933 File Offset: 0x00016B33
		internal virtual Label Label79 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001893C File Offset: 0x00016B3C
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00018944 File Offset: 0x00016B44
		internal virtual Button WindowsSavingJPGsDownloadedAsJFIFs
		{
			[CompilerGenerated]
			get
			{
				return this._WindowsSavingJPGsDownloadedAsJFIFs;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.WindowsSavingJPGsDownloadedAsJFIFs_Click);
				Button windowsSavingJPGsDownloadedAsJFIFs = this._WindowsSavingJPGsDownloadedAsJFIFs;
				if (windowsSavingJPGsDownloadedAsJFIFs != null)
				{
					windowsSavingJPGsDownloadedAsJFIFs.Click -= value2;
				}
				this._WindowsSavingJPGsDownloadedAsJFIFs = value;
				windowsSavingJPGsDownloadedAsJFIFs = this._WindowsSavingJPGsDownloadedAsJFIFs;
				if (windowsSavingJPGsDownloadedAsJFIFs != null)
				{
					windowsSavingJPGsDownloadedAsJFIFs.Click += value2;
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00018987 File Offset: 0x00016B87
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00018990 File Offset: 0x00016B90
		internal virtual Button Button83
		{
			[CompilerGenerated]
			get
			{
				return this._Button83;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button83_Click);
				Button button = this._Button83;
				if (button != null)
				{
					button.Click -= value2;
				}
				this._Button83 = value;
				button = this._Button83;
				if (button != null)
				{
					button.Click += value2;
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000189D3 File Offset: 0x00016BD3
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x000189DB File Offset: 0x00016BDB
		internal virtual Label Label48 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x04000030 RID: 48
		public Color Grad1;

		// Token: 0x04000031 RID: 49
		public Color Grad2;

		// Token: 0x04000032 RID: 50
		public int GradAngle;

		// Token: 0x04000033 RID: 51
		public bool UseGradient;

		// Token: 0x04000034 RID: 52
		public List<string> xyz;

		// Token: 0x04000035 RID: 53
		public FixWinFormEventHandler eventHandler;

		// Token: 0x04000036 RID: 54
		private string space;

		// Token: 0x04000037 RID: 55
		private Locations Locations;

		// Token: 0x04000038 RID: 56
		private BatchFileProvider batchFileProvider;

		// Token: 0x04000039 RID: 57
		private FileHandler fileHandler;

		// Token: 0x0400003B RID: 59
		private SupportedScreenSizes SupportedScrnSizes;

		// Token: 0x0200001B RID: 27
		public enum DeviceCap
		{
			// Token: 0x040001A9 RID: 425
			DRIVERVERSION,
			// Token: 0x040001AA RID: 426
			TECHNOLOGY = 2,
			// Token: 0x040001AB RID: 427
			HORZSIZE = 4,
			// Token: 0x040001AC RID: 428
			VERTSIZE = 6,
			// Token: 0x040001AD RID: 429
			HORZRES = 8,
			// Token: 0x040001AE RID: 430
			VERTRES = 10,
			// Token: 0x040001AF RID: 431
			BITSPIXEL = 12,
			// Token: 0x040001B0 RID: 432
			PLANES = 14,
			// Token: 0x040001B1 RID: 433
			NUMBRUSHES = 16,
			// Token: 0x040001B2 RID: 434
			NUMPENS = 18,
			// Token: 0x040001B3 RID: 435
			NUMMARKERS = 20,
			// Token: 0x040001B4 RID: 436
			NUMFONTS = 22,
			// Token: 0x040001B5 RID: 437
			NUMCOLORS = 24,
			// Token: 0x040001B6 RID: 438
			PDEVICESIZE = 26,
			// Token: 0x040001B7 RID: 439
			CURVECAPS = 28,
			// Token: 0x040001B8 RID: 440
			LINECAPS = 30,
			// Token: 0x040001B9 RID: 441
			POLYGONALCAPS = 32,
			// Token: 0x040001BA RID: 442
			TEXTCAPS = 34,
			// Token: 0x040001BB RID: 443
			CLIPCAPS = 36,
			// Token: 0x040001BC RID: 444
			RASTERCAPS = 38,
			// Token: 0x040001BD RID: 445
			ASPECTX = 40,
			// Token: 0x040001BE RID: 446
			ASPECTY = 42,
			// Token: 0x040001BF RID: 447
			ASPECTXY = 44,
			// Token: 0x040001C0 RID: 448
			SHADEBLENDCAPS,
			// Token: 0x040001C1 RID: 449
			LOGPIXELSX = 88,
			// Token: 0x040001C2 RID: 450
			LOGPIXELSY = 90,
			// Token: 0x040001C3 RID: 451
			SIZEPALETTE = 104,
			// Token: 0x040001C4 RID: 452
			NUMRESERVED = 106,
			// Token: 0x040001C5 RID: 453
			COLORRES = 108,
			// Token: 0x040001C6 RID: 454
			PHYSICALWIDTH = 110,
			// Token: 0x040001C7 RID: 455
			PHYSICALHEIGHT,
			// Token: 0x040001C8 RID: 456
			PHYSICALOFFSETX,
			// Token: 0x040001C9 RID: 457
			PHYSICALOFFSETY,
			// Token: 0x040001CA RID: 458
			SCALINGFACTORX,
			// Token: 0x040001CB RID: 459
			SCALINGFACTORY,
			// Token: 0x040001CC RID: 460
			VREFRESH,
			// Token: 0x040001CD RID: 461
			DESKTOPVERTRES,
			// Token: 0x040001CE RID: 462
			DESKTOPHORZRES,
			// Token: 0x040001CF RID: 463
			BLTALIGNMENT
		}
	}
}
