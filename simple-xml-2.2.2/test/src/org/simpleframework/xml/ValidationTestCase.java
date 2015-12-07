package org.simpleframework.xml;

import java.io.File;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.StringReader;
import java.io.StringWriter;
import java.io.Writer;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

import org.custommonkey.xmlunit.XMLTestCase;
import org.simpleframework.xml.core.Persister;
import org.simpleframework.xml.strategy.CycleStrategy;
import org.simpleframework.xml.strategy.Strategy;
import org.simpleframework.xml.stream.CamelCaseStyle;
import org.simpleframework.xml.stream.Format;
import org.simpleframework.xml.stream.HyphenStyle;
import org.simpleframework.xml.stream.Style;
import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;

public class ValidationTestCase extends XMLTestCase {

   private static TransformerFactory transformerFactory;
  
	private static Transformer transformer;   
   
   private static DocumentBuilderFactory builderFactory;

   private static DocumentBuilder builder;
        
   static  {
      try {           
         builderFactory = DocumentBuilderFactory.newInstance();
         builder = builderFactory.newDocumentBuilder();

         transformerFactory = TransformerFactory.newInstance();
         transformer = transformerFactory.newTransformer();
      } catch(Exception cause) {
         cause.printStackTrace();              
      }         
   }

   private static File directory;

   static {
        try {
            String path = System.getProperty("output");                                       
            directory = new File(path);
        } catch(Exception cause){
            directory = new File("output");
        }         
        if(!directory.exists()) {
            directory.mkdirs();                
        }
   }

   public void testDirectory() throws Exception {
      assertTrue(directory.exists());           
   }

   public static synchronized void validate(Serializer out, Object type) throws Exception {
      validate(type, out);
   }
    public static synchronized void validate(Object type, Serializer out) throws Exception {
        File destination = new File(directory, type.getClass().getSimpleName() + ".xml");
        OutputStream file = new FileOutputStream(destination);
        StringWriter buffer = new StringWriter();
        out.write(type, buffer);
        String text = buffer.toString();
        byte[] octets = text.getBytes("UTF-8");
        System.out.write(octets);
        System.out.flush();
        file.write(octets);
        file.close();        
        validate(text);
        
        File domDestination = new File(directory, type.getClass().getSimpleName() + ".dom.xml");
        File asciiDestination = new File(directory, type.getClass().getSimpleName() + ".ascii-dom.xml");
        OutputStream domFile = new FileOutputStream(domDestination);
        OutputStream asciiFile = new FileOutputStream(asciiDestination);
        Writer asciiOut = new OutputStreamWriter(asciiFile, "iso-8859-1");
        Document doc = builder.parse(new InputSource(new StringReader(text)));   
        
        transformer.setOutputProperty(OutputKeys.INDENT, "yes");
        transformer.transform(new DOMSource(doc), new StreamResult(domFile));
        transformer.transform(new DOMSource(doc), new StreamResult(asciiOut));    
        
        domFile.close();      
        asciiFile.close();
        
        out.validate(type.getClass(), text);
       
        File hyphenFile = new File(directory, type.getClass().getSimpleName() + ".hyphen.xml");
        Strategy strategy = new CycleStrategy("ID", "REFERER");
        Style style = new HyphenStyle();
        Format format = new Format(style);
        Persister hyphen = new Persister(strategy, format);
        
        hyphen.write(type, hyphenFile);
        hyphen.write(type, System.out);
        hyphen.read(type.getClass(), hyphenFile);
        
        File camelCaseFile = new File(directory, type.getClass().getSimpleName() + ".camel-case.xml");
        Style camelCaseStyle = new CamelCaseStyle(true, false);
        Format camelCaseFormat = new Format(camelCaseStyle);
        Persister camelCase = new Persister(strategy, camelCaseFormat);
        
        camelCase.write(type, camelCaseFile);
        camelCase.write(type, System.out);
        camelCase.read(type.getClass(), camelCaseFile);
    }
    
    public static synchronized Document parse(String text) throws Exception {
       return builder.parse(new InputSource(new StringReader(text)));   
    }

    public static synchronized void validate(String text) throws Exception {    
        builder.parse(new InputSource(new StringReader(text)));   
        System.out.println(text);
    }
    
    public void assertElementExists(String sourceXml, String pathExpression) throws Exception {
       assertMatch(sourceXml, pathExpression, new MatchAny(), true);
    }
    
    public void assertElementHasValue(String sourceXml, String pathExpression, String value) throws Exception {
       assertMatch(sourceXml, pathExpression, new ElementMatch(value), true);
    }
    
    public void assertElementHasAttribute(String sourceXml, String pathExpression, String name, String value) throws Exception {
       assertMatch(sourceXml, pathExpression, new AttributeMatch(name, value), true);
    }
    
    public void assertElementHasNamespace(String sourceXml, String pathExpression, String reference) throws Exception {
       assertMatch(sourceXml, pathExpression, new NamespaceMatch(reference), true);
    }
    
    public void assertElementDoesNotExist(String sourceXml, String pathExpression) throws Exception {
       assertMatch(sourceXml, pathExpression, new MatchAny(), false);
    }
    
    public void assertElementDoesNotHaveValue(String sourceXml, String pathExpression, String value) throws Exception {
       assertMatch(sourceXml, pathExpression, new ElementMatch(value), false);
    }
    
    public void assertElementDoesNotHaveAttribute(String sourceXml, String pathExpression, String name, String value) throws Exception {
       assertMatch(sourceXml, pathExpression, new AttributeMatch(name, value), false);
    }
    
