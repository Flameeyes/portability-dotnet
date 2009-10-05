
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

		private static string _permanentdir = null;

		private static void MakePermanentDataDir()
		{
			switch(System.Environment.OSVersion.Platform) {
			case PlatformID.Win32NT:
		        _permanentdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				break;
			case PlatformID.Unix:
				_permanentdir = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
				if ( _permanentdir == null || _permanentdir.Length == 0 )
					_permanentdir = System.IO.Path.Combine(HomeDir(), ".local/share/");
				break;
			case PlatformID.MacOSX:
				_permanentdir = System.IO.Path.Combine(HomeDir(), "Library\\Application Support");
				break;
			default:
				throw new System.Exception("Unsupported operating system");
			}

			_permanentdir = Path.Combine(_permanentdir, Path.Combine(Utils.CompanyName, Utils.ProductName));

			if (!System.IO.Directory.Exists(_permanentdir))
				System.IO.Directory.CreateDirectory(_permanentdir);
		}

		public static string PermanentDataDir
		{
			get {
				if ( _permanentdir == null )
					MakePermanentDataDir();

				return _permanentdir;
			}
		}

		public static string PermanentDataFile(string filename)
		{
			string full_file_path = Path.Combine(PermanentDataDir, filename);
			string full_path_base = Path.GetDirectoryName(full_file_path);
			if ( ! Directory.Exists(full_path_base) )
				Directory.CreateDirectory(full_path_base);

			return full_file_path;
		}

	}
}
