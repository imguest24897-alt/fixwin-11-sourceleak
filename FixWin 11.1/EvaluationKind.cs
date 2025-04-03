using System;
using System.Management;
using System.Windows.Forms;

namespace FixWin
{
	// Token: 0x0200000E RID: 14
	public class EvaluationKind
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x00018C34 File Offset: 0x00016E34
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
	}
}
