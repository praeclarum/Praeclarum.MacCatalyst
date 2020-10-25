using CoreFoundation;
using Foundation;
using Mono;
using Registrar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using UIKit;

namespace ObjCRuntime
{
	public static class Runtime
	{
		internal delegate void register_nsobject_delegate(IntPtr managed_obj, IntPtr native_obj, out int exception_gchandle);

		internal delegate void register_assembly_delegate(IntPtr assembly, out int exception_gchandle);

		internal delegate void throw_ns_exception_delegate(IntPtr exc);

		internal delegate void rethrow_managed_exception_delegate(uint original_exception_gchandle, out int exception_gchandle);

		internal delegate int create_ns_exception_delegate(IntPtr exc, out int exception_gchandle);

		internal delegate IntPtr unwrap_ns_exception_delegate(uint exc_handle, out int exception_gchandle);

		internal delegate IntPtr get_block_wrapper_creator_delegate(IntPtr method, int parameter, out int exception_gchandle);

		internal delegate IntPtr create_block_proxy_delegate(IntPtr method, IntPtr block, out int exception_gchandle);

		internal delegate IntPtr create_delegate_proxy_delegate(IntPtr method, IntPtr block, IntPtr signature, uint token_ref, out int exception_gchandle);

		internal delegate void register_entry_assembly_delegate(IntPtr assembly, out int exception_gchandle);

		internal delegate IntPtr get_class_delegate(IntPtr ptr, out int exception_gchandle);

		internal delegate IntPtr get_selector_delegate(IntPtr ptr, out int exception_gchandle);

		internal delegate void get_method_for_selector_delegate(IntPtr cls, IntPtr sel, bool is_static, IntPtr desc, out int exception_gchandle);

		internal delegate IntPtr get_nsobject_delegate(IntPtr obj, out int exception_gchandle);

		internal delegate bool has_nsobject_delegate(IntPtr obj, out int exception_gchandle);

		internal delegate IntPtr get_handle_for_inativeobject_delegate(IntPtr obj, out int exception_gchandle);

		internal delegate void unregister_nsobject_delegate(IntPtr native_obj, IntPtr managed_obj, out int exception_gchandle);

		internal delegate IntPtr try_get_or_construct_nsobject_delegate(IntPtr obj, out int exception_gchandle);

		internal delegate IntPtr get_inative_object_dynamic_delegate(IntPtr obj, bool owns, IntPtr type, out int exception_gchandle);

		internal delegate IntPtr get_method_from_token_delegate(uint token_ref, out int exception_gchandle);

		internal delegate IntPtr get_generic_method_from_token_delegate(IntPtr obj, uint token_ref, out int exception_gchandle);

		internal delegate IntPtr get_inative_object_static_delegate(IntPtr obj, bool owns, uint iface_token_ref, uint implementation_token_ref, out int exception_gchandle);

		internal delegate IntPtr get_nsobject_with_type_delegate(IntPtr obj, IntPtr type, out bool created, out int exception_gchandle);

		internal delegate void dispose_delegate(IntPtr mobj, out int exception_gchandle);

		internal delegate bool is_parameter_transient_delegate(IntPtr method, int parameter, out int exception_gchandle);

		internal delegate bool is_parameter_out_delegate(IntPtr method, int parameter, out int exception_gchandle);

		internal delegate void get_method_and_object_for_selector_delegate(IntPtr cls, IntPtr sel, bool is_static, IntPtr obj, ref IntPtr mthis, IntPtr desc, out int exception_gchandle);

		internal delegate int create_product_exception_for_error_delegate(int code, uint inner_exception_gchandle, string message, out int exception_gchandle);

		internal delegate IntPtr reflection_type_get_full_name_delegate(IntPtr type, out int exception_gchandle);

		internal delegate IntPtr lookup_managed_type_name_delegate(IntPtr klass, out int exception_gchandle);

		internal delegate MarshalManagedExceptionMode on_marshal_managed_exception_delegate(int exception, out int exception_gchandle);

		internal delegate MarshalObjectiveCExceptionMode on_marshal_objectivec_exception_delegate(IntPtr exception, bool throwManagedAsDefault, out int exception_gchandle);

		internal delegate IntPtr convert_smart_enum_to_nsstring_delegate(IntPtr value, out int exception_gchandle);

		internal delegate IntPtr convert_nsstring_to_smart_enum_delegate(IntPtr value, IntPtr type, out int exception_gchandle);

		internal delegate int create_runtime_exception_delegate(int code, IntPtr message, out int exception_gchandle);

		internal struct Delegates
		{
			public IntPtr register_nsobject;

			public IntPtr register_assembly;

			public IntPtr throw_ns_exception;

			public IntPtr rethrow_managed_exception;

			public IntPtr create_ns_exception;

			public IntPtr unwrap_ns_exception;

			public IntPtr get_block_wrapper_creator;

			public IntPtr create_block_proxy;

			public IntPtr create_delegate_proxy;

			public IntPtr register_entry_assembly;

			public IntPtr get_class;

			public IntPtr get_selector;

			public IntPtr get_method_for_selector;

			public IntPtr get_nsobject;

			public IntPtr has_nsobject;

			public IntPtr get_handle_for_inativeobject;

			public IntPtr unregister_nsobject;

			public IntPtr try_get_or_construct_nsobject;

			public IntPtr get_inative_object_dynamic;

			public IntPtr get_method_from_token;

			public IntPtr get_generic_method_from_token;

			public IntPtr get_inative_object_static;

			public IntPtr get_nsobject_with_type;

			public IntPtr dispose;

			public IntPtr is_parameter_transient;

			public IntPtr is_parameter_out;

			public IntPtr get_method_and_object_for_selector;

			public IntPtr create_product_exception_for_error;

			public IntPtr reflection_type_get_full_name;

			public IntPtr lookup_managed_type_name;

			public IntPtr on_marshal_managed_exception;

			public IntPtr on_marshal_objectivec_exception;

			public IntPtr convert_smart_enum_to_nsstring;

			public IntPtr convert_nsstring_to_smart_enum;

			public IntPtr create_runtime_exception;
		}

		internal struct MTRegistrationMap
		{
			public IntPtr assembly;

			public unsafe MTClassMap* map;

			public IntPtr full_token_references;

			public unsafe MTManagedClassMap* skipped_map;

			public unsafe MTProtocolWrapperMap* protocol_wrapper_map;

			public MTProtocolMap protocol_map;

			public int assembly_count;

			public int map_count;

			public int full_token_reference_count;

			public int skipped_map_count;

			public int protocol_wrapper_count;

			public int protocol_count;
		}

