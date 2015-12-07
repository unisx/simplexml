/*
* Caller.java June 2007
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
	
	/// <summary> The <code>Caller</code> acts as a means for the schema to invoke
	/// the callback methods on an object. This ensures that the correct
	/// method is invoked within the schema class. If the annotated method
	/// accepts a map then this will provide that map to the method. This
	/// also ensures that if specific annotation is not present in the 
	/// class that no action is taken on a persister callback. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Caller
	{
		
		/// <summary> This is the pointer to the schema class commit function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'commit '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Function commit_Renamed_Field;
		
		/// <summary> This is the pointer to the schema class validation function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'validate '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Function validate_Renamed_Field;
		
		/// <summary> This is the pointer to the schema class persist function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'persist '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Function persist_Renamed_Field;
		
		/// <summary> This is the pointer to the schema class complete function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'complete '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Function complete_Renamed_Field;
		
		/// <summary> This is the pointer to the schema class replace function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'replace '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Function replace_Renamed_Field;
		
		/// <summary> This is the pointer to the schema class resolve function.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'resolve '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Function resolve_Renamed_Field;
		
		/// <summary> This is the context that is used to invoke the functions.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> Constructor for the <code>Caller</code> object. This is used 
		/// to wrap the schema class such that callbacks from the persister
		/// can be dealt with in a seamless manner. This ensures that the
		/// correct function and arguments are provided to the functions.
		/// element and attribute XML annotations scanned from
		/// 
		/// </summary>
		/// <param name="schema">this is the scanner that contains the functions
		/// </param>
		/// <param name="context">this is the context used to acquire the session
		/// </param>
		public Caller(Scanner schema, Context context)
		{
			this.validate_Renamed_Field = schema.Validate;
			this.complete_Renamed_Field = schema.Complete;
			this.replace_Renamed_Field = schema.Replace;
			this.resolve_Renamed_Field = schema.Resolve;
			this.persist_Renamed_Field = schema.Persist;
			this.commit_Renamed_Field = schema.Commit;
			this.context = context;
		}
		
		/// <summary> This is used to replace the deserialized object with another
		/// instance, perhaps of a different type. This is useful when an
		/// XML schema class acts as a reference to another XML document
		/// which needs to be loaded externally to create an object of
		/// a different type.
		/// 
		/// </summary>
		/// <param name="source">the source object to invoke the function on
		/// 
		/// </param>
		/// <returns> this returns the object that acts as the replacement
		/// 
		/// </returns>
		/// <throws>  Exception if the replacement function cannot complete </throws>
		public virtual System.Object replace(System.Object source)
		{
			if (replace_Renamed_Field != null)
			{
				return replace_Renamed_Field.call(context, source);
			}
			return source;
		}
		
		/// <summary> This is used to replace the deserialized object with another
		/// instance, perhaps of a different type. This is useful when an
		/// XML schema class acts as a reference to another XML document
		/// which needs to be loaded externally to create an object of
		/// a different type.
		/// 
		/// </summary>
		/// <param name="source">the source object to invoke the function on 
		/// 
		/// </param>
		/// <returns> this returns the object that acts as the replacement
		/// 
		/// </returns>
		/// <throws>  Exception if the replacement function cannot complete </throws>
		public virtual System.Object resolve(System.Object source)
		{
			if (resolve_Renamed_Field != null)
			{
				return resolve_Renamed_Field.call(context, source);
			}
			return source;
		}
		
		/// <summary> This method is used to invoke the provided objects commit function
		/// during the deserialization process. The commit function must be
		/// marked with the <code>Commit</code> annotation so that when the
		/// object is deserialized the persister has a chance to invoke the
		/// function so that the object can build further data structures.
		/// 
		/// </summary>
		/// <param name="source">this is the object that has just been deserialized
		/// 
		/// </param>
		/// <throws>  Exception thrown if the commit process cannot complete </throws>
		public virtual void  commit(System.Object source)
		{
			if (commit_Renamed_Field != null)
			{
				commit_Renamed_Field.call(context, source);
			}
		}
		
		/// <summary> This method is used to invoke the provided objects validation
		/// function during the deserialization process. The validation function
		/// must be marked with the <code>Validate</code> annotation so that
		/// when the object is deserialized the persister has a chance to 
		/// invoke that function so that object can validate its field values.
		/// 
		/// </summary>
		/// <param name="source">this is the object that has just been deserialized
		/// 
		/// </param>
		/// <throws>  Exception thrown if the validation process failed </throws>
		public virtual void  validate(System.Object source)
		{
			if (validate_Renamed_Field != null)
			{
				validate_Renamed_Field.call(context, source);
			}
		}
		
		/// <summary> This method is used to invoke the provided objects persistence
		/// function. This is invoked during the serialization process to
		/// get the object a chance to perform an necessary preparation
		/// before the serialization of the object proceeds. The persist
		/// function must be marked with the <code>Persist</code> annotation.
		/// 
		/// </summary>
		/// <param name="source">the object that is about to be serialized
		/// 
		/// </param>
		/// <throws>  Exception thrown if the object cannot be persisted </throws>
		public virtual void  persist(System.Object source)
		{
			if (persist_Renamed_Field != null)
			{
				persist_Renamed_Field.call(context, source);
			}
		}
		
		/// <summary> This method is used to invoke the provided objects completion
		/// function. This is invoked after the serialization process has
		/// completed and gives the object a chance to restore its state
		/// if the persist function required some alteration or locking.
		/// This is marked with the <code>Complete</code> annotation.
		/// 
		/// </summary>
		/// <param name="source">this is the object that has been serialized
		/// 
		/// </param>
		/// <throws>  Exception thrown if the object cannot complete </throws>
		public virtual void  complete(System.Object source)
		{
			if (complete_Renamed_Field != null)
			{
				complete_Renamed_Field.call(context, source);
			}
		}
	}
}