package com.chatbot.superstitionbot.message;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.CallbackQuery;
import org.telegram.telegrambots.meta.api.objects.Message;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.InlineKeyboardMarkup;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.buttons.InlineKeyboardButton;

import com.chatbot.supersitionbot.stick.StickPropertyUtil;
import com.chatbot.superstitionbot.bot.SuperstitionBot;
import com.chatbot.superstitionbot.util.TelegramBotUtil;

public class WongTaiSinStickMessageHandler implements MessageHandler{

	private String order=MessageHandlerFactory.wongtaisin;
	
	@Override
	public SendMessage updateMessage(Message message) {
	      int stickNumber = new Random().nextInt(100)+1;
	      String title=StickPropertyUtil.getWongTaiSinStickTitle(stickNumber);
	      String poem=StickPropertyUtil.getWongTaiSinStickPoem(stickNumber);   
	      SendMessage responseMessage=TelegramBotUtil.createSendMessge(title+"\n\n詩曰：\n"+poem, message);
      	  if (message!=null) {
      		  InlineKeyboardMarkup markupInline = new InlineKeyboardMarkup();
      	       List<List<InlineKeyboardButton>> rowsInline = new ArrayList<>();
                 List<InlineKeyboardButton> rowInline = new ArrayList<>();
                 rowInline.add(TelegramBotUtil.createInlineKeyboardButton("解簽", order+" "+Integer.toString(stickNumber)));
                 rowsInline.add(rowInline);
                 markupInline.setKeyboard(rowsInline);
                 responseMessage.setReplyMarkup(markupInline);

      	}
	      return responseMessage;
	}
	
	
	public void popupDescription(CallbackQuery callbackQuery,SuperstitionBot bot) {
	    String stickNumber = callbackQuery.getData().split(" ")[1];
	    String description=StickPropertyUtil.getWongTaiSinStickDescription(stickNumber);
	    try {
	    	bot.execute(TelegramBotUtil.createAnswerCallbackQuery("第"+stickNumber+"簽解曰:\n"+description, callbackQuery));
	    }
	    catch (Exception e) {
	    	System.out.println(e.getStackTrace());
	    }
	}
}