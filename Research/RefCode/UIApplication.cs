using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace UIKit
{
	[Register("UIApplication", true)]
	[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
	public class UIApplication : UIResponder
	{
		public static class Notifications
		{
			public static NSObject ObserveBackgroundRefreshStatusDidChange(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(BackgroundRefreshStatusDidChangeNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveBackgroundRefreshStatusDidChange(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(BackgroundRefreshStatusDidChangeNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveContentSizeCategoryChanged(EventHandler<UIContentSizeCategoryChangedEventArgs> handler)
			{
				EventHandler<UIContentSizeCategoryChangedEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(ContentSizeCategoryChangedNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIContentSizeCategoryChangedEventArgs(notification));
				});
			}

			public static NSObject ObserveContentSizeCategoryChanged(NSObject objectToObserve, EventHandler<UIContentSizeCategoryChangedEventArgs> handler)
			{
				EventHandler<UIContentSizeCategoryChangedEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(ContentSizeCategoryChangedNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIContentSizeCategoryChangedEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveDidBecomeActive(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidBecomeActiveNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveDidBecomeActive(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidBecomeActiveNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveDidChangeStatusBarFrame(EventHandler<UIStatusBarFrameChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarFrameChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidChangeStatusBarFrameNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarFrameChangeEventArgs(notification));
				});
			}

			public static NSObject ObserveDidChangeStatusBarFrame(NSObject objectToObserve, EventHandler<UIStatusBarFrameChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarFrameChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidChangeStatusBarFrameNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarFrameChangeEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveDidChangeStatusBarOrientation(EventHandler<UIStatusBarOrientationChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarOrientationChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidChangeStatusBarOrientationNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarOrientationChangeEventArgs(notification));
				});
			}

			public static NSObject ObserveDidChangeStatusBarOrientation(NSObject objectToObserve, EventHandler<UIStatusBarOrientationChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarOrientationChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidChangeStatusBarOrientationNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarOrientationChangeEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveDidEnterBackground(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidEnterBackgroundNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveDidEnterBackground(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidEnterBackgroundNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveDidFinishLaunching(EventHandler<UIApplicationLaunchEventArgs> handler)
			{
				EventHandler<UIApplicationLaunchEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidFinishLaunchingNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIApplicationLaunchEventArgs(notification));
				});
			}

			public static NSObject ObserveDidFinishLaunching(NSObject objectToObserve, EventHandler<UIApplicationLaunchEventArgs> handler)
			{
				EventHandler<UIApplicationLaunchEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidFinishLaunchingNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIApplicationLaunchEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveDidReceiveMemoryWarning(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidReceiveMemoryWarningNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveDidReceiveMemoryWarning(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(DidReceiveMemoryWarningNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveProtectedDataDidBecomeAvailable(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(ProtectedDataDidBecomeAvailable, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveProtectedDataDidBecomeAvailable(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(ProtectedDataDidBecomeAvailable, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveProtectedDataWillBecomeUnavailable(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(ProtectedDataWillBecomeUnavailable, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveProtectedDataWillBecomeUnavailable(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(ProtectedDataWillBecomeUnavailable, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveSignificantTimeChange(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(SignificantTimeChangeNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveSignificantTimeChange(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(SignificantTimeChangeNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveUserDidTakeScreenshot(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(UserDidTakeScreenshotNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveUserDidTakeScreenshot(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(UserDidTakeScreenshotNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveWillChangeStatusBarFrame(EventHandler<UIStatusBarFrameChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarFrameChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillChangeStatusBarFrameNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarFrameChangeEventArgs(notification));
				});
			}

			public static NSObject ObserveWillChangeStatusBarFrame(NSObject objectToObserve, EventHandler<UIStatusBarFrameChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarFrameChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillChangeStatusBarFrameNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarFrameChangeEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveWillChangeStatusBarOrientation(EventHandler<UIStatusBarOrientationChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarOrientationChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillChangeStatusBarOrientationNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarOrientationChangeEventArgs(notification));
				});
			}

			public static NSObject ObserveWillChangeStatusBarOrientation(NSObject objectToObserve, EventHandler<UIStatusBarOrientationChangeEventArgs> handler)
			{
				EventHandler<UIStatusBarOrientationChangeEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillChangeStatusBarOrientationNotification, delegate(NSNotification notification)
				{
					handler2(null, new UIStatusBarOrientationChangeEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveWillEnterForeground(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillEnterForegroundNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveWillEnterForeground(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillEnterForegroundNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveWillResignActive(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillResignActiveNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveWillResignActive(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillResignActiveNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}

			public static NSObject ObserveWillTerminate(EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillTerminateNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				});
			}

			public static NSObject ObserveWillTerminate(NSObject objectToObserve, EventHandler<NSNotificationEventArgs> handler)
			{
				EventHandler<NSNotificationEventArgs> handler2 = handler;
				return NSNotificationCenter.DefaultCenter.AddObserver(WillTerminateNotification, delegate(NSNotification notification)
				{
					handler2(null, new NSNotificationEventArgs(notification));
				}, objectToObserve);
			}
		}

		private static Thread mainThread;

		public static bool CheckForIllegalCrossThreadCalls = true;

		public static bool CheckForEventAndDelegateMismatches = true;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static readonly IntPtr class_ptr = ObjCRuntime.Class.GetHandle("UIApplication");

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private object? __mt_WeakDelegate_var;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _BackgroundRefreshStatusDidChangeNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ContentSizeCategoryChangedNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _DidBecomeActiveNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _DidChangeStatusBarFrameNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _DidChangeStatusBarOrientationNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _DidEnterBackgroundNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _DidFinishLaunchingNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _DidReceiveMemoryWarningNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsAnnotationKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsBluetoothCentralsKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsBluetoothPeripheralsKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsCloudKitShareMetadataKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsLocalNotificationKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsLocationKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsNewsstandDownloadsKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsRemoteNotificationKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsShortcutItemKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsSourceApplicationKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsUrlKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsUserActivityDictionaryKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _LaunchOptionsUserActivityTypeKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _OpenSettingsUrlString;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ProtectedDataDidBecomeAvailable;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _ProtectedDataWillBecomeUnavailable;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _SignificantTimeChangeNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _StateRestorationBundleVersionKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _StateRestorationSystemVersionKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _StateRestorationTimestampKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _StateRestorationUserInterfaceIdiomKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _StatusBarFrameUserInfoKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _StatusBarOrientationUserInfoKey;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _UITrackingRunLoopMode;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _UserDidTakeScreenshotNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _WillChangeStatusBarFrameNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _WillChangeStatusBarOrientationNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _WillEnterForegroundNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _WillResignActiveNotification;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		private static NSString? _WillTerminateNotification;

		public override IntPtr ClassHandle => class_ptr;

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 10, 3, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 2, PlatformArchitecture.All, null)]
		public virtual string? AlternateIconName
		{
			[System.Runtime.CompilerServices.NullableContext(2)]
			[Introduced(PlatformName.iOS, 10, 3, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 10, 2, PlatformArchitecture.All, null)]
			[Export("alternateIconName")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("alternateIconName")));
				}
				return NSString.FromHandle(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("alternateIconName")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.TvOS, 10, 0, PlatformArchitecture.All, null)]
		public virtual nint ApplicationIconBadgeNumber
		{
			[Introduced(PlatformName.TvOS, 10, 0, PlatformArchitecture.All, null)]
			[Export("applicationIconBadgeNumber")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.nint_objc_msgSend(base.Handle, Selector.GetHandle("applicationIconBadgeNumber"));
				}
				return ObjCRuntime.Messaging.nint_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("applicationIconBadgeNumber"));
			}
			[Introduced(PlatformName.TvOS, 10, 0, PlatformArchitecture.All, null)]
			[Export("setApplicationIconBadgeNumber:")]
			set
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_nint(base.Handle, Selector.GetHandle("setApplicationIconBadgeNumber:"), value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_nint(base.SuperHandle, Selector.GetHandle("setApplicationIconBadgeNumber:"), value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual UIApplicationState ApplicationState
		{
			[Export("applicationState")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						return (UIApplicationState)ObjCRuntime.Messaging.Int64_objc_msgSend(base.Handle, Selector.GetHandle("applicationState"));
					}
					return (UIApplicationState)ObjCRuntime.Messaging.int_objc_msgSend(base.Handle, Selector.GetHandle("applicationState"));
				}
				if (IntPtr.Size == 8)
				{
					return (UIApplicationState)ObjCRuntime.Messaging.Int64_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("applicationState"));
				}
				return (UIApplicationState)ObjCRuntime.Messaging.int_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("applicationState"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		public virtual bool ApplicationSupportsShakeToEdit
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Export("applicationSupportsShakeToEdit")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("applicationSupportsShakeToEdit"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("applicationSupportsShakeToEdit"));
			}
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Export("setApplicationSupportsShakeToEdit:")]
			set
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool(base.Handle, Selector.GetHandle("setApplicationSupportsShakeToEdit:"), value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_bool(base.SuperHandle, Selector.GetHandle("setApplicationSupportsShakeToEdit:"), value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public virtual UIBackgroundRefreshStatus BackgroundRefreshStatus
		{
			[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			[Export("backgroundRefreshStatus")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						return (UIBackgroundRefreshStatus)ObjCRuntime.Messaging.Int64_objc_msgSend(base.Handle, Selector.GetHandle("backgroundRefreshStatus"));
					}
					return (UIBackgroundRefreshStatus)ObjCRuntime.Messaging.int_objc_msgSend(base.Handle, Selector.GetHandle("backgroundRefreshStatus"));
				}
				if (IntPtr.Size == 8)
				{
					return (UIBackgroundRefreshStatus)ObjCRuntime.Messaging.Int64_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("backgroundRefreshStatus"));
				}
				return (UIBackgroundRefreshStatus)ObjCRuntime.Messaging.int_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("backgroundRefreshStatus"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[ThreadSafe]
		public virtual double BackgroundTimeRemaining
		{
			[Export("backgroundTimeRemaining")]
			get
			{
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.Double_objc_msgSend(base.Handle, Selector.GetHandle("backgroundTimeRemaining"));
				}
				return ObjCRuntime.Messaging.Double_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("backgroundTimeRemaining"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		public virtual NSSet<UIScene> ConnectedScenes
		{
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Export("connectedScenes")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSSet<UIScene>>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("connectedScenes")));
				}
				return Runtime.GetNSObject<NSSet<UIScene>>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("connectedScenes")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.GetNotificationSettings' and 'UNUserNotificationCenter.GetNotificationCategories' instead.")]
		public virtual UIUserNotificationSettings CurrentUserNotificationSettings
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.GetNotificationSettings' and 'UNUserNotificationCenter.GetNotificationCategories' instead.")]
			[Export("currentUserNotificationSettings")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<UIUserNotificationSettings>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("currentUserNotificationSettings")));
				}
				return Runtime.GetNSObject<UIUserNotificationSettings>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("currentUserNotificationSettings")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public IUIApplicationDelegate Delegate
		{
			get
			{
				return WeakDelegate as IUIApplicationDelegate;
			}
			set
			{
				NSObject nSObject = value as NSObject;
				if (value != null && nSObject == null)
				{
					throw new ArgumentException("The object passed of type " + value.GetType()?.ToString() + " does not derive from NSObject");
				}
				WeakDelegate = nSObject;
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 8, 0, PlatformArchitecture.All, "Use 'CurrentUserNotificationSettings' or 'UNUserNotificationCenter.GetNotificationSettings' instead.")]
		public virtual UIRemoteNotificationType EnabledRemoteNotificationTypes
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 8, 0, PlatformArchitecture.All, "Use 'CurrentUserNotificationSettings' or 'UNUserNotificationCenter.GetNotificationSettings' instead.")]
			[Export("enabledRemoteNotificationTypes")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						return (UIRemoteNotificationType)ObjCRuntime.Messaging.UInt64_objc_msgSend(base.Handle, Selector.GetHandle("enabledRemoteNotificationTypes"));
					}
					return (UIRemoteNotificationType)ObjCRuntime.Messaging.UInt32_objc_msgSend(base.Handle, Selector.GetHandle("enabledRemoteNotificationTypes"));
				}
				if (IntPtr.Size == 8)
				{
					return (UIRemoteNotificationType)ObjCRuntime.Messaging.UInt64_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("enabledRemoteNotificationTypes"));
				}
				return (UIRemoteNotificationType)ObjCRuntime.Messaging.UInt32_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("enabledRemoteNotificationTypes"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool IdleTimerDisabled
		{
			[Export("isIdleTimerDisabled")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isIdleTimerDisabled"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isIdleTimerDisabled"));
			}
			[Export("setIdleTimerDisabled:")]
			set
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool(base.Handle, Selector.GetHandle("setIdleTimerDisabled:"), value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_bool(base.SuperHandle, Selector.GetHandle("setIdleTimerDisabled:"), value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
		[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
		public virtual bool IsIgnoringInteractionEvents
		{
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
			[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
			[Export("isIgnoringInteractionEvents")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isIgnoringInteractionEvents"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isIgnoringInteractionEvents"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		public virtual bool IsRegisteredForRemoteNotifications
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			[Export("isRegisteredForRemoteNotifications")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isRegisteredForRemoteNotifications"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isRegisteredForRemoteNotifications"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Should not be used for applications that support multiple scenes because it returns a key window across all connected scenes.")]
		[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Should not be used for applications that support multiple scenes because it returns a key window across all connected scenes.")]
		public virtual UIWindow KeyWindow
		{
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Should not be used for applications that support multiple scenes because it returns a key window across all connected scenes.")]
			[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Should not be used for applications that support multiple scenes because it returns a key window across all connected scenes.")]
			[Export("keyWindow")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<UIWindow>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("keyWindow")));
				}
				return Runtime.GetNSObject<UIWindow>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("keyWindow")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Provide a custom UI in your app instead if needed.")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[ThreadSafe]
		public virtual bool NetworkActivityIndicatorVisible
		{
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Provide a custom UI in your app instead if needed.")]
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Export("isNetworkActivityIndicatorVisible")]
			get
			{
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isNetworkActivityIndicatorVisible"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isNetworkActivityIndicatorVisible"));
			}
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Provide a custom UI in your app instead if needed.")]
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Export("setNetworkActivityIndicatorVisible:")]
			set
			{
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool(base.Handle, Selector.GetHandle("setNetworkActivityIndicatorVisible:"), value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_bool(base.SuperHandle, Selector.GetHandle("setNetworkActivityIndicatorVisible:"), value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		public virtual NSSet<UISceneSession> OpenSessions
		{
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Export("openSessions")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSSet<UISceneSession>>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("openSessions")));
				}
				return Runtime.GetNSObject<NSSet<UISceneSession>>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("openSessions")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public virtual NSString PreferredContentSizeCategory
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			[Export("preferredContentSizeCategory")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("preferredContentSizeCategory")));
				}
				return Runtime.GetNSObject<NSString>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("preferredContentSizeCategory")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool ProtectedDataAvailable
		{
			[Export("isProtectedDataAvailable")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isProtectedDataAvailable"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isProtectedDataAvailable"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.GetPendingNotificationRequests' instead.")]
		public virtual UILocalNotification[] ScheduledLocalNotifications
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.GetPendingNotificationRequests' instead.")]
			[Export("scheduledLocalNotifications", ArgumentSemantic.Copy)]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return NSArray.ArrayFromHandle<UILocalNotification>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("scheduledLocalNotifications")));
				}
				return NSArray.ArrayFromHandle<UILocalNotification>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("scheduledLocalNotifications")));
			}
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.GetPendingNotificationRequests' instead.")]
			[Export("setScheduledLocalNotifications:", ArgumentSemantic.Copy)]
			set
			{
				EnsureUIThread();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				NSArray nSArray = NSArray.FromNSObjects(value);
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("setScheduledLocalNotifications:"), nSArray.Handle);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("setScheduledLocalNotifications:"), nSArray.Handle);
				}
				nSArray.Dispose();
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[ThreadSafe]
		public static UIApplication SharedApplication
		{
			[Export("sharedApplication")]
			get
			{
				return Runtime.GetNSObject<UIApplication>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(class_ptr, Selector.GetHandle("sharedApplication")));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		public virtual UIApplicationShortcutItem[]? ShortcutItems
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
			[Export("shortcutItems", ArgumentSemantic.Copy)]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return NSArray.ArrayFromHandle<UIApplicationShortcutItem>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("shortcutItems")));
				}
				return NSArray.ArrayFromHandle<UIApplicationShortcutItem>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("shortcutItems")));
			}
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
			[Export("setShortcutItems:", ArgumentSemantic.Copy)]
			set
			{
				EnsureUIThread();
				NSArray nSArray = (value == null) ? null : NSArray.FromNSObjects(value);
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("setShortcutItems:"), nSArray?.Handle ?? IntPtr.Zero);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("setShortcutItems:"), nSArray?.Handle ?? IntPtr.Zero);
				}
				nSArray?.Dispose();
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use the 'StatusBarManager' property of the window scene instead.")]
		public virtual CGRect StatusBarFrame
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use the 'StatusBarManager' property of the window scene instead.")]
			[Export("statusBarFrame")]
			get
			{
				EnsureUIThread();
				CGRect retval;
				if (base.IsDirectBinding)
				{
					if (Runtime.Arch == Arch.DEVICE)
					{
						if (IntPtr.Size == 8)
						{
							return ObjCRuntime.Messaging.CGRect_objc_msgSend(base.Handle, Selector.GetHandle("statusBarFrame"));
						}
						ObjCRuntime.Messaging.CGRect_objc_msgSend_stret(out retval, base.Handle, Selector.GetHandle("statusBarFrame"));
					}
					else if (IntPtr.Size == 8)
					{
						ObjCRuntime.Messaging.CGRect_objc_msgSend_stret(out retval, base.Handle, Selector.GetHandle("statusBarFrame"));
					}
					else
					{
						ObjCRuntime.Messaging.CGRect_objc_msgSend_stret(out retval, base.Handle, Selector.GetHandle("statusBarFrame"));
					}
				}
				else if (Runtime.Arch == Arch.DEVICE)
				{
					if (IntPtr.Size == 8)
					{
						return ObjCRuntime.Messaging.CGRect_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("statusBarFrame"));
					}
					ObjCRuntime.Messaging.CGRect_objc_msgSendSuper_stret(out retval, base.SuperHandle, Selector.GetHandle("statusBarFrame"));
				}
				else if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.CGRect_objc_msgSendSuper_stret(out retval, base.SuperHandle, Selector.GetHandle("statusBarFrame"));
				}
				else
				{
					ObjCRuntime.Messaging.CGRect_objc_msgSendSuper_stret(out retval, base.SuperHandle, Selector.GetHandle("statusBarFrame"));
				}
				return retval;
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PrefersStatusBarHidden' instead.")]
		public virtual bool StatusBarHidden
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PrefersStatusBarHidden' instead.")]
			[Export("isStatusBarHidden")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("isStatusBarHidden"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("isStatusBarHidden"));
			}
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PrefersStatusBarHidden' instead.")]
			[Export("setStatusBarHidden:")]
			set
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool(base.Handle, Selector.GetHandle("setStatusBarHidden:"), value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_bool(base.SuperHandle, Selector.GetHandle("setStatusBarHidden:"), value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, null)]
		public virtual UIInterfaceOrientation StatusBarOrientation
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, null)]
			[Export("statusBarOrientation")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						return (UIInterfaceOrientation)ObjCRuntime.Messaging.Int64_objc_msgSend(base.Handle, Selector.GetHandle("statusBarOrientation"));
					}
					return (UIInterfaceOrientation)ObjCRuntime.Messaging.int_objc_msgSend(base.Handle, Selector.GetHandle("statusBarOrientation"));
				}
				if (IntPtr.Size == 8)
				{
					return (UIInterfaceOrientation)ObjCRuntime.Messaging.Int64_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("statusBarOrientation"));
				}
				return (UIInterfaceOrientation)ObjCRuntime.Messaging.int_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("statusBarOrientation"));
			}
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, null)]
			[Export("setStatusBarOrientation:")]
			set
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						ObjCRuntime.Messaging.void_objc_msgSend_Int64(base.Handle, Selector.GetHandle("setStatusBarOrientation:"), (long)value);
					}
					else
					{
						ObjCRuntime.Messaging.void_objc_msgSend_int(base.Handle, Selector.GetHandle("setStatusBarOrientation:"), (int)value);
					}
				}
				else if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_Int64(base.SuperHandle, Selector.GetHandle("setStatusBarOrientation:"), (long)value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_int(base.SuperHandle, Selector.GetHandle("setStatusBarOrientation:"), (int)value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use the 'InterfaceOrientation' property of the window scene instead.")]
		public virtual double StatusBarOrientationAnimationDuration
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use the 'InterfaceOrientation' property of the window scene instead.")]
			[Export("statusBarOrientationAnimationDuration")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.Double_objc_msgSend(base.Handle, Selector.GetHandle("statusBarOrientationAnimationDuration"));
				}
				return ObjCRuntime.Messaging.Double_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("statusBarOrientationAnimationDuration"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PreferredStatusBarStyle' instead.")]
		public virtual UIStatusBarStyle StatusBarStyle
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PreferredStatusBarStyle' instead.")]
			[Export("statusBarStyle")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						return (UIStatusBarStyle)ObjCRuntime.Messaging.Int64_objc_msgSend(base.Handle, Selector.GetHandle("statusBarStyle"));
					}
					return (UIStatusBarStyle)ObjCRuntime.Messaging.int_objc_msgSend(base.Handle, Selector.GetHandle("statusBarStyle"));
				}
				if (IntPtr.Size == 8)
				{
					return (UIStatusBarStyle)ObjCRuntime.Messaging.Int64_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("statusBarStyle"));
				}
				return (UIStatusBarStyle)ObjCRuntime.Messaging.int_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("statusBarStyle"));
			}
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PreferredStatusBarStyle' instead.")]
			[Export("setStatusBarStyle:")]
			set
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						ObjCRuntime.Messaging.void_objc_msgSend_Int64(base.Handle, Selector.GetHandle("setStatusBarStyle:"), (long)value);
					}
					else
					{
						ObjCRuntime.Messaging.void_objc_msgSend_int(base.Handle, Selector.GetHandle("setStatusBarStyle:"), (int)value);
					}
				}
				else if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_Int64(base.SuperHandle, Selector.GetHandle("setStatusBarStyle:"), (long)value);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_int(base.SuperHandle, Selector.GetHandle("setStatusBarStyle:"), (int)value);
				}
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 10, 3, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 2, PlatformArchitecture.All, null)]
		public virtual bool SupportsAlternateIcons
		{
			[Introduced(PlatformName.iOS, 10, 3, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 10, 2, PlatformArchitecture.All, null)]
			[Export("supportsAlternateIcons")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("supportsAlternateIcons"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("supportsAlternateIcons"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		public virtual bool SupportsMultipleScenes
		{
			[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
			[Export("supportsMultipleScenes")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return ObjCRuntime.Messaging.bool_objc_msgSend(base.Handle, Selector.GetHandle("supportsMultipleScenes"));
				}
				return ObjCRuntime.Messaging.bool_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("supportsMultipleScenes"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual UIUserInterfaceLayoutDirection UserInterfaceLayoutDirection
		{
			[Export("userInterfaceLayoutDirection")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					if (IntPtr.Size == 8)
					{
						return (UIUserInterfaceLayoutDirection)ObjCRuntime.Messaging.Int64_objc_msgSend(base.Handle, Selector.GetHandle("userInterfaceLayoutDirection"));
					}
					return (UIUserInterfaceLayoutDirection)ObjCRuntime.Messaging.int_objc_msgSend(base.Handle, Selector.GetHandle("userInterfaceLayoutDirection"));
				}
				if (IntPtr.Size == 8)
				{
					return (UIUserInterfaceLayoutDirection)ObjCRuntime.Messaging.Int64_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("userInterfaceLayoutDirection"));
				}
				return (UIUserInterfaceLayoutDirection)ObjCRuntime.Messaging.int_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("userInterfaceLayoutDirection"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[ThreadSafe]
		public virtual NSObject? WeakDelegate
		{
			[System.Runtime.CompilerServices.NullableContext(2)]
			[Export("delegate", ArgumentSemantic.Assign)]
			get
			{
				NSObject nSObject = (!base.IsDirectBinding) ? Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("delegate"))) : Runtime.GetNSObject(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("delegate")));
				MarkDirty();
				__mt_WeakDelegate_var = nSObject;
				return nSObject;
			}
			[System.Runtime.CompilerServices.NullableContext(2)]
			[Export("setDelegate:", ArgumentSemantic.Assign)]
			set
			{
				if (base.IsDirectBinding)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("setDelegate:"), value?.Handle ?? IntPtr.Zero);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("setDelegate:"), value?.Handle ?? IntPtr.Zero);
				}
				MarkDirty();
				__mt_WeakDelegate_var = value;
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual UIWindow[] Windows
		{
			[Export("windows")]
			get
			{
				EnsureUIThread();
				if (base.IsDirectBinding)
				{
					return NSArray.ArrayFromHandle<UIWindow>(ObjCRuntime.Messaging.IntPtr_objc_msgSend(base.Handle, Selector.GetHandle("windows")));
				}
				return NSArray.ArrayFromHandle<UIWindow>(ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("windows")));
			}
		}

		[Field("UIApplicationBackgroundFetchIntervalMinimum", "UIKit")]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public static double BackgroundFetchIntervalMinimum
		{
			[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				return Dlfcn.GetDouble(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationBackgroundFetchIntervalMinimum");
			}
		}

		[Field("UIApplicationBackgroundFetchIntervalNever", "UIKit")]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public static double BackgroundFetchIntervalNever
		{
			[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				return Dlfcn.GetDouble(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationBackgroundFetchIntervalNever");
			}
		}

		[Field("UIApplicationBackgroundRefreshStatusDidChangeNotification", "UIKit")]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[Advice("Use UIApplication.Notifications.ObserveBackgroundRefreshStatusDidChange helper method instead.")]
		public static NSString BackgroundRefreshStatusDidChangeNotification
		{
			[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_BackgroundRefreshStatusDidChangeNotification == null)
				{
					_BackgroundRefreshStatusDidChangeNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationBackgroundRefreshStatusDidChangeNotification");
				}
				return _BackgroundRefreshStatusDidChangeNotification;
			}
		}

		[Field("UIBackgroundTaskInvalid", "UIKit")]
		public static nint BackgroundTaskInvalid => Dlfcn.GetNInt(ObjCRuntime.Libraries.UIKit.Handle, "UIBackgroundTaskInvalid");

		[Field("UIContentSizeCategoryDidChangeNotification", "UIKit")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[Advice("Use UIApplication.Notifications.ObserveContentSizeCategoryChanged helper method instead.")]
		public static NSString ContentSizeCategoryChangedNotification
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_ContentSizeCategoryChangedNotification == null)
				{
					_ContentSizeCategoryChangedNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIContentSizeCategoryDidChangeNotification");
				}
				return _ContentSizeCategoryChangedNotification;
			}
		}

		[Field("UIApplicationDidBecomeActiveNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveDidBecomeActive helper method instead.")]
		public static NSString DidBecomeActiveNotification
		{
			get
			{
				if (_DidBecomeActiveNotification == null)
				{
					_DidBecomeActiveNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationDidBecomeActiveNotification");
				}
				return _DidBecomeActiveNotification;
			}
		}

		[Field("UIApplicationDidChangeStatusBarFrameNotification", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
		[Advice("Use UIApplication.Notifications.ObserveDidChangeStatusBarFrame helper method instead.")]
		public static NSString DidChangeStatusBarFrameNotification
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
			get
			{
				if (_DidChangeStatusBarFrameNotification == null)
				{
					_DidChangeStatusBarFrameNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationDidChangeStatusBarFrameNotification");
				}
				return _DidChangeStatusBarFrameNotification;
			}
		}

		[Field("UIApplicationDidChangeStatusBarOrientationNotification", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
		[Advice("Use UIApplication.Notifications.ObserveDidChangeStatusBarOrientation helper method instead.")]
		public static NSString DidChangeStatusBarOrientationNotification
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
			get
			{
				if (_DidChangeStatusBarOrientationNotification == null)
				{
					_DidChangeStatusBarOrientationNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationDidChangeStatusBarOrientationNotification");
				}
				return _DidChangeStatusBarOrientationNotification;
			}
		}

		[Field("UIApplicationDidEnterBackgroundNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveDidEnterBackground helper method instead.")]
		public static NSString DidEnterBackgroundNotification
		{
			get
			{
				if (_DidEnterBackgroundNotification == null)
				{
					_DidEnterBackgroundNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationDidEnterBackgroundNotification");
				}
				return _DidEnterBackgroundNotification;
			}
		}

		[Field("UIApplicationDidFinishLaunchingNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveDidFinishLaunching helper method instead.")]
		public static NSString DidFinishLaunchingNotification
		{
			get
			{
				if (_DidFinishLaunchingNotification == null)
				{
					_DidFinishLaunchingNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationDidFinishLaunchingNotification");
				}
				return _DidFinishLaunchingNotification;
			}
		}

		[Field("UIApplicationDidReceiveMemoryWarningNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveDidReceiveMemoryWarning helper method instead.")]
		public static NSString DidReceiveMemoryWarningNotification
		{
			get
			{
				if (_DidReceiveMemoryWarningNotification == null)
				{
					_DidReceiveMemoryWarningNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationDidReceiveMemoryWarningNotification");
				}
				return _DidReceiveMemoryWarningNotification;
			}
		}

		[Field("UIApplicationLaunchOptionsAnnotationKey", "UIKit")]
		public static NSString LaunchOptionsAnnotationKey
		{
			get
			{
				if (_LaunchOptionsAnnotationKey == null)
				{
					_LaunchOptionsAnnotationKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsAnnotationKey");
				}
				return _LaunchOptionsAnnotationKey;
			}
		}

		[Field("UIApplicationLaunchOptionsBluetoothCentralsKey", "UIKit")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsBluetoothCentralsKey
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsBluetoothCentralsKey == null)
				{
					_LaunchOptionsBluetoothCentralsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsBluetoothCentralsKey");
				}
				return _LaunchOptionsBluetoothCentralsKey;
			}
		}

		[Field("UIApplicationLaunchOptionsBluetoothPeripheralsKey", "UIKit")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsBluetoothPeripheralsKey
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsBluetoothPeripheralsKey == null)
				{
					_LaunchOptionsBluetoothPeripheralsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsBluetoothPeripheralsKey");
				}
				return _LaunchOptionsBluetoothPeripheralsKey;
			}
		}

		[Field("UIApplicationLaunchOptionsCloudKitShareMetadataKey", "UIKit")]
		[Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsCloudKitShareMetadataKey
		{
			[Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsCloudKitShareMetadataKey == null)
				{
					_LaunchOptionsCloudKitShareMetadataKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsCloudKitShareMetadataKey");
				}
				return _LaunchOptionsCloudKitShareMetadataKey;
			}
		}

		[Field("UIApplicationLaunchOptionsLocalNotificationKey", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenterDelegate.DidReceiveNotificationResponse' instead.")]
		public static NSString LaunchOptionsLocalNotificationKey
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenterDelegate.DidReceiveNotificationResponse' instead.")]
			get
			{
				if (_LaunchOptionsLocalNotificationKey == null)
				{
					_LaunchOptionsLocalNotificationKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsLocalNotificationKey");
				}
				return _LaunchOptionsLocalNotificationKey;
			}
		}

		[Field("UIApplicationLaunchOptionsLocationKey", "UIKit")]
		public static NSString LaunchOptionsLocationKey
		{
			get
			{
				if (_LaunchOptionsLocationKey == null)
				{
					_LaunchOptionsLocationKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsLocationKey");
				}
				return _LaunchOptionsLocationKey;
			}
		}

		[Field("UIApplicationLaunchOptionsNewsstandDownloadsKey", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsNewsstandDownloadsKey
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsNewsstandDownloadsKey == null)
				{
					_LaunchOptionsNewsstandDownloadsKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsNewsstandDownloadsKey");
				}
				return _LaunchOptionsNewsstandDownloadsKey;
			}
		}

		[Field("UIApplicationLaunchOptionsRemoteNotificationKey", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsRemoteNotificationKey
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsRemoteNotificationKey == null)
				{
					_LaunchOptionsRemoteNotificationKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsRemoteNotificationKey");
				}
				return _LaunchOptionsRemoteNotificationKey;
			}
		}

		[Field("UIApplicationLaunchOptionsShortcutItemKey", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsShortcutItemKey
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Introduced(PlatformName.iOS, 9, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsShortcutItemKey == null)
				{
					_LaunchOptionsShortcutItemKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsShortcutItemKey");
				}
				return _LaunchOptionsShortcutItemKey;
			}
		}

		[Field("UIApplicationLaunchOptionsSourceApplicationKey", "UIKit")]
		public static NSString LaunchOptionsSourceApplicationKey
		{
			get
			{
				if (_LaunchOptionsSourceApplicationKey == null)
				{
					_LaunchOptionsSourceApplicationKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsSourceApplicationKey");
				}
				return _LaunchOptionsSourceApplicationKey;
			}
		}

		[Field("UIApplicationLaunchOptionsURLKey", "UIKit")]
		public static NSString LaunchOptionsUrlKey
		{
			get
			{
				if (_LaunchOptionsUrlKey == null)
				{
					_LaunchOptionsUrlKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsURLKey");
				}
				return _LaunchOptionsUrlKey;
			}
		}

		[Field("UIApplicationLaunchOptionsUserActivityDictionaryKey", "UIKit")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsUserActivityDictionaryKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsUserActivityDictionaryKey == null)
				{
					_LaunchOptionsUserActivityDictionaryKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsUserActivityDictionaryKey");
				}
				return _LaunchOptionsUserActivityDictionaryKey;
			}
		}

		[Field("UIApplicationLaunchOptionsUserActivityTypeKey", "UIKit")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		public static NSString LaunchOptionsUserActivityTypeKey
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_LaunchOptionsUserActivityTypeKey == null)
				{
					_LaunchOptionsUserActivityTypeKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationLaunchOptionsUserActivityTypeKey");
				}
				return _LaunchOptionsUserActivityTypeKey;
			}
		}

		[Field("UIMinimumKeepAliveTimeout", "UIKit")]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'PushKit' for Voice Over IP applications.")]
		[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use 'PushKit' for Voice Over IP applications.")]
		public static double MinimumKeepAliveTimeout
		{
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'PushKit' for Voice Over IP applications.")]
			[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use 'PushKit' for Voice Over IP applications.")]
			get
			{
				return Dlfcn.GetDouble(ObjCRuntime.Libraries.UIKit.Handle, "UIMinimumKeepAliveTimeout");
			}
		}

		[Field("UIApplicationOpenSettingsURLString", "UIKit")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		public static NSString OpenSettingsUrlString
		{
			[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_OpenSettingsUrlString == null)
				{
					_OpenSettingsUrlString = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationOpenSettingsURLString");
				}
				return _OpenSettingsUrlString;
			}
		}

		[Field("UIApplicationProtectedDataDidBecomeAvailable", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveProtectedDataDidBecomeAvailable helper method instead.")]
		public static NSString ProtectedDataDidBecomeAvailable
		{
			get
			{
				if (_ProtectedDataDidBecomeAvailable == null)
				{
					_ProtectedDataDidBecomeAvailable = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationProtectedDataDidBecomeAvailable");
				}
				return _ProtectedDataDidBecomeAvailable;
			}
		}

		[Field("UIApplicationProtectedDataWillBecomeUnavailable", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveProtectedDataWillBecomeUnavailable helper method instead.")]
		public static NSString ProtectedDataWillBecomeUnavailable
		{
			get
			{
				if (_ProtectedDataWillBecomeUnavailable == null)
				{
					_ProtectedDataWillBecomeUnavailable = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationProtectedDataWillBecomeUnavailable");
				}
				return _ProtectedDataWillBecomeUnavailable;
			}
		}

		[Field("UIApplicationSignificantTimeChangeNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveSignificantTimeChange helper method instead.")]
		public static NSString SignificantTimeChangeNotification
		{
			get
			{
				if (_SignificantTimeChangeNotification == null)
				{
					_SignificantTimeChangeNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationSignificantTimeChangeNotification");
				}
				return _SignificantTimeChangeNotification;
			}
		}

		[Field("UIApplicationStateRestorationBundleVersionKey", "UIKit")]
		public static NSString StateRestorationBundleVersionKey
		{
			get
			{
				if (_StateRestorationBundleVersionKey == null)
				{
					_StateRestorationBundleVersionKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationStateRestorationBundleVersionKey");
				}
				return _StateRestorationBundleVersionKey;
			}
		}

		[Field("UIApplicationStateRestorationSystemVersionKey", "UIKit")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public static NSString StateRestorationSystemVersionKey
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_StateRestorationSystemVersionKey == null)
				{
					_StateRestorationSystemVersionKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationStateRestorationSystemVersionKey");
				}
				return _StateRestorationSystemVersionKey;
			}
		}

		[Field("UIApplicationStateRestorationTimestampKey", "UIKit")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		public static NSString StateRestorationTimestampKey
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_StateRestorationTimestampKey == null)
				{
					_StateRestorationTimestampKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationStateRestorationTimestampKey");
				}
				return _StateRestorationTimestampKey;
			}
		}

		[Field("UIApplicationStateRestorationUserInterfaceIdiomKey", "UIKit")]
		public static NSString StateRestorationUserInterfaceIdiomKey
		{
			get
			{
				if (_StateRestorationUserInterfaceIdiomKey == null)
				{
					_StateRestorationUserInterfaceIdiomKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationStateRestorationUserInterfaceIdiomKey");
				}
				return _StateRestorationUserInterfaceIdiomKey;
			}
		}

		[Field("UIApplicationStatusBarFrameUserInfoKey", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
		public static NSString StatusBarFrameUserInfoKey
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
			get
			{
				if (_StatusBarFrameUserInfoKey == null)
				{
					_StatusBarFrameUserInfoKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationStatusBarFrameUserInfoKey");
				}
				return _StatusBarFrameUserInfoKey;
			}
		}

		[Field("UIApplicationStatusBarOrientationUserInfoKey", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
		public static NSString StatusBarOrientationUserInfoKey
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
			get
			{
				if (_StatusBarOrientationUserInfoKey == null)
				{
					_StatusBarOrientationUserInfoKey = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationStatusBarOrientationUserInfoKey");
				}
				return _StatusBarOrientationUserInfoKey;
			}
		}

		[Field("UITrackingRunLoopMode", "UIKit")]
		[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
		public static NSString UITrackingRunLoopMode
		{
			[Unavailable(PlatformName.WatchOS, PlatformArchitecture.All, null)]
			get
			{
				if (_UITrackingRunLoopMode == null)
				{
					_UITrackingRunLoopMode = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UITrackingRunLoopMode");
				}
				return _UITrackingRunLoopMode;
			}
		}

		[Field("UIApplicationUserDidTakeScreenshotNotification", "UIKit")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[Advice("Use UIApplication.Notifications.ObserveUserDidTakeScreenshot helper method instead.")]
		public static NSString UserDidTakeScreenshotNotification
		{
			[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
			get
			{
				if (_UserDidTakeScreenshotNotification == null)
				{
					_UserDidTakeScreenshotNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationUserDidTakeScreenshotNotification");
				}
				return _UserDidTakeScreenshotNotification;
			}
		}

		[Field("UIApplicationWillChangeStatusBarFrameNotification", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
		[Advice("Use UIApplication.Notifications.ObserveWillChangeStatusBarFrame helper method instead.")]
		public static NSString WillChangeStatusBarFrameNotification
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
			get
			{
				if (_WillChangeStatusBarFrameNotification == null)
				{
					_WillChangeStatusBarFrameNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationWillChangeStatusBarFrameNotification");
				}
				return _WillChangeStatusBarFrameNotification;
			}
		}

		[Field("UIApplicationWillChangeStatusBarOrientationNotification", "UIKit")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
		[Advice("Use UIApplication.Notifications.ObserveWillChangeStatusBarOrientation helper method instead.")]
		public static NSString WillChangeStatusBarOrientationNotification
		{
			[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
			[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use 'ViewWillTransitionToSize' instead.")]
			get
			{
				if (_WillChangeStatusBarOrientationNotification == null)
				{
					_WillChangeStatusBarOrientationNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationWillChangeStatusBarOrientationNotification");
				}
				return _WillChangeStatusBarOrientationNotification;
			}
		}

		[Field("UIApplicationWillEnterForegroundNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveWillEnterForeground helper method instead.")]
		public static NSString WillEnterForegroundNotification
		{
			get
			{
				if (_WillEnterForegroundNotification == null)
				{
					_WillEnterForegroundNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationWillEnterForegroundNotification");
				}
				return _WillEnterForegroundNotification;
			}
		}

		[Field("UIApplicationWillResignActiveNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveWillResignActive helper method instead.")]
		public static NSString WillResignActiveNotification
		{
			get
			{
				if (_WillResignActiveNotification == null)
				{
					_WillResignActiveNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationWillResignActiveNotification");
				}
				return _WillResignActiveNotification;
			}
		}

		[Field("UIApplicationWillTerminateNotification", "UIKit")]
		[Advice("Use UIApplication.Notifications.ObserveWillTerminate helper method instead.")]
		public static NSString WillTerminateNotification
		{
			get
			{
				if (_WillTerminateNotification == null)
				{
					_WillTerminateNotification = Dlfcn.GetStringConstant(ObjCRuntime.Libraries.UIKit.Handle, "UIApplicationWillTerminateNotification");
				}
				return _WillTerminateNotification;
			}
		}

		[DllImport("__Internal")]
		private static extern int UIApplicationMain(int argc, string[] argv, IntPtr principalClassName, IntPtr delegateClassName);

		internal new static void Initialize()
		{
			if (mainThread == null)
			{
				SynchronizationContext.SetSynchronizationContext(new UIKitSynchronizationContext());
				mainThread = Thread.CurrentThread;
			}
		}

		public static void Main(string[] args, string principalClassName, string delegateClassName)
		{
			IntPtr intPtr = NSString.CreateNative(principalClassName);
			IntPtr intPtr2 = NSString.CreateNative(delegateClassName);
			try
			{
				Main(args, intPtr, intPtr2);
			}
			finally
			{
				NSString.ReleaseNative(intPtr2);
				NSString.ReleaseNative(intPtr);
			}
		}

		public static void Main(string[] args, Type principalClass, Type delegateClass)
		{
			Main(args, (principalClass == null) ? null : new Class(principalClass).Name, (delegateClass == null) ? null : new Class(delegateClass).Name);
		}

		public static void Main(string[] args)
		{
			Main(args, IntPtr.Zero, IntPtr.Zero);
		}

		private static void Main(string[] args, IntPtr principal, IntPtr @delegate)
		{
			Initialize();
			UIApplicationMain(args.Length, args, principal, @delegate);
		}

		public static void EnsureUIThread()
		{
			if (CheckForIllegalCrossThreadCalls && mainThread != null && mainThread != Thread.CurrentThread)
			{
				throw new UIKitThreadAccessException();
			}
		}

		internal static void EnsureEventAndDelegateAreNotMismatched(object del, Type expectedType)
		{
			if (CheckForEventAndDelegateMismatches && !expectedType.IsAssignableFrom(del.GetType()))
			{
				throw new InvalidOperationException($"Event registration is overwriting existing delegate. Either just use events or your own delegate: {del.GetType()} {expectedType}");
			}
		}

		internal static void EnsureDelegateAssignIsNotOverwritingInternalDelegate(object currentDelegateValue, object newDelegateValue, Type internalDelegateType)
		{
			if (CheckForEventAndDelegateMismatches && currentDelegateValue != null && newDelegateValue != null && currentDelegateValue.GetType().IsAssignableFrom(internalDelegateType) && !newDelegateValue.GetType().IsAssignableFrom(internalDelegateType))
			{
				throw new InvalidOperationException($"Event registration is overwriting existing delegate. Either just use events or your own delegate: {newDelegateValue.GetType()} {internalDelegateType}");
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Export("init")]
		public UIApplication()
			: base(NSObjectFlag.Empty)
		{
			EnsureUIThread();
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
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected UIApplication(NSObjectFlag t)
			: base(t)
		{
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal UIApplication(IntPtr handle)
			: base(handle)
		{
		}

		[Export("beginBackgroundTaskWithExpirationHandler:")]
		[ThreadSafe]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Advice("Overriding this method requires a call to the overriden method.")]
		[RequiresSuper]
		public unsafe virtual nint BeginBackgroundTask([BlockProxy(typeof(ObjCRuntime.Trampolines.NIDAction))] Action? backgroundTimeExpired)
		{
			BlockLiteral* ptr;
			if (backgroundTimeExpired == null)
			{
				ptr = null;
			}
			else
			{
				BlockLiteral blockLiteral = default(BlockLiteral);
				ptr = &blockLiteral;
				blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDAction.Handler, backgroundTimeExpired);
			}
			nint result = (!base.IsDirectBinding) ? ObjCRuntime.Messaging.nint_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("beginBackgroundTaskWithExpirationHandler:"), (IntPtr)(void*)ptr) : ObjCRuntime.Messaging.nint_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("beginBackgroundTaskWithExpirationHandler:"), (IntPtr)(void*)ptr);
			if (ptr != null)
			{
				ptr->CleanupBlock();
			}
			return result;
		}

		[Export("beginBackgroundTaskWithName:expirationHandler:")]
		[ThreadSafe]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Advice("Overriding this method requires a call to the overriden method.")]
		[RequiresSuper]
		public unsafe virtual nint BeginBackgroundTask(string taskName, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDAction))] Action expirationHandler)
		{
			if (taskName == null)
			{
				throw new ArgumentNullException("taskName");
			}
			if (expirationHandler == null)
			{
				throw new ArgumentNullException("expirationHandler");
			}
			IntPtr intPtr = NSString.CreateNative(taskName);
			BlockLiteral blockLiteral = default(BlockLiteral);
			BlockLiteral* ptr = &blockLiteral;
			blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDAction.Handler, expirationHandler);
			nint result = (!base.IsDirectBinding) ? ObjCRuntime.Messaging.nint_objc_msgSendSuper_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("beginBackgroundTaskWithName:expirationHandler:"), intPtr, (IntPtr)(void*)ptr) : ObjCRuntime.Messaging.nint_objc_msgSend_IntPtr_IntPtr(base.Handle, Selector.GetHandle("beginBackgroundTaskWithName:expirationHandler:"), intPtr, (IntPtr)(void*)ptr);
			NSString.ReleaseNative(intPtr);
			ptr->CleanupBlock();
			return result;
		}

		[Export("beginIgnoringInteractionEvents")]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
		[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void BeginIgnoringInteractionEvents()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("beginIgnoringInteractionEvents"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("beginIgnoringInteractionEvents"));
			}
		}

		[Export("beginReceivingRemoteControlEvents")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void BeginReceivingRemoteControlEvents()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("beginReceivingRemoteControlEvents"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("beginReceivingRemoteControlEvents"));
			}
		}

		[Export("canOpenURL:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool CanOpenUrl(NSUrl? url)
		{
			EnsureUIThread();
			if (url == null)
			{
				return false;
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("canOpenURL:"), (url == null) ? IntPtr.Zero : url!.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("canOpenURL:"), (url == null) ? IntPtr.Zero : url!.Handle);
		}

		[Export("cancelAllLocalNotifications")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.RemoveAllPendingNotificationRequests' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void CancelAllLocalNotifications()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("cancelAllLocalNotifications"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("cancelAllLocalNotifications"));
			}
		}

		[Export("cancelLocalNotification:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.RemovePendingNotificationRequests' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void CancelLocalNotification(UILocalNotification notification)
		{
			EnsureUIThread();
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("cancelLocalNotification:"), notification.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("cancelLocalNotification:"), notification.Handle);
			}
		}

		[Export("clearKeepAliveTimeout")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'PushKit' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ClearKeepAliveTimeout()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("clearKeepAliveTimeout"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("clearKeepAliveTimeout"));
			}
		}

		[Export("completeStateRestoration")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void CompleteStateRestoration()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("completeStateRestoration"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("completeStateRestoration"));
			}
		}

		[Export("endBackgroundTask:")]
		[ThreadSafe]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[Advice("Overriding this method requires a call to the overriden method.")]
		[RequiresSuper]
		public virtual void EndBackgroundTask(nint taskId)
		{
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_nint(base.Handle, Selector.GetHandle("endBackgroundTask:"), taskId);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_nint(base.SuperHandle, Selector.GetHandle("endBackgroundTask:"), taskId);
			}
		}

		[Export("endIgnoringInteractionEvents")]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
		[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use UIView's 'UserInteractionEnabled' property instead")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void EndIgnoringInteractionEvents()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("endIgnoringInteractionEvents"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("endIgnoringInteractionEvents"));
			}
		}

		[Export("endReceivingRemoteControlEvents")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void EndReceivingRemoteControlEvents()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("endReceivingRemoteControlEvents"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("endReceivingRemoteControlEvents"));
			}
		}

		[Export("extendStateRestoration")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ExtendStateRestoration()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("extendStateRestoration"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("extendStateRestoration"));
			}
		}

		[Export("ignoreSnapshotOnNextApplicationLaunch")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void IgnoreSnapshotOnNextApplicationLaunch()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("ignoreSnapshotOnNextApplicationLaunch"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("ignoreSnapshotOnNextApplicationLaunch"));
			}
		}

		[Export("openURL:")]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Please use the overload instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool OpenUrl(NSUrl url)
		{
			EnsureUIThread();
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("openURL:"), url.Handle);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("openURL:"), url.Handle);
		}

		[Export("openURL:options:completionHandler:")]
		[Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe virtual void OpenUrl(NSUrl url, NSDictionary options, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDActionArity1V4))] Action<bool>? completion)
		{
			EnsureUIThread();
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			BlockLiteral* ptr;
			if (completion == null)
			{
				ptr = null;
			}
			else
			{
				BlockLiteral blockLiteral = default(BlockLiteral);
				ptr = &blockLiteral;
				blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDActionArity1V4.Handler, completion);
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr(base.Handle, Selector.GetHandle("openURL:options:completionHandler:"), url.Handle, options.Handle, (IntPtr)(void*)ptr);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("openURL:options:completionHandler:"), url.Handle, options.Handle, (IntPtr)(void*)ptr);
			}
			if (ptr != null)
			{
				ptr->CleanupBlock();
			}
		}

		[Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public void OpenUrl(NSUrl url, UIApplicationOpenUrlOptions options, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDActionArity1V4))] Action<bool>? completion)
		{
			OpenUrl(url, options.GetDictionary(), completion);
		}

		[Introduced(PlatformName.iOS, 10, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public Task<bool> OpenUrlAsync(NSUrl url, UIApplicationOpenUrlOptions options)
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			OpenUrl(url, options, (Action<bool>)delegate(bool obj_)
			{
				tcs.SetResult(obj_);
			});
			return tcs.Task;
		}

		[Export("presentLocalNotificationNow:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.AddNotificationRequest' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void PresentLocalNotificationNow(UILocalNotification notification)
		{
			EnsureUIThread();
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("presentLocalNotificationNow:"), notification.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("presentLocalNotificationNow:"), notification.Handle);
			}
		}

		[Export("registerForRemoteNotificationTypes:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 8, 0, PlatformArchitecture.All, "Use 'RegisterUserNotifications', 'RegisterForNotifications'  or 'UNUserNotificationCenter.RequestAuthorization' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void RegisterForRemoteNotificationTypes(UIRemoteNotificationType types)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_UInt64(base.Handle, Selector.GetHandle("registerForRemoteNotificationTypes:"), (ulong)types);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_UInt32(base.Handle, Selector.GetHandle("registerForRemoteNotificationTypes:"), (uint)types);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_UInt64(base.SuperHandle, Selector.GetHandle("registerForRemoteNotificationTypes:"), (ulong)types);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_UInt32(base.SuperHandle, Selector.GetHandle("registerForRemoteNotificationTypes:"), (uint)types);
			}
		}

		[Export("registerForRemoteNotifications")]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void RegisterForRemoteNotifications()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("registerForRemoteNotifications"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("registerForRemoteNotifications"));
			}
		}

		[Export("registerObjectForStateRestoration:restorationIdentifier:")]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static void RegisterObjectForStateRestoration(IUIStateRestoring uistateRestoringObject, string restorationIdentifier)
		{
			EnsureUIThread();
			if (uistateRestoringObject == null)
			{
				throw new ArgumentNullException("uistateRestoringObject");
			}
			if (restorationIdentifier == null)
			{
				throw new ArgumentNullException("restorationIdentifier");
			}
			IntPtr intPtr = NSString.CreateNative(restorationIdentifier);
			ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(class_ptr, Selector.GetHandle("registerObjectForStateRestoration:restorationIdentifier:"), uistateRestoringObject.Handle, intPtr);
			NSString.ReleaseNative(intPtr);
		}

		[Export("registerUserNotificationSettings:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 8, 0, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.RequestAuthorization' and 'UNUserNotificationCenter.SetNotificationCategories' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void RegisterUserNotificationSettings(UIUserNotificationSettings notificationSettings)
		{
			EnsureUIThread();
			if (notificationSettings == null)
			{
				throw new ArgumentNullException("notificationSettings");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("registerUserNotificationSettings:"), notificationSettings.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("registerUserNotificationSettings:"), notificationSettings.Handle);
			}
		}

		[Export("requestSceneSessionActivation:userActivity:options:errorHandler:")]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe virtual void RequestSceneSessionActivation(UISceneSession? sceneSession, NSUserActivity? userActivity, UISceneActivationRequestOptions? options, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDActionArity1V0))] Action<NSError>? errorHandler)
		{
			EnsureUIThread();
			BlockLiteral* ptr;
			if (errorHandler == null)
			{
				ptr = null;
			}
			else
			{
				BlockLiteral blockLiteral = default(BlockLiteral);
				ptr = &blockLiteral;
				blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDActionArity1V0.Handler, errorHandler);
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr_IntPtr(base.Handle, Selector.GetHandle("requestSceneSessionActivation:userActivity:options:errorHandler:"), sceneSession?.Handle ?? IntPtr.Zero, userActivity?.Handle ?? IntPtr.Zero, options?.Handle ?? IntPtr.Zero, (IntPtr)(void*)ptr);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("requestSceneSessionActivation:userActivity:options:errorHandler:"), sceneSession?.Handle ?? IntPtr.Zero, userActivity?.Handle ?? IntPtr.Zero, options?.Handle ?? IntPtr.Zero, (IntPtr)(void*)ptr);
			}
			if (ptr != null)
			{
				ptr->CleanupBlock();
			}
		}

		[Export("requestSceneSessionDestruction:options:errorHandler:")]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe virtual void RequestSceneSessionDestruction(UISceneSession sceneSession, UISceneDestructionRequestOptions? options, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDActionArity1V0))] Action<NSError>? errorHandler)
		{
			EnsureUIThread();
			if (sceneSession == null)
			{
				throw new ArgumentNullException("sceneSession");
			}
			BlockLiteral* ptr;
			if (errorHandler == null)
			{
				ptr = null;
			}
			else
			{
				BlockLiteral blockLiteral = default(BlockLiteral);
				ptr = &blockLiteral;
				blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDActionArity1V0.Handler, errorHandler);
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr_IntPtr(base.Handle, Selector.GetHandle("requestSceneSessionDestruction:options:errorHandler:"), sceneSession.Handle, options?.Handle ?? IntPtr.Zero, (IntPtr)(void*)ptr);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("requestSceneSessionDestruction:options:errorHandler:"), sceneSession.Handle, options?.Handle ?? IntPtr.Zero, (IntPtr)(void*)ptr);
			}
			if (ptr != null)
			{
				ptr->CleanupBlock();
			}
		}

		[Export("requestSceneSessionRefresh:")]
		[Introduced(PlatformName.iOS, 13, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 13, 0, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void RequestSceneSessionRefresh(UISceneSession sceneSession)
		{
			EnsureUIThread();
			if (sceneSession == null)
			{
				throw new ArgumentNullException("sceneSession");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("requestSceneSessionRefresh:"), sceneSession.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("requestSceneSessionRefresh:"), sceneSession.Handle);
			}
		}

		[Export("scheduleLocalNotification:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 10, 0, PlatformArchitecture.None, "Use 'UNUserNotificationCenter.AddNotificationRequest' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ScheduleLocalNotification(UILocalNotification notification)
		{
			EnsureUIThread();
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("scheduleLocalNotification:"), notification.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("scheduleLocalNotification:"), notification.Handle);
			}
		}

		[Export("sendAction:to:from:forEvent:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual bool SendAction(Selector action, NSObject? target, NSObject? sender, UIEvent? forEvent)
		{
			EnsureUIThread();
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (base.IsDirectBinding)
			{
				return ObjCRuntime.Messaging.bool_objc_msgSend_IntPtr_IntPtr_IntPtr_IntPtr(base.Handle, Selector.GetHandle("sendAction:to:from:forEvent:"), action.Handle, target?.Handle ?? IntPtr.Zero, sender?.Handle ?? IntPtr.Zero, forEvent?.Handle ?? IntPtr.Zero);
			}
			return ObjCRuntime.Messaging.bool_objc_msgSendSuper_IntPtr_IntPtr_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("sendAction:to:from:forEvent:"), action.Handle, target?.Handle ?? IntPtr.Zero, sender?.Handle ?? IntPtr.Zero, forEvent?.Handle ?? IntPtr.Zero);
		}

		[Export("sendEvent:")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SendEvent(UIEvent uievent)
		{
			EnsureUIThread();
			if (uievent == null)
			{
				throw new ArgumentNullException("uievent");
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("sendEvent:"), uievent.Handle);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("sendEvent:"), uievent.Handle);
			}
		}

		[Export("setAlternateIconName:completionHandler:")]
		[Introduced(PlatformName.iOS, 10, 3, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 2, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe virtual void SetAlternateIconName(string? alternateIconName, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDActionArity1V0))] Action<NSError>? completionHandler)
		{
			EnsureUIThread();
			IntPtr intPtr = NSString.CreateNative(alternateIconName);
			BlockLiteral* ptr;
			if (completionHandler == null)
			{
				ptr = null;
			}
			else
			{
				BlockLiteral blockLiteral = default(BlockLiteral);
				ptr = &blockLiteral;
				blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDActionArity1V0.Handler, completionHandler);
			}
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr_IntPtr(base.Handle, Selector.GetHandle("setAlternateIconName:completionHandler:"), intPtr, (IntPtr)(void*)ptr);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr(base.SuperHandle, Selector.GetHandle("setAlternateIconName:completionHandler:"), intPtr, (IntPtr)(void*)ptr);
			}
			NSString.ReleaseNative(intPtr);
			if (ptr != null)
			{
				ptr->CleanupBlock();
			}
		}

		[Introduced(PlatformName.iOS, 10, 3, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.TvOS, 10, 2, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual Task SetAlternateIconNameAsync(string? alternateIconName)
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			SetAlternateIconName(alternateIconName, (Action<NSError>)delegate(NSError obj_)
			{
				if (obj_ != null)
				{
					tcs.SetException(new NSErrorException(obj_));
				}
				else
				{
					tcs.SetResult(result: true);
				}
			});
			return tcs.Task;
		}

		[Export("setKeepAliveTimeout:handler:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'PushKit' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe virtual bool SetKeepAliveTimeout(double timeout, [BlockProxy(typeof(ObjCRuntime.Trampolines.NIDAction))] Action? handler)
		{
			EnsureUIThread();
			BlockLiteral* ptr;
			if (handler == null)
			{
				ptr = null;
			}
			else
			{
				BlockLiteral blockLiteral = default(BlockLiteral);
				ptr = &blockLiteral;
				blockLiteral.SetupBlockUnsafe(ObjCRuntime.Trampolines.SDAction.Handler, handler);
			}
			bool result = (!base.IsDirectBinding) ? ObjCRuntime.Messaging.bool_objc_msgSendSuper_Double_IntPtr(base.SuperHandle, Selector.GetHandle("setKeepAliveTimeout:handler:"), timeout, (IntPtr)(void*)ptr) : ObjCRuntime.Messaging.bool_objc_msgSend_Double_IntPtr(base.Handle, Selector.GetHandle("setKeepAliveTimeout:handler:"), timeout, (IntPtr)(void*)ptr);
			if (ptr != null)
			{
				ptr->CleanupBlock();
			}
			return result;
		}

		[Export("setMinimumBackgroundFetchInterval:")]
		[Introduced(PlatformName.TvOS, 11, 0, PlatformArchitecture.All, null)]
		[Introduced(PlatformName.iOS, 7, 0, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 13, 0, PlatformArchitecture.None, "Use a 'BGAppRefreshTask' from 'BackgroundTasks' framework.")]
		[Deprecated(PlatformName.TvOS, 13, 0, PlatformArchitecture.None, "Use a 'BGAppRefreshTask' from 'BackgroundTasks' framework.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetMinimumBackgroundFetchInterval(double minimumBackgroundFetchInterval)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_Double(base.Handle, Selector.GetHandle("setMinimumBackgroundFetchInterval:"), minimumBackgroundFetchInterval);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_Double(base.SuperHandle, Selector.GetHandle("setMinimumBackgroundFetchInterval:"), minimumBackgroundFetchInterval);
			}
		}

		[Export("setNewsstandIconImage:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetNewsstandIconImage(UIImage? image)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("setNewsstandIconImage:"), image?.Handle ?? IntPtr.Zero);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("setNewsstandIconImage:"), image?.Handle ?? IntPtr.Zero);
			}
		}

		[Export("setStatusBarHidden:withAnimation:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PrefersStatusBarHidden' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetStatusBarHidden(bool state, UIStatusBarAnimation animation)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool_Int64(base.Handle, Selector.GetHandle("setStatusBarHidden:withAnimation:"), state, (long)animation);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_bool_int(base.Handle, Selector.GetHandle("setStatusBarHidden:withAnimation:"), state, (int)animation);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_bool_Int64(base.SuperHandle, Selector.GetHandle("setStatusBarHidden:withAnimation:"), state, (long)animation);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_bool_int(base.SuperHandle, Selector.GetHandle("setStatusBarHidden:withAnimation:"), state, (int)animation);
			}
		}

		[Export("setStatusBarHidden:animated:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 3, 2, PlatformArchitecture.All, "Use 'SetStatusBarHidden (bool, UIStatusBarAnimation)' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetStatusBarHidden(bool hidden, bool animated)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend_bool_bool(base.Handle, Selector.GetHandle("setStatusBarHidden:animated:"), hidden, animated);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_bool_bool(base.SuperHandle, Selector.GetHandle("setStatusBarHidden:animated:"), hidden, animated);
			}
		}

		[Export("setStatusBarOrientation:animated:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetStatusBarOrientation(UIInterfaceOrientation orientation, bool animated)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_Int64_bool(base.Handle, Selector.GetHandle("setStatusBarOrientation:animated:"), (long)orientation, animated);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_int_bool(base.Handle, Selector.GetHandle("setStatusBarOrientation:animated:"), (int)orientation, animated);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_Int64_bool(base.SuperHandle, Selector.GetHandle("setStatusBarOrientation:animated:"), (long)orientation, animated);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_int_bool(base.SuperHandle, Selector.GetHandle("setStatusBarOrientation:animated:"), (int)orientation, animated);
			}
		}

		[Export("setStatusBarStyle:animated:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[Deprecated(PlatformName.iOS, 9, 0, PlatformArchitecture.None, "Use 'UIViewController.PreferredStatusBarStyle' instead.")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void SetStatusBarStyle(UIStatusBarStyle statusBarStyle, bool animated)
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					ObjCRuntime.Messaging.void_objc_msgSend_Int64_bool(base.Handle, Selector.GetHandle("setStatusBarStyle:animated:"), (long)statusBarStyle, animated);
				}
				else
				{
					ObjCRuntime.Messaging.void_objc_msgSend_int_bool(base.Handle, Selector.GetHandle("setStatusBarStyle:animated:"), (int)statusBarStyle, animated);
				}
			}
			else if (IntPtr.Size == 8)
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_Int64_bool(base.SuperHandle, Selector.GetHandle("setStatusBarStyle:animated:"), (long)statusBarStyle, animated);
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper_int_bool(base.SuperHandle, Selector.GetHandle("setStatusBarStyle:animated:"), (int)statusBarStyle, animated);
			}
		}

		[Export("supportedInterfaceOrientationsForWindow:")]
		[Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual UIInterfaceOrientationMask SupportedInterfaceOrientationsForWindow([Transient] UIWindow window)
		{
			EnsureUIThread();
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (base.IsDirectBinding)
			{
				if (IntPtr.Size == 8)
				{
					return (UIInterfaceOrientationMask)ObjCRuntime.Messaging.UInt64_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("supportedInterfaceOrientationsForWindow:"), window.Handle);
				}
				return (UIInterfaceOrientationMask)ObjCRuntime.Messaging.UInt32_objc_msgSend_IntPtr(base.Handle, Selector.GetHandle("supportedInterfaceOrientationsForWindow:"), window.Handle);
			}
			if (IntPtr.Size == 8)
			{
				return (UIInterfaceOrientationMask)ObjCRuntime.Messaging.UInt64_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("supportedInterfaceOrientationsForWindow:"), window.Handle);
			}
			return (UIInterfaceOrientationMask)ObjCRuntime.Messaging.UInt32_objc_msgSendSuper_IntPtr(base.SuperHandle, Selector.GetHandle("supportedInterfaceOrientationsForWindow:"), window.Handle);
		}

		[Export("unregisterForRemoteNotifications")]
		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void UnregisterForRemoteNotifications()
		{
			EnsureUIThread();
			if (base.IsDirectBinding)
			{
				ObjCRuntime.Messaging.void_objc_msgSend(base.Handle, Selector.GetHandle("unregisterForRemoteNotifications"));
			}
			else
			{
				ObjCRuntime.Messaging.void_objc_msgSendSuper(base.SuperHandle, Selector.GetHandle("unregisterForRemoteNotifications"));
			}
		}

		[BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (base.Handle == IntPtr.Zero)
			{
				__mt_WeakDelegate_var = null;
			}
		}
	}
}
