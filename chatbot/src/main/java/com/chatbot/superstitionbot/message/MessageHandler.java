package com.chatbot.superstitionbot.message;

import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.Message;

public interface MessageHandler {
	SendMessage updateMessage(Message message);
}
