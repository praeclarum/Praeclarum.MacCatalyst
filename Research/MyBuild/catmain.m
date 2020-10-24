#define MONOMAC 1
#include <xamarin/xamarin.h>
#import <Foundation/NSDate.h>
#include <stdio.h>


extern "C" int xammac_setup ()
{
	printf("HI FRANK\n");
	xamarin_marshal_objectivec_exception_mode = MarshalObjectiveCExceptionModeDisable;
	xamarin_disable_lldb_attach = true;
	xamarin_log_level = 4;
	xamarin_arch_name = "x86_64";

	// xamarin_create_classes ();
	setenv ("MONO_GC_PARAMS", "major=marksweep", 1);
	xamarin_supports_dynamic_registration = TRUE;
	xamarin_mac_modern = TRUE;
	
	xamarin_initialize ();
	
	return 0;
}

