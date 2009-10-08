
using System;
using System.IO;

namespace Portability
{
	public static class Paths
	{
		public static string CombineMultiple(params string[] segments)
		{
			string combined = "";

			foreach(string segment in segments)
				combined = Path.Combine(combined, segment);

			return combined;
		}

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
			string full_file_path = Path.Combine(basedir, filepath);
			string full_path_base = Path.GetDirectoryName(full_file_path);
			if ( ! Directory.Exists(full_path_base) )
				Directory.CreateDirectory(full_path_base);

			return full_file_path;
		}

		public static string HomeDir()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		private static string _programdir;
		public static string ProgramDir
		{
			get
			{
				if ( _programdir == null )
				{
					System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
					_programdir = System.IO.Path.GetDirectoryName(a.Location);
				}

				return _programdir;
			}
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
					_cachedir = CombineMultiple(HomeDir(), ".cache");
				break;
			case PlatformID.MacOSX:
				_cachedir = CombineMultiple(HomeDir(), "Library", "Caches");
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

		public static string CacheFile(string filepath)
		{
			return GenericDataFile(CacheDir, filepath);
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
					_permanentdir = CombineMultiple(HomeDir(), ".local", "share");
				break;
			case PlatformID.MacOSX:
				_permanentdir = CombineMultiple(HomeDir(), "Library", "Application Support");
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

		private static string[] _executable_paths;
		public static string[] ExecutablesPaths
		{
			get {
				if ( _executable_paths == null ) {
					_executable_paths = Environment.
						GetEnvironmentVariable("PATH").
							Split(Environment.OSVersion.Platform == PlatformID.Win32NT ? ';' : ':');
				}

				return _executable_paths;
			}
		}

		public static string FindExecutable(string execname)
		{
			if ( Environment.OSVersion.Platform == PlatformID.Win32NT )
				execname = execname + ".exe";

			foreach ( string dir in ExecutablesPaths ) {
				string execpath = Path.Combine(dir, execname);
				if ( File.Exists( execpath ) )
					return execpath;
			}

			throw new FileNotFoundException("Unable to find executable", execname);
		}
	}
}
