using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using FixWin.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace FixWin
{
	// Token: 0x0200000F RID: 15
	[DesignerGenerated]
	public partial class Scan : Form
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00018CD4 File Offset: 0x00016ED4
		public Scan()
		{
			base.Load += this.Scan_Load;
			this.z = 1;
			this.InitializeComponent();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00018D3C File Offset: 0x00016F3C
		[DebuggerStepThrough]
		public void InitializeComponent()
		{
			this.components = new Container();
			this.ProgressBar1 = new ProgressBar();
			this.Panel1 = new Panel();
			this.Timer1 = new Timer(this.components);
			this.Close1 = new Button();
			this.Label1 = new Label();
			this.Label2 = new Label();
			this.Panel1.SuspendLayout();
			base.SuspendLayout();
			this.ProgressBar1.Location = new Point(73, 13);
			this.ProgressBar1.Margin = new Padding(3, 4, 3, 4);
			this.ProgressBar1.Name = "ProgressBar1";
			this.ProgressBar1.Size = new Size(668, 30);
			this.ProgressBar1.TabIndex = 0;
			this.Panel1.AutoScroll = true;
			this.Panel1.BorderStyle = BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.Label2);
			this.Panel1.Location = new Point(25, 80);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new Size(760, 284);
			this.Panel1.TabIndex = 2;
			this.Timer1.Interval = 700;
			this.Close1.Location = new Point(709, 368);
			this.Close1.Name = "Close1";
			this.Close1.Size = new Size(76, 32);
			this.Close1.TabIndex = 3;
			this.Close1.Text = "Close";
			this.Close1.UseVisualStyleBackColor = true;
			this.Label1.AutoSize = true;
			this.Label1.Location = new Point(25, 55);
			this.Label1.Name = "Label1";
			this.Label1.Size = new Size(169, 17);
			this.Label1.TabIndex = 4;
			this.Label1.Text = "Please wait while scanning...";
			this.Label2.AutoSize = true;
			this.Label2.ForeColor = SystemColors.ButtonShadow;
			this.Label2.Location = new Point(234, 121);
			this.Label2.Name = "Label2";
			this.Label2.Size = new Size(290, 17);
			this.Label2.TabIndex = 0;
			this.Label2.Text = "No error found! Your computer seems healthy :)";
			this.Label2.Visible = false;
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.ControlLightLight;
			base.ClientSize = new Size(813, 405);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.Close1);
			base.Controls.Add(this.Panel1);
			base.Controls.Add(this.ProgressBar1);
			this.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Margin = new Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			base.Name = "Scan";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Scan";
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000190D8 File Offset: 0x000172D8
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000190E0 File Offset: 0x000172E0
		internal virtual ProgressBar ProgressBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000190E9 File Offset: 0x000172E9
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000190F1 File Offset: 0x000172F1
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000190FA File Offset: 0x000172FA
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00019104 File Offset: 0x00017304
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00019147 File Offset: 0x00017347
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00019150 File Offset: 0x00017350
		internal virtual Button Close1
		{
			[CompilerGenerated]
			get
			{
				return this._Close1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Close_Click);
				Button close = this._Close1;
				if (close != null)
				{
					close.Click -= value2;
				}
				this._Close1 = value;
				close = this._Close1;
				if (close != null)
				{
					close.Click += value2;
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00019193 File Offset: 0x00017393
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0001919B File Offset: 0x0001739B
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000191A4 File Offset: 0x000173A4
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x000191AC File Offset: 0x000173AC
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060003B9 RID: 953 RVA: 0x000191B8 File Offset: 0x000173B8
		public void newButton_Click(object sender, EventArgs e)
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
				if (Conversions.ToDouble(((Button)sender).Name) != 1.0)
				{
					goto IL_B8;
				}
				IL_27:
				num2 = 3;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion", true).CreateSubKey("Policies").CreateSubKey("NonEnum").SetValue("{645FF040-5081-101B-9F08-00AA002F954E}", 0, RegistryValueKind.DWord);
				IL_68:
				num2 = 4;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true).CreateSubKey("HideDesktopIcons\\NewStartPanel").DeleteValue("{645FF040-5081-101B-9F08-00AA002F954E}", false);
				IL_99:
				num2 = 5;
				MyProject.Forms.Main_Form.Balloon();
				IL_AA:
				num2 = 6;
				((Button)sender).Enabled = false;
				IL_B8:
				num2 = 7;
				if (Conversions.ToDouble(((Button)sender).Name) != 2.0)
				{
					goto IL_1C0;
				}
				IL_D8:
				num2 = 8;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteValue("DisallowCpl", false);
				IL_109:
				num2 = 9;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteSubKeyTree("DisallowCpl", false);
				IL_13B:
				num2 = 10;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteValue("NoFolderOptions", false);
				IL_16D:
				num2 = 11;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").DeleteValue("NoFolderOptions", false);
				IL_19F:
				num2 = 12;
				MyProject.Forms.Main_Form.Balloon();
				IL_1B1:
				num2 = 13;
				((Button)sender).Enabled = false;
				IL_1C0:
				num2 = 14;
				if (Conversions.ToDouble(((Button)sender).Name) != 3.0)
				{
					goto IL_2C5;
				}
				IL_1E1:
				num2 = 15;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").SetValue("", "C:\\Windows\\System32\\imageres.dll,-54", RegistryValueKind.String);
				IL_222:
				num2 = 16;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").SetValue("empty", "C:\\Windows\\System32\\imageres.dll,-55", RegistryValueKind.String);
				IL_263:
				num2 = 17;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").SetValue("full", "C:\\Windows\\System32\\imageres.dll,-54", RegistryValueKind.String);
				IL_2A4:
				num2 = 18;
				MyProject.Forms.Main_Form.Balloon();
				IL_2B6:
				num2 = 19;
				((Button)sender).Enabled = false;
				IL_2C5:
				num2 = 20;
				if (Conversions.ToDouble(((Button)sender).Name) != 4.0)
				{
					goto IL_331;
				}
				IL_2E3:
				num2 = 21;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true).SetValue("Shell", "explorer.exe", RegistryValueKind.String);
				IL_310:
				num2 = 22;
				MyProject.Forms.Main_Form.Balloon();
				IL_322:
				num2 = 23;
				((Button)sender).Enabled = false;
				IL_331:
				num2 = 24;
				if (Conversions.ToDouble(((Button)sender).Name) != 5.0)
				{
					goto IL_39E;
				}
				IL_34F:
				num2 = 25;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true).SetValue("IconsOnly", 0, RegistryValueKind.DWord);
				IL_37D:
				num2 = 26;
				MyProject.Forms.Main_Form.Balloon();
				IL_38F:
				num2 = 27;
				((Button)sender).Enabled = false;
				IL_39E:
				num2 = 28;
				if (Conversions.ToDouble(((Button)sender).Name) != 6.0)
				{
					goto IL_40A;
				}
				IL_3BC:
				num2 = 29;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\SHOWALL", true).SetValue("CheckedValue", 1);
				IL_3E9:
				num2 = 30;
				MyProject.Forms.Main_Form.Balloon();
				IL_3FB:
				num2 = 31;
				((Button)sender).Enabled = false;
				IL_40A:
				num2 = 32;
				if (Conversions.ToDouble(((Button)sender).Name) != 7.0)
				{
					goto IL_485;
				}
				IL_428:
				num2 = 33;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft", true).CreateSubKey("Internet Explorer").CreateSubKey("Restrictions").DeleteValue("NoBrowserContextMenu", false);
				IL_464:
				num2 = 34;
				MyProject.Forms.Main_Form.Balloon();
				IL_476:
				num2 = 35;
				((Button)sender).Enabled = false;
				IL_485:
				num2 = 36;
				if (Conversions.ToDouble(((Button)sender).Name) != 8.0)
				{
					goto IL_54E;
				}
				IL_4A6:
				num2 = 37;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).SetValue("Disable Script Debugger", "yes", RegistryValueKind.String);
				IL_4D3:
				num2 = 38;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).SetValue("DisableScriptDebuggerIE", "yes", RegistryValueKind.String);
				IL_500:
				num2 = 39;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).SetValue("Error Dlg Displayed On Every Error", "no", RegistryValueKind.String);
				IL_52D:
				num2 = 40;
				MyProject.Forms.Main_Form.Balloon();
				IL_53F:
				num2 = 41;
				((Button)sender).Enabled = false;
				IL_54E:
				num2 = 42;
				if (Conversions.ToDouble(((Button)sender).Name) != 9.0)
				{
					goto IL_5EB;
				}
				IL_56C:
				num2 = 43;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true).SetValue("MaxConnectionsPer1_0Server", 10, RegistryValueKind.DWord);
				IL_59B:
				num2 = 44;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true).SetValue("MaxConnectionsPerServer", 10, RegistryValueKind.DWord);
				IL_5CA:
				num2 = 45;
				MyProject.Forms.Main_Form.Balloon();
				IL_5DC:
				num2 = 46;
				((Button)sender).Enabled = false;
				IL_5EB:
				num2 = 47;
				if (Conversions.ToDouble(((Button)sender).Name) != 10.0)
				{
					goto IL_662;
				}
				IL_609:
				num2 = 48;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows", true).CreateSubKey("SkyDrive").SetValue("DisableFileSync", 1, RegistryValueKind.DWord);
				IL_641:
				num2 = 49;
				MyProject.Forms.Main_Form.Balloon();
				IL_653:
				num2 = 50;
				((Button)sender).Enabled = false;
				IL_662:
				num2 = 51;
				if (Conversions.ToDouble(((Button)sender).Name) != 11.0)
				{
					goto IL_75B;
				}
				IL_683:
				num2 = 52;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true).CreateSubKey("Switcher").SetValue("CloseDWellTimeout", "300", RegistryValueKind.DWord);
				IL_6BA:
				num2 = 53;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true).CreateSubKey("Switcher").SetValue("MouseCloseThresholdPercent", "80", RegistryValueKind.DWord);
				IL_6F1:
				num2 = 54;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true).CreateSubKey("Switcher").SetValue("TouchCloseThresholdPercent", "80", RegistryValueKind.DWord);
				IL_728:
				num2 = 55;
				MyProject.Forms.Main_Form.RestartExplorer();
				IL_73A:
				num2 = 56;
				MyProject.Forms.Main_Form.Balloon();
				IL_74C:
				num2 = 57;
				((Button)sender).Enabled = false;
				IL_75B:
				num2 = 58;
				if (Conversions.ToDouble(((Button)sender).Name) != 12.0)
				{
					goto IL_7CC;
				}
				IL_779:
				num2 = 59;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("System").DeleteValue("DisableTaskMgr", false);
				IL_7AB:
				num2 = 60;
				MyProject.Forms.Main_Form.Balloon();
				IL_7BD:
				num2 = 61;
				((Button)sender).Enabled = false;
				IL_7CC:
				num2 = 62;
				if (Conversions.ToDouble(((Button)sender).Name) != 13.0)
				{
					goto IL_83D;
				}
				IL_7EA:
				num2 = 63;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft\\Windows", true).CreateSubKey("System").DeleteValue("DisableCMD", false);
				IL_81C:
				num2 = 64;
				MyProject.Forms.Main_Form.Balloon();
				IL_82E:
				num2 = 65;
				((Button)sender).Enabled = false;
				IL_83D:
				num2 = 66;
				if (Conversions.ToDouble(((Button)sender).Name) != 14.0)
				{
					goto IL_8AE;
				}
				IL_85B:
				num2 = 67;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("System").DeleteValue("DisableRegistryTools", false);
				IL_88D:
				num2 = 68;
				MyProject.Forms.Main_Form.Balloon();
				IL_89F:
				num2 = 69;
				((Button)sender).Enabled = false;
				IL_8AE:
				num2 = 70;
				if (Conversions.ToDouble(((Button)sender).Name) != 15.0)
				{
					goto IL_954;
				}
				IL_8CF:
				num2 = 71;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT", true).CreateSubKey("SystemRestore").DeleteValue("DisableConfig", false);
				IL_901:
				num2 = 72;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT", true).CreateSubKey("SystemRestore").DeleteValue("DisableSR", false);
				IL_933:
				num2 = 73;
				MyProject.Forms.Main_Form.Balloon();
				IL_945:
				num2 = 74;
				((Button)sender).Enabled = false;
				IL_954:
				num2 = 75;
				if (Conversions.ToDouble(((Button)sender).Name) != 16.0)
				{
					goto IL_9D2;
				}
				IL_972:
				num2 = 76;
				Interaction.Shell("net Start PlugPlay", AppWinStyle.Hide, true, -1);
				IL_983:
				num2 = 77;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\PlugPlay", true).SetValue("Start", 2, RegistryValueKind.DWord);
				IL_9B1:
				num2 = 78;
				MyProject.Forms.Main_Form.Balloon();
				IL_9C3:
				num2 = 79;
				((Button)sender).Enabled = false;
				IL_9D2:
				num2 = 80;
				if (Conversions.ToDouble(((Button)sender).Name) != 17.0)
				{
					goto IL_A3F;
				}
				IL_9F0:
				num2 = 81;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\StickyNotes", true).SetValue("PROMPT_ON_DELETE", 1, RegistryValueKind.DWord);
				IL_A1E:
				num2 = 82;
				MyProject.Forms.Main_Form.Balloon();
				IL_A30:
				num2 = 83;
				((Button)sender).Enabled = false;
				IL_A3F:
				num2 = 84;
				if (Conversions.ToDouble(((Button)sender).Name) != 18.0)
				{
					goto IL_AFE;
				}
				IL_A60:
				num2 = 85;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true).SetValue("Start_JumpListItems", 10, RegistryValueKind.DWord);
				IL_A8F:
				num2 = 86;
				MyProject.Computer.FileSystem.DeleteDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Recent\\AutomaticDestinations", DeleteDirectoryOption.DeleteAllContents);
				IL_AB6:
				num2 = 87;
				MyProject.Computer.FileSystem.DeleteDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Recent\\CustomDestinations", DeleteDirectoryOption.DeleteAllContents);
				IL_ADD:
				num2 = 88;
				MyProject.Forms.Main_Form.Balloon();
				IL_AEF:
				num2 = 89;
				((Button)sender).Enabled = false;
				IL_AFE:
				num2 = 90;
				if (Conversions.ToDouble(((Button)sender).Name) != 19.0)
				{
					goto IL_B65;
				}
				IL_B1C:
				num2 = 91;
				MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true).DeleteValue("TaskbarNoNotification", false);
				IL_B44:
				num2 = 92;
				MyProject.Forms.Main_Form.Balloon();
				IL_B56:
				num2 = 93;
				((Button)sender).Enabled = false;
				IL_B65:
				num2 = 94;
				if (Conversions.ToDouble(((Button)sender).Name) != 20.0)
				{
					goto IL_BE0;
				}
				IL_B83:
				num2 = 95;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft", true).CreateSubKey("Windows Script Host").CreateSubKey("Settings").DeleteValue("Enabled", false);
				IL_BBF:
				num2 = 96;
				MyProject.Forms.Main_Form.Balloon();
				IL_BD1:
				num2 = 97;
				((Button)sender).Enabled = false;
				IL_BE0:
				num2 = 98;
				if (Conversions.ToDouble(((Button)sender).Name) != 21.0)
				{
					goto IL_C56;
				}
				IL_BFE:
				num2 = 99;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true).CreateSubKey("Switcher").SetValue("BackStackMode", 1);
				IL_C35:
				num2 = 100;
				MyProject.Forms.Main_Form.Balloon();
				IL_C47:
				num2 = 101;
				((Button)sender).Enabled = false;
				IL_C56:
				num2 = 102;
				if (Conversions.ToDouble(((Button)sender).Name) != 22.0)
				{
					goto IL_CD3;
				}
				IL_C74:
				num2 = 103;
				MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\VSS", true).SetValue("Start", 2);
				IL_CA1:
				num2 = 104;
				Interaction.Shell("net start vss", AppWinStyle.Hide, false, -1);
				IL_CB2:
				num2 = 105;
				MyProject.Forms.Main_Form.Balloon();
				IL_CC4:
				num2 = 106;
				((Button)sender).Enabled = false;
				IL_CD3:
				goto IL_ED1;
				IL_CD8:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_E92:
				goto IL_EC6;
				IL_E94:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_EA4:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_E94;
			}
			IL_EC6:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_ED1:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001A0BC File Offset: 0x000182BC
		public void IsFix(string ErrorName)
		{
			int num;
			int num6;
			object obj;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				int num3 = this.z;
				int num4 = this.z;
				checked
				{
					for (int i = num3; i <= num4; i++)
					{
						IL_1D:
						num2 = 3;
						Button button = new Button();
						IL_26:
						num2 = 4;
						Label label = new Label();
						IL_2F:
						num2 = 5;
						button.Text = "Fix It!";
						IL_3D:
						num2 = 6;
						label.Text = ErrorName;
						IL_47:
						num2 = 7;
						this.Panel1.Controls.Add(button);
						IL_5B:
						num2 = 8;
						this.Panel1.Controls.Add(label);
						IL_6F:
						num2 = 9;
						Button button2 = button;
						IL_76:
						num2 = 10;
						button2.Name = Conversions.ToString(i);
						IL_87:
						num2 = 11;
						button2.Location = new Point(550, 34 * (this.p + 1) - 50);
						IL_A9:
						num2 = 12;
						button2.Size = (Size)new Point(75, 30);
						IL_C1:
						num2 = 13;
						button2.FlatStyle = FlatStyle.Standard;
						IL_CC:
						num2 = 14;
						button2.FlatAppearance.BorderColor = Color.Black;
						IL_E0:
						num2 = 15;
						button2.FlatAppearance.BorderSize = 3;
						IL_F0:
						button2 = null;
						IL_F3:
						num2 = 17;
						Label label2 = label;
						IL_FA:
						num2 = 18;
						label2.Location = new Point(30, 35 * (this.p + 1) - 46);
						IL_119:
						num2 = 19;
						label2.AutoSize = true;
						IL_124:
						label2 = null;
						IL_127:
						num2 = 21;
						button.Click += this.newButton_Click;
						IL_13D:
						num2 = 22;
					}
					IL_14E:
					goto IL_1FC;
					IL_153:;
				}
				int num5 = num6 + 1;
				num6 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num5);
				IL_1BD:
				goto IL_1F1;
				IL_1BF:
				num6 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_1CF:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num6 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_1BF;
			}
			IL_1F1:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_1FC:
			if (num6 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001A2EC File Offset: 0x000184EC
		public void Timer1_Tick(object sender, EventArgs e)
		{
			if (this.ProgressBar1.Value == 100)
			{
				this.Timer1.Stop();
				this.Label1.Text = "Scan results listed. You don’t have to follow all recommendations, just the ones you think may need to be fixed.";
				this.ScannedFixes();
				return;
			}
			ProgressBar progressBar;
			(progressBar = this.ProgressBar1).Value = checked(progressBar.Value + 50);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001A340 File Offset: 0x00018540
		public void Scan_Load(object sender, EventArgs e)
		{
			this.Timer1.Start();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000324E File Offset: 0x0000144E
		public void Close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001A350 File Offset: 0x00018550
		public void ScannedFixes()
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
				this.p = 1;
				IL_10:
				num2 = 3;
				if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true).CreateSubKey("HideDesktopIcons\\NewStartPanel").GetValue("{645FF040-5081-101B-9F08-00AA002F954E}"), 1, false))
				{
					goto IL_6A;
				}
				IL_4E:
				num2 = 4;
				this.IsFix("Recycle Bin icon is missing!");
				IL_5B:
				num2 = 5;
				ref int ptr = ref this.p;
				checked
				{
					this.p = ptr + 1;
					IL_6A:
					num2 = 6;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_79:
					num2 = 7;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("Explorer").GetValue("NoFolderOptions", 0), 1, false))
					{
						goto IL_DA;
					}
					IL_BD:
					num2 = 8;
					this.IsFix("Folder Options is missing from Control Panel.");
					IL_CA:
					num2 = 9;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_DA:
					num2 = 10;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_EA:
					num2 = 11;
					if (!Conversions.ToBoolean(Operators.OrObject(Operators.OrObject(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").GetValue("", ""), "C:\\Windows\\System32\\imageres.dll,-54", false)), Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").GetValue("empty", ""), "C:\\Windows\\System32\\imageres.dll,-55", false))), Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CLSID", true).CreateSubKey("{645FF040-5081-101B-9F08-00AA002F954E}").CreateSubKey("DefaultIcon").GetValue("full", ""), "C:\\Windows\\System32\\imageres.dll,-54", false)))))
					{
						goto IL_203;
					}
					IL_1E5:
					num2 = 12;
					this.IsFix("Recycle Bin icon doesn't refresh automatically.");
					IL_1F3:
					num2 = 13;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_203:
					num2 = 14;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_213:
					num2 = 15;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true).GetValue("Shell", ""), "explorer.exe", false))))
					{
						goto IL_274;
					}
					IL_256:
					num2 = 16;
					this.IsFix("Explorer doesn't start on startup");
					IL_264:
					num2 = 17;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_274:
					num2 = 18;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_284:
					num2 = 19;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true).GetValue("IconsOnly", ""), 0, false))))
					{
						goto IL_2E6;
					}
					IL_2C8:
					num2 = 20;
					this.IsFix("Thumbnails not showing in File Explorer");
					IL_2D6:
					num2 = 21;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_2E6:
					num2 = 22;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_2F6:
					num2 = 23;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\Hidden\\SHOWALL", true).GetValue("CheckedValue", 0), 1, false))))
					{
						goto IL_359;
					}
					IL_33B:
					num2 = 24;
					this.IsFix("\"Show hidden files, folders and drives\" option isn't shown in Folder Options");
					IL_349:
					num2 = 25;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_359:
					num2 = 26;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_369:
					num2 = 27;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft", true).CreateSubKey("Internet Explorer").CreateSubKey("Restrictions").GetValue("NoBrowserContextMenu", 0), 1, false))
					{
						goto IL_3D6;
					}
					IL_3B8:
					num2 = 28;
					this.IsFix("Right Click Context Menu of Internet Explorer is disabled");
					IL_3C6:
					num2 = 29;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_3D6:
					num2 = 30;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_3E6:
					num2 = 31;
					if (!Conversions.ToBoolean(Operators.OrObject(Operators.OrObject(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).GetValue("Disable Script Debugger", ""), "yes", false)), Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).GetValue("DisableScriptDebuggerIE", ""), "yes", false))), Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", true).GetValue("Error Dlg Displayed On Every Error", ""), "no", false)))))
					{
						goto IL_4C3;
					}
					IL_4A5:
					num2 = 32;
					this.IsFix("Runtime errors are appearing in Internet Explorer while surfing");
					IL_4B3:
					num2 = 33;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_4C3:
					num2 = 34;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_4D3:
					num2 = 35;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true).GetValue("MaxConnectionsPerServer", 0), 10, false))))
					{
						goto IL_537;
					}
					IL_519:
					num2 = 36;
					this.IsFix("Internet Explorer isn't optimised for Maximum Connections per server");
					IL_527:
					num2 = 37;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_537:
					num2 = 38;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_547:
					num2 = 39;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows", true).CreateSubKey("SkyDrive").GetValue("DisableFileSync", 0), 1, false))))
					{
						goto IL_5B4;
					}
					IL_596:
					num2 = 40;
					this.IsFix("OneDrive is enabled in background");
					IL_5A4:
					num2 = 41;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_5B4:
					num2 = 42;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_5C4:
					num2 = 43;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true).CreateSubKey("Switcher").GetValue("MouseCloseThresholdPercent", "20"), 80, false))))
					{
						goto IL_631;
					}
					IL_613:
					num2 = 44;
					this.IsFix("Closing Modern apps by drag and drop is very slow");
					IL_621:
					num2 = 45;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_631:
					num2 = 46;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_641:
					num2 = 47;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("System").GetValue("DisableTaskMgr", 0), 1, false))
					{
						goto IL_6A4;
					}
					IL_686:
					num2 = 48;
					this.IsFix("Task Manager has been disabled!");
					IL_694:
					num2 = 49;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_6A4:
					num2 = 50;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_6B4:
					num2 = 51;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Policies\\Microsoft\\Windows", true).CreateSubKey("System").GetValue("DisableCMD", 0), 1, false))
					{
						goto IL_717;
					}
					IL_6F9:
					num2 = 52;
					this.IsFix("Command Prompt has been disabled!");
					IL_707:
					num2 = 53;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_717:
					num2 = 54;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_727:
					num2 = 55;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies", true).CreateSubKey("System").GetValue("DisableRegistryTools", 0), 1, false))
					{
						goto IL_78A;
					}
					IL_76C:
					num2 = 56;
					this.IsFix("Registry Editor has been disabled!");
					IL_77A:
					num2 = 57;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_78A:
					num2 = 58;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_79A:
					num2 = 59;
					if (!Conversions.ToBoolean(Operators.OrObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT", true).CreateSubKey("SystemRestore").GetValue("DisableConfig", 0), 1, false), Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows NT", true).CreateSubKey("SystemRestore").GetValue("DisableSR", 0), 1, false))))
					{
						goto IL_847;
					}
					IL_829:
					num2 = 60;
					this.IsFix("System Restore has been disabled!");
					IL_837:
					num2 = 61;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_847:
					num2 = 62;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_857:
					num2 = 63;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\PlugPlay", true).GetValue("Start", 2), 2, false))))
					{
						goto IL_8BA;
					}
					IL_89C:
					num2 = 64;
					this.IsFix("Device Manager mayn't be detecting new devices since PlugPlay service isn't automatic");
					IL_8AA:
					num2 = 65;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_8BA:
					num2 = 66;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_8CA:
					num2 = 67;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\StickyNotes", true).GetValue("PROMPT_ON_DELETE", 1), 1, false))))
					{
						goto IL_92D;
					}
					IL_90F:
					num2 = 68;
					this.IsFix("Sticky Notes warning dialog box is not shown");
					IL_91D:
					num2 = 69;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_92D:
					num2 = 70;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_93D:
					num2 = 71;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true).GetValue("Start_JumpListItems", 10), 10, false))))
					{
						goto IL_9A2;
					}
					IL_984:
					num2 = 72;
					this.IsFix("Taskbar jumplists mayn't be shown properly");
					IL_992:
					num2 = 73;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_9A2:
					num2 = 74;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_9B2:
					num2 = 75;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true).GetValue("TaskbarNoNotification", 0), 1, false))
					{
						goto IL_A0B;
					}
					IL_9ED:
					num2 = 76;
					this.IsFix("Balloon tips aren't shown in notification area");
					IL_9FB:
					num2 = 77;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_A0B:
					num2 = 78;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_A1B:
					num2 = 79;
					if (!Operators.ConditionalCompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft", true).CreateSubKey("Windows Script Host").CreateSubKey("Settings").GetValue("Enabled", 1), 0, false))
					{
						goto IL_A88;
					}
					IL_A6A:
					num2 = 80;
					this.IsFix("Windows Script Host access is disabled");
					IL_A78:
					num2 = 81;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_A88:
					num2 = 82;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_A98:
					num2 = 83;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", true).CreateSubKey("Switcher").GetValue("BackStackMode", 1), 1, false))))
					{
						goto IL_B05;
					}
					IL_AE7:
					num2 = 84;
					this.IsFix("App Switcher isn't being displayed correctly");
					IL_AF5:
					num2 = 85;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_B05:
					num2 = 86;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_B15:
					num2 = 87;
					if (!Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(MyProject.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\VSS", true).GetValue("Start", 2), 2, false))))
					{
						goto IL_B78;
					}
					IL_B5A:
					num2 = 88;
					this.IsFix("You may face some errors while creating system image. Error Code: 0x8004230c");
					IL_B68:
					num2 = 89;
					ptr = ref this.p;
					this.p = ptr + 1;
					IL_B78:
					num2 = 90;
					ptr = ref this.z;
					this.z = ptr + 1;
					IL_B88:
					num2 = 91;
					if (this.p != 1)
					{
						goto IL_BA3;
					}
					IL_B94:
					num2 = 92;
					this.Label2.Visible = true;
					IL_BA3:
					goto IL_D69;
					IL_BA8:;
				}
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_D2A:
				goto IL_D5E;
				IL_D2C:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_D3C:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_D2C;
			}
			IL_D5E:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_D69:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x04000196 RID: 406
		private int z;

		// Token: 0x04000197 RID: 407
		private int p;
	}
}
