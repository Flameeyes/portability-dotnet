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

namespace Portability
{


	public abstract class SettingsManager
	{
		protected SettingsManager()
		{
		}

		/**
		 * @brief Gets the best manager for the running system (registry, gconf, plist, ...)
		 *
		 * @param softwareid A dotted form of the software id (e.g.: eu.flameeyes.myproject)
		 */
		static public SettingsManager Get(string softwareid) {
			switch(System.Environment.OSVersion.Platform) {
			case PlatformID.Unix:
				return new GConfManager(softwareid);
			default:
				return new RegistryManager(softwareid);
			}
		}

		public abstract string GetString(string setting);
		public abstract void SetString(string setting, string val);
	}
}