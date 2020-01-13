﻿using System;
using System.Runtime.InteropServices;

namespace NFluidsynth.Native
{
	internal static partial class LibFluidsynth
	{
		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int fluid_is_soundfont([MarshalAs(UnmanagedType.LPUTF8Str)] string filename);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int fluid_is_midifile([MarshalAs(UnmanagedType.LPUTF8Str)] string filename);

		[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr fluid_set_log_function(int severity, Logger.LoggerDelegate func, IntPtr data);
	}
}