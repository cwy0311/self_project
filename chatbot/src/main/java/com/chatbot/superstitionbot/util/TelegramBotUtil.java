package com.chatbot.superstitionbot.util;

import java.util.Calendar;
import java.util.Date;

import org.telegram.telegrambots.meta.api.methods.AnswerCallbackQuery;
import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.CallbackQuery;
import org.telegram.telegrambots.meta.api.objects.Message;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.buttons.InlineKeyboardButton;


public class TelegramBotUtil {
			
			
	public static InlineKeyboardButton createInlineKeyboardButton(String text,String callbackData) {
        InlineKeyboardButton inlineKeyboardButton=new InlineKeyboardButton();
        inlineKeyboardButton.setText(text);
        inlineKeyboardButton.setCallbackData(callbackData);
        return inlineKeyboardButton;
	}
	
	public static SendMessage createSendMessge(String text,Message message) {
		return new SendMessage().setChatId(message.getChatId()).setText(text);
	}
	
	public static AnswerCallbackQuery createAnswerCallbackQuery(String text,CallbackQuery callbackQuery) {
  		AnswerCallbackQuery answerCallbackQuery=new AnswerCallbackQuery();
  		answerCallbackQuery.setText(text);
  		answerCallbackQuery.setShowAlert(true);
  		answerCallbackQuery.setCallbackQueryId(callbackQuery.getId());
  		return answerCallbackQuery;
	}
	
	public static boolean isRequiredDailyUpdate(long time) {
		Calendar calendar=Calendar.getInstance();
		Date lastUpdateTime=new Date(time);
		calendar.setTime(lastUpdateTime);
		calendar.set(Calendar.HOUR_OF_DAY, 0);
		calendar.set(Calendar.MINUTE, 0);
		calendar.set(Calendar.SECOND, 0);
		calendar.set(Calendar.MILLISECOND, 0);
		calendar.add(Calendar.DATE,1);
		return calendar.getTime().before(new Date());
	}
}
