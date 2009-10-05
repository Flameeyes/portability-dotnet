
using System;
using System.IO;

namespace Portability
{
	public static class Paths
	{
		private static string _productpath = Path.Combine(Utils.CompanyName, Utils.ProductName);
		private static string CombineCreateProductDir(string basedir)
		{
			string result = Path.Combine(basedir, _productpath);
			if ( !Directory.Exists(result) )
				Directory.CreateDirectory(result);

			return result;
		}

		private static string GenericDataFile(string basedir, string filepath)
		{
			string full_file_path = Path.Combine(basedir, filename);
			string full_path_base = Path.GetDirectoryName(full_file_path);
			if ( ! Directory.Exists(full_path_base) )
				Directory.CreateDirectory(full_path_base);

			return full_file_path;
		}

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

			_cachedir = CombineCreateProductDir(_cachedir);
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

			_permanentdir = CombineCreateProductDir(_permanentdir);
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
			return GenericDataFile(PermanentDataDir, filename);
		}
	}
}
