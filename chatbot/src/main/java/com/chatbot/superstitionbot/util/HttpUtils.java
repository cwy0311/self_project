package com.chatbot.superstitionbot.util;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Map;
import javax.net.ssl.HostnameVerifier;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLSession;


public class HttpUtils {
	public static String sendPostHttps(String postUrl,Map<String,Object> parameters) {
		String temp="";
		try{
			trustAllHttpsCertificates();
			
	        HostnameVerifier hv = new HostnameVerifier() {
	            public boolean verify(String urlHostName, SSLSession session) {
	                return true;
	            }
	        };
	        HttpsURLConnection.setDefaultHostnameVerifier(hv);
	        URL url = new URL(postUrl);
	        HttpURLConnection urlConn = (HttpURLConnection) url.openConnection();
	        urlConn.setDoOutput(true);
	        urlConn.setRequestMethod("GET");
	        //urlConn.setRequestProperty("User-Agent", "Mozilla/4.0 (compatible; MSIE 5.0; Windows NT; DigExt)");
	        urlConn.setRequestProperty("Content-Type","application/x-www-form-urlencoded;charset=UTF-8");
	        OutputStream os = urlConn.getOutputStream();
	        String paraStr="";
	        if(parameters != null){
				for(String key : parameters.keySet() ){
					Object value = parameters.get(key);
					paraStr+=key+"="+value+"&";
				}			
			}
	        os.write(paraStr.getBytes());
	        os.flush();
	        os.close();
	       
	        InputStream is = urlConn.getInputStream();
	        ByteArrayOutputStream bos = new ByteArrayOutputStream();  
	        byte[] buf = new byte[100];
	        int len =is.read(buf);
	        while(len >= 0){
	        	bos.write(buf, 0, len);
				len = is.read(buf);
	        }
	        is.close();
	       
	        temp=new String(bos.toByteArray(), "UTF-8");
		}catch(Exception e){
			e.printStackTrace();
		}
        return temp;

    }
	
	public static void trustAllHttpsCertificates() throws Exception {

		// Create a trust manager that does not validate certificate chains:

		javax.net.ssl.TrustManager[] trustAllCerts = new javax.net.ssl.TrustManager[1];

		javax.net.ssl.TrustManager tm = new MiTM();

		trustAllCerts[0] = tm;

		javax.net.ssl.SSLContext sc = javax.net.ssl.SSLContext.getInstance("SSL");

		sc.init(null, trustAllCerts, null);

		javax.net.ssl.HttpsURLConnection.setDefaultSSLSocketFactory(sc
				.getSocketFactory());

	}
	
	
	public static class MiTM implements javax.net.ssl.TrustManager,	javax.net.ssl.X509TrustManager {
		public java.security.cert.X509Certificate[] getAcceptedIssuers() {
			return null;
		}
		
		public boolean isServerTrusted(java.security.cert.X509Certificate[] certs) {
			return true;
		}
		
		public boolean isClientTrusted(java.security.cert.X509Certificate[] certs) {
			return true;
		}
		
		public void checkServerTrusted(java.security.cert.X509Certificate[] certs, String authType)throws java.security.cert.CertificateException {
			return;
		}
		
		public void checkClientTrusted(java.security.cert.X509Certificate[] certs, String authType)throws java.security.cert.CertificateException {
			return;
		}
	}
}

