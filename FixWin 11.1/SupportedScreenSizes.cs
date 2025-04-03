using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace FixWin
{
	// Token: 0x02000010 RID: 16
	public class SupportedScreenSizes
	{
		// Token: 0x060003C0 RID: 960
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumDisplaySettingsExW([MarshalAs(UnmanagedType.LPWStr)] string lpszDeviceName, int iModeNum, ref SupportedScreenSizes.DEVMODEW lpDevMode, uint dwFlags);

		// Token: 0x060003C1 RID: 961 RVA: 0x0001B0EC File Offset: 0x000192EC
		public string[] GetSizesAsStrings(string gDeviceName = "")
		{
			List<string> list = new List<string>();
			foreach (Size size in this.GetSizes(gDeviceName))
			{
				list.Add(size.Width.ToString() + "x" + size.Height.ToString());
			}
			return list.ToArray();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001B154 File Offset: 0x00019354
		public Size[] GetSizes(string gDeviceName = "")
		{
			if (Operators.CompareString(gDeviceName, "", false) == 0)
			{
				gDeviceName = Screen.PrimaryScreen.DeviceName;
			}
			List<Size> list = new List<Size>();
			int num = 0;
			SupportedScreenSizes.DEVMODEW devmodew = default(SupportedScreenSizes.DEVMODEW);
			devmodew.dmFields = 1572864U;
			checked
			{
				devmodew.dmSize = (ushort)Marshal.SizeOf(typeof(SupportedScreenSizes.DEVMODEW));
				while (SupportedScreenSizes.EnumDisplaySettingsExW(gDeviceName, num, ref devmodew, 0U))
				{
					Size item = new Size((int)devmodew.dmPelsWidth, (int)devmodew.dmPelsHeight);
					if (!list.Contains(item))
					{
						list.Add(item);
					}
					num++;
				}
				return list.ToArray();
			}
		}

		// Token: 0x04000198 RID: 408
		private const int DM_PELSWIDTH = 524288;

		// Token: 0x04000199 RID: 409
		private const int DM_PELSHEIGHT = 1048576;

		// Token: 0x0200001D RID: 29
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DEVMODEW
		{
			// Token: 0x040001D4 RID: 468
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x040001D5 RID: 469
			public ushort dmSpecVersion;

			// Token: 0x040001D6 RID: 470
			public ushort dmDriverVersion;

			// Token: 0x040001D7 RID: 471
			public ushort dmSize;

			// Token: 0x040001D8 RID: 472
			public ushort dmDriverExtra;

			// Token: 0x040001D9 RID: 473
			public uint dmFields;

			// Token: 0x040001DA RID: 474
			public SupportedScreenSizes.Anonymous_7a7460d9_d99f_4e9a_9ebb_cdd10c08463d Union1;

			// Token: 0x040001DB RID: 475
			public short dmColor;

			// Token: 0x040001DC RID: 476
			public short dmDuplex;

			// Token: 0x040001DD RID: 477
			public short dmYResolution;

			// Token: 0x040001DE RID: 478
			public short dmTTOption;

			// Token: 0x040001DF RID: 479
			public short dmCollate;

			// Token: 0x040001E0 RID: 480
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			// Token: 0x040001E1 RID: 481
			public ushort dmLogPixels;

			// Token: 0x040001E2 RID: 482
			public uint dmBitsPerPel;

			// Token: 0x040001E3 RID: 483
			public uint dmPelsWidth;

			// Token: 0x040001E4 RID: 484
			public uint dmPelsHeight;

			// Token: 0x040001E5 RID: 485
			public SupportedScreenSizes.Anonymous_084dbe97_5806_4c28_a299_ed6037f61d90 Union2;

			// Token: 0x040001E6 RID: 486
			public uint dmDisplayFrequency;

			// Token: 0x040001E7 RID: 487
			public uint dmICMMethod;

			// Token: 0x040001E8 RID: 488
			public uint dmICMIntent;

			// Token: 0x040001E9 RID: 489
			public uint dmMediaType;

			// Token: 0x040001EA RID: 490
			public uint dmDitherType;

			// Token: 0x040001EB RID: 491
			public uint dmReserved1;

			// Token: 0x040001EC RID: 492
			public uint dmReserved2;

			// Token: 0x040001ED RID: 493
			public uint dmPanningWidth;

			// Token: 0x040001EE RID: 494
			public uint dmPanningHeight;
		}

		// Token: 0x0200001E RID: 30
		[StructLayout(LayoutKind.Explicit)]
		private struct Anonymous_7a7460d9_d99f_4e9a_9ebb_cdd10c08463d
		{
			// Token: 0x040001EF RID: 495
			[FieldOffset(0)]
			public SupportedScreenSizes.Anonymous_865d3c92_fe8c_4ee6_9601_a9eb2536957e Struct1;

			// Token: 0x040001F0 RID: 496
			[FieldOffset(0)]
			public SupportedScreenSizes.Anonymous_1b5f787e_41ca_472c_8595_3484490ffe0c Struct2;
		}

		// Token: 0x0200001F RID: 31
		[StructLayout(LayoutKind.Explicit)]
		private struct Anonymous_084dbe97_5806_4c28_a299_ed6037f61d90
		{
			// Token: 0x040001F1 RID: 497
			[FieldOffset(0)]
			public uint dmDisplayFlags;

			// Token: 0x040001F2 RID: 498
			[FieldOffset(0)]
			public uint dmNup;
		}

		// Token: 0x02000020 RID: 32
		private struct Anonymous_865d3c92_fe8c_4ee6_9601_a9eb2536957e
		{
			// Token: 0x040001F3 RID: 499
			public short dmOrientation;

			// Token: 0x040001F4 RID: 500
			public short dmPaperSize;

			// Token: 0x040001F5 RID: 501
			public short dmPaperLength;

			// Token: 0x040001F6 RID: 502
			public short dmPaperWidth;

			// Token: 0x040001F7 RID: 503
			public short dmScale;

			// Token: 0x040001F8 RID: 504
			public short dmCopies;

			// Token: 0x040001F9 RID: 505
			public short dmDefaultSource;

			// Token: 0x040001FA RID: 506
			public short dmPrintQuality;
		}

		// Token: 0x02000021 RID: 33
		private struct Anonymous_1b5f787e_41ca_472c_8595_3484490ffe0c
		{
			// Token: 0x040001FB RID: 507
			public SupportedScreenSizes.POINTL dmPosition;

			// Token: 0x040001FC RID: 508
			public uint dmDisplayOrientation;

			// Token: 0x040001FD RID: 509
			public uint dmDisplayFixedOutput;
		}

		// Token: 0x02000022 RID: 34
		private struct POINTL
		{
			// Token: 0x040001FE RID: 510
			public int x;

			// Token: 0x040001FF RID: 511
			public int y;
		}
	}
}
