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
using System.Collections.Generic;

namespace Portability
{
	public class GConfManager : SettingsManager
	{
		protected GConf.Client client = new GConf.Client();
		protected string base_app_path;

		public GConfManager()
		{
			base_app_path = String.Format("/apps/{0}/{1}/",
			                              Utils.CompanyName,
			                              Utils.ProductName);
		}

		protected object GetObject(string setting)
		{
			string setting_path = base_app_path + setting.Replace('.', '/');

			try {
				return client.Get(setting_path);
			} catch (GConf.NoSuchKeyException) {
				return null;
			}
		}

		public override string GetString (string setting)
		{
			return (string)GetObject(setting);
		}

		public override void SetString (string setting, string val)
		{
			string setting_path = base_app_path + setting.Replace('.', '/');

			client.Set(setting_path, val);
		}

	}
}
