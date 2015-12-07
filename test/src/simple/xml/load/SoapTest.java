package simple.xml.load;

import java.io.StringWriter;

import junit.framework.TestCase;
import simple.xml.Element;
import simple.xml.Attribute;
import simple.xml.Root;

public class SoapTest extends TestCase {
        
   // example from http://www.xml.com/pub/a/2000/02/09/feature/index.html       
   private static final String BALANCE = 
   "<?xml version='1.0'?>\n" + 
   "<SOAP:Envelope xmlns:SOAP='urn:schemas-xmlsoap-org:soap.v1'>\n"+
   "  <SOAP:Body>\n"+
   "     <i:getBalance xmlns:i='urn:develop-com:IBank'>\n"+
   "        <account>23619-22A</account>\n"+
   "     </i:getBalance>\n"+
   "  </SOAP:Body>\n"+
   "</SOAP:Envelope>\n";

   // example from http://www.w3schools.com/soap/soap_example.asp
   private static final String STOCK = 
   "<?xml version='1.0'?>\n"+
   "<soap:Envelope xmlns:soap='http://www.w3.org/2001/12/soap-envelope' soap:encodingStyle='http://www.w3.org/2001/12/soap-encoding'>"+
   "  <soap:Body xmlns:m='http://www.example.org/stock'>\n"+
   "     <m:getStockPriceResponse>\n"+
   "        <m:Price>34.5</m:Price>\n"+
   "     </m:getStockPriceResponse>\n"+
   "  </soap:Body>\n"+
   "</soap:Envelope>\n";

   @Root(name="soap:envelope")
   public static class Envelope {

      @Attribute(name="xmlns:soap")           
      protected String xmlns;       

      @Attribute(name="soap:encodingStyle", required=false)
      protected String encodingStyle;              
   }

   @Root(name="soap:body")
   public static class Body {
   
      @Attribute(name="xmlns:m", required=false)
      protected String xmlns;
   }


   public static class BalanceEnvelope extends Envelope {
           
      @Element(name="soap:body")
      private BalanceBody body;             
   }
   
   public static class BalanceBody extends Body {

      @Element(name="i:getBalance")           
      private Balance balance;           
   }

   @Root(name="i:getBalance")
   public static class Balance {

      @Attribute(name="xmlns:i")           
      private String xmlns;   

      @Element(name="account")
      private String account;      
   }

   public static class StockPriceEnvelope extends Envelope {
           
      @Element(name="soap:body")
      private StockPriceBody body;             
   }

   public static class StockPriceBody extends Body {

      @Element(name="m:getStockPriceResponse")
      private StockPrice stockPrice;                
   }

   @Root(name="m:getStockPriceResponse")
   public static class StockPrice {

      @Element(name="m:Price")           
      private String price;           
   }

   private Persister persister;

   public void setUp() throws Exception {
      persister = new Persister();
   }
	
   public void testBalanceEnvelope() throws Exception {    
      BalanceEnvelope envelope = persister.read(BalanceEnvelope.class, BALANCE);           

      assertEquals(envelope.xmlns, "urn:schemas-xmlsoap-org:soap.v1");
      assertEquals(envelope.body.balance.xmlns, "urn:develop-com:IBank");
      assertEquals(envelope.body.balance.account, "23619-22A");

      persister.write(envelope, System.err);
   }

   public void testStockPriceEnvelope() throws Exception {    
      StockPriceEnvelope envelope = persister.read(StockPriceEnvelope.class, STOCK);           

      assertEquals(envelope.xmlns, "http://www.w3.org/2001/12/soap-envelope");
      assertEquals(envelope.encodingStyle, "http://www.w3.org/2001/12/soap-encoding");
      assertEquals(envelope.body.xmlns, "http://www.example.org/stock");
      assertEquals(envelope.body.stockPrice.price, "34.5");

      persister.write(envelope, System.err);
   }

   
}