    public void assertElementDoesNotHaveNamespace(String sourceXml, String pathExpression, String reference) throws Exception {
       assertMatch(sourceXml, pathExpression, new NamespaceMatch(reference), false);
    }
    
    private void assertMatch(String sourceXml, String pathExpression, ExpressionMatch match, boolean assertTrue) throws Exception {
       Document document = parse(sourceXml);
       ExpressionMatcher matcher = new ExpressionMatcher(pathExpression, match);
       if(!assertTrue) {
          assertFalse("Document does have expression '"+pathExpression+"' with "+match.getDescription(), matcher.matches(document));
       } else {
          assertTrue("Document does not match expression '"+pathExpression+"' with "+match.getDescription(), matcher.matches(document));
       }
    }
    
    private static class ExpressionMatcher {
       private Pattern pattern;
       private String[] segments;
       private ExpressionMatch match;
       private String pathExpression;
       public ExpressionMatcher(String pathExpression, ExpressionMatch match) {
          this.segments = pathExpression.replaceAll("^\\/", "").split("\\/");
          this.pattern = Pattern.compile("^(.*)\\[([0-9]+)\\]$");
          this.pathExpression = pathExpression;
          this.match = match;
       }
       public boolean matches(Document document) {
          org.w3c.dom.Element element = document.getDocumentElement();
          if(!getLocalPart(element).equals(segments[0])) {
             return false;
          }
          for(int i = 1; i < segments.length; i++) {
             Matcher matcher = pattern.matcher(segments[i]);
             String path = segments[i];
             int index = 0;
             if(matcher.matches()) {
                String value = matcher.group(2);
                index = Integer.parseInt(value);
                path = matcher.group(1);
             }
             List<org.w3c.dom.Element> list = getElementsByTagName(element, path);
             if(index >= list.size()) {
                return false;
             }
             element = list.get(index);
             if(element == null) {
                return false;
             }
          }
          return match.match(element);
       }
       public String toString() {
          return pathExpression;
       }
    }
    
    public static List<org.w3c.dom.Element> getElementsByTagName(org.w3c.dom.Element element, String name) {
       List<org.w3c.dom.Element> list = new ArrayList<org.w3c.dom.Element>();
       NodeList allElements = element.getElementsByTagName("*");
       for(int i = 0; i < allElements.getLength(); i++) {
          Node node = allElements.item(i);
          if(node instanceof org.w3c.dom.Element && node.getParentNode() == element) {
             org.w3c.dom.Element itemNode = (org.w3c.dom.Element)node;
             String localName = getLocalPart(itemNode);
             if(localName.equals(name)) {
                list.add(itemNode);
             }
          }
       }
       return list;
    }
    
    private static String getLocalPart(org.w3c.dom.Element element) {
       if(element != null) {
          String tagName = element.getTagName();
          if(tagName != null) {
             return tagName.replaceAll(".*:", "");
          }
       }
       return null;
    }
    
    private static String getPrefix(org.w3c.dom.Element element) {
       if(element != null) {
          String tagName = element.getTagName();
          if(tagName != null && tagName.matches(".+:.+")) {
             return tagName.replaceAll(":.*", "");
          }
       }
       return null;  
    }
    
    public static interface ExpressionMatch{
       public boolean match(org.w3c.dom.Element element);
       public String getDescription();
    }
    
    public static class MatchAny implements ExpressionMatch {
       public boolean match(org.w3c.dom.Element element) {
          return element != null;
       }
       public String getDescription(){
          return "path";
       }
    }
    
    public static class ElementMatch implements ExpressionMatch {
       private final String text;
       public ElementMatch(String text){
          this.text = text;
       }
       public boolean match(org.w3c.dom.Element element) {
          if(element != null) {
             Node value = element.getFirstChild();
             return value != null && value.getNodeValue().equals(text);
          }
          return false;
       }
       public String getDescription() {
          return "text value equal to '"+text+"'";
       }
    }
    
    public static class NamespaceMatch implements ExpressionMatch {
       private final String reference;
       public NamespaceMatch(String reference) {
          this.reference = reference;
       }
       public boolean match(org.w3c.dom.Element element) {
          if(element != null) {
             String prefix = getPrefix(element); // a:element -> a
             if(prefix != null && prefix.equals("")) {
                prefix = null;
             }
             return match(element, prefix);
          }
          return false;
       }
       private boolean match(org.w3c.dom.Element element, String prefix) {
          if(element != null) {
             String currentPrefix = getPrefix(element); // if prefix is null, then this is inherited
             if((currentPrefix != null && prefix == null) ) {
                prefix = currentPrefix; // inherit parents
             }
             String name = "xmlns"; // default xmlns=<reference>
             if(prefix != null && !prefix.equals("")) {
                name = name + ":" + prefix; // xmlns:a=<reference>
             }
             String value = element.getAttribute(name);
             if(value == null || value.equals("")) {
                Node parent = element.getParentNode();
                if(parent instanceof org.w3c.dom.Element) {
                   return match((org.w3c.dom.Element)element.getParentNode(), prefix);
                }
             }
             return value != null && value.equals(reference);
          }
          return false;
       }
       public String getDescription(){
          return "namespace reference as '"+reference+"'";
       }
    }
    
    public static class AttributeMatch implements ExpressionMatch {
       private final String name;
       private final String value;
       public AttributeMatch(String name, String value) {
          this.name = name;
          this.value = value;
       }
       public boolean match(org.w3c.dom.Element element) {
          if(element != null) {
             String attribute = element.getAttribute(name);
             return attribute != null && attribute.equals(value);
          }
          return false;
       }
       public String getDescription() {
          return "attribute "+name+"='"+value+"'";
       }
    }
}