package com.chatbot.superstitionbot.message;

import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.Message;

import com.chatbot.superstitionbot.util.TelegramBotUtil;

public class HelpMessageHandler implements MessageHandler{

	@Override
	public SendMessage updateMessage(Message message) {
		String helpMessage=MessageHandlerFactory.help+" 幫助\n"
				+MessageHandlerFactory.contact+ " 提供意見\n"
				+MessageHandlerFactory.emperorguan+ " 關公求籤\n"
				+MessageHandlerFactory.chinesecupid+ " 月老求籤\n"
				+MessageHandlerFactory.guanyin+ " 觀音求籤\n"
				+MessageHandlerFactory.wongtaisin+ " 黃大仙求籤\n"
				+MessageHandlerFactory.zodiac+ " 黃道吉日\n"
				+MessageHandlerFactory.starsign+ " 每日星座運勢";
	
		return TelegramBotUtil.createSendMessge(helpMessage, message);
	}
}
