using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace FixWin
{
	// Token: 0x02000009 RID: 9
	public class ComputerHardwareInfo
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000235D File Offset: 0x0000055D
		public ComputerHardwareInfo()
		{
			this.bgw = new BackgroundWorker();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002370 File Offset: 0x00000570
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000023A8 File Offset: 0x000005A8
		public event ComputerHardwareInfo.OperationCompletedEventHandler OperationCompleted;

		// Token: 0x0600001E RID: 30 RVA: 0x000023E0 File Offset: 0x000005E0
		protected virtual void OnCompleted()
		{
			ComputerHardwareInfo.OperationCompletedEventHandler operationCompletedEvent = this.OperationCompletedEvent;
			if (operationCompletedEvent != null)
			{
				operationCompletedEvent(this, new ComputerHardwareInfo.OperationCompletedEventArgs());
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002404 File Offset: 0x00000604
		public bool OperationCurrentlyBusy
		{
			get
			{
				return this.bgw == null || this.bgw.IsBusy;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000242C File Offset: 0x0000062C
		public void GetHardwareInfo()
		{
			try
			{
				if (this.OperationCurrentlyBusy)
				{
					throw new ArgumentException("The operation is currently busy.");
				}
				this.Dispose();
				this.bgw = new BackgroundWorker
				{
					WorkerSupportsCancellation = true
				};
				ComputerHardwareInfo._operationSW = new Stopwatch();
				ComputerHardwareInfo._operationSW.Start();
				this.bgw.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024A0 File Offset: 0x000006A0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000024A8 File Offset: 0x000006A8
		private virtual BackgroundWorker bgw
		{
			[CompilerGenerated]
			get
			{
				return this._bgw;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				DoWorkEventHandler value2 = new DoWorkEventHandler(this.bgw_DoWork);
				RunWorkerCompletedEventHandler value3 = new RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
				BackgroundWorker bgw = this._bgw;
				if (bgw != null)
				{
					bgw.DoWork -= value2;
					bgw.RunWorkerCompleted -= value3;
				}
				this._bgw = value;
				bgw = this._bgw;
				if (bgw != null)
				{
					bgw.DoWork += value2;
					bgw.RunWorkerCompleted += value3;
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002508 File Offset: 0x00000708
		private void bgw_DoWork(object sender, DoWorkEventArgs e)
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
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
				IL_14:
				num2 = 3;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator();
				while (enumerator.MoveNext())
				{
					ManagementBaseObject managementBaseObject = enumerator.Current;
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					IL_33:
					num2 = 4;
					ComputerHardwareInfo._oSName = managementObject["name"].ToString();
					IL_4B:
					num2 = 5;
					ComputerHardwareInfo._oSVersion = managementObject["version"].ToString();
					IL_63:
					num2 = 6;
					ComputerHardwareInfo._computerName = managementObject["csname"].ToString();
					IL_7B:
					num2 = 7;
					ComputerHardwareInfo._windowsDir = managementObject["windowsdirectory"].ToString();
					IL_93:
					num2 = 8;
				}
				IL_9E:
				num2 = 9;
				if (enumerator != null)
				{
					((IDisposable)enumerator).Dispose();
				}
				IL_AC:
				num2 = 10;
				ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
				IL_BB:
				num2 = 11;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator2 = managementObjectSearcher2.Get().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					ManagementBaseObject managementBaseObject2 = enumerator2.Current;
					ManagementObject managementObject2 = (ManagementObject)managementBaseObject2;
					IL_DC:
					num2 = 12;
					ComputerHardwareInfo._RefreshRate = managementObject2["MaxRefreshRate"].ToString();
					IL_F5:
					num2 = 13;
					ComputerHardwareInfo._GraphicsCard = managementObject2["Caption"].ToString();
					IL_10E:
					num2 = 14;
				}
				IL_11A:
				num2 = 15;
				if (enumerator2 != null)
				{
					((IDisposable)enumerator2).Dispose();
				}
				IL_128:
				num2 = 16;
				ManagementObjectSearcher managementObjectSearcher3 = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor");
				IL_137:
				num2 = 17;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator3 = managementObjectSearcher3.Get().GetEnumerator();
				while (enumerator3.MoveNext())
				{
					ManagementBaseObject managementBaseObject3 = enumerator3.Current;
					ManagementObject managementObject3 = (ManagementObject)managementBaseObject3;
					IL_158:
					num2 = 18;
					ComputerHardwareInfo._MonitorType = managementObject3["Caption"].ToString();
					IL_171:
					num2 = 19;
				}
				IL_17D:
				num2 = 20;
				if (enumerator3 != null)
				{
					((IDisposable)enumerator3).Dispose();
				}
				IL_18B:
				num2 = 21;
				ManagementObjectSearcher managementObjectSearcher4 = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
				IL_19A:
				num2 = 22;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator4 = managementObjectSearcher4.Get().GetEnumerator();
				while (enumerator4.MoveNext())
				{
					ManagementBaseObject managementBaseObject4 = enumerator4.Current;
					ManagementObject managementObject4 = (ManagementObject)managementBaseObject4;
					IL_1BE:
					num2 = 23;
					ComputerHardwareInfo._computerManufacturer = managementObject4["manufacturer"].ToString();
					IL_1D7:
					num2 = 24;
					ComputerHardwareInfo._oSName = managementObject4["model"].ToString();
					IL_1F0:
					num2 = 25;
					ComputerHardwareInfo._computerSystemType = managementObject4["systemtype"].ToString();
					IL_209:
					num2 = 26;
					long computerPhysicalMemory_Bytes;
					if (!long.TryParse(managementObject4["totalphysicalmemory"].ToString(), out computerPhysicalMemory_Bytes))
					{
						IL_226:
						num2 = 27;
						ComputerHardwareInfo._computerPhysicalMemory_Bytes = 0L;
					}
					else
					{
						IL_232:
						num2 = 29;
						ComputerHardwareInfo._computerPhysicalMemory_Bytes = computerPhysicalMemory_Bytes;
					}
					IL_23C:
					num2 = 30;
				}
				IL_24B:
				num2 = 31;
				if (enumerator4 != null)
				{
					((IDisposable)enumerator4).Dispose();
				}
				IL_259:
				num2 = 32;
				ManagementObjectSearcher managementObjectSearcher5 = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
				IL_268:
				num2 = 33;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator5 = managementObjectSearcher5.Get().GetEnumerator();
				while (enumerator5.MoveNext())
				{
					ManagementBaseObject managementBaseObject5 = enumerator5.Current;
					ManagementObject managementObject5 = (ManagementObject)managementBaseObject5;
					IL_28C:
					num2 = 34;
					ComputerHardwareInfo._processorName = managementObject5["name"].ToString();
					IL_2A5:
					num2 = 35;
					ComputerHardwareInfo._processorID = managementObject5["processorID"].ToString();
					IL_2BE:
					num2 = 36;
					ComputerHardwareInfo._processorCores = managementObject5["NumberOfCores"].ToString();
					IL_2D7:
					num2 = 37;
					ComputerHardwareInfo._processorCurrentClockSpeed = managementObject5["CurrentClockSpeed"].ToString();
					IL_2F0:
					num2 = 38;
					ComputerHardwareInfo._processorMaxClockSpeed = managementObject5["MaxClockSpeed"].ToString();
					IL_309:
					num2 = 39;
					ComputerHardwareInfo._processorThreads = managementObject5["ThreadCount"].ToString();
					IL_322:
					num2 = 40;
					ComputerHardwareInfo._processorLogicalProc = managementObject5["NumberOfLogicalProcessors"].ToString();
					IL_33B:
					num2 = 41;
				}
				IL_34A:
				num2 = 42;
				if (enumerator5 != null)
				{
					((IDisposable)enumerator5).Dispose();
				}
				IL_358:
				num2 = 43;
				ManagementObjectSearcher managementObjectSearcher6 = new ManagementObjectSearcher("Select * FROM Win32_PhysicalMemory");
				IL_367:
				num2 = 44;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator6 = managementObjectSearcher6.Get().GetEnumerator();
				while (enumerator6.MoveNext())
				{
					ManagementBaseObject managementBaseObject6 = enumerator6.Current;
					ManagementObject managementObject6 = (ManagementObject)managementBaseObject6;
					IL_388:
					num2 = 45;
					ComputerHardwareInfo._totalRAM = managementObject6["Capacity"].ToString();
					IL_3A1:
					num2 = 46;
					ComputerHardwareInfo._speedRAM = managementObject6["Speed"].ToString();
					IL_3BA:
					num2 = 47;
				}
				IL_3C6:
				num2 = 48;
				if (enumerator6 != null)
				{
					((IDisposable)enumerator6).Dispose();
				}
				IL_3D4:
				num2 = 49;
				ManagementObjectSearcher managementObjectSearcher7 = new ManagementObjectSearcher("Select Name from Win32_Bios");
				IL_3E3:
				num2 = 50;
				ManagementObjectCollection.ManagementObjectEnumerator enumerator7 = managementObjectSearcher7.Get().GetEnumerator();
				while (enumerator7.MoveNext())
				{
					ManagementBaseObject managementBaseObject7 = enumerator7.Current;
					ManagementObject managementObject7 = (ManagementObject)managementBaseObject7;
					IL_404:
					num2 = 51;
					ComputerHardwareInfo._biosData = managementObject7.GetPropertyValue("Name").ToString();
					IL_41D:
					num2 = 52;
				}
				IL_429:
				num2 = 53;
				if (enumerator7 != null)
				{
					((IDisposable)enumerator7).Dispose();
				}
				IL_437:
				num2 = 54;
				ComputerHardwareInfo._mAC_Address = this.GetMACAddress();
				IL_445:
				num2 = 55;
				int fixedDriveCount = this.GetFixedDriveCount();
				IL_450:
				num2 = 56;
				if (fixedDriveCount <= -1)
				{
					goto IL_462;
				}
				IL_458:
				num2 = 57;
				ComputerHardwareInfo._fixedDriveCount = fixedDriveCount;
				IL_462:
				goto IL_59C;
				IL_467:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_55D:
				goto IL_591;
				IL_55F:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_56F:;
			}
			catch when (endfilter(obj is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj2;
				goto IL_55F;
			}
			IL_591:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_59C:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ComputerHardwareInfo._operationSW.Stop();
			this.OnCompleted();
			this.Dispose();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public string GetMACAddress()
		{
			string result;
			try
			{
				ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection instances = managementClass.GetInstances();
				string text = "";
				try
				{
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (Operators.CompareString(text, "", false) == 0 && Conversions.ToBoolean(managementObject["IPEnabled"]))
						{
							text = managementObject["MacAddress"].ToString();
							managementObject.Dispose();
							break;
						}
						managementObject.Dispose();
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
				instances.Dispose();
				managementClass.Dispose();
				text = text.Replace(":", "");
				result = text;
			}
			catch (Exception ex)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002BDC File Offset: 0x00000DDC
		private int GetFixedDriveCount()
		{
			int num = 0;
			checked
			{
				try
				{
					foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
					{
						if (driveInfo.IsReady && driveInfo.DriveType == DriveType.Fixed)
						{
							num++;
						}
					}
				}
				catch (Exception ex)
				{
					num = -1;
				}
				return num;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C3C File Offset: 0x00000E3C
		private static string GetFileSizeString(long bytes)
		{
			string result;
			if (bytes < 1024L)
			{
				result = string.Format("{0:n0} bytes", bytes);
			}
			else if (bytes < 1048576L)
			{
				result = string.Format("{0:n0} kB", (double)bytes / 1024.0);
			}
			else if (bytes >= 1048576L && bytes < 1073741824L)
			{
				result = string.Format("{0:n1} Megs", (double)bytes / 1048576.0);
			}
			else if (bytes >= 1073741824L && bytes < 1099511627776L)
			{
				result = string.Format("{0:n2} Gigs", (double)bytes / 1073741824.0);
			}
			else
			{
				result = string.Format("{0:n1} TeraBytes", (double)bytes / 1099511627776.0);
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002D15 File Offset: 0x00000F15
		private void Dispose()
		{
			if (this.bgw != null)
			{
				this.bgw.Dispose();
				this.bgw = null;
			}
		}

		// Token: 0x0400000F RID: 15
		private static Exception _operationException;

		// Token: 0x04000010 RID: 16
		private static Stopwatch _operationSW;

		// Token: 0x04000011 RID: 17
		private static string _oSName = "--";

		// Token: 0x04000012 RID: 18
		private static string _oSVersion = "--";

		// Token: 0x04000013 RID: 19
		private static string _windowsDir = "--";

		// Token: 0x04000014 RID: 20
		private static string _computerName = "--";

		// Token: 0x04000015 RID: 21
		private static string _computerManufacturer = "--";

		// Token: 0x04000016 RID: 22
		private static string _computerModel = "--";

		// Token: 0x04000017 RID: 23
		private static string _computerSystemType = "--";

		// Token: 0x04000018 RID: 24
		private static long _computerPhysicalMemory_Bytes;

		// Token: 0x04000019 RID: 25
		private static string _processorName = "--";

		// Token: 0x0400001A RID: 26
		private static string _processorID = "--";

		// Token: 0x0400001B RID: 27
		private static string _processorCores = "--";

		// Token: 0x0400001C RID: 28
		private static string _processorCurrentClockSpeed = "--";

		// Token: 0x0400001D RID: 29
		private static string _processorMaxClockSpeed = "--";

		// Token: 0x0400001E RID: 30
		private static string _processorThreads = "--";

		// Token: 0x0400001F RID: 31
		private static string _processorLogicalProc = "--";

		// Token: 0x04000020 RID: 32
		private static string _totalRAM = "--";

		// Token: 0x04000021 RID: 33
		private static string _freeRAM = "--";

		// Token: 0x04000022 RID: 34
		private static string _speedRAM = "--";

		// Token: 0x04000023 RID: 35
		private static string _RefreshRate = "--";

		// Token: 0x04000024 RID: 36
		private static string _GraphicsCard = "--";

		// Token: 0x04000025 RID: 37
		private static string _MonitorType = "--";

		// Token: 0x04000026 RID: 38
		private static string _mAC_Address = "--";

		// Token: 0x04000027 RID: 39
		private static string _biosData = "--";

		// Token: 0x04000028 RID: 40
		private static int _fixedDriveCount;

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x060003F4 RID: 1012
		public delegate void OperationCompletedEventHandler(object sender, ComputerHardwareInfo.OperationCompletedEventArgs e);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060003F8 RID: 1016
		private delegate string GetMACAddressDelegate();

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x060003FC RID: 1020
		private delegate int GetFixedDriveCountDelegate();

		// Token: 0x0200001A RID: 26
		public class OperationCompletedEventArgs : EventArgs
		{
			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001BAEA File Offset: 0x00019CEA
			public Exception OperationException
			{
				get
				{
					return ComputerHardwareInfo._operationException;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001BAF1 File Offset: 0x00019CF1
			public TimeSpan Elapsed
			{
				get
				{
					return ComputerHardwareInfo._operationSW.Elapsed;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001BB00 File Offset: 0x00019D00
			public string Elapsed_String
			{
				get
				{
					return string.Format("{0:00}:{1:00}", ComputerHardwareInfo._operationSW.Elapsed.TotalMinutes, ComputerHardwareInfo._operationSW.Elapsed.TotalSeconds);
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001BB45 File Offset: 0x00019D45
			public string OSName
			{
				get
				{
					return ComputerHardwareInfo._oSName;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001BB4C File Offset: 0x00019D4C
			public string OSVersion
			{
				get
				{
					return ComputerHardwareInfo._oSVersion;
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001BB53 File Offset: 0x00019D53
			public string WindowsDirectory
			{
				get
				{
					return ComputerHardwareInfo._windowsDir;
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001BB5A File Offset: 0x00019D5A
			public string ComputerName
			{
				get
				{
					return ComputerHardwareInfo._computerName;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001BB61 File Offset: 0x00019D61
			public string ComputerManufacturer
			{
				get
				{
					return ComputerHardwareInfo._computerManufacturer;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001BB68 File Offset: 0x00019D68
			public string ComputerModel
			{
				get
				{
					return ComputerHardwareInfo._computerModel;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001BB6F File Offset: 0x00019D6F
			public string ComputerSystemType
			{
				get
				{
					return ComputerHardwareInfo._computerSystemType;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001BB76 File Offset: 0x00019D76
			public long ComputerPhysicalMemory
			{
				get
				{
					return ComputerHardwareInfo._computerPhysicalMemory_Bytes;
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001BB7D File Offset: 0x00019D7D
			public string ComputerPhysicalMemory_String
			{
				get
				{
					return ComputerHardwareInfo.GetFileSizeString(ComputerHardwareInfo._computerPhysicalMemory_Bytes);
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001BB89 File Offset: 0x00019D89
			public string ProcessorName
			{
				get
				{
					return ComputerHardwareInfo._processorName;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600040B RID: 1035 RVA: 0x0001BB90 File Offset: 0x00019D90
			public string ProcessorCores
			{
				get
				{
					return ComputerHardwareInfo._processorCores;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001BB97 File Offset: 0x00019D97
			public string ProcessorCurrentClockSpeed
			{
				get
				{
					return ComputerHardwareInfo._processorCurrentClockSpeed;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001BB9E File Offset: 0x00019D9E
			public string ProcessorMaxClockSpeed
			{
				get
				{
					return ComputerHardwareInfo._processorMaxClockSpeed;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001BBA5 File Offset: 0x00019DA5
			public string ProcessorThread
			{
				get
				{
					return ComputerHardwareInfo._processorThreads;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001BBAC File Offset: 0x00019DAC
			public string ProcessorNumberOfLog
			{
				get
				{
					return ComputerHardwareInfo._processorLogicalProc;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001BBB3 File Offset: 0x00019DB3
			public string TotalRAM
			{
				get
				{
					return ComputerHardwareInfo._totalRAM;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001BBBA File Offset: 0x00019DBA
			public string SpeedOfRAM
			{
				get
				{
					return ComputerHardwareInfo._speedRAM;
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001BBC1 File Offset: 0x00019DC1
			public string MAXREFRATE
			{
				get
				{
					return ComputerHardwareInfo._RefreshRate;
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000413 RID: 1043 RVA: 0x0001BBC8 File Offset: 0x00019DC8
			public string GraphicsCard1
			{
				get
				{
					return ComputerHardwareInfo._GraphicsCard;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000414 RID: 1044 RVA: 0x0001BBCF File Offset: 0x00019DCF
			public string MonitorType
			{
				get
				{
					return ComputerHardwareInfo._MonitorType;
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000415 RID: 1045 RVA: 0x0001BBD6 File Offset: 0x00019DD6
			public string ProcessorID
			{
				get
				{
					return ComputerHardwareInfo._processorID;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x0001BBDD File Offset: 0x00019DDD
			public string MAC_Address
			{
				get
				{
					return ComputerHardwareInfo._mAC_Address;
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x0001BBE4 File Offset: 0x00019DE4
			public string BIOSData
			{
				get
				{
					return ComputerHardwareInfo._biosData;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001BBEB File Offset: 0x00019DEB
			public int FixedDriveCount
			{
				get
				{
					return ComputerHardwareInfo._fixedDriveCount;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001BBF2 File Offset: 0x00019DF2
			public string FixedDriveCount_String
			{
				get
				{
					return ComputerHardwareInfo._fixedDriveCount.ToString("n0");
				}
			}
		}
	}
}
