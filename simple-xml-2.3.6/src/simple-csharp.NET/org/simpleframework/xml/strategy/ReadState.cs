/*
* ReadState.java April 2007
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
using WeakCache = org.simpleframework.xml.util.WeakCache;
namespace org.simpleframework.xml.strategy
{
	
	/// <summary> The <code>ReadState</code> object is used to store all graphs that
	/// are currently been read with a given cycle strategy. The goal of
	/// this object is to act as a temporary store for graphs such that 
	/// when the persistence session has completed the read graph will be
	/// garbage collected. This ensures that there are no lingering object
	/// reference that could cause a memory leak. If a graph for the given
	/// session key does not exist then a new one is created. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.util.WeakCache">
	/// </seealso>
	class ReadState:WeakCache
	{
		/// <summary> This is the contract that specifies the attributes to use.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Object, ReadGraph >
		//UPGRADE_NOTE: Final was removed from the declaration of 'contract '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Contract contract;
		
		/// <summary> This is the loader used to load the classes for this.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'loader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Loader loader;
		
		/// <summary> Constructor for the <code>ReadState</code> object. This is used
		/// to create graphs that are used for reading objects from the XML
		/// document. The specified strategy is used to acquire the names
		/// of the special attributes used during the serialization.
		/// 
		/// </summary>
		/// <param name="contract">this is name scheme used by the cycle strategy 
		/// </param>
		public ReadState(Contract contract)
		{
			InitBlock();
			this.loader = new Loader();
			this.contract = contract;
		}
		
		/// <summary> This will acquire the graph using the specified session map. If
		/// a graph does not already exist mapped to the given session then
		/// one will be created and stored with the key provided. Once the
		/// specified key is garbage collected then so is the graph object.
		/// 
		/// </summary>
		/// <param name="map">this is typically the persistence session map used 
		/// 
		/// </param>
		/// <returns> returns a graph used for reading the XML document
		/// </returns>
		public virtual ReadGraph find(System.Object map)
		{
			ReadGraph read = fetch(map);
			
			if (read != null)
			{
				return read;
			}
			return create(map);
		}
		
		/// <summary> This will acquire the graph using the specified session map. If
		/// a graph does not already exist mapped to the given session then
		/// one will be created and stored with the key provided. Once the
		/// specified key is garbage collected then so is the graph object.
		/// 
		/// </summary>
		/// <param name="map">this is typically the persistence session map used 
		/// 
		/// </param>
		/// <returns> returns a graph used for reading the XML document
		/// </returns>
		private ReadGraph create(System.Object map)
		{
			ReadGraph read = fetch(map);
			
			if (read == null)
			{
				read = new ReadGraph(contract, loader);
				cache(map, read);
			}
			return read;
		}
	}
}