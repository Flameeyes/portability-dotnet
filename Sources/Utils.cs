
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

		public static void StreamCopy(Stream input, Stream output)
		{
			int copied = 0;

			while(input.CanRead)
			{
				byte[] tmpbuff = new byte[4096];
				int read = input.Read(tmpbuff, copied, 4096);
				output.Write(tmpbuff, copied, read);

				copied += read;
			}
		}
	}
}
