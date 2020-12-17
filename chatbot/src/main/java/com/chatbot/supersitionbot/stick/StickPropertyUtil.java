package com.chatbot.supersitionbot.stick;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class StickPropertyUtil {

	private static Properties emperorguan;
	private static Properties wongtaisin;
	private static Properties chinesecupid;
	private static Properties guanyin;

	static {

		loadAllProps();

	}

	synchronized static private void loadAllProps() {

		emperorguan = new Properties();
		wongtaisin = new Properties();
		chinesecupid= new Properties();
		guanyin= new Properties();
		
		InputStream emperorguanIn = null;
		InputStream wongtaisinIn = null;
		InputStream chinesecupidIn = null;
		InputStream guanyinIn = null;

		try {

			emperorguanIn = StickPropertyUtil.class.getClassLoader().getResourceAsStream("emperorguanstick.properties");
			emperorguan.load(emperorguanIn);
			
			wongtaisinIn = StickPropertyUtil.class.getClassLoader().getResourceAsStream("wongtaisinstick.properties");
			wongtaisin.load(wongtaisinIn);
			
			chinesecupidIn = StickPropertyUtil.class.getClassLoader().getResourceAsStream("chinesecupidstick.properties");
			chinesecupid.load(chinesecupidIn);
			
			guanyinIn = StickPropertyUtil.class.getClassLoader().getResourceAsStream("guanyinstick.properties");
			guanyin.load(guanyinIn);


		} catch (FileNotFoundException e) {

		} catch (IOException e) {


		} finally {

			try {

				if (null != chinesecupidIn) {
					chinesecupidIn.close();
				}
				if (null != wongtaisinIn) {
					wongtaisinIn.close();
				}
				if (null != emperorguanIn) {
					emperorguanIn.close();
				}
				if (null != guanyinIn) {
					guanyinIn.close();
				}

			} catch (IOException e) {

			}

		}

	}

	public static String getWongTaiSinStickDescription(String stickNum) {
		if (null == wongtaisin) {
			loadAllProps();
		}		
		return wongtaisin.getProperty("wongtaisinstick.description."+stickNum);
	}
	
	public static String getWongTaiSinStickTitle(int stickNum) {
		if (null == wongtaisin) {
			loadAllProps();
		}		
		return wongtaisin.getProperty("wongtaisinstick.title."+Integer.toString(stickNum));
	}
	
	public static String getWongTaiSinStickPoem(int stickNum) {
		if (null == wongtaisin) {
			loadAllProps();
		}		
		return wongtaisin.getProperty("wongtaisinstick.poem."+Integer.toString(stickNum));
	}
	
	
	
	public static String getEmperorguanStickDescription(String stickNum) {
		if (null == emperorguan) {
			loadAllProps();
		}		
		return emperorguan.getProperty("emperorguan.description2."+stickNum);
	}
	
	public static String getEmperorguanStickTitle(int stickNum) {
		if (null == emperorguan) {
			loadAllProps();
		}		
		return emperorguan.getProperty("emperorguan.title."+Integer.toString(stickNum));
	}
	
	public static String getEmperorguanStickPoem(int stickNum) {
		if (null == emperorguan) {
			loadAllProps();
		}		
		return emperorguan.getProperty("emperorguan.poem."+Integer.toString(stickNum));
	}
	
	public static String getEmperorguanStickParaphrase(String stickNum) {
		if (null == emperorguan) {
			loadAllProps();
		}		
		return emperorguan.getProperty("emperorguan.description1."+stickNum);		
	}
	
	
	public static String getChinesecupidStickDescription(String stickNum) {
		if (null == chinesecupid) {
			loadAllProps();
		}		
		return chinesecupid.getProperty("chinesecupidcontent.description."+stickNum);
	}
	
	public static String getChinesecupidStickResult(int stickNum) {
		if (null == chinesecupid) {
			loadAllProps();
		}		
		return chinesecupid.getProperty("chinesecupidcontent.result."+Integer.toString(stickNum));
	}
	
	public static String getChinesecupidStickPoem(int stickNum) {
		if (null == chinesecupid) {
			loadAllProps();
		}		
		return chinesecupid.getProperty("chinesecupidcontent.poem."+Integer.toString(stickNum));
	}
	

	public static String getGuanyinStickDescription(String stickNum) {
		if (null == guanyin) {
			loadAllProps();
		}		
		return guanyin.getProperty("guanyin.content.description."+stickNum);
	}
	
	public static String getGuanyinStickResult(int stickNum) {
		if (null == guanyin) {
			loadAllProps();
		}		
		return guanyin.getProperty("guanyin.content.result."+Integer.toString(stickNum));
	}
	
	public static String getGuanyinStickPoem(int stickNum) {
		if (null == guanyin) {
			loadAllProps();
		}		
		return guanyin.getProperty("guanyin.content.poem."+Integer.toString(stickNum));
	}
	
		
	
	
	
}
