package com.chatbot.superstitionbot.bot;


import org.springframework.stereotype.Component;
import org.telegram.telegrambots.ApiContextInitializer;
import org.telegram.telegrambots.bots.TelegramLongPollingBot;
import org.telegram.telegrambots.meta.TelegramBotsApi;
import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.methods.updatingmessages.EditMessageReplyMarkup;
import org.telegram.telegrambots.meta.api.objects.CallbackQuery;
import org.telegram.telegrambots.meta.api.objects.Update;
import org.telegram.telegrambots.meta.exceptions.TelegramApiException;

import com.chatbot.superstitionbot.message.MessageHandlerFactory;
import com.chatbot.superstitionbot.util.PropertyUtil;

@Component
public class SuperstitionBot extends TelegramLongPollingBot {
    private String username;
    
    private String token;
    
    private MessageHandlerFactory messageHandlerFactory;
	
    
    
    
    SuperstitionBot(){
    	messageHandlerFactory=new MessageHandlerFactory();
    	token=PropertyUtil.getProperty("telegram.bot.token");
    	username=PropertyUtil.getProperty("telegram.bot.username");
    }
    
    @Override
    public String getBotUsername() {
    	return username;
    }

    @Override
    public String getBotToken() {
    	return token;
    }

    @Override
    public void onUpdateReceived(Update update) {
    	if (update.hasCallbackQuery()) {
    		CallbackQuery callbackQuery=update.getCallbackQuery();
    		messageHandlerFactory.updateEvent(callbackQuery,this);
    	
    		return;
    	}
    	
    	
        if (update.hasMessage() && update.getMessage().hasText()) {
        	
            try {
	        	SendMessage message=messageHandlerFactory.updateMessage(update.getMessage());
	        	if (message!=null) {
		            execute(message);
	        	}
            } catch (TelegramApiException e) {
                e.printStackTrace();
            }
        }
    }

}