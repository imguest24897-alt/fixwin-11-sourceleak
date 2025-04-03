using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace FixWin.My
{
	// Token: 0x02000004 RID: 4
	[StandardModule]
	[HideModuleName]
	[GeneratedCode("MyTemplate", "11.0.0.0")]
	internal sealed class MyProject
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020DA File Offset: 0x000002DA
		[HelpKeyword("My.Computer")]
		internal static MyComputer Computer
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_ComputerObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E6 File Offset: 0x000002E6
		[HelpKeyword("My.Application")]
		internal static MyApplication Application
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_AppObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020F2 File Offset: 0x000002F2
		[HelpKeyword("My.User")]
		internal static User User
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_UserObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020FE File Offset: 0x000002FE
		[HelpKeyword("My.Forms")]
		internal static MyProject.MyForms Forms
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_MyFormsObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000210A File Offset: 0x0000030A
		[HelpKeyword("My.WebServices")]
		internal static MyProject.MyWebServices WebServices
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_MyWebServicesObjectProvider.GetInstance;
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly MyProject.ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new MyProject.ThreadSafeObjectProvider<MyComputer>();

		// Token: 0x04000002 RID: 2
		private static readonly MyProject.ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new MyProject.ThreadSafeObjectProvider<MyApplication>();

		// Token: 0x04000003 RID: 3
		private static readonly MyProject.ThreadSafeObjectProvider<User> m_UserObjectProvider = new MyProject.ThreadSafeObjectProvider<User>();

		// Token: 0x04000004 RID: 4
		private static MyProject.ThreadSafeObjectProvider<MyProject.MyForms> m_MyFormsObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyForms>();

		// Token: 0x04000005 RID: 5
		private static readonly MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices> m_MyWebServicesObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices>();

		// Token: 0x02000014 RID: 20
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
		internal sealed class MyForms
		{
			// Token: 0x060003D9 RID: 985 RVA: 0x0001B854 File Offset: 0x00019A54
			[DebuggerHidden]
			private static T Create__Instance__<T>(T Instance) where T : Form, new()
			{
				if (Instance == null || Instance.IsDisposed)
				{
					if (MyProject.MyForms.m_FormBeingCreated != null)
					{
						if (MyProject.MyForms.m_FormBeingCreated.ContainsKey(typeof(T)))
						{
							throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
						}
					}
					else
					{
						MyProject.MyForms.m_FormBeingCreated = new Hashtable();
					}
					MyProject.MyForms.m_FormBeingCreated.Add(typeof(T), null);
					try
					{
						return Activator.CreateInstance<T>();
					}
					catch (TargetInvocationException ex) when (ex.InnerException != null)
					{
						throw new InvalidOperationException(Utils.GetResourceString("WinForms_SeeInnerException", new string[]
						{
							ex.InnerException.Message
						}), ex.InnerException);
					}
					finally
					{
						MyProject.MyForms.m_FormBeingCreated.Remove(typeof(T));
					}
				}
				return Instance;
			}

			// Token: 0x060003DA RID: 986 RVA: 0x0001B958 File Offset: 0x00019B58
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance) where T : Form
			{
				instance.Dispose();
				instance = default(T);
			}

			// Token: 0x060003DB RID: 987 RVA: 0x00003284 File Offset: 0x00001484
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyForms()
			{
			}

			// Token: 0x060003DC RID: 988 RVA: 0x0001B96D File Offset: 0x00019B6D
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0001B97B File Offset: 0x00019B7B
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x060003DE RID: 990 RVA: 0x0001B983 File Offset: 0x00019B83
			[EditorBrowsable(EditorBrowsableState.Never)]
			internal new Type GetType()
			{
				return typeof(MyProject.MyForms);
			}

			// Token: 0x060003DF RID: 991 RVA: 0x0001B98F File Offset: 0x00019B8F
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001B997 File Offset: 0x00019B97
			// (set) Token: 0x060003E4 RID: 996 RVA: 0x0001B9FB File Offset: 0x00019BFB
			public Description Description
			{
				get
				{
					this.m_Description = MyProject.MyForms.Create__Instance__<Description>(this.m_Description);
					return this.m_Description;
				}
				set
				{
					if (value != this.m_Description)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<Description>(ref this.m_Description);
					}
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001B9B0 File Offset: 0x00019BB0
			// (set) Token: 0x060003E5 RID: 997 RVA: 0x0001BA20 File Offset: 0x00019C20
			public Main_Form Main_Form
			{
				get
				{
					this.m_Main_Form = MyProject.MyForms.Create__Instance__<Main_Form>(this.m_Main_Form);
					return this.m_Main_Form;
				}
				set
				{
					if (value != this.m_Main_Form)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<Main_Form>(ref this.m_Main_Form);
					}
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001B9C9 File Offset: 0x00019BC9
			// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001BA45 File Offset: 0x00019C45
			public Scan Scan
			{
				get
				{
					this.m_Scan = MyProject.MyForms.Create__Instance__<Scan>(this.m_Scan);
					return this.m_Scan;
				}
				set
				{
					if (value != this.m_Scan)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<Scan>(ref this.m_Scan);
					}
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001B9E2 File Offset: 0x00019BE2
			// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001BA6A File Offset: 0x00019C6A
			public WhatNew WhatNew
			{
				get
				{
					this.m_WhatNew = MyProject.MyForms.Create__Instance__<WhatNew>(this.m_WhatNew);
					return this.m_WhatNew;
				}
				set
				{
					if (value != this.m_WhatNew)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<WhatNew>(ref this.m_WhatNew);
					}
				}
			}

			// Token: 0x040001A2 RID: 418
			[ThreadStatic]
			private static Hashtable m_FormBeingCreated;

			// Token: 0x040001A3 RID: 419
			[EditorBrowsable(EditorBrowsableState.Never)]
			public Description m_Description;

			// Token: 0x040001A4 RID: 420
			[EditorBrowsable(EditorBrowsableState.Never)]
			public Main_Form m_Main_Form;

			// Token: 0x040001A5 RID: 421
			[EditorBrowsable(EditorBrowsableState.Never)]
			public Scan m_Scan;

			// Token: 0x040001A6 RID: 422
			[EditorBrowsable(EditorBrowsableState.Never)]
			public WhatNew m_WhatNew;
		}

		// Token: 0x02000015 RID: 21
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
		internal sealed class MyWebServices
		{
			// Token: 0x060003E8 RID: 1000 RVA: 0x0001B96D File Offset: 0x00019B6D
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0001B97B File Offset: 0x00019B7B
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x0001BA8F File Offset: 0x00019C8F
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			internal new Type GetType()
			{
				return typeof(MyProject.MyWebServices);
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0001B98F File Offset: 0x00019B8F
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x0001BA9C File Offset: 0x00019C9C
			[DebuggerHidden]
			private static T Create__Instance__<T>(T instance) where T : new()
			{
				T result;
				if (instance == null)
				{
					result = Activator.CreateInstance<T>();
				}
				else
				{
					result = instance;
				}
				return result;
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x0001BABC File Offset: 0x00019CBC
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance)
			{
				instance = default(T);
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x00003284 File Offset: 0x00001484
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyWebServices()
			{
			}
		}

		// Token: 0x02000016 RID: 22
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ComVisible(false)]
		internal sealed class ThreadSafeObjectProvider<T> where T : new()
		{
			// Token: 0x17000164 RID: 356
			// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001BAC5 File Offset: 0x00019CC5
			internal T GetInstance
			{
				[DebuggerHidden]
				get
				{
					if (MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
					{
						MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = Activator.CreateInstance<T>();
					}
					return MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
				}
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x00003284 File Offset: 0x00001484
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public ThreadSafeObjectProvider()
			{
			}

			// Token: 0x040001A7 RID: 423
			[CompilerGenerated]
			[ThreadStatic]
			private static T m_ThreadStaticValue;
		}
	}
}
