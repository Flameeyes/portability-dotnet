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
