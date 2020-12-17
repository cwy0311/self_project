package com.chatbot.superstitionbot.bot;


import org.telegram.telegrambots.ApiContextInitializer;
import org.telegram.telegrambots.meta.TelegramBotsApi;
import org.telegram.telegrambots.meta.exceptions.TelegramApiException;

public class Main{
    public static void main(String[] args) {
    	System.out.println("main start");
        ApiContextInitializer.init();
        //Telegram API
        TelegramBotsApi botsApi = new TelegramBotsApi();
        try {
            // register
            botsApi.registerBot(new SuperstitionBot());
        } catch (TelegramApiException e) {
            e.printStackTrace();
        }
    }
}