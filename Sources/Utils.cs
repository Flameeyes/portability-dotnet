/*
 * This file is part of Portability library.
 * Copyright © 2009 Diego E. Pettenò <flameeyes@flameeyes.eu>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 */

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
	}
}
