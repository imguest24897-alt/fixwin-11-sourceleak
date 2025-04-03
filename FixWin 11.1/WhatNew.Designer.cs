namespace FixWin
{
	// Token: 0x02000013 RID: 19
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class WhatNew : global::System.Windows.Forms.Form
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0001B304 File Offset: 0x00019504
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001B344 File Offset: 0x00019544
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.Button1 = new global::System.Windows.Forms.Button();
			this.changeLog = new global::System.Windows.Forms.RichTextBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.Button1.FlatAppearance.BorderSize = 0;
			this.Button1.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.Red;
			this.Button1.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.DodgerBlue;
			this.Button1.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.Button1.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Button1.Location = new global::System.Drawing.Point(605, 13);
			this.Button1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Button1.Name = "Button1";
			this.Button1.Size = new global::System.Drawing.Size(32, 27);
			this.Button1.TabIndex = 1;
			this.Button1.Text = "X";
			this.Button1.UseVisualStyleBackColor = true;
			this.changeLog.BackColor = global::System.Drawing.SystemColors.Control;
			this.changeLog.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.changeLog.Location = new global::System.Drawing.Point(23, 66);
			this.changeLog.Name = "changeLog";
			this.changeLog.ReadOnly = true;
			this.changeLog.Size = new global::System.Drawing.Size(614, 376);
			this.changeLog.TabIndex = 2;
			this.changeLog.Text = "";
			this.changeLog.ZoomFactor = 1.2f;
			this.Label1.AutoSize = true;
			this.Label1.Font = new global::System.Drawing.Font("Segoe UI Light", 20.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label1.Location = new global::System.Drawing.Point(18, 13);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(149, 37);
			this.Label1.TabIndex = 3;
			this.Label1.Text = "What's New";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 17f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(660, 453);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.changeLog);
			base.Controls.Add(this.Button1);
			this.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "WhatNew";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WhatNew";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400019B RID: 411
		private global::System.ComponentModel.IContainer components;
	}
}
