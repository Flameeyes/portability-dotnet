
using System;
using System.IO;

namespace Portability
{
	public static class Paths
	{
		public static string HomeDir()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		private static string _cachedir = null;
		
		private static void MakeCacheDir()
		{
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

			_cachedir = Path.Combine(_cachedir, Path.Combine(Utils.CompanyName, Utils.ProductName));

			if (!System.IO.Directory.Exists(_cachedir))
				System.IO.Directory.CreateDirectory(_cachedir);

		}

		public static string CacheDir
		{
			get {
				if ( _cachedir == null )
					MakeCacheDir();

				return _cachedir;
			}
		}
	}
}
