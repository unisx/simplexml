package simple.xml.load;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.SortedSet;
import java.util.Set;

import org.w3c.dom.Document;
import junit.framework.TestCase;
import simple.xml.Attribute;
import simple.xml.Element;
import simple.xml.ElementList;
import simple.xml.Root;

public class CollectionTest extends TestCase {
        
   public static final String LIST = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";  

   public static final String ARRAY_LIST = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list class='java.util.ArrayList'>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";     
   
   public static final String HASH_SET = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list class='java.util.HashSet'>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";    

   public static final String TREE_SET = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list class='java.util.TreeSet'>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";       
  
   public static final String ABSTRACT_LIST = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list class='simple.xml.load.CollectionTest$AbstractList'>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";  
  
   public static final String NOT_A_COLLECTION = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list class='java.util.Hashtable'>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";  
   
   public static final String MISSING_COLLECTION = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list class='example.MyCollection'>\n"+   
   "      <entry id='1'>\n"+
   "         <text>one</text>  \n\r"+
   "      </entry>\n\r"+
   "      <entry id='2'>\n"+
   "         <text>two</text>  \n\r"+
   "      </entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";  

   public static final String EXTENDED_ENTRY_LIST = 
   "<?xml version=\"1.0\"?>\n"+
   "<test name='example'>\n"+
   "   <list>\n"+   
   "      <extended-entry id='1' class='simple.xml.load.CollectionTest$ExtendedEntry'>\n"+
   "         <text>one</text>  \n\r"+
   "         <description>this is an extended entry</description>\n\r"+
   "      </extended-entry>\n\r"+
   "      <extended-entry id='2' class='simple.xml.load.CollectionTest$ExtendedEntry'>\n"+
   "         <text>two</text>  \n\r"+
   "         <description>this is the second one</description>\n"+
   "      </extended-entry>\n"+
   "      <entry id='3'>\n"+
   "         <text>three</text>  \n\r"+
   "      </entry>\n"+
   "   </list>\n"+
   "</test>";     
   
   @Root(name="entry")
   public static class Entry implements Comparable {

      @Attribute(name="id")           
      private int id;           
           
      @Element(name="text")
      private String text;        

      public int compareTo(Object entry) {
         return id - ((Entry)entry).id;              
      }   
   }

   @Root(name="extended-entry")
   public static class ExtendedEntry extends Entry {

      @Element(name="description")
      private String description;           
   }

   @Root(name="test")
   public static class EntrySet implements Iterable<Entry> {

      @ElementList(name="list", type=Entry.class)
      private Set<Entry> list;           

      @Attribute(name="name")
      private String name;

      public Iterator<Entry> iterator() {
         return list.iterator();
      }
   }
   
   @Root(name="test")
   public static class EntrySortedSet implements Iterable<Entry> {

      @ElementList(name="list", type=Entry.class)
      private SortedSet<Entry> list;           

      @Attribute(name="name")
      private String name;

      public Iterator<Entry> iterator() {
         return list.iterator();  
      }
   }

   @Root(name="test")
   public static class EntryList implements Iterable<Entry> {

      @ElementList(name="list", type=Entry.class)
      private List<Entry> list;           

      @Attribute(name="name")
      private String name;

      public Iterator<Entry> iterator() {
         return list.iterator();  
      }
   } 

   public abstract class AbstractList extends ArrayList {

      public AbstractList() {
         super();              
      }        
   }
        
	private Persister serializer;

	public void setUp() {
	   serializer = new Persister();
	}
	
   public void testSet() throws Exception {    
      EntrySet set = (EntrySet) serializer.read(EntrySet.class, LIST);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   }
   
   public void testSortedSet() throws Exception {    
      EntrySortedSet set = (EntrySortedSet) serializer.read(EntrySortedSet.class, LIST);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   }

   public void testList() throws Exception {    
      EntryList set = (EntryList) serializer.read(EntryList.class, LIST);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   }

   public void testHashSet() throws Exception {    
      EntrySet set = (EntrySet) serializer.read(EntrySet.class, HASH_SET);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   }  

   public void testTreeSet() throws Exception {    
      EntrySortedSet set = (EntrySortedSet) serializer.read(EntrySortedSet.class, TREE_SET);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   }  
   
   public void testArrayList() throws Exception {    
      EntryList list = (EntryList) serializer.read(EntryList.class, ARRAY_LIST);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : list) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   } 

   public void testSortedSetToSet() throws Exception {    
      EntrySet set = (EntrySet) serializer.read(EntrySet.class, TREE_SET);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);
   }  

   public void testExtendedEntry() throws Exception {    
      EntrySet set = (EntrySet) serializer.read(EntrySet.class, EXTENDED_ENTRY_LIST);
      int one = 0;
      int two = 0;
      int three = 0;
      
      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 1);
      assertEquals(two, 1);
      assertEquals(three, 1);

      Document doc = serializer.write(set, System.err);
      EntrySet other = (EntrySet) serializer.read(EntrySet.class, doc);

      for(Entry entry : set) {
         if(entry.id == 1 && entry.text.equals("one")) {              
            one++;
         }
         if(entry.id == 2 && entry.text.equals("two")) {              
            two++;
         }
         if(entry.id == 3 && entry.text.equals("three")) {              
            three++;
         }
      }         
      assertEquals(one, 2);
      assertEquals(two, 2);
      assertEquals(three, 2);

      serializer.write(other, System.err);      
   }    

   public void testSetToSortedSet() throws Exception {    
      boolean success = false;

      try {      
         EntrySortedSet set = (EntrySortedSet) serializer.read(EntrySortedSet.class, HASH_SET);
      } catch(InstantiationException e) {
         success = true;              
      }         
      assertTrue(success);
   }  

   public void testListToSet() throws Exception {    
      boolean success = false;

      try {      
         EntrySet set = (EntrySet) serializer.read(EntrySet.class, ARRAY_LIST);
      } catch(InstantiationException e) {
         success = true;              
      }         
      assertTrue(success);
   }  


   public void testAbstractList() throws Exception {    
      boolean success = false;

      try {      
         EntryList set = (EntryList) serializer.read(EntryList.class, ABSTRACT_LIST);
      } catch(InstantiationException e) {
         success = true;              
      }         
      assertTrue(success);
   }  

   public void testNotACollection() throws Exception {    
      boolean success = false;

      try {      
         EntryList set = (EntryList) serializer.read(EntryList.class, NOT_A_COLLECTION);
      } catch(InstantiationException e) {
         success = true;              
      }         
      assertTrue(success);
   }  
   
   public void testMissingCollection() throws Exception {
      boolean success = false;

      try {      
         EntrySet set = (EntrySet) serializer.read(EntrySet.class, MISSING_COLLECTION);
      } catch(ClassNotFoundException e) {
         success = true;              
      }         
      assertTrue(success);           
   }
}