		[Flags]
		internal enum MTTypeFlags : uint
		{
			None = 0x0,
			CustomType = 0x1,
			UserType = 0x2
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct MTClassMap
		{
			public IntPtr handle;

			public uint type_reference;

			public MTTypeFlags flags;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct MTManagedClassMap
		{
			public uint skipped_reference;

			public uint actual_reference;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct MTProtocolWrapperMap
		{
			public uint protocol_token;

			public uint wrapper_token;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct MTProtocolMap
		{
			public unsafe uint* protocol_tokens;

			public unsafe IntPtr* protocols;
		}

		internal struct Trampolines
		{
			public IntPtr tramp;

			public IntPtr stret_tramp;

			public IntPtr fpret_single_tramp;

			public IntPtr fpret_double_tramp;

			public IntPtr release_tramp;

			public IntPtr retain_tramp;

			public IntPtr static_tramp;

			public IntPtr ctor_tramp;

			public IntPtr x86_double_abi_stret_tramp;

			public IntPtr static_fpret_single_tramp;

			public IntPtr static_fpret_double_tramp;

			public IntPtr static_stret_tramp;

			public IntPtr x86_double_abi_static_stret_tramp;

			public IntPtr long_tramp;

			public IntPtr static_long_tramp;

			public IntPtr get_gchandle_tramp;

			public IntPtr set_gchandle_tramp;
		}

		[Flags]
		internal enum InitializationFlags
		{
			IsPartialStaticRegistrar = 0x1,
			DynamicRegistrar = 0x4,
			IsSimulator = 0x10
		}

		internal struct InitializationOptions
		{
			public int Size;

			public InitializationFlags Flags;

			public unsafe Delegates* Delegates;

			public unsafe Trampolines* Trampolines;

			public unsafe MTRegistrationMap* RegistrationMap;

			public MarshalObjectiveCExceptionMode MarshalObjectiveCExceptionMode;

			public MarshalManagedExceptionMode MarshalManagedExceptionMode;

			private IntPtr AssemblyLocations;

			public bool IsSimulator => (Flags & InitializationFlags.IsSimulator) == InitializationFlags.IsSimulator;
		}

		internal enum MissingCtorResolution
		{
			ThrowConstructor1NotFound,
			ThrowConstructor2NotFound,
			Ignore
		}

		private enum NXByteOrder
		{
			Unknown,
			LittleEndian,
			BigEndian
		}

		private struct NXArchInfo
		{
			private IntPtr name;

			public int CpuType;

			public int CpuSubType;

			public NXByteOrder ByteOrder;

			private IntPtr description;

			public string Name => Marshal.PtrToStringUTF8(name);

			public string Description => Marshal.PtrToStringUTF8(description);
		}

		private static Dictionary<IntPtrTypeValueTuple, Delegate> block_to_delegate_cache;

		private static Dictionary<Type, ConstructorInfo> intptr_ctor_cache;

		private static Dictionary<Type, ConstructorInfo> intptr_bool_ctor_cache;

		private static List<object> delegates;

		private static List<Assembly> assemblies;

		private static Dictionary<IntPtr, WeakReference> object_map;

		private static object lock_obj;

		private static IntPtr NSObjectClass;

		private static bool initialized;

		internal static IntPtrEqualityComparer IntPtrEqualityComparer;

		internal static TypeEqualityComparer TypeEqualityComparer;

		internal static global::Registrar.DynamicRegistrar Registrar;

		internal const uint INVALID_TOKEN_REF = uint.MaxValue;

		internal unsafe static InitializationOptions* options;

		private static bool has_autoreleasepool_in_thread_pool;

		private static MarshalObjectiveCExceptionMode objc_exception_mode;

		private static MarshalManagedExceptionMode managed_exception_mode;

		private static int MajorVersion = -1;

		private static int MinorVersion = -1;

		private static int BuildVersion = -1;

		public static bool IsARM64CallingConvention;

		internal const string ProductName = "Xamarin.iOS";

		internal const string AssemblyName = "Xamarin.iOS.dll";

		public static Arch Arch;

		[BindingImpl(BindingImplOptions.Optimizable)]
		public static bool DynamicRegistrationSupported => true;

		internal static bool Initialized => initialized;

		public static bool UseAutoreleasePoolInThreadPool
		{
			get
			{
				return has_autoreleasepool_in_thread_pool;
			}
			set
			{
				_ThreadPoolWaitCallback.SetDispatcher(value ? new Func<Func<bool>, bool>(ThreadPoolDispatcher) : null);
				has_autoreleasepool_in_thread_pool = value;
			}
		}

		public static event MarshalObjectiveCExceptionHandler MarshalObjectiveCException;

		public static event MarshalManagedExceptionHandler MarshalManagedException;

		[MonoPInvokeCallback(typeof(register_nsobject_delegate))]
		private static void register_nsobject(IntPtr managed_obj, IntPtr native_obj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				RegisterNSObject(managed_obj, native_obj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(register_assembly_delegate))]
		private static void register_assembly(IntPtr assembly, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				RegisterAssembly(assembly);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(throw_ns_exception_delegate))]
		private static void throw_ns_exception(IntPtr exc)
		{
			ThrowNSException(exc);
		}

		[MonoPInvokeCallback(typeof(rethrow_managed_exception_delegate))]
		private static void rethrow_managed_exception(uint original_exception_gchandle, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				RethrowManagedException(original_exception_gchandle);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(create_ns_exception_delegate))]
		private static int create_ns_exception(IntPtr exc, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return CreateNSException(exc);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return 0;
			}
		}

