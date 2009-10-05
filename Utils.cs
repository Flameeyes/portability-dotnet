
using System;
using System.IO;
using System.Reflection;

namespace Portability
{
	public static class Utils
	{
		private static Assembly _base_assembly;
		private static AssemblyCompanyAttribute _company;
		private static AssemblyProductAttribute _product;

		static Utils() {
			_base_assembly = Assembly.GetEntryAssembly();
			_company = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(_base_assembly, typeof(AssemblyCompanyAttribute));
			_product = (AssemblyProductAttribute)Attribute.GetCustomAttribute(_base_assembly, typeof(AssemblyProductAttribute));
		}

		public static string CompanyName
		{ get { return _company.Company; } }

		public static string ProductName
		{ get { return _product.Product; } }

		/* Copy streams in 4MiB increments; we can safely assume that anything
		 * running Mono *has* 4MiB of free memory at any point in time. */
		private static int StreamCopyBlockSize = 4 * 1024 * 1024;

		public static void StreamCopy(Stream input, Stream output)
		{
			int copied = 0;
			/* We cannot use unknown-length this way :( */
			long input_length = input.Length;

			while(input.CanRead)
			{
				int tocopy = (input_length > StreamCopyBlockSize) ? StreamCopyBlockSize : (int)input_length;
				byte[] tmpbuff = new byte[tocopy];
				int read = input.Read(tmpbuff, copied, tocopy);
				output.Write(tmpbuff, copied, read);

				copied += read;
				input_length -= read;
			}
		}
	}
}
