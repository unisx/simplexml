package simple.xml.load;

import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.w3c.dom.Document;
import junit.framework.TestCase;
import simple.xml.Serializer;
import simple.xml.Attribute;
import simple.xml.Element;
import simple.xml.Root;
import java.util.Map;

public class VersionTest extends TestCase {

   private static final String VERSION_ATTRIBUTE = "version";        

   private static final String VERSION_1 =
   "<?xml version=\"1.0\"?>\n"+
   "<Example version='1'>\n"+
   "   <text>text value</text>  \n\r"+
   "</Example>";

   private static final String VERSION_2 =
   "<?xml version=\"1.0\"?>\n"+
   "<Example version='2'>\n"+
   "   <name>example name</name>  \n\r"+
   "   <value>text value</value> \n"+
   "   <entry name='example'>\n"+   
   "      <value>text value</value> \n"+   
   "   </entry>\n"+
   "</Example>";
   
   
   public interface Versionable {
      
      public int getVersion();
   }

   @Root(name="Example")
   public static abstract class Example implements Versionable {

      @Attribute(name="version")
      private int version;
      
      public int getVersion() {
         return version;
      }
      
      public abstract String getValue();   
   }
   
   public static class Example1 extends Example {

      @Element(name="text")
      private String text;           

      public String getValue() {
         return text;              
      }
   }

   public static class Example2 extends Example {          

      @Element(name="name")
      private String name;

      @Element(name="value")
      private String value;

      @Element(name="entry")
      private Entry entry;

      public String getValue() {
         return value;              
      }
   }

   public static class Entry {

      @Attribute(name="name")
      private String name;           

      @Element(name="value")
      private String value;              
   }

   public class VersionStrategy implements Strategy {

      private String version;           

      public Class readRoot(Class type, org.w3c.dom.Element root, Map map) throws Exception {
         String version = root.getAttribute(VERSION_ATTRIBUTE);                       

         if(version != null){
            map.put(VERSION_ATTRIBUTE, version);
         }
         return readElement(type, root, map);
      }

      public Class readElement(Class type, org.w3c.dom.Element node, Map map) throws Exception {
         String value = type.getName() + map.get(VERSION_ATTRIBUTE);
     
         try {    
            return Class.forName(value);
         } catch(ClassNotFoundException e) {
            return null;                 
         }            
      }         

      public void writeRoot(Class field, Object value, org.w3c.dom.Element root, Map map) throws Exception {
         Class type = value.getClass();
         
         if(Versionable.class.isAssignableFrom(type)) {
            Versionable versionable = (Versionable)value;
            map.put(VERSION_ATTRIBUTE, String.valueOf(versionable.getVersion()));
         }              
         writeElement(field, value, root, map);
      }              

      public void writeElement(Class field, Object value, org.w3c.dom.Element node, Map map) throws Exception {
         node.setAttribute(VERSION_ATTRIBUTE, (String)map.get(VERSION_ATTRIBUTE));
      }
   }

   public void testVersion1() throws Exception {    
      Strategy strategy = new VersionStrategy();           
      Serializer persister = new Persister(strategy);
      Example example = persister.read(Example.class, VERSION_1);
      
      assertTrue(example instanceof Example1);
      assertEquals(example.getValue(), "text value");
   }

   public void testVersion2() throws Exception {    
      Strategy strategy = new VersionStrategy();           
      Serializer persister = new Persister(strategy);
      Example example = persister.read(Example.class, VERSION_2);      
      
      assertTrue(example instanceof Example2);
      assertEquals(example.getValue(), "text value");
   }
}
