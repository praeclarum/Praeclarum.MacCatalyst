using CoreAnimation;
using CoreGraphics;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UIKit;

namespace Foundation
{
	[StructLayout(LayoutKind.Sequential)]
	[Register("NSObject", true)]
	public class NSObject : IEquatable<NSObject>, INativeObject, IDisposable, INSObjectProtocol
	{
		[Flags]
		private enum Flags : byte
		{
			Disposed = 0x1,
			NativeRef = 0x2,
			IsDirectBinding = 0x4,
			RegisteredToggleRef = 0x8,
			InFinalizerQueue = 0x10,
			HasManagedRef = 0x20,
			IsCustomType = 0x80
		}

		[Register("__NSObject_Disposer")]
		[Preserve(AllMembers = true)]
		internal class NSObject_Disposer : NSObject
		{
			private static readonly List<NSObject> drainList1 = new List<NSObject>();

			private static readonly List<NSObject> drainList2 = new List<NSObject>();

			private static List<NSObject> handles = drainList1;

			private new static readonly IntPtr class_ptr = ObjCRuntime.Class.GetHandle("__NSObject_Disposer");

			private static readonly object lock_obj = new object();

			private NSObject_Disposer()
			{
			}

			internal static void Add(NSObject handle)
			{
				bool flag;
				lock (lock_obj)
				{
					handles.Add(handle);
					flag = (handles.Count == 1);
				}
				if (flag)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_bool(class_ptr, Selector.GetHandle("performSelectorOnMainThread:withObject:waitUntilDone:"), Selector.GetHandle("drain:"), IntPtr.Zero, arg3: false);
				}
			}

			[Export("drain:")]
			private static void Drain(NSObject ctx)
			{
				List<NSObject> list;
				lock (lock_obj)
				{
					list = handles;
					if (handles == drainList1)
					{
						handles = drainList2;
					}
					else
					{
						handles = drainList1;
					}
				}
				foreach (NSObject item in list)
				{
					item.ReleaseManagedRef();
				}
				list.Clear();
			}
		}

		[Register("__XamarinObjectObserver")]
		private class Observer : NSObject
		{
			private WeakReference obj;

			private Action<NSObservedChange> cback;

			private NSString key;

			public Observer(NSObject obj, NSString key, Action<NSObservedChange> observer)
			{
				if (observer == null)
				{
					throw new ArgumentNullException("observer");
				}
				this.obj = new WeakReference(obj);
				this.key = key;
				cback = observer;
				base.IsDirectBinding = false;
			}

