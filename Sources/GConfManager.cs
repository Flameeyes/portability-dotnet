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
			                              Utils.ProductName).
				Replace(' ', '_').Replace('.', '-');
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
