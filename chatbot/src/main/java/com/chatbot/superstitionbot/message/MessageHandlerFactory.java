package com.chatbot.superstitionbot.message;

import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.CallbackQuery;
import org.telegram.telegrambots.meta.api.objects.Message;

import com.chatbot.superstitionbot.bot.SuperstitionBot;
import com.chatbot.superstitionbot.util.PropertyUtil;

public class MessageHandlerFactory {
	public static final String help="/help";
	public static final String starsign="/starsign";
	public static final String zodiac="/zodiac";
	public static final String wongtaisin="/wongtaisin";
	public static final String emperorguan="/emperorguan";
	public static final String chinesecupid="/chinesecupid";
	public static final String guanyin="/guanyin";
	public static final String contact="/contact";
	
	
	public SendMessage updateMessage(Message message) {

		
		
		String[] split=message.getText().toLowerCase().split(" ");
		if (split.length>0 && split[0].startsWith("/")) {
			switch(split[0]) {
				case(help):
					return new HelpMessageHandler().updateMessage(message);
				case(starsign):
					return new StarSignMessageHandler().updateMessage(message);
				case(zodiac):
					return new ZodiacMessageHandler().updateMessage(message);
				case(wongtaisin):
					return new WongTaiSinStickMessageHandler().updateMessage(message);
				case(emperorguan):
					return new EmperorguanStickMessageHandler().updateMessage(message);
				case(chinesecupid):
					return new ChinesecupidStickMessageHandler().updateMessage(message);
				case(guanyin):
					return new GuanyinStickMessageHandler().updateMessage(message);
				case(contact):
					return new ContactMessageHandler().updateMessage(message);
			}
			
			
		}
		return null;
	}
	
	public void updateEvent(CallbackQuery callbackQuery,SuperstitionBot bot) {
		String[] split=callbackQuery.getData().toLowerCase().split(" ");
		if (split.length>0 && split[0].startsWith("/")) {
			switch(split[0]) {
				case(wongtaisin):
					new WongTaiSinStickMessageHandler().popupDescription(callbackQuery,bot);
				case(emperorguan):
					new EmperorguanStickMessageHandler().popupDescription(callbackQuery,bot);
				case(chinesecupid):
					new ChinesecupidStickMessageHandler().popupDescription(callbackQuery,bot);
				case(guanyin):
					new GuanyinStickMessageHandler().popupDescription(callbackQuery,bot);
				case(starsign):
					new StarSignMessageHandler().showStarSignDetail(callbackQuery,bot);
				
			}
			
			
		}
	}

}
