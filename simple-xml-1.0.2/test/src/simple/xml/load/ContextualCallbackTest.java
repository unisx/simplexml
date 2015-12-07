package simple.xml.load;

import junit.framework.TestCase;
import simple.xml.ElementList;
import simple.xml.Attribute;
import simple.xml.Element;
import simple.xml.Root;
import java.util.Map;

public class ContextualCallbackTest extends TestCase {
        
   private static final String SOURCE =
   "<?xml version=\"1.0\"?>\n"+
   "<root number='1234' flag='true'>\n"+
   "   <value>complete</value>  \n\r"+
   "</root>";
   
   @Root(name="root")
   public static class Entry {

      @Attribute(name="number", required=false)
      private int number = 9999;     

      @Attribute(name="flag")
      private boolean bool;
      
      @Element(name="value", required=false)
      private String value = "default";

      private boolean validated;

      private boolean committed;

      private boolean persisted;

      private boolean completed;

      @Validate
      public void validate(Map map) {
         validated = true;                       
      }

      @Commit
      public void commit(Map map) {
         if(validated) {              
            committed = true;              
         }            
      }

      @Persist
      public void persist(Map map) {
         persisted = true;              
      }              

      @Complete
      public void complete(Map map) {
         if(persisted) {              
            completed = true;               
         }            
      }

      public boolean isCommitted() {
         return committed;              
      }

      public boolean isValidated() {
         return validated;              
      }

      public boolean isPersisted() {
         return persisted;              
      }

      public boolean isCompleted() {
         return completed;              
      }

      public int getNumber() {
         return number;              
      }         

      public boolean getFlag() {
         return bool;              
      }

      public String getValue() {
         return value;              
      }
   }
        
   private Persister persister;

   public void setUp() {
      persister = new Persister();
   }
	
   public void testReadCallbacks() throws Exception {    
      Entry entry = persister.read(Entry.class, SOURCE);

      assertEquals("complete", entry.getValue());
      assertEquals(1234, entry.getNumber());
      assertEquals(true, entry.getFlag());      
      assertTrue(entry.isValidated());
      assertTrue(entry.isCommitted());
   }

   public void testWriteCallbacks() throws Exception {
      Entry entry = new Entry();

      assertFalse(entry.isCompleted());
      assertFalse(entry.isPersisted());
      
      persister.write(entry, System.out);

      assertEquals("default", entry.getValue());
      assertEquals(9999, entry.getNumber());
      assertTrue(entry.isPersisted());
      assertTrue(entry.isCompleted());
   }
}
