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

namespace Portability
{


	public abstract class SettingsManager
	{
		protected SettingsManager()
		{
		}

		static public SettingsManager Get() {
			switch(System.Environment.OSVersion.Platform) {
			case PlatformID.Unix:
				try {
					return new GConfManager();
				} catch(DllNotFoundException) {
					/* We might not have gnome, and thus GConf, available.
					 * But we don't want to fail in that case either so we
					 * decide to go with the Mono emulation of the registry
					 * instead (which is bad, but it works).
					 */
					return new RegistryManager();
				}
			default:
				return new RegistryManager();
			}
		}

		public abstract string GetString(string setting);
		public abstract void SetString(string setting, string val);
	}
}
