namespace FixWin
{
	// Token: 0x0200000A RID: 10
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class Description : global::System.Windows.Forms.Form
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002D54 File Offset: 0x00000F54
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

		// Token: 0x0600002B RID: 43 RVA: 0x00002D94 File Offset: 0x00000F94
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.Label1 = new global::System.Windows.Forms.Label();
			this.DescriptionText = new global::System.Windows.Forms.Label();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.CauseText = new global::System.Windows.Forms.Label();
			this.Button1 = new global::System.Windows.Forms.Button();
			this.Label2 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.Label1.AutoSize = true;
			this.Label1.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label1.Location = new global::System.Drawing.Point(12, 15);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(83, 17);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Description:";
			this.DescriptionText.Location = new global::System.Drawing.Point(103, 15);
			this.DescriptionText.Name = "DescriptionText";
			this.DescriptionText.Size = new global::System.Drawing.Size(533, 76);
			this.DescriptionText.TabIndex = 1;
			this.DescriptionText.Text = "Label2";
			this.Label3.AutoSize = true;
			this.Label3.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label3.Location = new global::System.Drawing.Point(15, 115);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(58, 17);
			this.Label3.TabIndex = 2;
			this.Label3.Text = "Manual:";
			this.CauseText.Location = new global::System.Drawing.Point(103, 115);
			this.CauseText.Name = "CauseText";
			this.CauseText.Size = new global::System.Drawing.Size(533, 194);
			this.CauseText.TabIndex = 1;
			this.CauseText.Text = "Label2";
			this.Button1.Location = new global::System.Drawing.Point(560, 326);
			this.Button1.Name = "Button1";
			this.Button1.Size = new global::System.Drawing.Size(92, 29);
			this.Button1.TabIndex = 3;
			this.Button1.Text = "Close";
			this.Button1.UseVisualStyleBackColor = true;
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(18, 340);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(294, 17);
			this.Label2.TabIndex = 4;
			this.Label2.Text = "Tip: Double click the Manual instructions to copy!";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 17f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = global::System.Drawing.Color.White;
			base.ClientSize = new global::System.Drawing.Size(664, 366);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Button1);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.CauseText);
			base.Controls.Add(this.DescriptionText);
			base.Controls.Add(this.Label1);
			this.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			base.Name = "Description";
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Description";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000029 RID: 41
		private global::System.ComponentModel.IContainer components;
	}
}
