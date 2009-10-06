
using System;

namespace Portability
{
	public static class Paths
	{
		public static string HomeDir()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		private static string _cachedir = null;
		
		public static string CacheDir(string product)
		{
			if ( _cachedir == null ) {
				switch(System.Environment.OSVersion.Platform) {
				case PlatformID.Win32NT:
			        _cachedir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					break;
				case PlatformID.Unix:
					_cachedir = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
					if ( _cachedir == null || _cachedir.Length == 0 )
						_cachedir = System.IO.Path.Combine(HomeDir(), ".cache");
					break;
				case PlatformID.MacOSX:
					_cachedir = System.IO.Path.Combine(HomeDir(), "Library\\Caches");
					break;
				default:
					throw new System.Exception("Unsupported operating system");
				}
			}

			string dir = System.IO.Path.Combine(_cachedir, product);

	        if (!System.IO.Directory.Exists(dir))
	            System.IO.Directory.CreateDirectory(dir);

			return dir;
		}
	}
}
