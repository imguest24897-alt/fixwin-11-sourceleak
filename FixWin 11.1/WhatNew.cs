using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using FixWin.My.Resources;
using Microsoft.VisualBasic.CompilerServices;

namespace FixWin
{
	// Token: 0x02000013 RID: 19
	[DesignerGenerated]
	public partial class WhatNew : Form
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0001B2A0 File Offset: 0x000194A0
		public WhatNew()
		{
			base.Load += this.WhatNew_Load;
			base.MouseUp += this.WhatNew_MouseUp;
			base.MouseMove += this.WhatNew_MouseMove;
			base.MouseDown += this.WhatNew_MouseDown;
			this.InitializeComponent();
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001B621 File Offset: 0x00019821
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0001B62C File Offset: 0x0001982C
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001B66F File Offset: 0x0001986F
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0001B678 File Offset: 0x00019878
		internal virtual RichTextBox changeLog
		{
			[CompilerGenerated]
			get
			{
				return this._changeLog;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.changeLog_MouseDown);
				MouseEventHandler value3 = new MouseEventHandler(this.changeLog_MouseMove);
				MouseEventHandler value4 = new MouseEventHandler(this.changeLog_MouseUp);
				RichTextBox changeLog = this._changeLog;
				if (changeLog != null)
				{
					changeLog.MouseDown -= value2;
					changeLog.MouseMove -= value3;
					changeLog.MouseUp -= value4;
				}
				this._changeLog = value;
				changeLog = this._changeLog;
				if (changeLog != null)
				{
					changeLog.MouseDown += value2;
					changeLog.MouseMove += value3;
					changeLog.MouseUp += value4;
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001B6F1 File Offset: 0x000198F1
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0001B6F9 File Offset: 0x000198F9
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060003D1 RID: 977 RVA: 0x0001B702 File Offset: 0x00019902
		private void WhatNew_Load(object sender, EventArgs e)
		{
			this.changeLog.Text = Resources.Changelog;
			this.Button1.Focus();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000324E File Offset: 0x0000144E
		private void Button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001B720 File Offset: 0x00019920
		private void WhatNew_MouseUp(object sender, MouseEventArgs e)
		{
			this.drag = false;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001B72C File Offset: 0x0001992C
		private void WhatNew_MouseMove(object sender, MouseEventArgs e)
		{
			checked
			{
				if (this.drag)
				{
					base.Top = Cursor.Position.Y - this.mousey;
					base.Left = Cursor.Position.X - this.mousex;
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001B778 File Offset: 0x00019978
		private void WhatNew_MouseDown(object sender, MouseEventArgs e)
		{
			this.drag = true;
			checked
			{
				this.mousex = Cursor.Position.X - base.Left;
				this.mousey = Cursor.Position.Y - base.Top;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001B7C0 File Offset: 0x000199C0
		private void changeLog_MouseDown(object sender, MouseEventArgs e)
		{
			this.drag = true;
			checked
			{
				this.mousex = Cursor.Position.X - base.Left;
				this.mousey = Cursor.Position.Y - base.Top;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001B808 File Offset: 0x00019A08
		private void changeLog_MouseMove(object sender, MouseEventArgs e)
		{
			checked
			{
				if (this.drag)
				{
					base.Top = Cursor.Position.Y - this.mousey;
					base.Left = Cursor.Position.X - this.mousex;
				}
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001B720 File Offset: 0x00019920
		private void changeLog_MouseUp(object sender, MouseEventArgs e)
		{
			this.drag = false;
		}

		// Token: 0x0400019F RID: 415
		private bool drag;

		// Token: 0x040001A0 RID: 416
		private int mousex;

		// Token: 0x040001A1 RID: 417
		private int mousey;
	}
}
