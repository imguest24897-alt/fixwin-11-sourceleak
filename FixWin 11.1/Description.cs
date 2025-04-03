using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using FixWin.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace FixWin
{
	// Token: 0x0200000A RID: 10
	[DesignerGenerated]
	public partial class Description : Form
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002D31 File Offset: 0x00000F31
		public Description()
		{
			base.Load += this.Description_Load;
			this.InitializeComponent();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000316F File Offset: 0x0000136F
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00003177 File Offset: 0x00001377
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003180 File Offset: 0x00001380
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00003188 File Offset: 0x00001388
		internal virtual Label DescriptionText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003191 File Offset: 0x00001391
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003199 File Offset: 0x00001399
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000031A2 File Offset: 0x000013A2
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000031AC File Offset: 0x000013AC
		internal virtual Label CauseText
		{
			[CompilerGenerated]
			get
			{
				return this._CauseText;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.CauseText_MouseDoubleClick);
				Label causeText = this._CauseText;
				if (causeText != null)
				{
					causeText.MouseDoubleClick -= value2;
				}
				this._CauseText = value;
				causeText = this._CauseText;
				if (causeText != null)
				{
					causeText.MouseDoubleClick += value2;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000031EF File Offset: 0x000013EF
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000031F8 File Offset: 0x000013F8
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
				EventHandler value2 = new EventHandler(this.Button1_Click);
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000323B File Offset: 0x0000143B
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00003243 File Offset: 0x00001443
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000038 RID: 56 RVA: 0x0000324C File Offset: 0x0000144C
		private void Description_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000324E File Offset: 0x0000144E
		private void Button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003256 File Offset: 0x00001456
		private void CauseText_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			MyProject.Computer.Clipboard.SetText(this.CauseText.Text);
			Interaction.MsgBox("Copied to clipboard!", MsgBoxStyle.Information, "FixWin 10");
		}
	}
}
