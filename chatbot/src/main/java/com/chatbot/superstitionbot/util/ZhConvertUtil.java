package com.chatbot.superstitionbot.util;


import com.spreada.utils.chinese.ZHConverter;


public class ZhConvertUtil {
	public static String Simplified2TraditionalConvert(String text) {
			ZHConverter converter = ZHConverter.getInstance(ZHConverter.TRADITIONAL);
			return converter.convert(text);
		
		//http://api.zhconvert.org/convert
		
	}
}
