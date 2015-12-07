/*
* Resolver.java February 2001
*
* Copyright (C) 2001, Niall Gallagher <niallg@users.sf.net>
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
//UPGRADE_TODO: The type 'java.util.LinkedHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedHashMap = java.util.LinkedHashMap;
namespace org.simpleframework.xml.util
{
	
	/// <summary> This is used to store <code>Match</code> objects, which can then be
	/// retrieved using a string by comparing that string to the pattern of
	/// the <code>Match</code> objects. Patterns consist of characters
	/// with either the '*' or '?' characters as wild characters. The '*'
	/// character is completely wild meaning that is will match nothing or
	/// a long sequence of characters. The '?' character matches a single
	/// character.
	/// <p>
	/// If the '?' character immediately follows the '*' character then the
	/// match is made as any sequence of characters up to the first match 
	/// of the next character. For example "/*?/index.jsp" will match all 
	/// files preceeded by only a single path. So "/pub/index.jsp" will
	/// match, however "/pub/bin/index.jsp" will not, as it has two paths.
	/// So, in effect the '*?' sequence will match anything or nothing up
	/// to the first occurence of the next character in the pattern.
	/// <p>
	/// A design goal of the <code>Resolver</code> was to make it capable
	/// of  high performance. In order to achieve a high performance the 
	/// <code>Resolver</code> can cache the resolutions it makes so that if
	/// the same text is given to the <code>Resolver.resolve</code> method 
	/// a cached result can be retrived quickly which will decrease the 
	/// length of time and work required to perform the match.
	/// <p>
	/// The semantics of the resolver are such that the last pattern added
	/// with a wild string is the first one checked for a match. This means
	/// that if a sequence of insertions like <code>add(x)</code> followed
	/// by <code>add(y)</code> is made, then a <code>resolve(z)</code> will
	/// result in a comparison to y first and then x, if z matches y then 
	/// it is given as the result and if z does not match y and matches x 
	/// then x is returned, remember if z matches both x and y then y will 
	/// be the result due to the fact that is was the last pattern added.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public class Resolver
	{
		/// <summary> Caches the text resolutions made to reduce the work required.</summary>
		private void  InitBlock()
		{
			
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			if (list != null)
			{
				return list;
			}
			char[] array = text.toCharArray();
			
			if (array == null)
			{
				return null;
			}
			return resolveAll(text, array);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			List < M > list = new ArrayList < M >();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(M match: stack)
			{
				System.String wild = match.getPattern();
				
				if (match(array, wild.ToCharArray()))
				{
					cache.put(text, list);
					list.add(match);
				}
			}
			return list;
			return stack.sequence();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< M extends Match > extends AbstractSet < M >
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Cache cache;
		
		/// <summary> Stores the matches added to the resolver in resolution order.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'stack '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Stack stack;
		
		/// <summary> The default constructor will create a <code>Resolver</code>
		/// without a large cache size. This is intended for use when 
		/// the requests for <code>resolve</code> tend to use strings
		/// that are reasonably similar. If the strings issued to this
		/// instance are dramatically different then the cache tends 
		/// to be an overhead rather than a bonus.
		/// </summary>
		public Resolver()
		{
			InitBlock();
			this.stack = new Stack(this);
			this.cache = new Cache(this);
		}
		
		/// <summary> This will search the patterns in this <code>Resolver</code> to
		/// see if there is a pattern in it that matches the string given.
		/// This will search the patterns from the last entered pattern to
		/// the first entered. So that the last entered patterns are the
		/// most searched patterns and will resolve it first if it matches.
		/// 
		/// </summary>
		/// <param name="text">this is the string that is to be matched by this
		/// 
		/// </param>
		/// <returns> this will return the first match within the resolver
		/// </returns>
		public virtual M resolve(System.String text)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			if (list == null)
			{
				list = resolveAll(text);
			}
			if (list.isEmpty())
			{
				return null;
			}
			return list.get_Renamed(0);
		}
		
		/// <summary> This will search the patterns in this <code>Resolver</code> to
		/// see if there is a pattern in it that matches the string given.
		/// This will search the patterns from the last entered pattern to
		/// the first entered. So that the last entered patterns are the
		/// most searched patterns and will resolve it first if it matches.
		/// 
		/// </summary>
		/// <param name="text">this is the string that is to be matched by this
		/// 
		/// </param>
		/// <returns> this will return all of the matches within the resolver
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < M > resolveAll(String text)
		
		/// <summary> This will search the patterns in this <code>Resolver</code> to
		/// see if there is a pattern in it that matches the string given.
		/// This will search the patterns from the last entered pattern to
		/// the first entered. So that the last entered patterns are the
		/// most searched patterns and will resolve it first if it matches.
		/// 
		/// </summary>
		/// <param name="text">this is the string that is to be matched by this
		/// </param>
		/// <param name="array">this is the character array of the text string
		/// 
		/// </param>
		/// <returns> this will return all of the matches within the resolver
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < M > resolveAll(String text, char [] array)
		
		/// <summary> This inserts the <code>Match</code> implementation into the set
		/// so that it can be used for resolutions. The last added match is
		/// the first resolved. Because this changes the state of the 
		/// resolver this clears the cache as it may affect resolutions.
		/// 
		/// </summary>
		/// <param name="match">this is the match that is to be inserted to this
		/// 
		/// </param>
		/// <returns> returns true if the addition succeeded, always true
		/// </returns>
		public virtual bool add(M match)
		{
			stack.push(match);
			return true;
		}
		
		/// <summary> This returns an <code>Iterator</code> that iterates over the
		/// matches in insertion order. So the first match added is the
		/// first retrieved from the <code>Iterator</code>. This order is
		/// used to ensure that resolver can be serialized properly.
		/// 
		/// </summary>
		/// <returns> returns an iterator for the sequence of insertion
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < M > iterator()
		
		/// <summary> This is used to remove the <code>Match</code> implementation
		/// from the resolver. This clears the cache as the removal of
		/// a match may affect the resoultions existing in the cache. The
		/// <code>equals</code> method of the match must be implemented.
		/// 
		/// </summary>
		/// <param name="match">this is the match that is to be removed
		/// 
		/// </param>
		/// <returns> true of the removal of the match was successful
		/// </returns>
		public virtual bool remove(M match)
		{
			cache.clear();
			System.Boolean tempBoolean;
			tempBoolean = stack.Contains(match);
			stack.Remove(match);
			return tempBoolean;
		}
		
		/// <summary> Returns the number of matches that have been inserted into 
		/// the <code>Resolver</code>. Although this is a set, it does 
		/// not mean that matches cannot used the same pattern string.
		/// 
		/// </summary>
		/// <returns> this returns the number of matches within the set
		/// </returns>
		public virtual int size()
		{
			return stack.Count;
		}
		
		/// <summary> This is used to clear all matches from the set. This ensures
		/// that the resolver contains no matches and that the resolution 
		/// cache is cleared. This is used to that the set can be reused
		/// and have new pattern matches inserted into it for resolution.
		/// </summary>
		public virtual void  clear()
		{
			cache.clear();
			stack.Clear();
		}
		
		/// <summary> This acts as a driver to the <code>match</code> method so that
		/// the offsets can be used as zeros for the start of matching for 
		/// the <code>match(char[],int,char[],int)</code>. method. This is
		/// also used as the initializing driver for the recursive method.
		/// 
		/// </summary>
		/// <param name="text">this is the buffer that is to be resolved
		/// </param>
		/// <param name="wild">this is the pattern that will be used
		/// </param>
		private bool match(char[] text, char[] wild)
		{
			return match(text, 0, wild, 0);
		}
		
		/// <summary> This will be used to check to see if a certain buffer matches
		/// the pattern if it does then it returns <code>true</code>. This
		/// is a recursive method that will attempt to match the buffers 
		/// based on the wild characters '?' and '*'. If there is a match
		/// then this returns <code>true</code>.
		/// 
		/// </summary>
		/// <param name="text">this is the buffer that is to be resolved
		/// </param>
		/// <param name="off">this is the read offset for the text buffer
		/// </param>
		/// <param name="wild">this is the pattern that will be used
		/// </param>
		/// <param name="pos">this is the read offset for the wild buffer
		/// </param>
		private bool match(char[] text, int off, char[] wild, int pos)
		{
			while (pos < wild.Length && off < text.Length)
			{
				/* examine chars */
				if (wild[pos] == '*')
				{
					while (wild[pos] == '*')
					{
						/* totally wild */
						if (++pos >= wild.Length)
						/* if finished */
							return true;
					}
					if (wild[pos] == '?')
					{
						/* *? is special */
						if (++pos >= wild.Length)
							return true;
					}
					for (; off < text.Length; off++)
					{
						/* find next matching char */
						if (text[off] == wild[pos] || wild[pos] == '?')
						{
							/* match */
							if (wild[pos - 1] != '?')
							{
								if (match(text, off, wild, pos))
									return true;
							}
							else
							{
								break;
							}
						}
					}
					if (text.Length == off)
						return false;
				}
				if (text[off++] != wild[pos++])
				{
					if (wild[pos - 1] != '?')
						return false; /* if not equal */
				}
			}
			if (wild.Length == pos)
			{
				/* if wild is finished */
				return text.Length == off; /* is text finished */
			}
			while (wild[pos] == '*')
			{
				/* ends in all stars */
				if (++pos >= wild.Length)
				/* if finished */
					return true;
			}
			return false;
		}
		
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Cache' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> This is used to cache resolutions made so that the matches can
		/// be acquired the next time without performing the resolution.
		/// This is an LRU cache so regardless of the number of resolutions
		/// made this will not result in a memory leak for the resolver.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Cache:LinkedHashMap
		{
			/// <summary> Constructor for the <code>Cache</code> object. This is a
			/// constructor that creates the linked hash map such that 
			/// it will purge the entries that are oldest within the map.
			/// </summary>
			private void  InitBlock(Resolver enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
			}
			private Resolver enclosingInstance;
			public Resolver Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< String, List < M >>
			public Cache(Resolver enclosingInstance):base(1024, 0.75f, false)
			{
				InitBlock(enclosingInstance);
			}
			
			/// <summary> This is used to remove the eldest entry from the LRU cache.
			/// The eldest entry is removed from the cache if the size of
			/// the map grows larger than the maximum entiries permitted.
			/// 
			/// </summary>
			/// <param name="entry">this is the eldest entry that can be removed
			/// 
			/// </param>
			/// <returns> this returns true if the entry should be removed
			/// </returns>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public virtual bool removeEldestEntry(ref System.Collections.DictionaryEntry entry)
			{
				return Enclosing_Instance.size() > 1024;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Stack' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> This is used to store the <code>Match</code> implementations in
		/// resolution order. Resolving the match objects is performed so
		/// that the last inserted match object is the first used in the
		/// resolution process. This gives priority to the last inserted.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		//UPGRADE_TODO: Class 'java.util.LinkedList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLinkedList'"
		[Serializable]
		private class Stack:System.Collections.ArrayList
		{
			public Stack(Resolver enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			/// <summary> The <code>push</code> method is used to push the match to 
			/// the top of the stack. This also ensures that the cache is
			/// cleared so the semantics of the resolver are not affected.
			/// 
			/// </summary>
			/// <param name="match">this is the match to be inserted to the stack
			/// </param>
			private void  InitBlock(Resolver enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
				return new Sequence(this);
			}
			private Resolver enclosingInstance;
			public Resolver Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< M >
			public virtual void  push(M match)
			{
				Enclosing_Instance.cache.clear();
				Insert(0, match);
			}
			
			/// <summary> The <code>purge</code> method is used to purge a match from
			/// the provided position. This also ensures that the cache is
			/// cleared so that the semantics of the resolver do not change.
			/// 
			/// </summary>
			/// <param name="index">the index of the match that is to be removed
			/// </param>
			public virtual void  purge(int index)
			{
				Enclosing_Instance.cache.clear();
				System.Object tempObject;
				tempObject = this[index];
				RemoveAt(index);
				System.Object generatedAux2 = tempObject;
			}
			
			/// <summary> This is returned from the <code>Resolver.iterator</code> so
			/// that matches can be iterated in insertion order. When a
			/// match is removed from this iterator then it clears the cache
			/// and removed the match from the <code>Stack</code> object.
			/// 
			/// </summary>
			/// <returns> returns an iterator to iterate in insertion order
			/// </returns>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Iterator < M > sequence()
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Sequence' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			/// <summary> The is used to order the <code>Match</code> objects in the
			/// insertion order. Iterating in insertion order allows the
			/// resolver object to be serialized and deserialized to and
			/// from an XML document without disruption resolution order.
			/// 
			/// </summary>
			/// <author>  Niall Gallagher
			/// </author>
			private class Sequence : System.Collections.IEnumerator
			{
				/// <summary> The cursor used to acquire objects from the stack.</summary>
				private void  InitBlock(Stack enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
					
				}
				private Stack enclosingInstance;
				/// <summary> This returns the <code>Match</code> object at the cursor
				/// position. If the cursor has reached the start of the 
				/// list then this returns null instead of the first match.
				/// 
				/// </summary>
				/// <returns> this returns the match from the cursor position
				/// </returns>
				public virtual System.Object Current
				{
					get
					{
						//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
						if (MoveNext())
						{
							return Enclosing_Instance[--cursor];
						}
						return null;
					}
					
				}
				public Stack Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				< M >
				private int cursor;
				
				/// <summary> Constructor for the <code>Sequence</code> object. This is
				/// used to position the cursor at the end of the list so the
				/// first inserted match is the first returned from this.
				/// </summary>
				public Sequence(Stack enclosingInstance)
				{
					InitBlock(enclosingInstance);
					this.cursor = Enclosing_Instance.Count;
				}
				
				/// <summary> This is used to determine if the cursor has reached the
				/// start of the list. When the cursor reaches the start of
				/// the list then this method returns false.
				/// 
				/// </summary>
				/// <returns> this returns true if there are more matches left
				/// </returns>
				public virtual bool MoveNext()
				{
					return cursor > 0;
				}
				
				/// <summary> Removes the match from the cursor position. This also
				/// ensures that the cache is cleared so that resolutions
				/// made before the removal do not affect the semantics.
				/// </summary>
				//UPGRADE_NOTE: The equivalent of method 'java.util.Iterator.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public virtual void  remove()
				{
					Enclosing_Instance.purge(cursor);
				}
				//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
				virtual public void  Reset()
				{
				}
			}
		}
	}
}