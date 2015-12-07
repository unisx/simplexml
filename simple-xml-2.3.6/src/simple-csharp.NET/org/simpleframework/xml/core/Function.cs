/*
* Function.java February 2008
*
* Copyright (C) 2008, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Function</code> object is used to encapsulated the method
	/// that is called when serializing an object. This contains details 
	/// on the type of method represented and ensures that reflection is
	/// not required each time the method is to be invoked.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Function
	{
		
		/// <summary> This is the method that is to be invoked by the function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'method '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Reflection.MethodInfo method;
		
		/// <summary> This is used to determine if the method takes the map.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'contextual '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private bool contextual;
		
		/// <summary> Constructor for the <code>Function</code> object. This is used
		/// to create an object that wraps the provided method it ensures
		/// that no reflection is required when the method is to be called.
		/// 
		/// </summary>
		/// <param name="method">this is the method that is to be wrapped by this
		/// </param>
		public Function(System.Reflection.MethodInfo method):this(method, false)
		{
		}
		
		/// <summary> Constructor for the <code>Function</code> object. This is used
		/// to create an object that wraps the provided method it ensures
		/// that no reflection is required when the method is to be called.
		/// 
		/// </summary>
		/// <param name="method">this is the method that is to be wrapped by this
		/// </param>
		/// <param name="contextual">determines if the method is a contextual one
		/// </param>
		public Function(System.Reflection.MethodInfo method, bool contextual)
		{
			this.contextual = contextual;
			this.method = method;
		}
		
		/// <summary> This method used to invoke the callback method of the provided
		/// object. This will acquire the session map from the context. If
		/// the provided object is not null then this will return null.
		/// 
		/// </summary>
		/// <param name="context">this is the context that contains the session
		/// </param>
		/// <param name="source">this is the object to invoke the function on
		/// 
		/// </param>
		/// <returns> this returns the result of the method invocation
		/// </returns>
		public virtual System.Object call(Context context, System.Object source)
		{
			if (source != null)
			{
				Session session = context.Session;
				System.Collections.IDictionary table = session.Map;
				
				if (contextual)
				{
					return method.invoke(source, table);
				}
				return method.invoke(source);
			}
			return null;
		}
	}
}