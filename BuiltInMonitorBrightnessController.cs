using System.Linq;
using System.Management;

namespace SyncMonitorBrightness
{
    public static class BuiltInMonitorBrightnessController
	{
		static ManagementClass wmiMonitorBrightnessClass = new ManagementClass(@"\\.\ROOT\WMI:WmiMonitorBrightness");
		static ManagementClass wmiMonitorBrightnessMethodsClass = new ManagementClass(@"\\.\ROOT\WMI:WmiMonitorBrightnessMethods");

		public static byte Get()
		{
			using (ManagementObjectCollection brightnessInstances = wmiMonitorBrightnessClass.GetInstances())
			{
				var brightnessObj = brightnessInstances
					.Cast<ManagementObject>()
					.FirstOrDefault()
					;

				var val = (byte?)(brightnessObj?.GetPropertyValue("CurrentBrightness"));
				return val ?? default;
			}
		}

		public static void Set(byte brightness)
		{
			using (var brightnessInstances = wmiMonitorBrightnessMethodsClass.GetInstances())
			{
				var brightnessObj = brightnessInstances
					.Cast<ManagementObject>()
					.FirstOrDefault()
					;

				var args = new object[] { 3000, brightness };

				brightnessObj?.InvokeMethod("WmiSetBrightness", args);
			}
		}
	}






}