			[Preserve(Conditional = true)]
			public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
			{
				if (keyPath == key && context == base.Handle)
				{
					cback(new NSObservedChange(change));
				}
				else
				{
					base.ObserveValue(keyPath, ofObject, change, context);
				}
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (obj != null)
					{
						((NSObject)obj.Target)?.RemoveObserver(this, key, base.Handle);
					}
					obj = null;
					cback = null;
				}
				else
				{
					Console.Error.WriteLine("Warning: observer object was not disposed manually with Dispose()");
				}
				base.Dispose(disposing);
			}
		}

		private const string selConformsToProtocol = "conformsToProtocol:";

		private const string selEncodeWithCoder = "encodeWithCoder:";

		public static readonly Assembly PlatformAssembly = typeof(NSObject).Assembly;

		private IntPtr handle;

		private IntPtr class_handle;

		private Flags flags;

		[Obsolete("Use 'PlatformAssembly' for easier code sharing across platforms.")]
		public static readonly Assembly MonoTouchAssembly = typeof(NSObject).Assembly;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static readonly IntPtr class_ptr = ObjCRuntime.Class.GetHandle("NSObject");

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ChangeIndexesKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ChangeKindKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ChangeNewKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ChangeNotificationIsPriorKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ChangeOldKey;

		private bool disposed
		{
			get
			{
				return (flags & Flags.Disposed) == Flags.Disposed;
			}
			set
			{
				flags = (value ? (flags | Flags.Disposed) : (flags & ~Flags.Disposed));
			}
		}

		internal bool IsRegisteredToggleRef
		{
			get
			{
				return (flags & Flags.RegisteredToggleRef) == Flags.RegisteredToggleRef;
			}
			set
			{
				flags = (value ? (flags | Flags.RegisteredToggleRef) : (flags & ~Flags.RegisteredToggleRef));
			}
		}

		protected internal bool IsDirectBinding
		{
			get
			{
				return (flags & Flags.IsDirectBinding) == Flags.IsDirectBinding;
			}
			set
			{
				flags = (value ? (flags | Flags.IsDirectBinding) : (flags & ~Flags.IsDirectBinding));
			}
		}

		internal bool InFinalizerQueue => (flags & Flags.InFinalizerQueue) == Flags.InFinalizerQueue;

		private bool IsCustomType
		{
			get
			{
				bool flag = (flags & Flags.IsCustomType) == Flags.IsCustomType;
				if (!flag)
				{
					flag = ObjCRuntime.Class.IsCustomType(GetType());
					if (flag)
					{
						flags |= Flags.IsCustomType;
					}
				}
				return flag;
			}
		}

		public unsafe IntPtr SuperHandle
		{
			get
			{
				if (handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException(GetType().Name);
				}
				if (class_handle == IntPtr.Zero)
				{
					class_handle = ClassHandle;
				}
				fixed (IntPtr* value = &handle)
				{
					return (IntPtr)(void*)value;
				}
			}
		}

		public IntPtr Handle
		{
			get
			{
				return handle;
			}
			set
			{
				if (!(handle == value))
				{
					if (handle != IntPtr.Zero)
					{
						Runtime.UnregisterNSObject(handle);
					}
					handle = value;
					if (handle != IntPtr.Zero)
					{
						Runtime.RegisterNSObject(this, handle);
					}
				}
			}
		}

		public virtual IntPtr ClassHandle => class_ptr;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
		public virtual NSAttributedString[] AccessibilityAttributedUserInputLabels
		{
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("accessibilityAttributedUserInputLabels", ArgumentSemantic.Copy)]
			get
			{
				if (IsDirectBinding)
				{
					return NSArray.ArrayFromHandle<NSAttributedString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("accessibilityAttributedUserInputLabels")));
				}
				return NSArray.ArrayFromHandle<NSAttributedString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("accessibilityAttributedUserInputLabels")));
			}
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("setAccessibilityAttributedUserInputLabels:", ArgumentSemantic.Copy)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				NSArray nSArray = NSArray.FromNSObjects(value);
				if (IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("setAccessibilityAttributedUserInputLabels:"), nSArray.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("setAccessibilityAttributedUserInputLabels:"), nSArray.Handle);
				}
				nSArray.Dispose();
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
		public virtual bool AccessibilityRespondsToUserInteraction
		{
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("accessibilityRespondsToUserInteraction")]
			get
			{
				if (IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(Handle, Selector.GetHandle("accessibilityRespondsToUserInteraction"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(SuperHandle, Selector.GetHandle("accessibilityRespondsToUserInteraction"));
			}
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("setAccessibilityRespondsToUserInteraction:")]
			set
			{
				if (IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool(Handle, Selector.GetHandle("setAccessibilityRespondsToUserInteraction:"), value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_bool(SuperHandle, Selector.GetHandle("setAccessibilityRespondsToUserInteraction:"), value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
		public virtual string? AccessibilityTextualContext
		{
			[System.Runtime.CompilerServices.NullableContext(2)]
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("accessibilityTextualContext", ArgumentSemantic.Retain)]
			get
			{
				if (IsDirectBinding)
				{
					return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("accessibilityTextualContext")));
				}
				return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("accessibilityTextualContext")));
			}
			[System.Runtime.CompilerServices.NullableContext(2)]
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("setAccessibilityTextualContext:", ArgumentSemantic.Retain)]
			set
			{
				IntPtr arg = NSString.CreateNative(value);
				if (IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("setAccessibilityTextualContext:"), arg);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("setAccessibilityTextualContext:"), arg);
				}
				NSString.ReleaseNative(arg);
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
		public virtual string[] AccessibilityUserInputLabels
		{
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("accessibilityUserInputLabels", ArgumentSemantic.Retain)]
			get
			{
				if (IsDirectBinding)
				{
					return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("accessibilityUserInputLabels")));
				}
				return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("accessibilityUserInputLabels")));
			}
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.MacOSX, PlatformArchitecture.All, null)]
			[Export("setAccessibilityUserInputLabels:", ArgumentSemantic.Retain)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				NSArray nSArray = NSArray.FromStrings(value);
				if (IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("setAccessibilityUserInputLabels:"), nSArray.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("setAccessibilityUserInputLabels:"), nSArray.Handle);
				}
				nSArray.Dispose();
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual Class Class
		{
			[Export("class")]
			get
			{
				IntPtr value = (!IsDirectBinding) ? ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("class")) : ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("class"));
				if (!(value == IntPtr.Zero))
				{
					return new Class(value);
				}
				return null;
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string DebugDescription
		{
			[Export("debugDescription")]
			get
			{
				if (IsDirectBinding)
				{
					return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("debugDescription")));
				}
				return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("debugDescription")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string Description
		{
			[Export("description")]
			get
			{
				if (IsDirectBinding)
				{
					return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("description")));
				}
				return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("description")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool IsProxy
		{
			[Export("isProxy")]
			get
			{
				if (IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(Handle, Selector.GetHandle("isProxy"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(SuperHandle, Selector.GetHandle("isProxy"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual nuint RetainCount
		{
			[Export("retainCount")]
			get
			{
				if (IsDirectBinding)
				{
					return ObjCRuntime.Messaging.nuint_objc_msgSend(Handle, Selector.GetHandle("retainCount"));
				}
				return ObjCRuntime.Messaging.nuint_objc_msgSendSuper(SuperHandle, Selector.GetHandle("retainCount"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject Self
		{
			[Export("self")]
			get
			{
				if (IsDirectBinding)
				{
					return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("self")));
				}
				return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("self")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual Class Superclass
		{
			[Export("superclass")]
			get
			{
				IntPtr value = (!IsDirectBinding) ? ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("superclass")) : ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("superclass"));
				if (!(value == IntPtr.Zero))
				{
					return new Class(value);
				}
				return null;
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSZone Zone
		{
			[Export("zone")]
			get
			{
				IntPtr value = (!IsDirectBinding) ? ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("zone")) : ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("zone"));
				if (!(value == IntPtr.Zero))
				{
					return new NSZone(value);
				}
				return null;
			}
		}

		[Field("NSKeyValueChangeIndexesKey", "Foundation")]
		public static NSString ChangeIndexesKey
		{
			get
			{
				if (_ChangeIndexesKey == null)
				{
					_ChangeIndexesKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSKeyValueChangeIndexesKey");
				}
				return _ChangeIndexesKey;
			}
		}

		[Field("NSKeyValueChangeKindKey", "Foundation")]
		public static NSString ChangeKindKey
		{
			get
			{
				if (_ChangeKindKey == null)
				{
					_ChangeKindKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSKeyValueChangeKindKey");
				}
				return _ChangeKindKey;
			}
		}

		[Field("NSKeyValueChangeNewKey", "Foundation")]
		public static NSString ChangeNewKey
		{
			get
			{
				if (_ChangeNewKey == null)
				{
					_ChangeNewKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSKeyValueChangeNewKey");
				}
				return _ChangeNewKey;
			}
		}

		[Field("NSKeyValueChangeNotificationIsPriorKey", "Foundation")]
		public static NSString ChangeNotificationIsPriorKey
		{
			get
			{
				if (_ChangeNotificationIsPriorKey == null)
				{
					_ChangeNotificationIsPriorKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSKeyValueChangeNotificationIsPriorKey");
				}
				return _ChangeNotificationIsPriorKey;
			}
		}

		[Field("NSKeyValueChangeOldKey", "Foundation")]
		public static NSString ChangeOldKey
		{
			get
			{
				if (_ChangeOldKey == null)
				{
					_ChangeOldKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSKeyValueChangeOldKey");
				}
				return _ChangeOldKey;
			}
		}

		[Export("init")]
		public NSObject()
		{
			bool alloced = AllocIfNeeded();
			InitializeObject(alloced);
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public NSObject(NSObjectFlag x)
		{
			bool alloced = AllocIfNeeded();
			InitializeObject(alloced);
		}

		public NSObject(IntPtr handle)
			: this(handle, alloced: false)
		{
		}

		public NSObject(IntPtr handle, bool alloced)
		{
			this.handle = handle;
			InitializeObject(alloced);
		}

		~NSObject()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		internal static IntPtr Initialize()
		{
			return class_ptr;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterToggleRef(NSObject obj, IntPtr handle, bool isCustomType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void xamarin_release_managed_ref(IntPtr handle, NSObject managed_obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void xamarin_create_managed_ref(IntPtr handle, NSObject obj, bool retain);

		public static bool IsNewRefcountEnabled()
		{
			return true;
		}

		protected void MarkDirty()
		{
			MarkDirty(allowCustomTypes: false);
		}

		internal void MarkDirty(bool allowCustomTypes)
		{
			if (!IsRegisteredToggleRef && (allowCustomTypes || !IsCustomType))
			{
				IsRegisteredToggleRef = true;
				RegisterToggleRef(this, Handle, allowCustomTypes);
			}
		}

		private void InitializeObject(bool alloced)
		{
			if (alloced && handle == IntPtr.Zero && ObjCRuntime.Class.ThrowOnInitFailure)
			{
				if (ClassHandle == IntPtr.Zero)
				{
					throw new Exception($"Could not create an native instance of the type '{GetType().FullName}': the native class hasn't been loaded.\nIt is possible to ignore this condition by setting ObjCRuntime.Class.ThrowOnInitFailure to false.");
				}
				throw new Exception($"Failed to create a instance of the native type '{new Class(ClassHandle).Name}'.\nIt is possible to ignore this condition by setting ObjCRuntime.Class.ThrowOnInitFailure to false.");
			}
			IsDirectBinding = (GetType().Assembly == PlatformAssembly);
			Runtime.RegisterNSObject(this, handle);
			if ((flags & Flags.NativeRef) != Flags.NativeRef)
			{
				CreateManagedRef(!alloced);
			}
		}

		private void CreateManagedRef(bool retain)
		{
			xamarin_create_managed_ref(handle, this, retain);
		}

		private void ReleaseManagedRef()
		{
			xamarin_release_managed_ref(handle, this);
		}

		private static bool IsProtocol(Type type, IntPtr protocol)
		{
			while (type != typeof(NSObject) && type != null)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(ProtocolAttribute), inherit: false);
				ProtocolAttribute protocolAttribute = (ProtocolAttribute)((customAttributes.Length != 0) ? customAttributes[0] : null);
				if (protocolAttribute != null && !protocolAttribute.IsInformal)
				{
					string protocol2;
					if (!string.IsNullOrEmpty(protocolAttribute.Name))
					{
						protocol2 = protocolAttribute.Name;
					}
					else
					{
						customAttributes = type.GetCustomAttributes(typeof(RegisterAttribute), inherit: false);
						RegisterAttribute registerAttribute = (RegisterAttribute)((customAttributes.Length != 0) ? customAttributes[0] : null);
						protocol2 = ((registerAttribute == null || string.IsNullOrEmpty(registerAttribute.Name)) ? type.Name : registerAttribute.Name);
					}
					IntPtr protocol3 = Runtime.GetProtocol(protocol2);
					if (protocol3 != IntPtr.Zero && protocol3 == protocol)
					{
						return true;
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		[Preserve]
		private bool InvokeConformsToProtocol(IntPtr protocol)
		{
			return ConformsToProtocol(protocol);
		}

		[Export("conformsToProtocol:")]
		[Preserve]
		[BindingImpl(BindingImplOptions.Optimizable)]
		public virtual bool ConformsToProtocol(IntPtr protocol)
		{
			bool flag = IsDirectBinding;
			if (flag && GetType().Assembly != PlatformAssembly)
			{
				object[] customAttributes = GetType().GetCustomAttributes(typeof(RegisterAttribute), inherit: false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					flag = ((RegisterAttribute)customAttributes[0]).IsWrapper;
				}
			}
			if ((!flag) ? ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("conformsToProtocol:"), protocol) : ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(Handle, Selector.GetHandle("conformsToProtocol:"), protocol))
			{
				return true;
			}
			if (!Runtime.DynamicRegistrationSupported)
			{
				return false;
			}
			object[] customAttributes2 = GetType().GetCustomAttributes(typeof(AdoptsAttribute), inherit: true);
			for (int i = 0; i < customAttributes2.Length; i++)
			{
				if (((AdoptsAttribute)customAttributes2[i]).ProtocolHandle == protocol)
				{
					return true;
				}
			}
			if (IsProtocol(GetType(), protocol))
			{
				return true;
			}
			Type[] interfaces = GetType().GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (IsProtocol(interfaces[i], protocol))
				{
					return true;
				}
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void DangerousRelease()
		{
			DangerousRelease(handle);
		}

		internal static void DangerousRelease(IntPtr handle)
		{
			if (!(handle == IntPtr.Zero))
			{
				ObjCRuntime.Messaging.void_objc_msgSend(handle, Selector.GetHandle("release"));
			}
		}

		internal static void DangerousRetain(IntPtr handle)
		{
			if (!(handle == IntPtr.Zero))
			{
				ObjCRuntime.Messaging.void_objc_msgSend(handle, Selector.GetHandle("retain"));
			}
		}

		internal static void DangerousAutorelease(IntPtr handle)
		{
			ObjCRuntime.Messaging.void_objc_msgSend(handle, Selector.GetHandle("autorelease"));
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public NSObject DangerousRetain()
		{
			ObjCRuntime.Messaging.void_objc_msgSend(handle, Selector.GetHandle("retain"));
			return this;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public NSObject DangerousAutorelease()
		{
			ObjCRuntime.Messaging.void_objc_msgSend(handle, Selector.GetHandle("autorelease"));
			return this;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void InitializeHandle(IntPtr handle)
		{
			InitializeHandle(handle, "init*");
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void InitializeHandle(IntPtr handle, string initSelector)
		{
			if (this.handle == IntPtr.Zero && ObjCRuntime.Class.ThrowOnInitFailure)
			{
				if (ClassHandle == IntPtr.Zero)
				{
					throw new Exception($"Could not create an native instance of the type '{GetType().FullName}': the native class hasn't been loaded.\nIt is possible to ignore this condition by setting ObjCRuntime.Class.ThrowOnInitFailure to false.");
				}
				throw new Exception($"Failed to create a instance of the native type '{new Class(ClassHandle).Name}'.\nIt is possible to ignore this condition by setting ObjCRuntime.Class.ThrowOnInitFailure to false.");
			}
			if (handle == IntPtr.Zero && ObjCRuntime.Class.ThrowOnInitFailure)
			{
				Handle = IntPtr.Zero;
				throw new Exception($"Could not initialize an instance of the type '{GetType().FullName}': the native '{initSelector}' method returned nil.\nIt is possible to ignore this condition by setting ObjCRuntime.Class.ThrowOnInitFailure to false.");
			}
			Handle = handle;
		}

		private bool AllocIfNeeded()
		{
			if (handle == IntPtr.Zero)
			{
				handle = ObjCRuntime.Messaging.IntPtr_objc_msgSend(ObjCRuntime.Class.GetHandle(GetType()), Selector.GetHandle("alloc"));
				return true;
			}
			return false;
		}

		private IntPtr GetObjCIvar(string name)
		{
			object_getInstanceVariable(handle, name, out IntPtr val);
			return val;
		}

		[Obsolete("Do not use; this API does not properly retain/release existing/new values, so leaks and/or crashes may occur.")]
		public NSObject GetNativeField(string name)
		{
			IntPtr objCIvar = GetObjCIvar(name);
			if (objCIvar == IntPtr.Zero)
			{
				return null;
			}
			return Runtime.GetNSObject(objCIvar);
		}

		private void SetObjCIvar(string name, IntPtr value)
		{
			object_setInstanceVariable(handle, name, value);
		}

		[Obsolete("Do not use; this API does not properly retain/release existing/new values, so leaks and/or crashes may occur.")]
		public void SetNativeField(string name, NSObject value)
		{
			if (value == null)
			{
				SetObjCIvar(name, IntPtr.Zero);
			}
			else
			{
				SetObjCIvar(name, value.Handle);
			}
		}

		[DllImport("/usr/lib/libobjc.dylib")]
		private static extern void object_getInstanceVariable(IntPtr obj, string name, out IntPtr val);

		[DllImport("/usr/lib/libobjc.dylib")]
		private static extern void object_setInstanceVariable(IntPtr obj, string name, IntPtr val);

		private void InvokeOnMainThread(Selector sel, NSObject obj, bool wait)
		{
			ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_bool(Handle, Selector.GetHandle("performSelectorOnMainThread:withObject:waitUntilDone:"), sel.Handle, obj?.Handle ?? IntPtr.Zero, wait);
		}

		public void BeginInvokeOnMainThread(Selector sel, NSObject obj)
		{
			InvokeOnMainThread(sel, obj, wait: false);
		}

		public void InvokeOnMainThread(Selector sel, NSObject obj)
		{
			InvokeOnMainThread(sel, obj, wait: true);
		}

		public void BeginInvokeOnMainThread(Action action)
		{
			NSAsyncActionDispatcher nSAsyncActionDispatcher = new NSAsyncActionDispatcher(action);
			ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_bool(nSAsyncActionDispatcher.Handle, Selector.GetHandle("performSelectorOnMainThread:withObject:waitUntilDone:"), Selector.GetHandle("xamarinApplySelector"), nSAsyncActionDispatcher.Handle, arg3: false);
		}

		internal void BeginInvokeOnMainThread(SendOrPostCallback cb, object state)
		{
			NSAsyncSynchronizationContextDispatcher nSAsyncSynchronizationContextDispatcher = new NSAsyncSynchronizationContextDispatcher(cb, state);
			ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_bool(nSAsyncSynchronizationContextDispatcher.Handle, Selector.GetHandle("performSelectorOnMainThread:withObject:waitUntilDone:"), Selector.GetHandle("xamarinApplySelector"), nSAsyncSynchronizationContextDispatcher.Handle, arg3: false);
		}

		public void InvokeOnMainThread(Action action)
		{
			using (NSActionDispatcher nSActionDispatcher = new NSActionDispatcher(action))
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_bool(nSActionDispatcher.Handle, Selector.GetHandle("performSelectorOnMainThread:withObject:waitUntilDone:"), Selector.GetHandle("xamarinApplySelector"), nSActionDispatcher.Handle, arg3: true);
			}
		}

		internal void InvokeOnMainThread(SendOrPostCallback cb, object state)
		{
			using (NSSynchronizationContextDispatcher nSSynchronizationContextDispatcher = new NSSynchronizationContextDispatcher(cb, state))
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_bool(nSSynchronizationContextDispatcher.Handle, Selector.GetHandle("performSelectorOnMainThread:withObject:waitUntilDone:"), Selector.GetHandle("xamarinApplySelector"), nSSynchronizationContextDispatcher.Handle, arg3: true);
			}
		}

		public static NSObject FromObject(object obj)
		{
			if (obj == null)
			{
				return NSNull.Null;
			}
			Type type = obj.GetType();
			if (type == typeof(NSObject) || type.IsSubclassOf(typeof(NSObject)))
			{
				return (NSObject)obj;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return new NSNumber((bool)obj);
			case TypeCode.Char:
				return new NSNumber((char)obj);
			case TypeCode.SByte:
				return new NSNumber((sbyte)obj);
			case TypeCode.Byte:
				return new NSNumber((byte)obj);
			case TypeCode.Int16:
				return new NSNumber((short)obj);
			case TypeCode.UInt16:
				return new NSNumber((ushort)obj);
			case TypeCode.Int32:
				return new NSNumber((int)obj);
			case TypeCode.UInt32:
				return new NSNumber((uint)obj);
			case TypeCode.Int64:
				return new NSNumber((long)obj);
			case TypeCode.UInt64:
				return new NSNumber((ulong)obj);
			case TypeCode.Single:
				return new NSNumber((float)obj);
			case TypeCode.Double:
				return new NSNumber((double)obj);
			case TypeCode.String:
				return new NSString((string)obj);
			default:
			{
				if (type == typeof(IntPtr))
				{
					return NSValue.ValueFromPointer((IntPtr)obj);
				}
				if (type == typeof(SizeF))
				{
					return NSValue.FromSizeF((SizeF)obj);
				}
				if (type == typeof(RectangleF))
				{
					return NSValue.FromRectangleF((RectangleF)obj);
				}
				if (type == typeof(PointF))
				{
					return NSValue.FromPointF((PointF)obj);
				}
				if (type == typeof(nint))
				{
					return NSNumber.FromNInt((nint)obj);
				}
				if (type == typeof(nuint))
				{
					return NSNumber.FromNUInt((nuint)obj);
				}
				if (type == typeof(nfloat))
				{
					return NSNumber.FromNFloat((nfloat)obj);
				}
				if (type == typeof(CGSize))
				{
					return NSValue.FromCGSize((CGSize)obj);
				}
				if (type == typeof(CGRect))
				{
					return NSValue.FromCGRect((CGRect)obj);
				}
				if (type == typeof(CGPoint))
				{
					return NSValue.FromCGPoint((CGPoint)obj);
				}
				if (type == typeof(CGAffineTransform))
				{
					return NSValue.FromCGAffineTransform((CGAffineTransform)obj);
				}
				if (type == typeof(UIEdgeInsets))
				{
					return NSValue.FromUIEdgeInsets((UIEdgeInsets)obj);
				}
				if (type == typeof(CATransform3D))
				{
					return NSValue.FromCATransform3D((CATransform3D)obj);
				}
				INativeObject nativeObject = obj as INativeObject;
				if (nativeObject != null)
				{
					return Runtime.GetNSObject(nativeObject.Handle);
				}
				return null;
			}
			}
		}

		public void SetValueForKeyPath(IntPtr handle, NSString keyPath)
		{
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(Handle, Selector.GetHandle("setValue:forKeyPath:"), handle, keyPath.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("setValue:forKeyPath:"), handle, keyPath.Handle);
			}
		}

		public override int GetHashCode()
		{
			if (!IsDirectBinding)
			{
				return base.GetHashCode();
			}
			return GetNativeHash().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			NSObject nSObject = obj as NSObject;
			if (nSObject == null)
			{
				return false;
			}
			bool isDirectBinding = IsDirectBinding;
			if (isDirectBinding != nSObject.IsDirectBinding)
			{
				return false;
			}
			if (!isDirectBinding)
			{
				return this == nSObject;
			}
			return IsEqual(nSObject);
		}

		public bool Equals(NSObject obj)
		{
			return Equals((object)obj);
		}

		public override string ToString()
		{
			return Description ?? base.ToString();
		}

		public virtual void Invoke(Action action, double delay)
		{
			new NSAsyncActionDispatcher(action).PerformSelector(NSDispatcher.Selector, null, delay);
		}

		public virtual void Invoke(Action action, TimeSpan delay)
		{
			new NSAsyncActionDispatcher(action).PerformSelector(NSDispatcher.Selector, null, delay.TotalSeconds);
		}

		internal void ClearHandle()
		{
			handle = IntPtr.Zero;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (handle != IntPtr.Zero)
			{
				if (disposing)
				{
					ReleaseManagedRef();
				}
				else
				{
					NSObject_Disposer.Add(this);
				}
			}
		}

		public IDisposable AddObserver(string key, NSKeyValueObservingOptions options, Action<NSObservedChange> observer)
		{
			return AddObserver(new NSString(key), options, observer);
		}

		public IDisposable AddObserver(NSString key, NSKeyValueObservingOptions options, Action<NSObservedChange> observer)
		{
			Observer observer2 = new Observer(this, key, observer);
			AddObserver(observer2, key, options, observer2.Handle);
			return observer2;
		}

		public static NSObject Alloc(Class kls)
		{
			return new NSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend(kls.Handle, Selector.GetHandle("alloc")), alloced: true);
		}

		public void Init()
		{
			if (handle == IntPtr.Zero)
			{
				throw new Exception("you have not allocated the native object");
			}
			handle = ObjCRuntime.Messaging.IntPtr_objc_msgSend(handle, Selector.GetHandle("init"));
		}

		public static void InvokeInBackground(Action action)
		{
			Thread thread = new Thread(delegate(object v)
			{
				((Action)v)();
			});
			thread.IsBackground = true;
			thread.Start(action);
		}

		[Export("addObserver:forKeyPath:options:context:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void AddObserver(NSObject observer, NSString keyPath, NSKeyValueObservingOptions options, IntPtr context)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_UInt64_IntPtr(Handle, Selector.GetHandle("addObserver:forKeyPath:options:context:"), observer.Handle, keyPath.Handle, (ulong)options, context);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_UInt32_IntPtr(Handle, Selector.GetHandle("addObserver:forKeyPath:options:context:"), observer.Handle, keyPath.Handle, (uint)options, context);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_UInt64_IntPtr(SuperHandle, Selector.GetHandle("addObserver:forKeyPath:options:context:"), observer.Handle, keyPath.Handle, (ulong)options, context);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_UInt32_IntPtr(SuperHandle, Selector.GetHandle("addObserver:forKeyPath:options:context:"), observer.Handle, keyPath.Handle, (uint)options, context);
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public void AddObserver(NSObject observer, string keyPath, NSKeyValueObservingOptions options, IntPtr context)
		{
			AddObserver(observer, (NSString)keyPath, options, context);
		}

		[Export("automaticallyNotifiesObserversForKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static bool AutomaticallyNotifiesObserversForKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IntPtr arg = NSString.CreateNative(key);
			bool result = ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(class_ptr, Selector.GetHandle("automaticallyNotifiesObserversForKey:"), arg);
			NSString.ReleaseNative(arg);
			return result;
		}

		[Export("awakeFromNib")]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Advice("Overriding this method requires a call to the overriden method.")]
		[RequiresSuper]
		public virtual void AwakeFromNib()
		{
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(Handle, Selector.GetHandle("awakeFromNib"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(SuperHandle, Selector.GetHandle("awakeFromNib"));
			}
		}

		[Export("cancelPreviousPerformRequestsWithTarget:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static void CancelPreviousPerformRequest(NSObject aTarget)
		{
			if (aTarget == null)
			{
				throw new ArgumentNullException("aTarget");
			}
			ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(class_ptr, Selector.GetHandle("cancelPreviousPerformRequestsWithTarget:"), aTarget.Handle);
		}

		[Export("cancelPreviousPerformRequestsWithTarget:selector:object:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static void CancelPreviousPerformRequest(NSObject aTarget, Selector selector, NSObject? argument)
		{
			if (aTarget == null)
			{
				throw new ArgumentNullException("aTarget");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr(class_ptr, Selector.GetHandle("cancelPreviousPerformRequestsWithTarget:selector:object:"), aTarget.Handle, selector.Handle, argument?.Handle ?? IntPtr.Zero);
		}

		[Export("copy")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[return: Release]
		public virtual NSObject Copy()
		{
			if (!(this is INSCopying))
			{
				throw new InvalidOperationException("Type does not conform to NSCopying");
			}
			NSObject nSObject = (!IsDirectBinding) ? Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("copy"))) : Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("copy")));
			if (nSObject != null)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(nSObject.Handle, Selector.GetHandle("release"));
			}
			return nSObject;
		}

		[Export("didChange:valuesAtIndexes:forKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void DidChange(NSKeyValueChange changeKind, NSIndexSet indexes, NSString forKey)
		{
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			if (forKey == null)
			{
				throw new ArgumentNullException("forKey");
			}
			if (IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_UInt64_IntPtr_IntPtr(Handle, Selector.GetHandle("didChange:valuesAtIndexes:forKey:"), (ulong)changeKind, indexes.Handle, forKey.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_UInt32_IntPtr_IntPtr(Handle, Selector.GetHandle("didChange:valuesAtIndexes:forKey:"), (uint)changeKind, indexes.Handle, forKey.Handle);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_UInt64_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("didChange:valuesAtIndexes:forKey:"), (ulong)changeKind, indexes.Handle, forKey.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_UInt32_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("didChange:valuesAtIndexes:forKey:"), (uint)changeKind, indexes.Handle, forKey.Handle);
			}
		}

		[Export("didChangeValueForKey:withSetMutation:usingObjects:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void DidChange(NSString forKey, NSKeyValueSetMutationKind mutationKind, NSSet objects)
		{
			if (forKey == null)
			{
				throw new ArgumentNullException("forKey");
			}
			if (objects == null)
			{
				throw new ArgumentNullException("objects");
			}
			if (IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_UInt64_IntPtr(Handle, Selector.GetHandle("didChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (ulong)mutationKind, objects.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_UInt32_IntPtr(Handle, Selector.GetHandle("didChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (uint)mutationKind, objects.Handle);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_UInt64_IntPtr(SuperHandle, Selector.GetHandle("didChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (ulong)mutationKind, objects.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_UInt32_IntPtr(SuperHandle, Selector.GetHandle("didChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (uint)mutationKind, objects.Handle);
			}
		}

		[Export("didChangeValueForKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void DidChangeValue(string forKey)
		{
			if (forKey == null)
			{
				throw new ArgumentNullException("forKey");
			}
			IntPtr arg = NSString.CreateNative(forKey);
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("didChangeValueForKey:"), arg);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("didChangeValueForKey:"), arg);
			}
			NSString.ReleaseNative(arg);
		}

		[Export("doesNotRecognizeSelector:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void DoesNotRecognizeSelector(Selector sel)
		{
			if (sel == null)
			{
				throw new ArgumentNullException("sel");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("doesNotRecognizeSelector:"), sel.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("doesNotRecognizeSelector:"), sel.Handle);
			}
		}

		[Export("dictionaryWithValuesForKeys:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSDictionary GetDictionaryOfValuesFromKeys(NSString[] keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			NSArray nSArray = NSArray.FromNSObjects(keys);
			NSDictionary result = (!IsDirectBinding) ? Runtime.GetNSObject<NSDictionary>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("dictionaryWithValuesForKeys:"), nSArray.Handle)) : Runtime.GetNSObject<NSDictionary>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(Handle, Selector.GetHandle("dictionaryWithValuesForKeys:"), nSArray.Handle));
			nSArray.Dispose();
			return result;
		}

		[Export("keyPathsForValuesAffectingValueForKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static NSSet GetKeyPathsForValuesAffecting(NSString key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return Runtime.GetNSObject<NSSet>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(class_ptr, Selector.GetHandle("keyPathsForValuesAffectingValueForKey:"), key.Handle));
		}

		[Export("methodForSelector:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual IntPtr GetMethodForSelector(Selector sel)
		{
			if (sel == null)
			{
				throw new ArgumentNullException("sel");
			}
			if (IsDirectBinding)
			{
				return ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(Handle, Selector.GetHandle("methodForSelector:"), sel.Handle);
			}
			return ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("methodForSelector:"), sel.Handle);
		}

		[Export("hash")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual nuint GetNativeHash()
		{
			if (IsDirectBinding)
			{
				return ObjCRuntime.Messaging.nuint_objc_msgSend(Handle, Selector.GetHandle("hash"));
			}
			return ObjCRuntime.Messaging.nuint_objc_msgSendSuper(SuperHandle, Selector.GetHandle("hash"));
		}

		[Export("isEqual:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool IsEqual(NSObject? anObject)
		{
			if (IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(Handle, Selector.GetHandle("isEqual:"), anObject?.Handle ?? IntPtr.Zero);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("isEqual:"), anObject?.Handle ?? IntPtr.Zero);
		}

		[Export("isKindOfClass:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool IsKindOfClass(Class? aClass)
		{
			if (IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(Handle, Selector.GetHandle("isKindOfClass:"), aClass?.Handle ?? IntPtr.Zero);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("isKindOfClass:"), aClass?.Handle ?? IntPtr.Zero);
		}

		[Export("isMemberOfClass:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool IsMemberOfClass(Class? aClass)
		{
			if (IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(Handle, Selector.GetHandle("isMemberOfClass:"), aClass?.Handle ?? IntPtr.Zero);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("isMemberOfClass:"), aClass?.Handle ?? IntPtr.Zero);
		}

		[Export("mutableCopy")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[return: Release]
		public virtual NSObject MutableCopy()
		{
			if (!(this is INSMutableCopying))
			{
				throw new InvalidOperationException("Type does not conform to NSMutableCopying");
			}
			NSObject nSObject = (!IsDirectBinding) ? Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(SuperHandle, Selector.GetHandle("mutableCopy"))) : Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend(Handle, Selector.GetHandle("mutableCopy")));
			if (nSObject != null)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(nSObject.Handle, Selector.GetHandle("release"));
			}
			return nSObject;
		}

		[Export("observeValueForKeyPath:ofObject:change:context:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (ofObject == null)
			{
				throw new ArgumentNullException("ofObject");
			}
			if (change == null)
			{
				throw new ArgumentNullException("change");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr_IntPtr(Handle, Selector.GetHandle("observeValueForKeyPath:ofObject:change:context:"), keyPath.Handle, ofObject.Handle, change.Handle, context);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("observeValueForKeyPath:ofObject:change:context:"), keyPath.Handle, ofObject.Handle, change.Handle, context);
			}
		}

		[Export("performSelector:withObject:afterDelay:inModes:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void PerformSelector(Selector selector, NSObject? withObject, double afterDelay, NSString[] nsRunLoopModes)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (nsRunLoopModes == null)
			{
				throw new ArgumentNullException("nsRunLoopModes");
			}
			NSArray nSArray = NSArray.FromNSObjects(nsRunLoopModes);
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_Double_IntPtr(Handle, Selector.GetHandle("performSelector:withObject:afterDelay:inModes:"), selector.Handle, withObject?.Handle ?? IntPtr.Zero, afterDelay, nSArray.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_Double_IntPtr(SuperHandle, Selector.GetHandle("performSelector:withObject:afterDelay:inModes:"), selector.Handle, withObject?.Handle ?? IntPtr.Zero, afterDelay, nSArray.Handle);
			}
			nSArray.Dispose();
		}

		[Export("performSelector:withObject:afterDelay:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void PerformSelector(Selector selector, NSObject? withObject, double delay)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_Double(Handle, Selector.GetHandle("performSelector:withObject:afterDelay:"), selector.Handle, withObject?.Handle ?? IntPtr.Zero, delay);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_Double(SuperHandle, Selector.GetHandle("performSelector:withObject:afterDelay:"), selector.Handle, withObject?.Handle ?? IntPtr.Zero, delay);
			}
		}

		[Export("performSelector:onThread:withObject:waitUntilDone:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void PerformSelector(Selector selector, NSThread onThread, NSObject? withObject, bool waitUntilDone)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (onThread == null)
			{
				throw new ArgumentNullException("onThread");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr_bool(Handle, Selector.GetHandle("performSelector:onThread:withObject:waitUntilDone:"), selector.Handle, onThread.Handle, withObject?.Handle ?? IntPtr.Zero, waitUntilDone);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr_bool(SuperHandle, Selector.GetHandle("performSelector:onThread:withObject:waitUntilDone:"), selector.Handle, onThread.Handle, withObject?.Handle ?? IntPtr.Zero, waitUntilDone);
			}
		}

		[Export("performSelector:onThread:withObject:waitUntilDone:modes:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void PerformSelector(Selector selector, NSThread onThread, NSObject? withObject, bool waitUntilDone, NSString[] nsRunLoopModes)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (onThread == null)
			{
				throw new ArgumentNullException("onThread");
			}
			if (nsRunLoopModes == null)
			{
				throw new ArgumentNullException("nsRunLoopModes");
			}
			NSArray nSArray = NSArray.FromNSObjects(nsRunLoopModes);
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr_bool_IntPtr(Handle, Selector.GetHandle("performSelector:onThread:withObject:waitUntilDone:modes:"), selector.Handle, onThread.Handle, withObject?.Handle ?? IntPtr.Zero, waitUntilDone, nSArray.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr_bool_IntPtr(SuperHandle, Selector.GetHandle("performSelector:onThread:withObject:waitUntilDone:modes:"), selector.Handle, onThread.Handle, withObject?.Handle ?? IntPtr.Zero, waitUntilDone, nSArray.Handle);
			}
			nSArray.Dispose();
		}

		[Export("performSelector:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject PerformSelector(Selector aSelector)
		{
			if (aSelector == null)
			{
				throw new ArgumentNullException("aSelector");
			}
			if (IsDirectBinding)
			{
				return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(Handle, Selector.GetHandle("performSelector:"), aSelector.Handle));
			}
			return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("performSelector:"), aSelector.Handle));
		}

		[Export("performSelector:withObject:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject PerformSelector(Selector aSelector, NSObject? anObject)
		{
			if (aSelector == null)
			{
				throw new ArgumentNullException("aSelector");
			}
			if (IsDirectBinding)
			{
				return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr(Handle, Selector.GetHandle("performSelector:withObject:"), aSelector.Handle, anObject?.Handle ?? IntPtr.Zero));
			}
			return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("performSelector:withObject:"), aSelector.Handle, anObject?.Handle ?? IntPtr.Zero));
		}

		[Export("performSelector:withObject:withObject:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject PerformSelector(Selector aSelector, NSObject? object1, NSObject? object2)
		{
			if (aSelector == null)
			{
				throw new ArgumentNullException("aSelector");
			}
			if (IsDirectBinding)
			{
				return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr_IntPtr(Handle, Selector.GetHandle("performSelector:withObject:withObject:"), aSelector.Handle, object1?.Handle ?? IntPtr.Zero, object2?.Handle ?? IntPtr.Zero));
			}
			return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("performSelector:withObject:withObject:"), aSelector.Handle, object1?.Handle ?? IntPtr.Zero, object2?.Handle ?? IntPtr.Zero));
		}

		[Export("prepareForInterfaceBuilder")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void PrepareForInterfaceBuilder()
		{
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(Handle, Selector.GetHandle("prepareForInterfaceBuilder"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(SuperHandle, Selector.GetHandle("prepareForInterfaceBuilder"));
			}
		}

		[Export("removeObserver:forKeyPath:context:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void RemoveObserver(NSObject observer, NSString keyPath, IntPtr context)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr(Handle, Selector.GetHandle("removeObserver:forKeyPath:context:"), observer.Handle, keyPath.Handle, context);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("removeObserver:forKeyPath:context:"), observer.Handle, keyPath.Handle, context);
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public void RemoveObserver(NSObject observer, string keyPath, IntPtr context)
		{
			RemoveObserver(observer, (NSString)keyPath, context);
		}

		[Export("removeObserver:forKeyPath:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void RemoveObserver(NSObject observer, NSString keyPath)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(Handle, Selector.GetHandle("removeObserver:forKeyPath:"), observer.Handle, keyPath.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("removeObserver:forKeyPath:"), observer.Handle, keyPath.Handle);
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public void RemoveObserver(NSObject observer, string keyPath)
		{
			RemoveObserver(observer, (NSString)keyPath);
		}

		[Export("respondsToSelector:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool RespondsToSelector(Selector? sel)
		{
			if (IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(Handle, Selector.GetHandle("respondsToSelector:"), (sel == null) ? IntPtr.Zero : sel!.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("respondsToSelector:"), (sel == null) ? IntPtr.Zero : sel!.Handle);
		}

		[Export("setNilValueForKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetNilValueForKey(NSString key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("setNilValueForKey:"), key.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("setNilValueForKey:"), key.Handle);
			}
		}

		[Export("setValue:forKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetValueForKey(NSObject value, NSString key)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(Handle, Selector.GetHandle("setValue:forKey:"), value.Handle, key.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("setValue:forKey:"), value.Handle, key.Handle);
			}
		}

		[Export("setValue:forKeyPath:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetValueForKeyPath(NSObject value, NSString keyPath)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(Handle, Selector.GetHandle("setValue:forKeyPath:"), value.Handle, keyPath.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("setValue:forKeyPath:"), value.Handle, keyPath.Handle);
			}
		}

		[Export("setValue:forUndefinedKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetValueForUndefinedKey(NSObject value, NSString undefinedKey)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (undefinedKey == null)
			{
				throw new ArgumentNullException("undefinedKey");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(Handle, Selector.GetHandle("setValue:forUndefinedKey:"), value.Handle, undefinedKey.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("setValue:forUndefinedKey:"), value.Handle, undefinedKey.Handle);
			}
		}

		[Export("setValuesForKeysWithDictionary:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetValuesForKeysWithDictionary(NSDictionary keyedValues)
		{
			if (keyedValues == null)
			{
				throw new ArgumentNullException("keyedValues");
			}
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("setValuesForKeysWithDictionary:"), keyedValues.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("setValuesForKeysWithDictionary:"), keyedValues.Handle);
			}
		}

		[Export("valueForKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject ValueForKey(NSString key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (IsDirectBinding)
			{
				return Runtime.GetNSObject(ObjCRuntime.Messaging.xamarin_IntPtr_objc_msgSend_IntPtr(Handle, Selector.GetHandle("valueForKey:"), key.Handle));
			}
			return Runtime.GetNSObject(ObjCRuntime.Messaging.xamarin_IntPtr_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("valueForKey:"), key.Handle));
		}

		[Export("valueForKeyPath:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject ValueForKeyPath(NSString keyPath)
		{
			if (keyPath == null)
			{
				throw new ArgumentNullException("keyPath");
			}
			if (IsDirectBinding)
			{
				return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(Handle, Selector.GetHandle("valueForKeyPath:"), keyPath.Handle));
			}
			return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("valueForKeyPath:"), keyPath.Handle));
		}

		[Export("valueForUndefinedKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSObject ValueForUndefinedKey(NSString key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (IsDirectBinding)
			{
				return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(Handle, Selector.GetHandle("valueForUndefinedKey:"), key.Handle));
			}
			return Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("valueForUndefinedKey:"), key.Handle));
		}

		[Export("willChange:valuesAtIndexes:forKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void WillChange(NSKeyValueChange changeKind, NSIndexSet indexes, NSString forKey)
		{
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			if (forKey == null)
			{
				throw new ArgumentNullException("forKey");
			}
			if (IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_UInt64_IntPtr_IntPtr(Handle, Selector.GetHandle("willChange:valuesAtIndexes:forKey:"), (ulong)changeKind, indexes.Handle, forKey.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_UInt32_IntPtr_IntPtr(Handle, Selector.GetHandle("willChange:valuesAtIndexes:forKey:"), (uint)changeKind, indexes.Handle, forKey.Handle);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_UInt64_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("willChange:valuesAtIndexes:forKey:"), (ulong)changeKind, indexes.Handle, forKey.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_UInt32_IntPtr_IntPtr(SuperHandle, Selector.GetHandle("willChange:valuesAtIndexes:forKey:"), (uint)changeKind, indexes.Handle, forKey.Handle);
			}
		}

		[Export("willChangeValueForKey:withSetMutation:usingObjects:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void WillChange(NSString forKey, NSKeyValueSetMutationKind mutationKind, NSSet objects)
		{
			if (forKey == null)
			{
				throw new ArgumentNullException("forKey");
			}
			if (objects == null)
			{
				throw new ArgumentNullException("objects");
			}
			if (IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_UInt64_IntPtr(Handle, Selector.GetHandle("willChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (ulong)mutationKind, objects.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_UInt32_IntPtr(Handle, Selector.GetHandle("willChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (uint)mutationKind, objects.Handle);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_UInt64_IntPtr(SuperHandle, Selector.GetHandle("willChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (ulong)mutationKind, objects.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_UInt32_IntPtr(SuperHandle, Selector.GetHandle("willChangeValueForKey:withSetMutation:usingObjects:"), forKey.Handle, (uint)mutationKind, objects.Handle);
			}
		}

		[Export("willChangeValueForKey:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void WillChangeValue(string forKey)
		{
			if (forKey == null)
			{
				throw new ArgumentNullException("forKey");
			}
			IntPtr arg = NSString.CreateNative(forKey);
			if (IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(Handle, Selector.GetHandle("willChangeValueForKey:"), arg);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(SuperHandle, Selector.GetHandle("willChangeValueForKey:"), arg);
			}
			NSString.ReleaseNative(arg);
		}
	}
}
