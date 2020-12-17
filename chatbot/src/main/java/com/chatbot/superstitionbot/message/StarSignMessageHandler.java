package com.chatbot.superstitionbot.message;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;

import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.methods.updatingmessages.EditMessageReplyMarkup;
import org.telegram.telegrambots.meta.api.methods.updatingmessages.EditMessageText;
import org.telegram.telegrambots.meta.api.objects.CallbackQuery;
import org.telegram.telegrambots.meta.api.objects.Message;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.InlineKeyboardMarkup;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.buttons.InlineKeyboardButton;

import com.chatbot.supersitionbot.cache.Cache;
import com.chatbot.supersitionbot.cache.CacheManager;
import com.chatbot.supersitionbot.crawler.CrawlerManager;
import com.chatbot.superstitionbot.bot.SuperstitionBot;
import com.chatbot.superstitionbot.util.TelegramBotUtil;

public class StarSignMessageHandler implements MessageHandler{
	private String keyboardOrder=MessageHandlerFactory.starsign;
	
	private String lastUpdateText="starsign.lastupdate";
	private String starSignTimeText="starsign.content.time";
	private String starSignGeneralContentText="starsign.content.general.";
	private String starSignLoveContentText="starsign.content.love.";
	private String starSignCareerContentText="starsign.content.career.";
	private String starSignFortuneContentText="starsign.content.fortune.";
	
	
	@Override
	public SendMessage updateMessage(Message message) {
		SendMessage responseMessage =TelegramBotUtil.createSendMessge("選擇你的星座", message);
		
		  InlineKeyboardMarkup markupInline = new InlineKeyboardMarkup();
	      List<List<InlineKeyboardButton>> rowsInline = new ArrayList<>();
          rowsInline.add(Arrays.asList(new InlineKeyboardButton[]{
        		  TelegramBotUtil.createInlineKeyboardButton("金牛", keyboardOrder+" "+Integer.toString(1)),
        		  TelegramBotUtil.createInlineKeyboardButton("雙子", keyboardOrder+" "+Integer.toString(2)),
        		  TelegramBotUtil.createInlineKeyboardButton("巨蟹", keyboardOrder+" "+Integer.toString(3)),
        		  TelegramBotUtil.createInlineKeyboardButton("獅子", keyboardOrder+" "+Integer.toString(4)),
          }));
          rowsInline.add(Arrays.asList(new InlineKeyboardButton[]{
        		  TelegramBotUtil.createInlineKeyboardButton("處女", keyboardOrder+" "+Integer.toString(5)),
        		  TelegramBotUtil.createInlineKeyboardButton("天秤", keyboardOrder+" "+Integer.toString(6)),
        		  TelegramBotUtil.createInlineKeyboardButton("天蠍", keyboardOrder+" "+Integer.toString(7)),
        		  TelegramBotUtil.createInlineKeyboardButton("射手", keyboardOrder+" "+Integer.toString(8)),
          }));
          rowsInline.add(Arrays.asList(new InlineKeyboardButton[]{
        		  TelegramBotUtil.createInlineKeyboardButton("摩羯", keyboardOrder+" "+Integer.toString(9)),
        		  TelegramBotUtil.createInlineKeyboardButton("水瓶", keyboardOrder+" "+Integer.toString(10)),
        		  TelegramBotUtil.createInlineKeyboardButton("雙魚", keyboardOrder+" "+Integer.toString(11)),
        		  TelegramBotUtil.createInlineKeyboardButton("牡羊", keyboardOrder+" "+Integer.toString(0)),
          }));

          markupInline.setKeyboard(rowsInline);
          responseMessage.setReplyMarkup(markupInline);		
		
		
		return responseMessage;
	}
	
