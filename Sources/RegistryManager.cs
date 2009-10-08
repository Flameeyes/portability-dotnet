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

namespace Portability
{
	public class RegistryManager : SettingsManager
	{
		protected RegistryKey key;

		public RegistryManager()
		{
			key = Registry.CurrentUser.CreateSubKey(String.Format("SOFTWARE/{0}/{1}",
			                                                      Utils.CompanyName,
			                                                      Utils.ProductName));
		}

		public override string GetString (string setting)
		{
			try {
				return key.GetValue(setting, null).ToString();
			} catch ( Exception ) {
				return null;
			}
		}

		public override void SetString (string setting, string val)
		{
			key.SetValue(setting, val, RegistryValueKind.String);
		}
	}
}