/*
* Builder.java April 2009
*
* Copyright (C) 2009, Niall Gallagher <niallg@users.sf.net>
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
	
	/// <summary> The <code>Builder</code> object is used to represent an single
	/// constructor within an object. It contains the actual constructor
	/// as well as the list of parameters. Each builder will score its
	/// weight when given a <code>Criteria</code> object. This allows
	/// the deserialization process to find the most suitable one to
	/// use when instantiating an object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Builder
	{
		/// <summary> This is used to determine if this <code>Builder</code> is a
		/// default constructor. If the class does contain a no argument
		/// constructor then this will return true.
		/// 
		/// </summary>
		/// <returns> true if the class has a default constructor
		/// </returns>
		virtual public bool Default
		{
			get
			{
				return index.size() == 0;
			}
			
		}
		
		/// <summary> This is the list of parameters in the order of declaration. </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final List < Parameter > list;
		
		/// <summary> This is the factory that is used to instantiate the object.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Reflection.ConstructorInfo factory;
		
		/// <summary> This is the map that contains the parameters to be used.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Index index;
		
		/// <summary> Constructor for the <code>Builder</code> object. This is used
		/// to create a factory like object used for instantiating objects.
		/// Each builder will score its suitability using the parameters
		/// it is provided.
		/// 
		/// </summary>
		/// <param name="factory">this is the factory used for instantiation
		/// </param>
		/// <param name="index">this is the map of parameters that are declared
		/// </param>
		public Builder(System.Reflection.ConstructorInfo factory, Index index)
		{
			this.list = index.getParameters();
			this.factory = factory;
			this.index = index;
		}
		
		/// <summary> This is used to acquire the named <code>Parameter</code> from
		/// the builder. A parameter is taken from the constructor which
		/// contains annotations for each object that is required. These
		/// parameters must have a matching field or method.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the parameter to be acquired
		/// 
		/// </param>
		/// <returns> this returns the named parameter for the builder
		/// </returns>
		public virtual Parameter getParameter(System.String name)
		{
			return index.get_Renamed(name);
		}
		
		/// <summary> This is used to instantiate the object using the default no
		/// argument constructor. If for some reason the object can not be
		/// instantiated then this will throw an exception with the reason.
		/// 
		/// </summary>
		/// <returns> this returns the object that has been instantiated
		/// </returns>
		public virtual System.Object getInstance()
		{
			//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
			if (!factory.isAccessible())
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				factory.setAccessible(true);
			}
			return factory.newInstance();
		}
		
		/// <summary> This is used to instantiate the object using a constructor that
		/// takes deserialized objects as arguments. The object that have
		/// been deserialized can be taken from the <code>Criteria</code>
		/// object which contains the deserialized values.
		/// 
		/// </summary>
		/// <param name="criteria">this contains the criteria to be used
		/// 
		/// </param>
		/// <returns> this returns the object that has been instantiated
		/// </returns>
		public virtual System.Object getInstance(Criteria criteria)
		{
			System.Object[] values = list.toArray();
			
			for (int i = 0; i < list.size(); i++)
			{
				System.String name = list.get_Renamed(i).getName();
				Variable variable = criteria.remove(name);
				System.Object value_Renamed = variable.Value;
				
				values[i] = value_Renamed;
			}
			return getInstance(values);
		}
		
		/// <summary> This is used to instantiate the object using a constructor that
		/// takes deserialized objects as arguments. The objects that have
		/// been deserialized are provided in declaration order so they can
		/// be passed to the constructor to instantiate the object.
		/// 
		/// </summary>
		/// <param name="list">this is the list of objects used for instantiation
		/// 
		/// </param>
		/// <returns> this returns the object that has been instantiated
		/// </returns>
		private System.Object getInstance(System.Object[] list)
		{
			//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
			if (!factory.isAccessible())
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				factory.setAccessible(true);
			}
			return factory.Invoke(list);
		}
		
		/// <summary> This is used to score this <code>Builder</code> object so that
		/// it can be weighed amongst other constructors. The builder that
		/// scores the highest is the one that is used for instantiation.
		/// 
		/// </summary>
		/// <param name="criteria">this contains the criteria to be used
		/// 
		/// </param>
		/// <returns> this returns the score based on the criteria provided
		/// </returns>
		public virtual int score(Criteria criteria)
		{
			int score = 0;
			
			for (int i = 0; i < list.size(); i++)
			{
				System.String name = list.get_Renamed(i).getName();
				Label label = criteria.get_Renamed(name);
				
				if (label == null)
				{
					return - 1;
				}
				score++;
			}
			return score;
		}
		
		/// <summary> This is used to acquire a descriptive name for the builder.
		/// Providing a name is useful in debugging and when exceptions are
		/// thrown as it describes the constructor the builder represents.
		/// 
		/// </summary>
		/// <returns> this returns the name of the constructor to be used
		/// </returns>
		public override System.String ToString()
		{
			return factory.ToString();
		}
	}
}