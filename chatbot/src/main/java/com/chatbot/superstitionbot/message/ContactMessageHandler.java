package com.chatbot.superstitionbot.message;

import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.Message;

import com.chatbot.superstitionbot.util.PropertyUtil;
import com.chatbot.superstitionbot.util.TelegramBotUtil;

public class ContactMessageHandler implements MessageHandler{

	@Override
	public SendMessage updateMessage(Message message) {
		return TelegramBotUtil.createSendMessge(PropertyUtil.getProperty("telegram.bot.contact"), message);
	}
}
