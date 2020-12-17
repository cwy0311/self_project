package com.chatbot.superstitionbot.message;


import java.util.Date;
import java.util.List;

import org.springframework.stereotype.Component;
import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.Message;

import com.chatbot.supersitionbot.cache.Cache;
import com.chatbot.supersitionbot.cache.CacheManager;
import com.chatbot.supersitionbot.crawler.CrawlerManager;
import com.chatbot.superstitionbot.util.TelegramBotUtil;

@Component
public class ZodiacMessageHandler implements MessageHandler{
	private String lastUpdateText="zadiac.lastupdate";
	private String todayZodiacContentText="zadiac.lastestcontent";
	
	
	@Override
	public SendMessage updateMessage(Message message) {
		
		Cache lastUpdate = CacheManager.getContent(lastUpdateText);
		Cache latestContent = CacheManager.getContent(todayZodiacContentText);
		try {
			if (lastUpdate==null || latestContent==null || lastUpdate.getValue()==null || lastUpdate.getValue()==null) {
				updateCacheInfo();
				latestContent=CacheManager.getContent(todayZodiacContentText);
			}
			else if(lastUpdate.isExpired() || TelegramBotUtil.isRequiredDailyUpdate((long)CacheManager.getContent(lastUpdateText).getValue())) {
				System.out.println("Update");
				updateCacheInfo();		
				latestContent=CacheManager.getContent(todayZodiacContentText);
			}
			
			if (latestContent!=null && latestContent.getValue()!=null) {
				return TelegramBotUtil.createSendMessge((String)latestContent.getValue(), message);
			}
		}
		catch (Exception e) {
			System.out.println(e.getStackTrace());
		}
		
		return TelegramBotUtil.createSendMessge("黃道吉日 查詢暫時有問題!", message);
	}
	

	
	private void updateCacheInfo() {
		List<String> zodiacInfo=CrawlerManager.getZodiacInfo();
		if (zodiacInfo!=null && zodiacInfo.size()==3) {
			//update every 2 hours
			CacheManager.putContent(lastUpdateText, new Date().getTime(), 7200);
			CacheManager.putContent(todayZodiacContentText,zodiacInfo.get(0)+"\n"+zodiacInfo.get(1)+"\n"+zodiacInfo.get(2), 7200);
		}
	}
}
