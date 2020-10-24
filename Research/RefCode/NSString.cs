using CloudKit;
using ObjCRuntime;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Foundation
{
	[Register("NSString", true)]
	public class NSString : NSObject, IComparable<NSString>, INSCoding, INativeObject, IDisposable, INSCopying, INSItemProviderReading, INSItemProviderWriting, INSMutableCopying, INSSecureCoding, ICKRecordValue
	{
		private const string selDataUsingEncodingAllow = "dataUsingEncoding:allowLossyConversion:";

		private const string selUTF8String = "UTF8String";

		private const string selInitWithCharactersLength = "initWithCharacters:length:";

		public static readonly NSString Empty = new NSString(string.Empty);

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static readonly IntPtr class_ptr = ObjCRuntime.Class.GetHandle("NSString");

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionAllowLossyKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionDisallowedEncodingsKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionFromWindowsKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionLikelyLanguageKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionLossySubstitutionKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionSuggestedEncodingsKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _EncodingDetectionUseOnlySuggestedEncodingsKey;

		public char this[nint idx] => _characterAtIndex(idx);

		public override IntPtr ClassHandle => class_ptr;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool IsAbsolutePath
		{
			[Export("isAbsolutePath")]
			get
			{
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isAbsolutePath"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isAbsolutePath"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString LastPathComponent
		{
			[Export("lastPathComponent")]
			get
			{
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("lastPathComponent")));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("lastPathComponent")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual nint Length
		{
			[Export("length")]
			get
			{
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.nint_objc_msgSend(base.Handle, Selector.GetHandle("length"));
				}
				return ObjCRuntime.Messaging.nint_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("length"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		public virtual NSString LocalizedCapitalizedString
		{
			[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
			[Export("localizedCapitalizedString")]
			get
			{
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("localizedCapitalizedString")));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("localizedCapitalizedString")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		public virtual NSString LocalizedLowercaseString
		{
			[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
			[Export("localizedLowercaseString")]
			get
			{
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("localizedLowercaseString")));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("localizedLowercaseString")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		public virtual NSString LocalizedUppercaseString
		{
			[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
			[Export("localizedUppercaseString")]
			get
			{
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("localizedUppercaseString")));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("localizedUppercaseString")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string[] PathComponents
		{
			[Export("pathComponents")]
			get
			{
				if (base.IsDirectBinding)
				{
					return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("pathComponents")));
				}
				return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("pathComponents")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString PathExtension
		{
			[Export("pathExtension")]
			get
			{
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("pathExtension")));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("pathExtension")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		public static string[] ReadableTypeIdentifiers
		{
			[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
			[Export("readableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
			get
			{
				return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(class_ptr, Selector.GetHandle("readableTypeIdentifiersForItemProvider")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		public static string[] WritableTypeIdentifiers
		{
			[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
			[Export("writableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
			get
			{
				return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(class_ptr, Selector.GetHandle("writableTypeIdentifiersForItemProvider")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		public virtual string[] WritableTypeIdentifiersForItemProvider
		{
			[Export("writableTypeIdentifiersForItemProvider", ArgumentSemantic.Copy)]
			get
			{
				if (base.IsDirectBinding)
				{
					return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("writableTypeIdentifiersForItemProvider")));
				}
				return NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("writableTypeIdentifiersForItemProvider")));
			}
		}

		[Field("NSStringEncodingDetectionAllowLossyKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionAllowLossyKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionAllowLossyKey == null)
				{
					_EncodingDetectionAllowLossyKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionAllowLossyKey");
				}
				return _EncodingDetectionAllowLossyKey;
			}
		}

		[Field("NSStringEncodingDetectionDisallowedEncodingsKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionDisallowedEncodingsKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionDisallowedEncodingsKey == null)
				{
					_EncodingDetectionDisallowedEncodingsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionDisallowedEncodingsKey");
				}
				return _EncodingDetectionDisallowedEncodingsKey;
			}
		}

		[Field("NSStringEncodingDetectionFromWindowsKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionFromWindowsKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionFromWindowsKey == null)
				{
					_EncodingDetectionFromWindowsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionFromWindowsKey");
				}
				return _EncodingDetectionFromWindowsKey;
			}
		}

		[Field("NSStringEncodingDetectionLikelyLanguageKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionLikelyLanguageKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionLikelyLanguageKey == null)
				{
					_EncodingDetectionLikelyLanguageKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionLikelyLanguageKey");
				}
				return _EncodingDetectionLikelyLanguageKey;
			}
		}

		[Field("NSStringEncodingDetectionLossySubstitutionKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionLossySubstitutionKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionLossySubstitutionKey == null)
				{
					_EncodingDetectionLossySubstitutionKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionLossySubstitutionKey");
				}
				return _EncodingDetectionLossySubstitutionKey;
			}
		}

		[Field("NSStringEncodingDetectionSuggestedEncodingsKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionSuggestedEncodingsKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionSuggestedEncodingsKey == null)
				{
					_EncodingDetectionSuggestedEncodingsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionSuggestedEncodingsKey");
				}
				return _EncodingDetectionSuggestedEncodingsKey;
			}
		}

		[Field("NSStringEncodingDetectionUseOnlySuggestedEncodingsKey", "Foundation")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		internal static NSString EncodingDetectionUseOnlySuggestedEncodingsKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
			get
			{
				if (_EncodingDetectionUseOnlySuggestedEncodingsKey == null)
				{
					_EncodingDetectionUseOnlySuggestedEncodingsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.Foundation.Handle, "NSStringEncodingDetectionUseOnlySuggestedEncodingsKey");
				}
				return _EncodingDetectionUseOnlySuggestedEncodingsKey;
			}
		}

		public NSData Encode(NSStringEncoding enc, bool allowLossyConversion = false)
		{
			return new NSData(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_bool(base.Handle, Selector.GetHandle("dataUsingEncoding:allowLossyConversion:"), (IntPtr)(int)enc, allowLossyConversion));
		}

		public static NSString FromData(NSData data, NSStringEncoding encoding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			NSString result = null;
			try
			{
				result = new NSString(data, encoding);
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		public int CompareTo(NSString other)
		{
			return (int)Compare(other);
		}

		[Obsolete("Use 'GetLocalizedUserNotificationString' that takes 'NSString' to preserve localization.")]
		public static string GetLocalizedUserNotificationString(string key, params NSObject[] arguments)
		{
			return GetLocalizedUserNotificationString((NSString)key, arguments);
		}

		internal NSString(IntPtr handle, bool alloced)
			: base(handle, alloced)
		{
		}

		private unsafe static IntPtr CreateWithCharacters(IntPtr handle, string str, int offset, int length, bool autorelease = false)
		{
			fixed (char* ptr = str)
			{
				IntPtr arg = (IntPtr)(void*)(ptr + offset);
				handle = ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr(handle, Selector.GetHandle("initWithCharacters:length:"), arg, (IntPtr)length);
				if (autorelease)
				{
					NSObject.DangerousAutorelease(handle);
				}
				return handle;
			}
		}

		public static IntPtr CreateNative(string str)
		{
			return CreateNative(str, autorelease: false);
		}

		public static IntPtr CreateNative(string str, bool autorelease)
		{
			if (str == null)
			{
				return IntPtr.Zero;
			}
			return CreateNative(str, 0, str.Length, autorelease);
		}

		public static IntPtr CreateNative(string value, int start, int length)
		{
			return CreateNative(value, start, length, autorelease: false);
		}

		public static IntPtr CreateNative(string value, int start, int length, bool autorelease)
		{
			if (value == null)
			{
				return IntPtr.Zero;
			}
			if (start < 0 || start > value.Length)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (length < 0 || start > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			return CreateWithCharacters(ObjCRuntime.Messaging.IntPtr_objc_msgSend(class_ptr, Selector.GetHandle("alloc")), value, start, length, autorelease);
		}

		public static void ReleaseNative(IntPtr handle)
		{
			NSObject.DangerousRelease(handle);
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public NSString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			base.Handle = CreateWithCharacters(base.Handle, str, 0, str.Length);
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public NSString(string value, int start, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (start < 0 || start > value.Length)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (length < 0 || start > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			base.Handle = CreateWithCharacters(base.Handle, value, start, length);
		}

		public override string ToString()
		{
			return FromHandle(base.Handle);
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public static implicit operator string(NSString str)
		{
			return str?.ToString();
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public static explicit operator NSString(string str)
		{
			if (str == null)
			{
				return null;
			}
			return new NSString(str);
		}

		public static string FromHandle(IntPtr usrhandle)
		{
			if (usrhandle == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAuto(ObjCRuntime.Messaging.IntPtr_objc_msgSend(usrhandle, Selector.GetHandle("UTF8String")));
		}

		public static bool Equals(NSString a, NSString b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			if (a.Handle == b.Handle)
			{
				return true;
			}
			return a.IsEqualTo(b.Handle);
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public static bool operator ==(NSString a, NSString b)
		{
			return Equals(a, b);
		}

		[System.Runtime.CompilerServices.NullableContext(0)]
		public static bool operator !=(NSString a, NSString b)
		{
			return !Equals(a, b);
		}

		public override bool Equals(object obj)
		{
			return Equals(this, obj as NSString);
		}

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format(IntPtr fmt);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_1(IntPtr fmt, IntPtr arg1);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_2(IntPtr fmt, IntPtr arg1, IntPtr arg2);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_3(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_4(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_5(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_6(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_7(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_8(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7, IntPtr arg8);

		[DllImport("__Internal")]
		private static extern IntPtr xamarin_localized_string_format_9(IntPtr fmt, IntPtr arg1, IntPtr arg2, IntPtr arg3, IntPtr arg4, IntPtr arg5, IntPtr arg6, IntPtr arg7, IntPtr arg8, IntPtr arg9);

		public static NSString LocalizedFormat(string format, params object[] args)
		{
			using (NSString format2 = new NSString(format))
			{
				return LocalizedFormat(format2, args);
			}
		}

		public static NSString LocalizedFormat(NSString format, params object[] args)
		{
			int num = args.Length;
			NSObject[] array = new NSObject[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = NSObject.FromObject(args[i]);
			}
			return LocalizedFormat(format, array);
		}

		public static NSString LocalizedFormat(NSString format, NSObject[] args)
		{
			switch (args.Length)
			{
			case 0:
				return new NSString(xamarin_localized_string_format(format.Handle));
			case 1:
				return new NSString(xamarin_localized_string_format_1(format.Handle, args[0].Handle));
			case 2:
				return new NSString(xamarin_localized_string_format_2(format.Handle, args[0].Handle, args[1].Handle));
			case 3:
				return new NSString(xamarin_localized_string_format_3(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle));
			case 4:
				return new NSString(xamarin_localized_string_format_4(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle, args[3].Handle));
			case 5:
				return new NSString(xamarin_localized_string_format_5(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle, args[3].Handle, args[4].Handle));
			case 6:
				return new NSString(xamarin_localized_string_format_6(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle, args[3].Handle, args[4].Handle, args[5].Handle));
			case 7:
				return new NSString(xamarin_localized_string_format_7(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle, args[3].Handle, args[4].Handle, args[5].Handle, args[6].Handle));
			case 8:
				return new NSString(xamarin_localized_string_format_8(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle, args[3].Handle, args[4].Handle, args[5].Handle, args[6].Handle, args[7].Handle));
			case 9:
				return new NSString(xamarin_localized_string_format_9(format.Handle, args[0].Handle, args[1].Handle, args[2].Handle, args[3].Handle, args[4].Handle, args[5].Handle, args[6].Handle, args[7].Handle, args[8].Handle));
			default:
				throw new Exception("Unsupported number of arguments, maximum number is 9");
			}
		}

		public NSString TransliterateString(NSStringTransform transform, bool reverse)
		{
			return TransliterateString(transform.GetConstant(), reverse);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[DesignatedInitializer]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Export("init")]
		public NSString()
			: base(NSObjectFlag.Empty)
		{
			if (base.IsDirectBinding)
			{
				InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("init")), "init");
			}
			else
			{
				InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("init")), "init");
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[DesignatedInitializer]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Export("initWithCoder:")]
		public NSString(NSCoder coder)
			: base(NSObjectFlag.Empty)
		{
			if (base.IsDirectBinding)
			{
				InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("initWithCoder:"), coder.Handle), "initWithCoder:");
			}
			else
			{
				InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("initWithCoder:"), coder.Handle), "initWithCoder:");
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected NSString(NSObjectFlag t)
			: base(t)
		{
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal NSString(IntPtr handle)
			: base(handle)
		{
		}

		[Export("initWithData:encoding:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public NSString(NSData data, NSStringEncoding encoding)
			: base(NSObjectFlag.Empty)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_UInt64(base.Handle, Selector.GetHandle("initWithData:encoding:"), data.Handle, (ulong)encoding), "initWithData:encoding:");
				}
				else
				{
					InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_UInt32(base.Handle, Selector.GetHandle("initWithData:encoding:"), data.Handle, (uint)encoding), "initWithData:encoding:");
				}
			}
			else if (IntPtr.Size == 8)
			{
				InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_UInt64(base.SuperHandle, Selector.GetHandle("initWithData:encoding:"), data.Handle, (ulong)encoding), "initWithData:encoding:");
			}
			else
			{
				InitializeHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_UInt32(base.SuperHandle, Selector.GetHandle("initWithData:encoding:"), data.Handle, (uint)encoding), "initWithData:encoding:");
			}
		}

		[Export("stringByAbbreviatingWithTildeInPath")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString AbbreviateTildeInPath()
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("stringByAbbreviatingWithTildeInPath")));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("stringByAbbreviatingWithTildeInPath")));
		}

		[Export("stringByAppendingPathComponent:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString AppendPathComponent(NSString str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("stringByAppendingPathComponent:"), str.Handle));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("stringByAppendingPathComponent:"), str.Handle));
		}

		[Export("stringByAppendingPathExtension:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString AppendPathExtension(NSString str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("stringByAppendingPathExtension:"), str.Handle));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("stringByAppendingPathExtension:"), str.Handle));
		}

		[Export("stringsByAppendingPaths:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string[] AppendPaths(string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			NSArray nSArray = NSArray.FromStrings(paths);
			string[] result = (!base.IsDirectBinding) ? NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("stringsByAppendingPaths:"), nSArray.Handle)) : NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("stringsByAppendingPaths:"), nSArray.Handle));
			nSArray.Dispose();
			return result;
		}

		[Export("capitalizedStringWithLocale:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string Capitalize(NSLocale? locale)
		{
			if (base.IsDirectBinding)
			{
				return FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("capitalizedStringWithLocale:"), locale?.Handle ?? IntPtr.Zero));
			}
			return FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("capitalizedStringWithLocale:"), locale?.Handle ?? IntPtr.Zero));
		}

		[Export("commonPrefixWithString:options:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString CommonPrefix(NSString aString, NSStringCompareOptions options)
		{
			if (aString == null)
			{
				throw new ArgumentNullException("aString");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_UInt64(base.Handle, Selector.GetHandle("commonPrefixWithString:options:"), aString.Handle, (ulong)options));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_UInt32(base.Handle, Selector.GetHandle("commonPrefixWithString:options:"), aString.Handle, (uint)options));
			}
			if (IntPtr.Size == 8)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_UInt64(base.SuperHandle, Selector.GetHandle("commonPrefixWithString:options:"), aString.Handle, (ulong)options));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_UInt32(base.SuperHandle, Selector.GetHandle("commonPrefixWithString:options:"), aString.Handle, (uint)options));
		}

		[Export("compare:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSComparisonResult Compare(NSString aString)
		{
			if (aString == null)
			{
				throw new ArgumentNullException("aString");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("compare:"), aString.Handle);
				}
				return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("compare:"), aString.Handle);
			}
			if (IntPtr.Size == 8)
			{
				return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("compare:"), aString.Handle);
			}
			return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("compare:"), aString.Handle);
		}

		[Export("compare:options:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSComparisonResult Compare(NSString aString, NSStringCompareOptions mask)
		{
			if (aString == null)
			{
				throw new ArgumentNullException("aString");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSend_IntPtr_UInt64(base.Handle, Selector.GetHandle("compare:options:"), aString.Handle, (ulong)mask);
				}
				return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSend_IntPtr_UInt32(base.Handle, Selector.GetHandle("compare:options:"), aString.Handle, (uint)mask);
			}
			if (IntPtr.Size == 8)
			{
				return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSendSuper_IntPtr_UInt64(base.SuperHandle, Selector.GetHandle("compare:options:"), aString.Handle, (ulong)mask);
			}
			return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSendSuper_IntPtr_UInt32(base.SuperHandle, Selector.GetHandle("compare:options:"), aString.Handle, (uint)mask);
		}

		[Export("compare:options:range:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSComparisonResult Compare(NSString aString, NSStringCompareOptions mask, NSRange range)
		{
			if (aString == null)
			{
				throw new ArgumentNullException("aString");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSend_IntPtr_UInt64_NSRange(base.Handle, Selector.GetHandle("compare:options:range:"), aString.Handle, (ulong)mask, range);
				}
				return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSend_IntPtr_UInt32_NSRange(base.Handle, Selector.GetHandle("compare:options:range:"), aString.Handle, (uint)mask, range);
			}
			if (IntPtr.Size == 8)
			{
				return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSendSuper_IntPtr_UInt64_NSRange(base.SuperHandle, Selector.GetHandle("compare:options:range:"), aString.Handle, (ulong)mask, range);
			}
			return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSendSuper_IntPtr_UInt32_NSRange(base.SuperHandle, Selector.GetHandle("compare:options:range:"), aString.Handle, (uint)mask, range);
		}

		[Export("compare:options:range:locale:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSComparisonResult Compare(NSString aString, NSStringCompareOptions mask, NSRange range, NSLocale? locale)
		{
			if (aString == null)
			{
				throw new ArgumentNullException("aString");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSend_IntPtr_UInt64_NSRange_IntPtr(base.Handle, Selector.GetHandle("compare:options:range:locale:"), aString.Handle, (ulong)mask, range, locale?.Handle ?? IntPtr.Zero);
				}
				return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSend_IntPtr_UInt32_NSRange_IntPtr(base.Handle, Selector.GetHandle("compare:options:range:locale:"), aString.Handle, (uint)mask, range, locale?.Handle ?? IntPtr.Zero);
			}
			if (IntPtr.Size == 8)
			{
				return (NSComparisonResult)ObjCRuntime.Messaging.Int64_objc_msgSendSuper_IntPtr_UInt64_NSRange_IntPtr(base.SuperHandle, Selector.GetHandle("compare:options:range:locale:"), aString.Handle, (ulong)mask, range, locale?.Handle ?? IntPtr.Zero);
			}
			return (NSComparisonResult)ObjCRuntime.Messaging.int_objc_msgSendSuper_IntPtr_UInt32_NSRange_IntPtr(base.SuperHandle, Selector.GetHandle("compare:options:range:locale:"), aString.Handle, (uint)mask, range, locale?.Handle ?? IntPtr.Zero);
		}

		[Export("containsString:")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool Contains(NSString str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("containsString:"), str.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("containsString:"), str.Handle);
		}

		[Export("copyWithZone:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[return: Release]
		public virtual NSObject Copy(NSZone? zone)
		{
			NSObject nSObject = (!base.IsDirectBinding) ? Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("copyWithZone:"), zone?.Handle ?? IntPtr.Zero)) : Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("copyWithZone:"), zone?.Handle ?? IntPtr.Zero));
			if (nSObject != null)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(nSObject.Handle, Selector.GetHandle("release"));
			}
			return nSObject;
		}

		[Export("stringByDeletingLastPathComponent")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString DeleteLastPathComponent()
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("stringByDeletingLastPathComponent")));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("stringByDeletingLastPathComponent")));
		}

		[Export("stringByDeletingPathExtension")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString DeletePathExtension()
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("stringByDeletingPathExtension")));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("stringByDeletingPathExtension")));
		}

		[Export("stringEncodingForData:encodingOptions:convertedString:usedLossyConversion:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static nuint DetectStringEncoding(NSData rawData, NSDictionary options, out string convertedString, out bool usedLossyConversion)
		{
			if (rawData == null)
			{
				throw new ArgumentNullException("rawData");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			IntPtr arg = IntPtr.Zero;
			nuint result = ObjCRuntime.Messaging.nuint_objc_msgSend_IntPtr_IntPtr_ref_IntPtr_out_Boolean(class_ptr, Selector.GetHandle("stringEncodingForData:encodingOptions:convertedString:usedLossyConversion:"), rawData.Handle, options.Handle, ref arg, out usedLossyConversion);
			convertedString = FromHandle(arg);
			return result;
		}

		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static nuint DetectStringEncoding(NSData rawData, EncodingDetectionOptions options, out string convertedString, out bool usedLossyConversion)
		{
			return DetectStringEncoding(rawData, options.GetDictionary(), out convertedString, out usedLossyConversion);
		}

		[Export("encodeWithCoder:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void EncodeTo(NSCoder encoder)
		{
			if (encoder == null)
			{
				throw new ArgumentNullException("encoder");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("encodeWithCoder:"), encoder.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("encodeWithCoder:"), encoder.Handle);
			}
		}

		[Export("stringByExpandingTildeInPath")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString ExpandTildeInPath()
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("stringByExpandingTildeInPath")));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("stringByExpandingTildeInPath")));
		}

		[Export("itemProviderVisibilityForRepresentationWithTypeIdentifier:")]
		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSItemProviderRepresentationVisibility GetItemProviderVisibilityForTypeIdentifier(string typeIdentifier)
		{
			if (typeIdentifier == null)
			{
				throw new ArgumentNullException("typeIdentifier");
			}
			IntPtr intPtr = CreateNative(typeIdentifier);
			NSItemProviderRepresentationVisibility result = (NSItemProviderRepresentationVisibility)(base.IsDirectBinding ? ((IntPtr.Size != 8) ? ObjCRuntime.Messaging.int_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("itemProviderVisibilityForRepresentationWithTypeIdentifier:"), intPtr) : ObjCRuntime.Messaging.Int64_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("itemProviderVisibilityForRepresentationWithTypeIdentifier:"), intPtr)) : ((IntPtr.Size != 8) ? ObjCRuntime.Messaging.int_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("itemProviderVisibilityForRepresentationWithTypeIdentifier:"), intPtr) : ObjCRuntime.Messaging.Int64_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("itemProviderVisibilityForRepresentationWithTypeIdentifier:"), intPtr)));
			ReleaseNative(intPtr);
			return result;
		}

		[Export("getLineStart:end:contentsEnd:forRange:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void GetLineStart(out nuint startPtr, out nuint lineEndPtr, out nuint contentsEndPtr, NSRange range)
		{
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_out_nuint_out_nuint_out_nuint_NSRange(base.Handle, Selector.GetHandle("getLineStart:end:contentsEnd:forRange:"), out startPtr, out lineEndPtr, out contentsEndPtr, range);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_out_nuint_out_nuint_out_nuint_NSRange(base.SuperHandle, Selector.GetHandle("getLineStart:end:contentsEnd:forRange:"), out startPtr, out lineEndPtr, out contentsEndPtr, range);
			}
		}

		[Export("localizedUserNotificationStringForKey:arguments:")]
		[Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.WatchOS, 3, 0, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 14, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static NSString GetLocalizedUserNotificationString(NSString key, params NSObject[]? arguments)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			NSArray nSArray = (arguments == null) ? null : NSArray.FromNSObjects(arguments);
			NSString nSObject = Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr(class_ptr, Selector.GetHandle("localizedUserNotificationStringForKey:arguments:"), key.Handle, nSArray?.Handle ?? IntPtr.Zero));
			nSArray?.Dispose();
			return nSObject;
		}

		[Export("objectWithItemProviderData:typeIdentifier:error:")]
		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static NSString? GetObject(NSData data, string typeIdentifier, out NSError? outError)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (typeIdentifier == null)
			{
				throw new ArgumentNullException("typeIdentifier");
			}
			IntPtr arg = IntPtr.Zero;
			IntPtr intPtr = CreateNative(typeIdentifier);
			NSString nSObject = Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr_ref_IntPtr(class_ptr, Selector.GetHandle("objectWithItemProviderData:typeIdentifier:error:"), data.Handle, intPtr, ref arg));
			ReleaseNative(intPtr);
			outError = Runtime.GetNSObject<NSError>(arg);
			return nSObject;
		}

		[Export("getParagraphStart:end:contentsEnd:forRange:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void GetParagraphPositions(out nuint paragraphStartPosition, out nuint paragraphEndPosition, out nuint contentsEndPosition, NSRange range)
		{
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_out_nuint_out_nuint_out_nuint_NSRange(base.Handle, Selector.GetHandle("getParagraphStart:end:contentsEnd:forRange:"), out paragraphStartPosition, out paragraphEndPosition, out contentsEndPosition, range);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_out_nuint_out_nuint_out_nuint_NSRange(base.SuperHandle, Selector.GetHandle("getParagraphStart:end:contentsEnd:forRange:"), out paragraphStartPosition, out paragraphEndPosition, out contentsEndPosition, range);
			}
		}

		[Export("paragraphRangeForRange:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSRange GetParagraphRange(NSRange range)
		{
			NSRange retval;
			if (base.IsDirectBinding)
			{
				if (Runtime.Arch != 0)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.NSRange_objc_msgSend_NSRange(base.Handle, Selector.GetHandle("paragraphRangeForRange:"), range);
					}
					return ObjCRuntime.Messaging.NSRange_objc_msgSend_NSRange(base.Handle, Selector.GetHandle("paragraphRangeForRange:"), range);
				}
				if (IntPtr.Size == 8)
				{
					return ObjCRuntime.Messaging.NSRange_objc_msgSend_NSRange(base.Handle, Selector.GetHandle("paragraphRangeForRange:"), range);
				}
				ObjCRuntime.Messaging.NSRange_objc_msgSend_stret_NSRange(out retval, base.Handle, Selector.GetHandle("paragraphRangeForRange:"), range);
			}
			else
			{
				if (Runtime.Arch != 0)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_NSRange(base.SuperHandle, Selector.GetHandle("paragraphRangeForRange:"), range);
					}
					return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_NSRange(base.SuperHandle, Selector.GetHandle("paragraphRangeForRange:"), range);
				}
				if (IntPtr.Size == 8)
				{
					return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_NSRange(base.SuperHandle, Selector.GetHandle("paragraphRangeForRange:"), range);
				}
				ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_stret_NSRange(out retval, base.SuperHandle, Selector.GetHandle("paragraphRangeForRange:"), range);
			}
			return retval;
		}

		[Export("variantFittingPresentationWidth:")]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString GetVariantFittingPresentationWidth(nint width)
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_nint(base.Handle, Selector.GetHandle("variantFittingPresentationWidth:"), width));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_nint(base.SuperHandle, Selector.GetHandle("variantFittingPresentationWidth:"), width));
		}

		[Export("hasPrefix:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool HasPrefix(NSString prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("hasPrefix:"), prefix.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("hasPrefix:"), prefix.Handle);
		}

		[Export("hasSuffix:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool HasSuffix(NSString suffix)
		{
			if (suffix == null)
			{
				throw new ArgumentNullException("suffix");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("hasSuffix:"), suffix.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("hasSuffix:"), suffix.Handle);
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public bool IsEqualTo(IntPtr handle)
		{
			return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("isEqualToString:"), handle);
		}

		[Export("lineRangeForRange:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSRange LineRangeForRange(NSRange range)
		{
			NSRange retval;
			if (base.IsDirectBinding)
			{
				if (Runtime.Arch != 0)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.NSRange_objc_msgSend_NSRange(base.Handle, Selector.GetHandle("lineRangeForRange:"), range);
					}
					return ObjCRuntime.Messaging.NSRange_objc_msgSend_NSRange(base.Handle, Selector.GetHandle("lineRangeForRange:"), range);
				}
				if (IntPtr.Size == 8)
				{
					return ObjCRuntime.Messaging.NSRange_objc_msgSend_NSRange(base.Handle, Selector.GetHandle("lineRangeForRange:"), range);
				}
				ObjCRuntime.Messaging.NSRange_objc_msgSend_stret_NSRange(out retval, base.Handle, Selector.GetHandle("lineRangeForRange:"), range);
			}
			else
			{
				if (Runtime.Arch != 0)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_NSRange(base.SuperHandle, Selector.GetHandle("lineRangeForRange:"), range);
					}
					return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_NSRange(base.SuperHandle, Selector.GetHandle("lineRangeForRange:"), range);
				}
				if (IntPtr.Size == 8)
				{
					return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_NSRange(base.SuperHandle, Selector.GetHandle("lineRangeForRange:"), range);
				}
				ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_stret_NSRange(out retval, base.SuperHandle, Selector.GetHandle("lineRangeForRange:"), range);
			}
			return retval;
		}

		[Export("loadDataWithTypeIdentifier:forItemProviderCompletionHandler:")]
		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe virtual NSProgress? LoadData(string typeIdentifier, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDActionArity2V8))] Action<NSData, NSError> completionHandler)
		{
			if (typeIdentifier == null)
			{
				throw new ArgumentNullException("typeIdentifier");
			}
			if (completionHandler == null)
			{
				throw new ArgumentNullException("completionHandler");
			}
			IntPtr intPtr = CreateNative(typeIdentifier);
			BlockLiteral blockLiteral = default(BlockLiteral);
			BlockLiteral* ptr = &blockLiteral;
			blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDActionArity2V8.Handler, completionHandler);
			NSProgress result = (!base.IsDirectBinding) ? Runtime.GetNSObject<NSProgress>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("loadDataWithTypeIdentifier:forItemProviderCompletionHandler:"), intPtr, (IntPtr)(void*)ptr)) : Runtime.GetNSObject<NSProgress>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr(base.Handle, Selector.GetHandle("loadDataWithTypeIdentifier:forItemProviderCompletionHandler:"), intPtr, (IntPtr)(void*)ptr));
			ReleaseNative(intPtr);
			ptr->CleanupBlock();
			return result;
		}

		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual Task<NSData> LoadDataAsync(string typeIdentifier)
		{
			TaskCompletionSource<NSData> tcs = new TaskCompletionSource<NSData>();
			LoadData(typeIdentifier, (Action<NSData, NSError>)delegate(NSData arg1_, NSError arg2_)
			{
				if (arg2_ != null)
				{
					tcs.SetException(new NSErrorException(arg2_));
				}
				else
				{
					tcs.SetResult(arg1_);
				}
			});
			return tcs.Task;
		}

		[Introduced(PlatformName.WatchOS, 4, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 13, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 11, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual Task<NSData> LoadDataAsync(string typeIdentifier, out NSProgress result)
		{
			TaskCompletionSource<NSData> tcs = new TaskCompletionSource<NSData>();
			result = LoadData(typeIdentifier, (Action<NSData, NSError>)delegate(NSData arg1_, NSError arg2_)
			{
				if (arg2_ != null)
				{
					tcs.SetException(new NSErrorException(arg2_));
				}
				else
				{
					tcs.SetResult(arg1_);
				}
			});
			return tcs.Task;
		}

		[Export("localizedCaseInsensitiveContainsString:")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 10, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool LocalizedCaseInsensitiveContains(NSString str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("localizedCaseInsensitiveContainsString:"), str.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("localizedCaseInsensitiveContainsString:"), str.Handle);
		}

		[Export("localizedStandardContainsString:")]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool LocalizedStandardContainsString(NSString str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("localizedStandardContainsString:"), str.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("localizedStandardContainsString:"), str.Handle);
		}

		[Export("localizedStandardRangeOfString:")]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSRange LocalizedStandardRangeOfString(NSString str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			NSRange retval;
			if (base.IsDirectBinding)
			{
				if (Runtime.Arch != 0)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.NSRange_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
					}
					return ObjCRuntime.Messaging.NSRange_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
				}
				if (IntPtr.Size == 8)
				{
					return ObjCRuntime.Messaging.NSRange_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
				}
				ObjCRuntime.Messaging.NSRange_objc_msgSend_stret_IntPtr(out retval, base.Handle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
			}
			else
			{
				if (Runtime.Arch != 0)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
					}
					return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
				}
				if (IntPtr.Size == 8)
				{
					return ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
				}
				ObjCRuntime.Messaging.NSRange_objc_msgSendSuper_stret_IntPtr(out retval, base.SuperHandle, Selector.GetHandle("localizedStandardRangeOfString:"), str.Handle);
			}
			return retval;
		}

		[Export("mutableCopyWithZone:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[return: Release]
		public virtual NSObject MutableCopy(NSZone? zone)
		{
			NSObject nSObject = (!base.IsDirectBinding) ? Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("mutableCopyWithZone:"), zone?.Handle ?? IntPtr.Zero)) : Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("mutableCopyWithZone:"), zone?.Handle ?? IntPtr.Zero));
			if (nSObject != null)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(nSObject.Handle, Selector.GetHandle("release"));
			}
			return nSObject;
		}

		[Export("pathWithComponents:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static string[] PathWithComponents(string[] components)
		{
			if (components == null)
			{
				throw new ArgumentNullException("components");
			}
			NSArray nSArray = NSArray.FromStrings(components);
			string[] result = NSArray.StringArrayFromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(class_ptr, Selector.GetHandle("pathWithComponents:"), nSArray.Handle));
			nSArray.Dispose();
			return result;
		}

		[Export("stringByReplacingCharactersInRange:withString:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString Replace(NSRange range, NSString replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_NSRange_IntPtr(base.Handle, Selector.GetHandle("stringByReplacingCharactersInRange:withString:"), range, replacement.Handle));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_NSRange_IntPtr(base.SuperHandle, Selector.GetHandle("stringByReplacingCharactersInRange:withString:"), range, replacement.Handle));
		}

		[Export("stringByResolvingSymlinksInPath")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString ResolveSymlinksInPath()
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("stringByResolvingSymlinksInPath")));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("stringByResolvingSymlinksInPath")));
		}

		[Export("componentsSeparatedByString:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString[] SeparateComponents(NSString separator)
		{
			if (separator == null)
			{
				throw new ArgumentNullException("separator");
			}
			if (base.IsDirectBinding)
			{
				return NSArray.ArrayFromHandle<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("componentsSeparatedByString:"), separator.Handle));
			}
			return NSArray.ArrayFromHandle<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("componentsSeparatedByString:"), separator.Handle));
		}

		[Export("componentsSeparatedByCharactersInSet:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString[] SeparateComponents(NSCharacterSet separator)
		{
			if (separator == null)
			{
				throw new ArgumentNullException("separator");
			}
			if (base.IsDirectBinding)
			{
				return NSArray.ArrayFromHandle<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("componentsSeparatedByCharactersInSet:"), separator.Handle));
			}
			return NSArray.ArrayFromHandle<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("componentsSeparatedByCharactersInSet:"), separator.Handle));
		}

		[Export("stringByStandardizingPath")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString StandarizePath()
		{
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("stringByStandardizingPath")));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("stringByStandardizingPath")));
		}

		[Export("lowercaseStringWithLocale:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string ToLower(NSLocale locale)
		{
			if (locale == null)
			{
				throw new ArgumentNullException("locale");
			}
			if (base.IsDirectBinding)
			{
				return FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("lowercaseStringWithLocale:"), locale.Handle));
			}
			return FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("lowercaseStringWithLocale:"), locale.Handle));
		}

		[Export("uppercaseStringWithLocale:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual string ToUpper(NSLocale locale)
		{
			if (locale == null)
			{
				throw new ArgumentNullException("locale");
			}
			if (base.IsDirectBinding)
			{
				return FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("uppercaseStringWithLocale:"), locale.Handle));
			}
			return FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("uppercaseStringWithLocale:"), locale.Handle));
		}

		[Export("stringByApplyingTransform:reverse:")]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.MacOSX, 10, 11, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual NSString? TransliterateString(NSString transform, bool reverse)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (base.IsDirectBinding)
			{
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend_IntPtr_bool(base.Handle, Selector.GetHandle("stringByApplyingTransform:reverse:"), transform.Handle, reverse));
			}
			return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_IntPtr_bool(base.SuperHandle, Selector.GetHandle("stringByApplyingTransform:reverse:"), transform.Handle, reverse));
		}

		[Export("characterAtIndex:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		internal virtual char _characterAtIndex(nint index)
		{
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.Char_objc_msgSend_nint(base.Handle, Selector.GetHandle("characterAtIndex:"), index);
			}
			return ObjCRuntime.Messaging.Char_objc_msgSendSuper_nint(base.SuperHandle, Selector.GetHandle("characterAtIndex:"), index);
		}
	}
}
