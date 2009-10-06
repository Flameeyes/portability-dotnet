/*
 * This file is part of Portability library.
 * Copyright © 2009 Diego E. Pettenò <flameeyes@gmail.com>
 *
 * Portability is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * Portability is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with Portability.
 * If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using Microsoft.Win32;

namespace PortableSettings
{


	public class RegistryManager : Manager
	{
		protected RegistryKey key;

		public RegistryManager(string[] softwareid)
		{
			string company = String.Format("{0}.{1}", softwareid[0], softwareid[1]);
			string productname = softwareid[2];

			for (int i = 3; i < softwareid.Length; i++)
				productname += String.Format(".{0}", softwareid[i]);

			key = Registry.CurrentUser.CreateSubKey(String.Format("SOFTWARE/{0}/{1}",
			                                                      company, productname));
		}

		static protected string joinval(string[] array)
		{
			string retval = "";

			foreach(string component in array)
			{
				retval += component + '.';
			}

			retval = retval.TrimEnd('.');

			return retval;
		}

		public override string GetString (params string[] setting)
		{
			return key.GetValue(joinval(setting), null).ToString();
		}

		public override void SetString (string val, params string[] setting)
		{
			key.SetValue(joinval(setting), val, RegistryValueKind.String);
		}
	}
}