	public void showStarSignDetail(CallbackQuery callbackQuery,SuperstitionBot bot) {
	    try {
	    	String[] data=callbackQuery.getData().split(" ");
	    	String starSignNumber=data[1];

	    	int starSignDetailCategory = data.length>=3? Integer.parseInt(data[2]):1;
	    	Cache lastUpdate = CacheManager.getContent(lastUpdateText);
	    	if (lastUpdate==null || lastUpdate.getValue()==null || lastUpdate.isExpired() || TelegramBotUtil.isRequiredDailyUpdate((long)CacheManager.getContent(lastUpdateText).getValue())){
	    		updateStarSignCacheInfo();
	    	}
	    	Cache starSignTime = CacheManager.getContent(starSignTimeText);
	    	String detailStart=starSignTime.getValue().toString()+"\n";
	    	String detail = detailStart+
	    					(starSignDetailCategory==1?CacheManager.getContent(starSignGeneralContentText+starSignNumber).getValue().toString():
	    					starSignDetailCategory==2?CacheManager.getContent(starSignLoveContentText+starSignNumber).getValue().toString():
	    					starSignDetailCategory==3?CacheManager.getContent(starSignCareerContentText+starSignNumber).getValue().toString():
	    					CacheManager.getContent(starSignFortuneContentText+starSignNumber).getValue().toString());
	    	//Cache general = CacheManager.getContent(starSignGeneralContentText+starSignNumber);
	    	//Cache love = CacheManager.getContent(starSignLoveContentText+starSignNumber);
	    	//Cache career = CacheManager.getContent(starSignCareerContentText+starSignNumber);
	    	//Cache fortune = CacheManager.getContent(starSignFortuneContentText+starSignNumber);
	    	
			EditMessageText editMessage=new EditMessageText();
			editMessage.setMessageId(callbackQuery.getMessage().getMessageId());
			editMessage.setChatId(callbackQuery.getMessage().getChatId());
			editMessage.setText(detail);
			InlineKeyboardMarkup markupInline = new InlineKeyboardMarkup();
		    List<List<InlineKeyboardButton>> rowsInline = new ArrayList<>();
	        rowsInline.add(Arrays.asList(new InlineKeyboardButton[]{
	        	TelegramBotUtil.createInlineKeyboardButton("整體", keyboardOrder+" "+starSignNumber+" "+Integer.toString(1)),
	        	TelegramBotUtil.createInlineKeyboardButton("愛情", keyboardOrder+" "+starSignNumber+" "+Integer.toString(2)),
	        	TelegramBotUtil.createInlineKeyboardButton("事業", keyboardOrder+" "+starSignNumber+" "+Integer.toString(3)),
	        	TelegramBotUtil.createInlineKeyboardButton("財運", keyboardOrder+" "+starSignNumber+" "+Integer.toString(4)),
	        }));	    
	        markupInline.setKeyboard(rowsInline);
	        editMessage.setReplyMarkup(markupInline);
	    	bot.execute(editMessage);
	    }
	    catch (Exception e) {
	    	System.out.println(e.getStackTrace());
	    }
	}
	
	private void updateStarSignCacheInfo() {
		for (int starSignIndex=0;starSignIndex<12;starSignIndex++) {
			List<String> starSignInfo=CrawlerManager.getStarSignInfo(starSignIndex);
			if (starSignInfo!=null && starSignInfo.size()==5) {
				//update every 6 hours
				CacheManager.putContent(starSignTimeText, starSignInfo.get(0),21600);
				CacheManager.putContent(starSignGeneralContentText+Integer.toString(starSignIndex), starSignInfo.get(1),21600);
				CacheManager.putContent(starSignLoveContentText+Integer.toString(starSignIndex), starSignInfo.get(2),21600);
				CacheManager.putContent(starSignCareerContentText+Integer.toString(starSignIndex), starSignInfo.get(3),21600);
				CacheManager.putContent(starSignFortuneContentText+Integer.toString(starSignIndex), starSignInfo.get(4),21600);
			}
		}
		CacheManager.putContent(lastUpdateText, new Date().getTime(), 21600);
	}
}
