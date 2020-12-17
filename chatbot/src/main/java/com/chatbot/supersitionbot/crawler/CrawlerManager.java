package com.chatbot.supersitionbot.crawler;
import java.util.ArrayList;
import java.util.List;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import com.chatbot.superstitionbot.util.HttpUtils;
import com.chatbot.superstitionbot.util.PropertyUtil;


public class CrawlerManager {
	
	private static String zodiacUrl=PropertyUtil.getProperty("telegram.zodiac.url");
	private static String starsignUrl=PropertyUtil.getProperty("telegram.starsign.url");
	
    private CrawlerManager() {
        super();
    }
	
	public static List<String> getZodiacInfo() {
		List<String> info=new ArrayList<>();
				
		try {
			Document document = Jsoup.connect(zodiacUrl).timeout(10000).get(); 
			Element element = document.getElementById("qna");
			Elements content = element.select("li");
			if (content.size()>=11) {
				info.add(content.get(0).text());
				info.add(content.get(9).text());
				info.add(content.get(10).text());
			}
		}
		catch (Exception e) {
			System.out.println(e.getStackTrace());
		}
		return info;
	}
	
	public static List<String> getStarSignInfo(int index){
		List<String> info=new ArrayList<>();
		if (index<0 || index>=12) return info;
		try {
			HttpUtils.trustAllHttpsCertificates();
			Document document = Jsoup.connect(starsignUrl+Integer.toString(index)).timeout(10000).get(); 
			info.add(document.getElementById("iAcDay").select("select option[selected]").text());
			Elements elements = document.getElementsByClass("TODAY_CONTENT");
			if (elements.size()>0) {
				Elements contents = elements.select("p");
				if (contents.size()>=8) {
					info.add(contents.get(1).text());
					info.add(contents.get(3).text());
					info.add(contents.get(5).text());
					info.add(contents.get(7).text());
				}
			}			
		}
		catch (Exception e) {
			System.out.println(e.getStackTrace());
		}
		return info;		
		
	}
}



