package simple.xml.load;

import java.io.StringReader;
import java.util.HashMap;
import java.util.Collection;

import junit.framework.TestCase;
import simple.xml.ElementList;
import simple.xml.Attribute;
import simple.xml.Element;
import simple.xml.Root;

public class PerformanceTest extends TestCase {

   private static final int ITERATIONS = 5000; 

   private static final int MAXIMUM = 60000;   
        
   private static final String ENTRY =
   "<?xml version=\"1.0\"?>\n"+
   "<root number='1234' flag='true'>\n"+
   "   <name>${example.name}</name>  \n\r"+
   "   <path>${example.path}</path>\n"+
   "   <constant>${no.override}</constant>\n"+
   "   <text>\n"+
   "     Some example text where ${example.name} is replaced"+
   "     with the system property value and the path is "+
   "     replaced with the path ${example.path}"+
   "   </text>\n"+
   "   <list class='java.util.ArrayList'>\n"+
   "     <entry key='name.1'>\n"+
   "        <value>value.1</value>\n"+
   "     </entry>\n"+
   "     <entry key='name.2'>\n"+
   "        <value>value.2</value>\n"+
   "     </entry>\n"+
   "     <entry key='name.3'>\n"+
   "        <value>value.4</value>\n"+
   "     </entry>\n"+
   "     <entry key='name.4'>\n"+
   "        <value>value.4</value>\n"+
   "     </entry>\n"+
   "     <entry key='name.5'>\n"+
   "        <value>value.5</value>\n"+
   "     </entry>\n"+
   "  </list>\n"+ 
   "</root>";
   
   @Root(name="root")
   public static class RootEntry {

      @Attribute(name="number")
      private int number;     

      @Attribute(name="flag")
      private boolean bool;
      
      @Element(name="constant")
      private String constant;

      @Element(name="name")
      private String name;

      @Element(name="path")
      private String path;

      @Element(name="text")
      private String text;

      @ElementList(name="list", type=ElementEntry.class)
      private Collection list;              
   }

   @Root(name="entry")
   public static class ElementEntry {

      @Attribute(name="key")
      private String name;

      @Element(name="value")
      private String value;                 
   }

   static {
      System.setProperty("example.name", "some name");
      System.setProperty("example.path", "/some/path");
      System.setProperty("no.override", "some constant");
   } 
        
   private Persister systemSerializer;

   public void setUp() {
      systemSerializer = new Persister();
   }
	
   public void testSystem() throws Exception {    
      long start = System.currentTimeMillis();

      for(int i = 0; i < ITERATIONS; i++) {
         systemSerializer.read(RootEntry.class, new StringReader(ENTRY));
      }
      long duration = System.currentTimeMillis() - start;
      
      assertTrue(duration < MAXIMUM);
   }
}
