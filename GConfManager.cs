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

namespace PortableSettings
{
	public class GConfManager : Manager
	{
		protected GConf.Client client = new GConf.Client();
		protected string base_app_path;

		static protected string joinpath(string[] array, bool stripslash)
		{
			string retval = "";

			foreach(string component in array)
			{
				retval += component + "/";
			}

			if ( stripslash )
				retval = retval.TrimEnd('/');

			return retval;
		}

		public GConfManager(string[] softwareid)
		{
			base_app_path = "/apps/" + joinpath(softwareid, false);
		}

		protected object GetObject(string[] setting)
		{
			string setting_path = base_app_path + joinpath(setting, true);

			try {
				return client.Get(setting_path);
			} catch (GConf.NoSuchKeyException) {
				return null;
			}
		}

		public override string GetString (params string[] setting)
		{
			return (string)GetObject(setting);
		}

		public override void SetString (string val, params string[] setting)
		{
			string setting_path = base_app_path + joinpath(setting, true);

			client.Set(setting_path, val);
		}

	}
}
