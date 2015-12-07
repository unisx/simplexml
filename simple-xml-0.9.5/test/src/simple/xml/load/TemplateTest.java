package simple.xml.load;

import java.io.StringReader;

import junit.framework.TestCase;
import simple.xml.Attribute;
import simple.xml.ElementList;
import simple.xml.Element;
import simple.xml.Root;
import java.util.List;
import java.util.Map;

public class TemplateTest extends TestCase {

   private static final String EXAMPLE =
   "<?xml version=\"1.0\"?>\n"+
   "<test name='test'>\n"+
   "   <config>\n"+
   "     <var name='name' value='Niall Gallagher'/>\n"+
   "     <var name='mail' value='niallg@users.sf.net'/>\n"+
   "     <var name='title' value='Mr'/>\n"+
   "   </config>\n"+
   "   <details>  \n\r"+
   "     <title>${title}</title> \n"+
   "     <mail>${mail}</mail> \n"+
   "     <name>${name}</name> \n"+
   "   </details>\n"+
   "</test>";

   @Root(name="var")
   public static class Variable {

      @Attribute(name="name")           
      private String name;           
           
      @Attribute(name="value")
      private String value;           

      @Commit
      public void commit(Map map) {
         map.put(name, value);              
      }
   }

   @Root(name="test")
   public static class Example {

      @Attribute(name="name")
      private String name;

      @ElementList(name="config", type=Variable.class)
      private List list;

      @Element(name="details")
      private Details details;
   }
        
   public static class Details {

      @Element(name="title")
      private String title;

      @Element(name="mail")
      private String mail;

      @Element(name="name")
      private String name;
   }

   
	private Persister serializer;

	public void setUp() {
	   serializer = new Persister();
	}
	
   public void testTemplate() throws Exception {    
      Example example = (Example) serializer.read(Example.class, EXAMPLE);
      
      assertEquals(example.name, "test");
      assertEquals(example.details.title, "Mr");
      assertEquals(example.details.mail, "niallg@users.sf.net");
      assertEquals(example.details.name, "Niall Gallagher");

      serializer.write(example, System.err);
   }
}
