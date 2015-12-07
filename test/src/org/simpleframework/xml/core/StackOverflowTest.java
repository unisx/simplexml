package org.simpleframework.xml.core;

import java.util.ArrayList;
import java.util.List;

import junit.framework.TestCase;

import org.simpleframework.xml.Element;
import org.simpleframework.xml.ElementList;
import org.simpleframework.xml.Root;

public class StackOverflowTest extends TestCase {
   
   private static final String NEW_BENEFIT = 
   "<newBenefit>"+
   "   <office>AAAAA</office>"+
   "   <recordNumber>1046</recordNumber>"+
   "   <type>A</type>"+
   "</newBenefit>";
   
   private static final String BENEFIT_MUTATION = 
   "<benefitMutation>"+
   "   <office>AAAAA</office>"+
   "   <recordNumber>1046</recordNumber>"+
   "   <type>A</type>"+
   "   <comment>comment</comment>"+
   "</benefitMutation>";

   @Root
   public static class Delivery {

      @ElementList(inline = true, required = false, name = "newBenefit")
      private List<NewBenefit> listNewBenefit = new ArrayList<NewBenefit>();

      @ElementList(inline = true, required = false, name = "benefitMutation")
      private List<BenefitMutation> listBenefitMutation = new ArrayList<BenefitMutation>();

   }

   public static class NewBenefit {

      @Element
      private String office;

      @Element
      private String recordNumber;

      @Element
      private String type;
   }

   public static class BenefitMutation extends NewBenefit {

      @Element(required = false)
      private String comment;
   }
   
   public void testStackOverflow() throws Exception {
      StringBuilder builder = new StringBuilder();
      Persister persister = new Persister();
      builder.append("<delivery>");
      
      for(int i = 0; i < 50000; i++) {
         double value = (Math.random() * 10.0);
         String text = null;
         
         if(value % 2 == 0) {
            text = NEW_BENEFIT;
         } else {
            text = BENEFIT_MUTATION;
         }
         builder.append(text);
      }
      builder.append("</delivery>");
      
      Delivery delivery = persister.read(Delivery.class, builder.toString());
      
      assertEquals(delivery.listBenefitMutation.size() + delivery.listNewBenefit.size(), 50000);
   }
   

}
