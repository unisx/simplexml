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

public class StrategyTest extends TestCase {

   private static final String ATTRIBUTE_NAME = "example-attribute";        

   private static final String ELEMENT_NAME = "example-element";

   private static final String ATTRIBUTE =
   "<?xml version=\"1.0\"?>\n"+
   "<root key='attribute-example-key' example-attribute='simple.xml.load.StrategyTest$AttributeExample'>\n"+
   "   <text>attribute-example-text</text>  \n\r"+
   "</root>";

   private static final String ELEMENT =
   "<?xml version=\"1.0\"?>\n"+
   "<root version='1.2'>\n"+
   "   <example-element>simple.xml.load.StrategyTest$ElementExample</example-element>\n"+
   "   <name>element-example-name</name>  \n\r"+
   "   <value>element-example-value</value> \n"+
   "</root>";

   @Root(name="root")
   public static abstract class Example {

      public abstract String getValue();   
      
      public abstract String getKey();
   }
   
   public static class AttributeExample extends Example {

      @Attribute(name="key")           
      public String key;           
           
      @Element(name="text")
      public String text;           

      public String getValue() {
         return text;              
      }
      
      public String getKey() {
         return key;
      }
   }

   
   public static class ElementExample extends Example {

      @Attribute(name="version")
      public float version;           

      @Element(name="name")
      public String name;

      @Element(name="value")
      public String value;

      public String getValue() {
         return value;              
      }
      
      public String getKey() {
         return name;
      }
   }

   public class ElementStrategy implements Strategy {

      public int writeRootCount = 0;           

      public int readRootCount = 0;

      private StrategyTest test;

      public ElementStrategy(StrategyTest test){
         this.test = test;              
      }

      public Class readRoot(Class type, org.w3c.dom.Element root, Map map) throws Exception {
         readRootCount++;              
         return readElement(type, root, map);              
      }

      public Class readElement(Class type, org.w3c.dom.Element node, Map map) throws Exception {
         NodeList list = node.getElementsByTagName(ELEMENT_NAME);              
         Document doc = node.getOwnerDocument();
         
         if(readRootCount != 1) {
            test.assertTrue("Root must only be read once", false);                 
         }
         for(int i = 0; i < list.getLength(); i++) {
            Node next = list.item(i);            
            
            if(next instanceof org.w3c.dom.Element) {
               Node child = next.getFirstChild();
               String value = child.getNodeValue();
               
               node.removeChild(next);
               return Class.forName(value);
            }
         }
         return null;
      }         

      public void writeRoot(Class field, Object value, org.w3c.dom.Element root, Map map) throws Exception {
         writeRootCount++;              
         writeElement(field, value, root, map);              
      }              

      public void writeElement(Class field, Object value, org.w3c.dom.Element node, Map map) throws Exception {
         if(writeRootCount != 1) {
            test.assertTrue("Root must be written only once", false);                 
         }              
         if(field != value.getClass()) {              
            Document doc = node.getOwnerDocument();              
            org.w3c.dom.Element child = doc.createElement(ELEMENT_NAME);         
         
            child.setTextContent(value.getClass().getName());
            node.appendChild(child);
         }            
      }
   }                  

   public class AttributeStrategy implements Strategy {

      public int writeRootCount = 0;           

      public int readRootCount = 0;

      private StrategyTest test;

      public AttributeStrategy(StrategyTest test){
         this.test = test;              
      }

      public Class readRoot(Class type, org.w3c.dom.Element root, Map map) throws Exception {
         readRootCount++;
         return readElement(type, root, map);              
      }

      public Class readElement(Class type, org.w3c.dom.Element node, Map map) throws Exception {
         String value = node.getAttribute(ATTRIBUTE_NAME);

         if(readRootCount != 1) {
            test.assertTrue("Root must only be read once", false);                 
         }         
         if(value != null) {
            node.removeAttribute(ATTRIBUTE_NAME);
            return Class.forName(value);
         }
         return null;
      }         

      public void writeRoot(Class field, Object value, org.w3c.dom.Element root, Map map) throws Exception {                       
         writeRootCount++;              
         writeElement(field, value, root, map);              
      }              

      public void writeElement(Class field, Object value, org.w3c.dom.Element node, Map map) throws Exception {
         if(writeRootCount != 1) {
            test.assertTrue("Root must be written only once", false);                 
         }                 
         if(field != value.getClass()) {                       
            node.setAttribute(ATTRIBUTE_NAME, value.getClass().getName());
         }            
      }
   }

   public void testAttributeStrategy() throws Exception {    
      AttributeStrategy strategy = new AttributeStrategy(this);           
      Serializer persister = new Persister(strategy);
      Example example = persister.read(Example.class, ATTRIBUTE);
      
      assertTrue(example instanceof AttributeExample);
      assertEquals(example.getValue(), "attribute-example-text");
      assertEquals(example.getKey(), "attribute-example-key");
      assertEquals(1, strategy.readRootCount);
      assertEquals(0, strategy.writeRootCount);
      
      persister.write(example, System.err);
      
      assertEquals(1, strategy.readRootCount);
      assertEquals(1, strategy.writeRootCount);
   }

   public void testElementStrategy() throws Exception {    
      ElementStrategy strategy = new ElementStrategy(this);           
      Serializer persister = new Persister(strategy);
      Example example = persister.read(Example.class, ELEMENT);      
      
      assertTrue(example instanceof ElementExample);
      assertEquals(example.getValue(), "element-example-value");
      assertEquals(example.getKey(), "element-example-name");     
      assertEquals(1, strategy.readRootCount);
      assertEquals(0, strategy.writeRootCount);
      
      persister.write(example, System.err);
      
      assertEquals(1, strategy.readRootCount);
      assertEquals(1, strategy.writeRootCount);
   }
}