		[MonoPInvokeCallback(typeof(unwrap_ns_exception_delegate))]
		private static IntPtr unwrap_ns_exception(uint exc_handle, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return UnwrapNSException(exc_handle);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_block_wrapper_creator_delegate))]
		private static IntPtr get_block_wrapper_creator(IntPtr method, int parameter, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetBlockWrapperCreator(method, parameter);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(create_block_proxy_delegate))]
		private static IntPtr create_block_proxy(IntPtr method, IntPtr block, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return CreateBlockProxy(method, block);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(create_delegate_proxy_delegate))]
		private static IntPtr create_delegate_proxy(IntPtr method, IntPtr block, IntPtr signature, uint token_ref, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return CreateDelegateProxy(method, block, signature, token_ref);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(register_entry_assembly_delegate))]
		private static void register_entry_assembly(IntPtr assembly, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				RegisterEntryAssembly(assembly);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(get_class_delegate))]
		private static IntPtr get_class(IntPtr ptr, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetClass(ptr);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_selector_delegate))]
		private static IntPtr get_selector(IntPtr ptr, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetSelector(ptr);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_method_for_selector_delegate))]
		private static void get_method_for_selector(IntPtr cls, IntPtr sel, bool is_static, IntPtr desc, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				GetMethodForSelector(cls, sel, is_static, desc);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(get_nsobject_delegate))]
		private static IntPtr get_nsobject(IntPtr obj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetNSObjectWrapped(obj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(has_nsobject_delegate))]
		private static bool has_nsobject(IntPtr obj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return HasNSObject(obj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return false;
			}
		}

		[MonoPInvokeCallback(typeof(get_handle_for_inativeobject_delegate))]
		private static IntPtr get_handle_for_inativeobject(IntPtr obj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetHandleForINativeObject(obj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(unregister_nsobject_delegate))]
		private static void unregister_nsobject(IntPtr native_obj, IntPtr managed_obj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				UnregisterNSObject(native_obj, managed_obj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(try_get_or_construct_nsobject_delegate))]
		private static IntPtr try_get_or_construct_nsobject(IntPtr obj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return TryGetOrConstructNSObjectWrapped(obj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_inative_object_dynamic_delegate))]
		private static IntPtr get_inative_object_dynamic(IntPtr obj, bool owns, IntPtr type, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetINativeObject_Dynamic(obj, owns, type);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_method_from_token_delegate))]
		private static IntPtr get_method_from_token(uint token_ref, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetMethodFromToken(token_ref);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_generic_method_from_token_delegate))]
		private static IntPtr get_generic_method_from_token(IntPtr obj, uint token_ref, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetGenericMethodFromToken(obj, token_ref);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_inative_object_static_delegate))]
		private static IntPtr get_inative_object_static(IntPtr obj, bool owns, uint iface_token_ref, uint implementation_token_ref, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetINativeObject_Static(obj, owns, iface_token_ref, implementation_token_ref);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(get_nsobject_with_type_delegate))]
		private static IntPtr get_nsobject_with_type(IntPtr obj, IntPtr type, out bool created, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return GetNSObjectWithType(obj, type, out created);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				created = false;
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(dispose_delegate))]
		private static void dispose(IntPtr mobj, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				Dispose(mobj);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(is_parameter_transient_delegate))]
		private static bool is_parameter_transient(IntPtr method, int parameter, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return IsParameterTransient(method, parameter);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return false;
			}
		}

		[MonoPInvokeCallback(typeof(is_parameter_out_delegate))]
		private static bool is_parameter_out(IntPtr method, int parameter, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return IsParameterOut(method, parameter);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return false;
			}
		}

		[MonoPInvokeCallback(typeof(get_method_and_object_for_selector_delegate))]
		private static void get_method_and_object_for_selector(IntPtr cls, IntPtr sel, bool is_static, IntPtr obj, ref IntPtr mthis, IntPtr desc, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				GetMethodAndObjectForSelector(cls, sel, is_static, obj, ref mthis, desc);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
			}
		}

		[MonoPInvokeCallback(typeof(create_product_exception_for_error_delegate))]
		private static int create_product_exception_for_error(int code, uint inner_exception_gchandle, string message, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return CreateProductException(code, inner_exception_gchandle, message);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return 0;
			}
		}

		[MonoPInvokeCallback(typeof(reflection_type_get_full_name_delegate))]
		private static IntPtr reflection_type_get_full_name(IntPtr type, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return TypeGetFullName(type);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(lookup_managed_type_name_delegate))]
		private static IntPtr lookup_managed_type_name(IntPtr klass, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return LookupManagedTypeName(klass);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(on_marshal_managed_exception_delegate))]
		private static MarshalManagedExceptionMode on_marshal_managed_exception(int exception, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return OnMarshalManagedException(exception);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return MarshalManagedExceptionMode.Default;
			}
		}

		[MonoPInvokeCallback(typeof(on_marshal_objectivec_exception_delegate))]
		private static MarshalObjectiveCExceptionMode on_marshal_objectivec_exception(IntPtr exception, bool throwManagedAsDefault, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return OnMarshalObjectiveCException(exception, throwManagedAsDefault);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return MarshalObjectiveCExceptionMode.Default;
			}
		}

		[MonoPInvokeCallback(typeof(convert_smart_enum_to_nsstring_delegate))]
		private static IntPtr convert_smart_enum_to_nsstring(IntPtr value, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return ConvertSmartEnumToNSString(value);
			}
			catch (Exception value2)
			{
				GCHandle value3 = GCHandle.Alloc(value2, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value3).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(convert_nsstring_to_smart_enum_delegate))]
		private static IntPtr convert_nsstring_to_smart_enum(IntPtr value, IntPtr type, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return ConvertNSStringToSmartEnum(value, type);
			}
			catch (Exception value2)
			{
				GCHandle value3 = GCHandle.Alloc(value2, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value3).ToInt32();
				return (IntPtr)0;
			}
		}

		[MonoPInvokeCallback(typeof(create_runtime_exception_delegate))]
		private static int create_runtime_exception(int code, IntPtr message, out int exception_gchandle)
		{
			exception_gchandle = 0;
			try
			{
				return CreateRuntimeException(code, message);
			}
			catch (Exception value)
			{
				GCHandle value2 = GCHandle.Alloc(value, GCHandleType.Normal);
				exception_gchandle = GCHandle.ToIntPtr(value2).ToInt32();
				return 0;
			}
		}

		[BindingImpl(BindingImplOptions.Optimizable)]
		private unsafe static void RegisterDelegates(InitializationOptions* options)
		{
			options->Delegates->throw_ns_exception = GetFunctionPointer(new throw_ns_exception_delegate(throw_ns_exception));
			options->Delegates->rethrow_managed_exception = GetFunctionPointer(new rethrow_managed_exception_delegate(rethrow_managed_exception));
			options->Delegates->create_ns_exception = GetFunctionPointer(new create_ns_exception_delegate(create_ns_exception));
			options->Delegates->unwrap_ns_exception = GetFunctionPointer(new unwrap_ns_exception_delegate(unwrap_ns_exception));
			options->Delegates->create_block_proxy = GetFunctionPointer(new create_block_proxy_delegate(create_block_proxy));
			options->Delegates->create_delegate_proxy = GetFunctionPointer(new create_delegate_proxy_delegate(create_delegate_proxy));
			options->Delegates->get_class = GetFunctionPointer(new get_class_delegate(get_class));
			options->Delegates->get_selector = GetFunctionPointer(new get_selector_delegate(get_selector));
			options->Delegates->has_nsobject = GetFunctionPointer(new has_nsobject_delegate(has_nsobject));
			options->Delegates->get_handle_for_inativeobject = GetFunctionPointer(new get_handle_for_inativeobject_delegate(get_handle_for_inativeobject));
			options->Delegates->unregister_nsobject = GetFunctionPointer(new unregister_nsobject_delegate(unregister_nsobject));
			options->Delegates->try_get_or_construct_nsobject = GetFunctionPointer(new try_get_or_construct_nsobject_delegate(try_get_or_construct_nsobject));
			options->Delegates->get_inative_object_dynamic = GetFunctionPointer(new get_inative_object_dynamic_delegate(get_inative_object_dynamic));
			options->Delegates->get_method_from_token = GetFunctionPointer(new get_method_from_token_delegate(get_method_from_token));
			options->Delegates->get_generic_method_from_token = GetFunctionPointer(new get_generic_method_from_token_delegate(get_generic_method_from_token));
			options->Delegates->get_inative_object_static = GetFunctionPointer(new get_inative_object_static_delegate(get_inative_object_static));
			options->Delegates->get_nsobject_with_type = GetFunctionPointer(new get_nsobject_with_type_delegate(get_nsobject_with_type));
			options->Delegates->dispose = GetFunctionPointer(new dispose_delegate(dispose));
			options->Delegates->create_product_exception_for_error = GetFunctionPointer(new create_product_exception_for_error_delegate(create_product_exception_for_error));
			options->Delegates->reflection_type_get_full_name = GetFunctionPointer(new reflection_type_get_full_name_delegate(reflection_type_get_full_name));
			options->Delegates->lookup_managed_type_name = GetFunctionPointer(new lookup_managed_type_name_delegate(lookup_managed_type_name));
			options->Delegates->on_marshal_managed_exception = GetFunctionPointer(new on_marshal_managed_exception_delegate(on_marshal_managed_exception));
			options->Delegates->on_marshal_objectivec_exception = GetFunctionPointer(new on_marshal_objectivec_exception_delegate(on_marshal_objectivec_exception));
			options->Delegates->create_runtime_exception = GetFunctionPointer(new create_runtime_exception_delegate(create_runtime_exception));
			if (DynamicRegistrationSupported)
			{
				RegisterDelegatesDynamic(options);
			}
		}

		private unsafe static void RegisterDelegatesDynamic(InitializationOptions* options)
		{
			options->Delegates->register_nsobject = GetFunctionPointer(new register_nsobject_delegate(register_nsobject));
			options->Delegates->register_assembly = GetFunctionPointer(new register_assembly_delegate(register_assembly));
			options->Delegates->get_block_wrapper_creator = GetFunctionPointer(new get_block_wrapper_creator_delegate(get_block_wrapper_creator));
			options->Delegates->register_entry_assembly = GetFunctionPointer(new register_entry_assembly_delegate(register_entry_assembly));
			options->Delegates->get_method_for_selector = GetFunctionPointer(new get_method_for_selector_delegate(get_method_for_selector));
			options->Delegates->get_nsobject = GetFunctionPointer(new get_nsobject_delegate(get_nsobject));
			options->Delegates->is_parameter_transient = GetFunctionPointer(new is_parameter_transient_delegate(is_parameter_transient));
			options->Delegates->is_parameter_out = GetFunctionPointer(new is_parameter_out_delegate(is_parameter_out));
			options->Delegates->get_method_and_object_for_selector = GetFunctionPointer(new get_method_and_object_for_selector_delegate(get_method_and_object_for_selector));
			options->Delegates->convert_smart_enum_to_nsstring = GetFunctionPointer(new convert_smart_enum_to_nsstring_delegate(convert_smart_enum_to_nsstring));
			options->Delegates->convert_nsstring_to_smart_enum = GetFunctionPointer(new convert_nsstring_to_smart_enum_delegate(convert_nsstring_to_smart_enum));
		}

		[Preserve]
		[BindingImpl(BindingImplOptions.Optimizable)]
		private unsafe static void Initialize(InitializationOptions* options)
		{
			if (options->Size != Marshal.SizeOf(typeof(InitializationOptions)))
			{
				string text = "Version mismatch between the native Xamarin.iOS runtime and Xamarin.iOS.dll. Please reinstall Xamarin.iOS.";
				Console.Error.WriteLine(text);
				throw ErrorHelper.CreateError(8001, text);
			}
			if (IntPtr.Size != sizeof(nint))
			{
				string text2 = string.Format("Native type size mismatch between Xamarin.iOS.dll and the executing architecture. Xamarin.iOS.dll was built for {0}-bit, while the current process is {1}-bit.", (IntPtr.Size == 4) ? "64" : "32", (IntPtr.Size == 4) ? "32" : "64");
				Console.Error.WriteLine(text2);
				throw ErrorHelper.CreateError(8010, text2);
			}
			IntPtrEqualityComparer = new IntPtrEqualityComparer();
			TypeEqualityComparer = new TypeEqualityComparer();
			Runtime.options = options;
			delegates = new List<object>();
			object_map = new Dictionary<IntPtr, WeakReference>(IntPtrEqualityComparer);
			intptr_ctor_cache = new Dictionary<Type, ConstructorInfo>(TypeEqualityComparer);
			intptr_bool_ctor_cache = new Dictionary<Type, ConstructorInfo>(TypeEqualityComparer);
			lock_obj = new object();
			NSObjectClass = NSObject.Initialize();
			if (DynamicRegistrationSupported)
			{
				Registrar = new global::Registrar.DynamicRegistrar();
			}
			RegisterDelegates(options);
			Class.Initialize(options);
			SystemDependencyProvider.Initialize();
			InitializePlatform(options);
			UseAutoreleasePoolInThreadPool = true;
			IsARM64CallingConvention = GetIsARM64CallingConvention();
			objc_exception_mode = options->MarshalObjectiveCExceptionMode;
			managed_exception_mode = options->MarshalManagedExceptionMode;
			initialized = true;
		}

		private static bool ThreadPoolDispatcher(Func<bool> callback)
		{
			using (new NSAutoreleasePool())
			{
				return callback();
			}
		}

		private static MarshalObjectiveCExceptionMode OnMarshalObjectiveCException(IntPtr exception_handle, bool throwManagedAsDefault)
		{
			if (throwManagedAsDefault && Runtime.MarshalObjectiveCException == null)
			{
				return MarshalObjectiveCExceptionMode.ThrowManagedException;
			}
			if (Runtime.MarshalObjectiveCException != null)
			{
				NSException nSObject = GetNSObject<NSException>(exception_handle);
				MarshalObjectiveCExceptionEventArgs marshalObjectiveCExceptionEventArgs = new MarshalObjectiveCExceptionEventArgs
				{
					Exception = nSObject,
					ExceptionMode = (throwManagedAsDefault ? MarshalObjectiveCExceptionMode.ThrowManagedException : objc_exception_mode)
				};
				Runtime.MarshalObjectiveCException(null, marshalObjectiveCExceptionEventArgs);
				return marshalObjectiveCExceptionEventArgs.ExceptionMode;
			}
			return objc_exception_mode;
		}

		private static MarshalManagedExceptionMode OnMarshalManagedException(int exception_handle)
		{
			if (Runtime.MarshalManagedException != null)
			{
				Exception exception = GCHandle.FromIntPtr(new IntPtr(exception_handle)).Target as Exception;
				MarshalManagedExceptionEventArgs marshalManagedExceptionEventArgs = new MarshalManagedExceptionEventArgs
				{
					Exception = exception,
					ExceptionMode = managed_exception_mode
				};
				Runtime.MarshalManagedException(null, marshalManagedExceptionEventArgs);
				return marshalManagedExceptionEventArgs.ExceptionMode;
			}
			return managed_exception_mode;
		}

		private static IntPtr GetFunctionPointer(Delegate d)
		{
			delegates.Add(d);
			return Marshal.GetFunctionPointerForDelegate(d);
		}

		private static IntPtr ConvertSmartEnumToNSString(IntPtr value_handle)
		{
			object target = GCHandle.FromIntPtr(value_handle).Target;
			Type type = target.GetType();
			if (!Registrar.IsSmartEnum(type, out MethodBase getConstantMethod, out MethodBase _))
			{
				throw ErrorHelper.CreateError(8024, "Could not find a valid extension type for the smart enum '" + type.FullName + "'. Please file a bug at https://github.com/xamarin/xamarin-macios/issues/new.");
			}
			NSString nSString = (NSString)((MethodInfo)getConstantMethod).Invoke(null, new object[1]
			{
				target
			});
			if (nSString == null)
			{
				return IntPtr.Zero;
			}
			nSString.DangerousRetain().DangerousAutorelease();
			return nSString.Handle;
		}

		private static IntPtr ConvertNSStringToSmartEnum(IntPtr value, IntPtr type)
		{
			Type type2 = (Type)ObjectWrapper.Convert(type);
			NSString nSObject = GetNSObject<NSString>(value);
			if (!Registrar.IsSmartEnum(type2, out MethodBase _, out MethodBase getValueMethod))
			{
				throw ErrorHelper.CreateError(8024, "Could not find a valid extension type for the smart enum '" + type2.FullName + "'. Please file a bug at https://github.com/xamarin/xamarin-macios/issues/new.");
			}
			return GCHandle.ToIntPtr(GCHandle.Alloc(((MethodInfo)getValueMethod).Invoke(null, new object[1]
			{
				nSObject
			})));
		}

		private static void RegisterNSObject(IntPtr managed_obj, IntPtr native_obj)
		{
			RegisterNSObject((NSObject)ObjectWrapper.Convert(managed_obj), native_obj);
		}

		private static void RegisterAssembly(IntPtr a)
		{
			RegisterAssembly((Assembly)ObjectWrapper.Convert(a));
		}

		private static void RegisterEntryAssembly(IntPtr a)
		{
			RegisterEntryAssembly((Assembly)ObjectWrapper.Convert(a));
		}

		private static void ThrowNSException(IntPtr ns_exception)
		{
			throw new MonoTouchException(new NSException(ns_exception));
		}

		private static void RethrowManagedException(uint exception_gchandle)
		{
			ExceptionDispatchInfo.Capture((Exception)GCHandle.FromIntPtr((IntPtr)(long)exception_gchandle).Target).Throw();
		}

		private static int CreateNSException(IntPtr ns_exception)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new MonoTouchException(GetNSObject<NSException>(ns_exception)))).ToInt32();
		}

		private static int CreateRuntimeException(int code, IntPtr message)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(ErrorHelper.CreateError(code, Marshal.PtrToStringAuto(message)))).ToInt32();
		}

		private static IntPtr UnwrapNSException(uint exc_handle)
		{
			return (GCHandle.FromIntPtr(new IntPtr(exc_handle)).Target as MonoTouchException)?.NSException.DangerousRetain().DangerousAutorelease().Handle ?? IntPtr.Zero;
		}

		private static IntPtr GetBlockWrapperCreator(IntPtr method, int parameter)
		{
			return ObjectWrapper.Convert(GetBlockWrapperCreator((MethodInfo)ObjectWrapper.Convert(method), parameter));
		}

		private static IntPtr CreateBlockProxy(IntPtr method, IntPtr block)
		{
			return ObjectWrapper.Convert(CreateBlockProxy((MethodInfo)ObjectWrapper.Convert(method), block));
		}

		private static IntPtr CreateDelegateProxy(IntPtr method, IntPtr @delegate, IntPtr signature, uint token_ref)
		{
			return BlockLiteral.GetBlockForDelegate((MethodInfo)ObjectWrapper.Convert(method), ObjectWrapper.Convert(@delegate), token_ref, Marshal.PtrToStringAuto(signature));
		}

		private static Assembly GetEntryAssembly()
		{
			return Assembly.GetEntryAssembly();
		}

		internal static void RegisterAssemblies()
		{
			RegisterEntryAssembly(GetEntryAssembly());
		}

		internal static void RegisterEntryAssembly(Assembly entry_assembly)
		{
			List<Assembly> list = new List<Assembly>();
			list.Add(NSObject.PlatformAssembly);
			if (entry_assembly != null)
			{
				if (true)
				{
					CollectReferencedAssemblies(list, entry_assembly);
				}
			}
			else
			{
				Console.WriteLine("Could not find the entry assembly.");
			}
			foreach (Assembly item in list)
			{
				RegisterAssembly(item);
			}
		}

		private static void CollectReferencedAssemblies(List<Assembly> assemblies, Assembly assembly)
		{
			assemblies.Add(assembly);
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			foreach (AssemblyName assemblyRef in referencedAssemblies)
			{
				try
				{
					Assembly assembly2 = Assembly.Load(assemblyRef);
					if (!assemblies.Contains(assembly2))
					{
						CollectReferencedAssemblies(assemblies, assembly2);
					}
				}
				catch (FileNotFoundException ex)
				{
					NSLog("Could not find `{0}` referenced by assembly `{1}`.", ex.FileName, assembly.FullName);
				}
			}
		}

		internal static IEnumerable<Assembly> GetAssemblies()
		{
			return Registrar.GetAssemblies();
		}

		internal static string ComputeSignature(MethodInfo method, bool isBlockSignature)
		{
			return Registrar.ComputeSignature(method, isBlockSignature);
		}

		[BindingImpl(BindingImplOptions.Optimizable)]
		public static void RegisterAssembly(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (!DynamicRegistrationSupported)
			{
				throw ErrorHelper.CreateError(8026, "Runtime.RegisterAssembly is not supported when the dynamic registrar has been linked away.");
			}
			if (assemblies == null)
			{
				assemblies = new List<Assembly>();
				Class.Register(typeof(NSObject));
			}
			if (!assemblies.Contains(a))
			{
				assemblies.Add(a);
				Registrar.RegisterAssembly(a);
			}
		}

		private static IntPtr GetClass(IntPtr klass)
		{
			return ObjectWrapper.Convert(new Class(klass));
		}

		private static IntPtr GetSelector(IntPtr sel)
		{
			return ObjectWrapper.Convert(new Selector(sel));
		}

		private static void GetMethodForSelector(IntPtr cls, IntPtr sel, bool is_static, IntPtr desc)
		{
			Registrar.GetMethodDescription(Class.Lookup(cls), sel, is_static, desc);
		}

		private static IntPtr GetNSObjectWrapped(IntPtr ptr)
		{
			return ObjectWrapper.Convert(TryGetNSObject(ptr, evenInFinalizerQueue: true));
		}

		private static bool HasNSObject(IntPtr ptr)
		{
			return TryGetNSObject(ptr) != null;
		}

		private static IntPtr GetHandleForINativeObject(IntPtr ptr)
		{
			return ((INativeObject)ObjectWrapper.Convert(ptr)).Handle;
		}

		private static void UnregisterNSObject(IntPtr native_obj, IntPtr managed_obj)
		{
			NativeObjectHasDied(native_obj, ObjectWrapper.Convert(managed_obj) as NSObject);
		}

		private static IntPtr GetMethodFromToken(uint token_ref)
		{
			MethodBase methodBase = Class.ResolveMethodTokenReference(token_ref);
			if (methodBase != null)
			{
				return ObjectWrapper.Convert(methodBase);
			}
			return IntPtr.Zero;
		}

		private static IntPtr GetGenericMethodFromToken(IntPtr obj, uint token_ref)
		{
			MethodBase methodBase = Class.ResolveMethodTokenReference(token_ref);
			if (methodBase == null)
			{
				return IntPtr.Zero;
			}
			return ObjectWrapper.Convert(FindClosedMethod(((ObjectWrapper.Convert(obj) as NSObject) ?? throw ErrorHelper.CreateError(8023, $"An instance object is required to construct a closed generic method for the open generic method: {methodBase.DeclaringType.FullName}.{methodBase.Name} (token reference: 0x{token_ref:X}). Please file a bug report at https://github.com/xamarin/xamarin-macios/issues/new.")).GetType(), methodBase));
		}

		private static IntPtr TryGetOrConstructNSObjectWrapped(IntPtr ptr)
		{
			return ObjectWrapper.Convert(GetNSObject(ptr, MissingCtorResolution.Ignore, evenInFinalizerQueue: true));
		}

		private static IntPtr GetINativeObject_Dynamic(IntPtr ptr, bool owns, IntPtr type_ptr)
		{
			Type target_type = (Type)ObjectWrapper.Convert(type_ptr);
			return ObjectWrapper.Convert(GetINativeObject(ptr, owns, target_type));
		}

		private static IntPtr GetINativeObject_Static(IntPtr ptr, bool owns, uint iface_token, uint implementation_token)
		{
			Type target_type = Class.ResolveTypeTokenReference(iface_token);
			Type implementation = Class.ResolveTypeTokenReference(implementation_token);
			return ObjectWrapper.Convert(GetINativeObject(ptr, owns, target_type, implementation));
		}

		private static IntPtr GetNSObjectWithType(IntPtr ptr, IntPtr type_ptr, out bool created)
		{
			Type target_type = (Type)ObjectWrapper.Convert(type_ptr);
			return ObjectWrapper.Convert(GetNSObject(ptr, target_type, MissingCtorResolution.ThrowConstructor1NotFound, evenInFinalizerQueue: true, out created));
		}

		private static void Dispose(IntPtr mobj)
		{
			((IDisposable)ObjectWrapper.Convert(mobj)).Dispose();
		}

		private static bool IsParameterTransient(IntPtr info, int parameter)
		{
			MethodInfo methodInfo = ObjectWrapper.Convert(info) as MethodInfo;
			if (methodInfo == null)
			{
				return false;
			}
			methodInfo = methodInfo.GetBaseDefinition();
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length <= parameter)
			{
				return false;
			}
			return parameters[parameter].IsDefined(typeof(TransientAttribute), inherit: false);
		}

		private static bool IsParameterOut(IntPtr info, int parameter)
		{
			MethodInfo methodInfo = ObjectWrapper.Convert(info) as MethodInfo;
			if (methodInfo == null)
			{
				return false;
			}
			methodInfo = methodInfo.GetBaseDefinition();
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length <= parameter)
			{
				return false;
			}
			return parameters[parameter].IsOut;
		}

		private static void GetMethodAndObjectForSelector(IntPtr klass, IntPtr sel, bool is_static, IntPtr obj, ref IntPtr mthis, IntPtr desc)
		{
			Registrar.GetMethodDescriptionAndObject(Class.Lookup(klass), sel, is_static, obj, ref mthis, desc);
		}

		private static int CreateProductException(int code, uint inner_exception_gchandle, string msg)
		{
			Exception ex = null;
			if (inner_exception_gchandle != 0)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(new IntPtr(inner_exception_gchandle));
				ex = (Exception)gCHandle.Target;
				gCHandle.Free();
			}
			Exception value = (ex == null) ? ErrorHelper.CreateError(code, msg) : ErrorHelper.CreateError(code, ex, msg);
			return GCHandle.ToIntPtr(GCHandle.Alloc(value, GCHandleType.Normal)).ToInt32();
		}

		private static IntPtr TypeGetFullName(IntPtr type)
		{
			return Marshal.StringToHGlobalAuto(((Type)ObjectWrapper.Convert(type)).FullName);
		}

		private static IntPtr LookupManagedTypeName(IntPtr klass)
		{
			return Marshal.StringToHGlobalAuto(Class.LookupFullName(klass));
		}

		private static MethodInfo GetBlockProxyAttributeMethod(MethodInfo method, int parameter)
		{
			object[] customAttributes = method.GetParameters()[parameter].GetCustomAttributes(typeof(BlockProxyAttribute), inherit: true);
			if (customAttributes.Length == 1)
			{
				try
				{
					return (customAttributes[0] as BlockProxyAttribute).Type.GetMethod("Create");
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

		internal static ProtocolMemberAttribute GetProtocolMemberAttribute(Type type, string selector, MethodInfo method)
		{
			IEnumerable<ProtocolMemberAttribute> customAttributes = type.GetCustomAttributes<ProtocolMemberAttribute>();
			if (customAttributes == null)
			{
				return null;
			}
			foreach (ProtocolMemberAttribute item in customAttributes)
			{
				if (item.IsStatic == method.IsStatic && !(item.Selector != selector))
				{
					if (!item.IsProperty)
					{
						ParameterInfo[] parameters = method.GetParameters();
						Type[]? parameterType = item.ParameterType;
						if (((parameterType != null) ? parameterType!.Length : 0) != parameters.Length)
						{
							continue;
						}
						bool flag = false;
						for (int i = 0; i < parameters.Length; i++)
						{
							Type type2 = parameters[i].ParameterType;
							bool isByRef = type2.IsByRef;
							if (isByRef)
							{
								type2 = type2.GetElementType();
							}
							if (isByRef != item.ParameterByRef[i])
							{
								flag = true;
								break;
							}
							if (type2 != item.ParameterType[i])
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
					}
					return item;
				}
			}
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		private static MethodInfo GetBlockWrapperCreator(MethodInfo method, int parameter)
		{
			MethodInfo right = method;
			MethodInfo right2 = null;
			Type[] array = null;
			while (method != right2)
			{
				right2 = method;
				MethodInfo blockProxyAttributeMethod = GetBlockProxyAttributeMethod(method, parameter);
				if (blockProxyAttributeMethod != null)
				{
					return blockProxyAttributeMethod;
				}
				method = method.GetBaseDefinition();
			}
			string text = null;
			Type[] interfaces = method.DeclaringType.GetInterfaces();
			foreach (Type type in interfaces)
			{
				if (!type.IsDefined(typeof(ProtocolAttribute), inherit: false))
				{
					continue;
				}
				InterfaceMapping interfaceMap = method.DeclaringType.GetInterfaceMap(type);
				for (int j = 0; j < interfaceMap.TargetMethods.Length; j++)
				{
					if (interfaceMap.TargetMethods[j] == right)
					{
						MethodInfo blockProxyAttributeMethod2 = GetBlockProxyAttributeMethod(interfaceMap.InterfaceMethods[j], parameter);
						if (blockProxyAttributeMethod2 != null)
						{
							return blockProxyAttributeMethod2;
						}
					}
				}
				if (text == null)
				{
					text = (GetExportAttribute(method)?.Selector ?? string.Empty);
				}
				if (!string.IsNullOrEmpty(text))
				{
					ProtocolMemberAttribute protocolMemberAttribute = GetProtocolMemberAttribute(type, text, method);
					if (protocolMemberAttribute != null && protocolMemberAttribute.ParameterBlockProxy!.Length > parameter && protocolMemberAttribute.ParameterBlockProxy[parameter] != null)
					{
						return protocolMemberAttribute.ParameterBlockProxy[parameter]!.GetMethod("Create");
					}
				}
				string str = string.Empty;
				if (!string.IsNullOrEmpty(type.Namespace))
				{
					str = type.Namespace + ".";
				}
				str = str + type.Name.Substring(1) + "_Extensions";
				Type type2 = type.Assembly.GetType(str, throwOnError: false);
				if (!(type2 != null))
				{
					continue;
				}
				if (array == null)
				{
					ParameterInfo[] parameters = method.GetParameters();
					array = new Type[parameters.Length + 1];
					for (int k = 0; k < parameters.Length; k++)
					{
						array[k + 1] = parameters[k].ParameterType;
					}
				}
				array[0] = type;
				MethodInfo method2 = type2.GetMethod(method.Name, BindingFlags.Static | BindingFlags.Public, null, array, null);
				if (method2 != null)
				{
					MethodInfo blockProxyAttributeMethod3 = GetBlockProxyAttributeMethod(method2, parameter + 1);
					if (blockProxyAttributeMethod3 != null)
					{
						return blockProxyAttributeMethod3;
					}
				}
			}
			throw new RuntimeException(8009, true, "Unable to locate the block to delegate conversion method for the method {0}.{1}'s parameter #{2}. Please file a bug at https://github.com/xamarin/xamarin-macios/issues/new.", method.DeclaringType.FullName, method.Name, parameter + 1);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		private static Delegate CreateBlockProxy(MethodInfo method, IntPtr block)
		{
			return (Delegate)method.Invoke(null, new object[1]
			{
				block
			});
		}

		internal static Delegate GetDelegateForBlock(IntPtr methodPtr, Type type)
		{
			if (block_to_delegate_cache == null)
			{
				block_to_delegate_cache = new Dictionary<IntPtrTypeValueTuple, Delegate>();
			}
			IntPtrTypeValueTuple key = new IntPtrTypeValueTuple(methodPtr, type);
			Delegate value;
			lock (block_to_delegate_cache)
			{
				if (block_to_delegate_cache.TryGetValue(key, out value))
				{
					return value;
				}
			}
			value = Marshal.GetDelegateForFunctionPointer(methodPtr, type);
			lock (block_to_delegate_cache)
			{
				block_to_delegate_cache[key] = value;
				return value;
			}
		}

		private unsafe static MethodBase FindMethod(IntPtr typeptr, IntPtr methodptr, int paramCount, IntPtr* paramptr)
		{
			Type type = Type.GetType(Marshal.PtrToStringAuto(typeptr));
			string text = Marshal.PtrToStringAuto(methodptr);
			string[] array = new string[paramCount];
			for (int i = 0; i < paramCount; i++)
			{
				array[i] = Marshal.PtrToStringAuto(paramptr[i]);
			}
			MethodBase[] array2;
			MethodBase[] constructors;
			if (text == ".ctor")
			{
				constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				array2 = constructors;
			}
			else
			{
				constructors = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				array2 = constructors;
			}
			constructors = array2;
			foreach (MethodBase methodBase in constructors)
			{
				if (methodBase.Name != text)
				{
					continue;
				}
				ParameterInfo[] parameters = methodBase.GetParameters();
				if (parameters.Length != paramCount)
				{
					continue;
				}
				bool flag = true;
				for (int k = 0; k < paramCount; k++)
				{
					Type parameterType = parameters[k].ParameterType;
					string text2 = parameterType.AssemblyQualifiedName;
					if (parameterType.IsGenericType)
					{
						int num = 0;
						while ((num = text2.IndexOf(", Version=", num, StringComparison.OrdinalIgnoreCase)) != -1)
						{
							int num2 = text2.IndexOf(']', num);
							text2 = ((num2 == -1) ? text2.Substring(0, num) : text2.Remove(num, num2 - num));
						}
					}
					if (parameterType.Name != array[k] && !text2.StartsWith(array[k], StringComparison.Ordinal))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return methodBase;
				}
			}
			throw ErrorHelper.CreateError(8002, "Could not find the method '{0}' in the type '{1}'.", text, type.FullName);
		}

		internal static void UnregisterNSObject(IntPtr ptr)
		{
			lock (lock_obj)
			{
				object_map.Remove(ptr);
			}
		}

		private static void NativeObjectHasDied(IntPtr ptr, NSObject managed_obj)
		{
			lock (lock_obj)
			{
				if (object_map.TryGetValue(ptr, out WeakReference value) && (managed_obj == null || value.Target == managed_obj))
				{
					object_map.Remove(ptr);
				}
				managed_obj?.ClearHandle();
			}
		}

		internal static void RegisterNSObject(NSObject obj, IntPtr ptr)
		{
			lock (lock_obj)
			{
				object_map[ptr] = new WeakReference(obj, trackResurrection: true);
				obj.Handle = ptr;
			}
		}

		internal static PropertyInfo FindPropertyInfo(MethodInfo accessor)
		{
			if (!accessor.IsSpecialName)
			{
				return null;
			}
			PropertyInfo[] properties = accessor.DeclaringType.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.GetGetMethod() == accessor)
				{
					return propertyInfo;
				}
				if (propertyInfo.GetSetMethod() == accessor)
				{
					return propertyInfo;
				}
			}
			return null;
		}

		internal static ExportAttribute GetExportAttribute(MethodInfo method)
		{
			ExportAttribute customAttribute = method.GetCustomAttribute<ExportAttribute>();
			if (customAttribute == null)
			{
				PropertyInfo propertyInfo = FindPropertyInfo(method);
				if (propertyInfo != null)
				{
					customAttribute = propertyInfo.GetCustomAttribute<ExportAttribute>();
				}
			}
			return customAttribute;
		}

		private static NSObject IgnoreConstructionError(IntPtr ptr, IntPtr klass, Type type)
		{
			return null;
		}

		private static void MissingCtor(IntPtr ptr, IntPtr klass, Type type, MissingCtorResolution resolution)
		{
			if (klass == IntPtr.Zero)
			{
				klass = Class.GetClassForObject(ptr);
			}
			string format;
			switch (resolution)
			{
			default:
				return;
			case MissingCtorResolution.Ignore:
				return;
			case MissingCtorResolution.ThrowConstructor1NotFound:
				format = "Failed to marshal the Objective-C object 0x{0} (type: {1}). Could not find an existing managed instance for this object, nor was it possible to create a new managed instance (because the type '{2}' does not have a constructor that takes one IntPtr argument).";
				break;
			case MissingCtorResolution.ThrowConstructor2NotFound:
				format = "Failed to marshal the Objective-C object 0x{0} (type: {1}). Could not find an existing managed instance for this object, nor was it possible to create a new managed instance (because the type '{2}' does not have a constructor that takes two (IntPtr, bool) arguments).";
				break;
			}
			throw ErrorHelper.CreateError(8027, string.Format(format, ptr.ToString("x"), new Class(klass).Name, type.FullName));
		}

		private static NSObject ConstructNSObject(IntPtr ptr, IntPtr klass, MissingCtorResolution missingCtorResolution)
		{
			Type type = Class.Lookup(klass);
			if (type != null)
			{
				return ConstructNSObject<NSObject>(ptr, type, missingCtorResolution);
			}
			return new NSObject(ptr);
		}

		internal static T ConstructNSObject<T>(IntPtr ptr) where T : NSObject
		{
			return ConstructNSObject<T>(ptr, typeof(T), MissingCtorResolution.ThrowConstructor1NotFound);
		}

		private static T ConstructNSObject<T>(IntPtr ptr, Type type, MissingCtorResolution missingCtorResolution) where T : class, INativeObject
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ConstructorInfo intPtrConstructor = GetIntPtrConstructor(type);
			if (intPtrConstructor == null)
			{
				MissingCtor(ptr, IntPtr.Zero, type, missingCtorResolution);
				return null;
			}
			return (T)intPtrConstructor.Invoke(new object[1]
			{
				ptr
			});
		}

		private static T ConstructINativeObject<T>(IntPtr ptr, bool owns, Type type, MissingCtorResolution missingCtorResolution) where T : class, INativeObject
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			ConstructorInfo intPtr_BoolConstructor = GetIntPtr_BoolConstructor(type);
			if (intPtr_BoolConstructor == null)
			{
				MissingCtor(ptr, IntPtr.Zero, type, missingCtorResolution);
			}
			return (T)intPtr_BoolConstructor.Invoke(new object[2]
			{
				ptr,
				owns
			});
		}

		private static ConstructorInfo GetIntPtrConstructor(Type type)
		{
			lock (intptr_ctor_cache)
			{
				if (intptr_ctor_cache.TryGetValue(type, out ConstructorInfo value))
				{
					return value;
				}
			}
			ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < constructors.Length; i++)
			{
				ParameterInfo[] parameters = constructors[i].GetParameters();
				if (parameters.Length == 1 && parameters[0].ParameterType == typeof(IntPtr))
				{
					lock (intptr_ctor_cache)
					{
						intptr_ctor_cache[type] = constructors[i];
					}
					return constructors[i];
				}
			}
			return null;
		}

		private static ConstructorInfo GetIntPtr_BoolConstructor(Type type)
		{
			lock (intptr_bool_ctor_cache)
			{
				if (intptr_bool_ctor_cache.TryGetValue(type, out ConstructorInfo value))
				{
					return value;
				}
			}
			ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < constructors.Length; i++)
			{
				ParameterInfo[] parameters = constructors[i].GetParameters();
				if (parameters.Length == 2 && parameters[0].ParameterType == typeof(IntPtr) && parameters[1].ParameterType == typeof(bool))
				{
					lock (intptr_bool_ctor_cache)
					{
						intptr_bool_ctor_cache[type] = constructors[i];
					}
					return constructors[i];
				}
			}
			return null;
		}

		public static NSObject TryGetNSObject(IntPtr ptr)
		{
			return TryGetNSObject(ptr, evenInFinalizerQueue: false);
		}

		internal static NSObject TryGetNSObject(IntPtr ptr, bool evenInFinalizerQueue = false)
		{
			lock (lock_obj)
			{
				if (object_map.TryGetValue(ptr, out WeakReference value))
				{
					NSObject nSObject = (NSObject)value.Target;
					if (nSObject == null)
					{
						return null;
					}
					if (nSObject.InFinalizerQueue)
					{
						if (!evenInFinalizerQueue)
						{
							return null;
						}
						if (nSObject.IsDirectBinding && !nSObject.IsRegisteredToggleRef)
						{
							return null;
						}
					}
					return nSObject;
				}
			}
			return null;
		}

		public static NSObject GetNSObject(IntPtr ptr)
		{
			return GetNSObject(ptr, MissingCtorResolution.ThrowConstructor1NotFound);
		}

		internal static NSObject GetNSObject(IntPtr ptr, MissingCtorResolution missingCtorResolution, bool evenInFinalizerQueue = false)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			NSObject nSObject = TryGetNSObject(ptr, evenInFinalizerQueue);
			if (nSObject != null)
			{
				return nSObject;
			}
			return ConstructNSObject(ptr, Class.GetClassForObject(ptr), missingCtorResolution);
		}

		public static T GetNSObject<T>(IntPtr ptr) where T : NSObject
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			object obj = TryGetNSObject(ptr);
			T val;
			if (obj == null)
			{
				IntPtr classForObject = Class.GetClassForObject(ptr);
				Type type;
				if (classForObject != NSObjectClass)
				{
					type = Class.Lookup(classForObject);
					if (type == typeof(NSObject))
					{
						type = typeof(T);
					}
					else if (typeof(T).IsGenericType)
					{
						type = typeof(T);
					}
					else if (!type.IsSubclassOf(typeof(T)) && Messaging.bool_objc_msgSend_IntPtr(ptr, Selector.GetHandle("isKindOfClass:"), Class.GetHandle(typeof(T))))
					{
						type = typeof(T);
					}
				}
				else
				{
					type = typeof(NSObject);
				}
				val = ConstructNSObject<T>(ptr, type, MissingCtorResolution.ThrowConstructor1NotFound);
			}
			else
			{
				val = (obj as T);
				if (val == null)
				{
					throw new InvalidCastException($"Unable to cast object of type '{obj.GetType().FullName}' to type '{typeof(T).FullName}'");
				}
			}
			return val;
		}

		public static T GetNSObject<T>(IntPtr ptr, bool owns) where T : NSObject
		{
			T nSObject = GetNSObject<T>(ptr);
			if (owns)
			{
				nSObject?.DangerousRelease();
			}
			return nSObject;
		}

		private static NSObject GetNSObject(IntPtr ptr, Type target_type, MissingCtorResolution missingCtorResolution, bool evenInFinalizerQueue, out bool created)
		{
			created = false;
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			NSObject nSObject = TryGetNSObject(ptr, evenInFinalizerQueue);
			if (nSObject != null)
			{
				return nSObject;
			}
			IntPtr classForObject = Class.GetClassForObject(ptr);
			if (classForObject != NSObjectClass)
			{
				Type type = Class.Lookup(classForObject);
				if (!(type == typeof(NSObject)))
				{
					if (type.IsSubclassOf(target_type))
					{
						target_type = type;
					}
					else if (!Messaging.bool_objc_msgSend_IntPtr(ptr, Selector.GetHandle("isKindOfClass:"), Class.GetHandle(target_type)))
					{
						target_type = type;
					}
				}
			}
			else
			{
				target_type = typeof(NSObject);
			}
			created = true;
			return ConstructNSObject<NSObject>(ptr, target_type, MissingCtorResolution.ThrowConstructor1NotFound);
		}

		private static Type LookupINativeObjectImplementation(IntPtr ptr, Type target_type, Type implementation = null, bool forced_type = false)
		{
			if (!typeof(NSObject).IsAssignableFrom(target_type))
			{
				implementation = target_type;
			}
			else
			{
				IntPtr classForObject = Class.GetClassForObject(ptr);
				if (classForObject == NSObjectClass)
				{
					if (implementation == null)
					{
						implementation = target_type;
					}
				}
				else
				{
					Type type = Class.Lookup(classForObject, !forced_type);
					if (target_type.IsAssignableFrom(type))
					{
						implementation = type;
					}
					else if (implementation == null)
					{
						implementation = target_type;
					}
				}
			}
			if (implementation.IsInterface)
			{
				implementation = FindProtocolWrapperType(implementation);
			}
			return implementation;
		}

		public static INativeObject GetINativeObject(IntPtr ptr, bool owns, Type target_type)
		{
			return GetINativeObject(ptr, owns, target_type, null);
		}

		private static INativeObject GetINativeObject(IntPtr ptr, bool owns, Type target_type, Type implementation)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			NSObject nSObject = TryGetNSObject(ptr);
			if (nSObject != null && target_type.IsAssignableFrom(nSObject.GetType()))
			{
				return nSObject;
			}
			if (nSObject != null && !target_type.IsInterface)
			{
				throw new InvalidCastException($"Unable to cast object of type '{nSObject.GetType().FullName}' to type '{target_type.FullName}'.");
			}
			implementation = LookupINativeObjectImplementation(ptr, target_type, implementation);
			if (implementation.IsSubclassOf(typeof(NSObject)))
			{
				if (nSObject != null)
				{
					throw ErrorHelper.CreateError(8004, "Cannot create an instance of {0} for the native object 0x{1} (of type '{2}'), because another instance already exists for this native object (of type {3}).", implementation.FullName, ptr.ToString("x"), Class.class_getName(Class.GetClassForObject(ptr)), nSObject.GetType().FullName);
				}
				return ConstructNSObject<INativeObject>(ptr, implementation, MissingCtorResolution.ThrowConstructor1NotFound);
			}
			return ConstructINativeObject<INativeObject>(ptr, owns, implementation, MissingCtorResolution.ThrowConstructor2NotFound);
		}

		public static T GetINativeObject<T>(IntPtr ptr, bool owns) where T : class, INativeObject
		{
			return GetINativeObject<T>(ptr, forced_type: false, owns);
		}

		public static T GetINativeObject<T>(IntPtr ptr, bool forced_type, bool owns) where T : class, INativeObject
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			NSObject nSObject = TryGetNSObject(ptr);
			T val = nSObject as T;
			if (val != null)
			{
				return val;
			}
			if (nSObject != null && !forced_type && !typeof(T).IsInterface && typeof(NSObject).IsAssignableFrom(typeof(T)))
			{
				throw new InvalidCastException($"Unable to cast object of type '{nSObject.GetType().FullName}' to type '{typeof(T).FullName}'.");
			}
			Type type = LookupINativeObjectImplementation(ptr, typeof(T), null, forced_type);
			if (type.IsSubclassOf(typeof(NSObject)))
			{
				if (nSObject != null && !forced_type)
				{
					throw ErrorHelper.CreateError(8004, "Cannot create an instance of {0} for the native object 0x{1} (of type '{2}'), because another instance already exists for this native object (of type {3}).", type.FullName, ptr.ToString("x"), Class.class_getName(Class.GetClassForObject(ptr)), nSObject.GetType().FullName);
				}
				return ConstructNSObject<T>(ptr, type, MissingCtorResolution.ThrowConstructor1NotFound);
			}
			return ConstructINativeObject<T>(ptr, owns, type, MissingCtorResolution.ThrowConstructor2NotFound);
		}

		private unsafe static Type FindProtocolWrapperType(Type type)
		{
			if (type == null || !type.IsInterface)
			{
				return null;
			}
			if (options->RegistrationMap != null)
			{
				uint tokenReference = Class.GetTokenReference(type, throw_exception: false);
				if (tokenReference != uint.MaxValue)
				{
					uint num = xamarin_find_protocol_wrapper_type(tokenReference);
					if (num != uint.MaxValue)
					{
						return Class.ResolveTypeTokenReference(num);
					}
				}
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(ProtocolAttribute), inherit: false);
			ProtocolAttribute protocolAttribute = (ProtocolAttribute)((customAttributes.Length != 0) ? customAttributes[0] : null);
			if (protocolAttribute == null || protocolAttribute.WrapperType == null)
			{
				throw ErrorHelper.CreateError(4125, "The registrar found an invalid interface '{0}': The interface must have a Protocol attribute specifying its wrapper type.", type.FullName);
			}
			return protocolAttribute.WrapperType;
		}

		[DllImport("__Internal")]
		private static extern uint xamarin_find_protocol_wrapper_type(uint token_ref);

		public static IntPtr GetProtocol(string protocol)
		{
			return Protocol.objc_getProtocol(protocol);
		}

		internal unsafe static IntPtr GetProtocolForType(Type type)
		{
			MTRegistrationMap* registrationMap = options->RegistrationMap;
			if (registrationMap != null && registrationMap->protocol_count > 0)
			{
				uint tokenReference = Class.GetTokenReference(type);
				uint* protocol_tokens = registrationMap->protocol_map.protocol_tokens;
				for (int i = 0; i < registrationMap->protocol_count; i++)
				{
					if (protocol_tokens[i] == tokenReference)
					{
						return registrationMap->protocol_map.protocols[i];
					}
				}
			}
			if (type.IsInterface)
			{
				ProtocolAttribute customAttribute = type.GetCustomAttribute<ProtocolAttribute>(inherit: false);
				if (customAttribute != null)
				{
					IntPtr intPtr = Protocol.objc_getProtocol(customAttribute.Name);
					if (intPtr != IntPtr.Zero)
					{
						return intPtr;
					}
				}
			}
			throw new ArgumentException($"'{type.FullName}' is an unknown protocol");
		}

		public static void ConnectMethod(Type type, MethodInfo method, Selector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			ConnectMethod(type, method, new ExportAttribute(selector.Name));
		}

		[BindingImpl(BindingImplOptions.Optimizable)]
		public static void ConnectMethod(Type type, MethodInfo method, ExportAttribute export)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (export == null)
			{
				throw new ArgumentNullException("export");
			}
			if (!DynamicRegistrationSupported)
			{
				throw ErrorHelper.CreateError(8026, "Runtime.ConnectMethod is not supported when the dynamic registrar has been linked away.");
			}
			Registrar.RegisterMethod(type, method, export);
		}

		public static void ConnectMethod(MethodInfo method, Selector selector)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			ConnectMethod(method.DeclaringType, method, selector);
		}

		[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
		private static extern void NSLog(IntPtr format, [MarshalAs(UnmanagedType.LPStr)] string s);

		[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation", EntryPoint = "NSLog")]
		private static extern void NSLog_arm64(IntPtr format, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, [MarshalAs(UnmanagedType.LPStr)] string s);

		[BindingImpl(BindingImplOptions.Optimizable)]
		internal static void NSLog(string format, params object[] args)
		{
			IntPtr intPtr = NSString.CreateNative("%s");
			string s = (args == null || args.Length == 0) ? format : string.Format(format, args);
			if (IsARM64CallingConvention)
			{
				NSLog_arm64(intPtr, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, s);
			}
			else
			{
				NSLog(intPtr, s);
			}
			NSString.ReleaseNative(intPtr);
		}

		internal static bool CheckSystemVersion(int major, int minor, string systemVersion)
		{
			return CheckSystemVersion(major, minor, 0, systemVersion);
		}

		internal static bool CheckSystemVersion(int major, int minor, int build, string systemVersion)
		{
			if (MajorVersion == -1)
			{
				string[] array = systemVersion.Split(new char[1]
				{
					'.'
				});
				if (array.Length < 1 || !int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out MajorVersion))
				{
					MajorVersion = 2;
				}
				if (array.Length < 2 || !int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out MinorVersion))
				{
					MinorVersion = 0;
				}
				if (array.Length < 3 || !int.TryParse(array[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out BuildVersion))
				{
					BuildVersion = 0;
				}
			}
			if (MajorVersion > major)
			{
				return true;
			}
			if (MajorVersion < major)
			{
				return false;
			}
			if (MinorVersion > minor)
			{
				return true;
			}
			if (MinorVersion < minor)
			{
				return false;
			}
			if (BuildVersion < build)
			{
				return false;
			}
			return true;
		}

		internal unsafe static IntPtr CloneMemory(IntPtr source, long length)
		{
			IntPtr intPtr = Marshal.AllocHGlobal((IntPtr)length);
			Buffer.MemoryCopy((void*)source, (void*)intPtr, length, length);
			return intPtr;
		}

		[DllImport("/usr/lib/libSystem.dylib")]
		internal static extern void memcpy(IntPtr target, IntPtr source, nint n);

		[DllImport("/usr/lib/libSystem.dylib")]
		internal unsafe static extern void memcpy(byte* target, byte* source, nint n);

		internal unsafe static bool StringEquals(IntPtr utf8, string str)
		{
			byte* ptr = (byte*)(void*)utf8;
			for (int i = 0; i < str.Length; i++)
			{
				byte b = ptr[i];
				if (b > 127)
				{
					return string.Equals(Marshal.PtrToStringUTF8(utf8), str);
				}
				if (b != (short)str[i])
				{
					return false;
				}
			}
			return ptr[str.Length] == 0;
		}

		internal static MethodInfo FindClosedMethod(Type closed_type, MethodBase open_method)
		{
			if (!open_method.ContainsGenericParameters)
			{
				return (MethodInfo)open_method;
			}
			Type type = closed_type;
			do
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == open_method.DeclaringType)
				{
					closed_type = type;
					break;
				}
				type = type.BaseType;
			}
			while (type != null);
			MethodInfo[] methods = closed_type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.MetadataToken == open_method.MetadataToken)
				{
					return methodInfo;
				}
			}
			throw ErrorHelper.CreateError(8003, "Failed to find the closed generic method '{0}' on the type '{1}'.", open_method.Name, closed_type.FullName);
		}

		[DllImport("__Internal", EntryPoint = "xamarin_release_block_on_main_thread")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static extern void ReleaseBlockOnMainThread(IntPtr block);

		internal static T ThrowOnNull<T>(T obj, string name, string message = null) where T : class
		{
			return obj ?? throw new ArgumentNullException(name, message);
		}

		[DllImport("/usr/lib/libSystem.dylib")]
		private unsafe static extern NXArchInfo* NXGetLocalArchInfo();

		[BindingImpl(BindingImplOptions.Optimizable)]
		private static bool GetIsARM64CallingConvention()
		{
			if (IntPtr.Size == 8)
			{
				return Arch == Arch.DEVICE;
			}
			return false;
		}

		private unsafe static void InitializePlatform(InitializationOptions* options)
		{
			if (options->IsSimulator)
			{
				Arch = Arch.SIMULATOR;
			}
			UIApplication.Initialize();
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static List<WeakReference> GetSurfacedObjects()
		{
			lock (lock_obj)
			{
				List<WeakReference> list = new List<WeakReference>(object_map.Count);
				foreach (KeyValuePair<IntPtr, WeakReference> item in object_map)
				{
					list.Add(item.Value);
				}
				return list;
			}
		}

		public static void StartWWAN(Uri uri, Action<Exception> callback)
		{
			DispatchQueue.DefaultGlobalQueue.DispatchAsync(delegate
			{
				Exception ex = null;
				try
				{
					StartWWAN(uri);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				NSRunLoop.Main.BeginInvokeOnMainThread(delegate
				{
					callback(ex);
				});
			});
		}

		[DllImport("__Internal")]
		private static extern void xamarin_start_wwan(string uri);

		public static void StartWWAN(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (uri.Scheme != "http" && uri.Scheme != "https")
			{
				throw new ArgumentException("uri is not a valid http or https uri", uri.ToString());
			}
			if (Arch != Arch.SIMULATOR)
			{
				xamarin_start_wwan(uri.ToString());
			}
		}
	}
}
