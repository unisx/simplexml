/*
* Collector.java December 2007
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Collector</code> object is used to store variables for
	/// a deserialized object. Each variable contains the label and value
	/// for a field or method. The <code>Composite</code> object uses
	/// this to store deserialized values before committing them to the
	/// objects methods and fields. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Composite">
	/// </seealso>
	class Collector : Criteria
	{
		private void  InitBlock()
		{
			return registry.iterator();
		}
		
		/// <summary> This is the registry containing all the variables collected.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'registry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Registry registry;
		
		/// <summary> This is the context object used by the serialization process.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> Constructor for the <code>Collector</code> object. This is 
		/// used to store variables for an objects fields and methods.
		/// Each variable is stored using the name of the label.
		/// 
		/// </summary>
		/// <param name="context">this is the context for the deserialization
		/// </param>
		public Collector(Context context)
		{
			InitBlock();
			this.registry = new Registry(this);
			this.context = context;
		}
		
		
		/// <summary> This is used to get the <code>Variable</code> that represents
		/// a deserialized object. The variable contains all the meta
		/// data for the field or method and the value that is to be set
		/// on the method or field.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the variable to be acquired
		/// 
		/// </param>
		/// <returns> this returns the named variable if it exists
		/// </returns>
		public virtual Variable get_Renamed(System.String name)
		{
			//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
			return registry[name];
		}
		
		/// <summary> This is used to remove the <code>Variable</code> from this
		/// criteria object. When removed, the variable will no longer be
		/// used to set the method or field when the <code>commit</code>
		/// method is invoked.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the variable to be removed
		/// 
		/// </param>
		/// <returns> this returns the named variable if it exists
		/// </returns>
		public virtual Variable remove(System.String name)
		{
			System.Object tempObject;
			tempObject = registry[name];
			registry.Remove(name);
			return tempObject;
		}
		
		/// <summary> This is used to acquire an iterator over the named variables.
		/// Providing an <code>Iterator</code> allows the criteria to be
		/// used in a for each loop. This is primarily for convenience.
		/// 
		/// </summary>
		/// <returns> this returns an iterator of all the variable names
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < String > iterator()
		
		/// <summary> This is used to create a <code>Variable</code> and set it for
		/// this criteria. The variable can be retrieved at a later stage
		/// using the name of the label. This allows for repeat reads as
		/// the variable can be used to acquire the labels converter.
		/// 
		/// </summary>
		/// <param name="label">this is the label used to create the variable
		/// </param>
		/// <param name="value">this is the value of the object to be read
		/// </param>
		public virtual void  set_Renamed(Label label, System.Object value_Renamed)
		{
			Variable variable = new Variable(label, value_Renamed);
			
			if (label != null)
			{
				System.String name = label.getName(context);
				System.String real = label.getName();
				
				if (!registry.ContainsKey(name))
				{
					registry[real] = variable;
					registry[name] = variable;
				}
			}
		}
		
		/// <summary> This is used to set the values for the methods and fields of
		/// the specified object. Invoking this performs the population
		/// of an object being deserialized. It ensures that each value 
		/// is set after the XML element has been fully read.
		/// 
		/// </summary>
		/// <param name="source">this is the object that is to be populated
		/// </param>
		public virtual void  commit(System.Object source)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Variable entry: set)
			{
				Contact contact = entry.getContact();
				System.Object value_Renamed = entry.getValue();
				
				contact.set_Renamed(source, value_Renamed);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Registry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The <code>Registry</code> object is used to store variables 
		/// for the collector. All variables are stored under its name so
		/// that they can be later retrieved and used to populate the
		/// object when deserialization of all variables has finished.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		[Serializable]
		private class Registry:System.Collections.Hashtable
		{
			public Registry(Collector enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			/// <summary> This is used to iterate over the names of the variables
			/// in the registry. This is primarily used for convenience
			/// so that the variables can be acquired in a for each loop.
			/// 
			/// </summary>
			/// <returns> an iterator containing the names of the variables
			/// </returns>
			private void  InitBlock(Collector enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
				//UPGRADE_TODO: Method 'java.util.HashMap.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapkeySet'"
				return new SupportClass.HashSetSupport(Keys).GetEnumerator();
			}
			private Collector enclosingInstance;
			public Collector Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< String, Variable >
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Iterator < String > iterator()
		}
	}
}