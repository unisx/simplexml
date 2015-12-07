/*
* PackageMatcher.java May 2007
*
* Copyright (C) 2007, Niall Gallagher <niallg@users.sf.net>
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
* implied. See the License for the specific language governing 
* permissions and limitations under the License.
*/
using System;
//UPGRADE_TODO: The type 'java.util.Currency' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Currency = java.util.Currency;
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>PackageMatcher</code> object is used to match the stock
	/// transforms to Java packages. This is used to match useful types 
	/// from the <code>java.lang</code> and <code>java.util</code> packages
	/// as well as other Java packages. This matcher groups types by their
	/// package names and attempts to search the stock transforms for a
	/// suitable match. If no match can be found this throws an exception.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.DefaultMatcher">
	/// </seealso>
	class PackageMatcher : Matcher
	{
		
		/// <summary> Constructor for the <code>PackageMatcher</code> object. The
		/// package matcher is used to resolve a transform instance to
		/// convert object types to an from strings. If a match cannot
		/// be found with this matcher then an exception is thrown.
		/// </summary>
		public PackageMatcher():base()
		{
		}
		
		/// <summary> This method attempts to perform a resolution of the transform
		/// based on its package prefix. This allows this matcher to create
		/// a logical group of transforms within a single method based on
		/// the types package prefix. If no transform can be found then
		/// this will throw an exception.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a transform for
		/// 
		/// </param>
		/// <returns> the transform that is used to transform that type
		/// </returns>
		public virtual Transform match(System.Type type)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String name = type.FullName;
			
			if (name.StartsWith("java.lang"))
			{
				return matchLanguage(type);
			}
			if (name.StartsWith("java.util"))
			{
				return matchUtility(type);
			}
			if (name.StartsWith("java.net"))
			{
				return matchURL(type);
			}
			if (name.StartsWith("java.io"))
			{
				return matchFile(type);
			}
			if (name.StartsWith("java.sql"))
			{
				return matchSQL(type);
			}
			if (name.StartsWith("java.math"))
			{
				return matchMath(type);
			}
			return matchEnum(type);
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that are <code>Enum</code> implementations. If the type is not
		/// an enumeration then this will return null.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchEnum(System.Type type)
		{
			if (type.isEnum())
			{
				return new EnumTransform(type);
			}
			return null;
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that relate to the <code>java.lang</code> package. If the type
		/// does not resolve to a valid transform then this method will 
		/// throw an exception to indicate that no stock transform exists
		/// for the specified type.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchLanguage(System.Type type)
		{
			if (type == typeof(System.Boolean))
			{
				return new BooleanTransform();
			}
			if (type == typeof(System.Int32))
			{
				return new IntegerTransform();
			}
			if (type == typeof(System.Int64))
			{
				return new LongTransform();
			}
			if (type == typeof(System.Double))
			{
				return new DoubleTransform();
			}
			if (type == typeof(System.Single))
			{
				return new FloatTransform();
			}
			if (type == typeof(System.Int16))
			{
				return new ShortTransform();
			}
			if (type == typeof(System.SByte))
			{
				return new ByteTransform();
			}
			if (type == typeof(System.Char))
			{
				return new CharacterTransform();
			}
			if (type == typeof(System.String))
			{
				return new StringTransform();
			}
			if (type == typeof(System.Type))
			{
				return new ClassTransform();
			}
			return null;
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that relate to the <code>java.math</code> package. If the type
		/// does not resolve to a valid transform then this method will 
		/// throw an exception to indicate that no stock transform exists
		/// for the specified type.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchMath(System.Type type)
		{
			//UPGRADE_TODO: Class 'java.math.BigDecimal' was converted to 'System.Decimal' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javamathBigDecimal'"
			if (type == typeof(System.Decimal))
			{
				return new BigDecimalTransform();
			}
			//UPGRADE_TODO: Class 'java.math.BigInteger' was converted to 'System.Decimal' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javamathBigInteger'"
			if (type == typeof(System.Decimal))
			{
				return new BigIntegerTransform();
			}
			return null;
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that relate to the <code>java.util</code> package. If the type
		/// does not resolve to a valid transform then this method will 
		/// throw an exception to indicate that no stock transform exists
		/// for the specified type.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchUtility(System.Type type)
		{
			if (type == typeof(System.DateTime))
			{
				return new DateTransform(type);
			}
			if (type == typeof(System.Globalization.CultureInfo))
			{
				return new LocaleTransform();
			}
			if (type == typeof(Currency))
			{
				return new CurrencyTransform();
			}
			if (type == typeof(System.Globalization.GregorianCalendar))
			{
				return new GregorianCalendarTransform();
			}
			if (type == typeof(System.TimeZone))
			{
				return new TimeZoneTransform();
			}
			return null;
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that relate to the <code>java.sql</code> package. If the type
		/// does not resolve to a valid transform then this method will 
		/// throw an exception to indicate that no stock transform exists
		/// for the specified type.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchSQL(System.Type type)
		{
			if (type == typeof(System.DateTime))
			{
				return new DateTransform(type);
			}
			if (type == typeof(System.DateTime))
			{
				return new DateTransform(type);
			}
			if (type == typeof(System.DateTime))
			{
				return new DateTransform(type);
			}
			return null;
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that relate to the <code>java.io</code> package. If the type
		/// does not resolve to a valid transform then this method will 
		/// throw an exception to indicate that no stock transform exists
		/// for the specified type.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchFile(System.Type type)
		{
			if (type == typeof(System.IO.FileInfo))
			{
				return new FileTransform();
			}
			return null;
		}
		
		/// <summary> This is used to resolve <code>Transform</code> implementations
		/// that relate to the <code>java.net</code> package. If the type
		/// does not resolve to a valid transform then this method will 
		/// throw an exception to indicate that no stock transform exists
		/// for the specified type.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a stock transform for
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform matchURL(System.Type type)
		{
			if (type == typeof(System.Uri))
			{
				return new URLTransform();
			}
			return null;
		}
	}
}