package com.chatbot.supersitionbot.cache;

import java.util.Date;
import java.util.HashMap;

public class CacheManager {
	private static HashMap<String,Cache> cacheMap = new HashMap<>();

    /**
     * This class is singleton so private constructor is used.
     */
    private CacheManager() {
            super();
    }

    private synchronized static Cache getCache(String key) {
            return (Cache)cacheMap.get(key);
    }

    private synchronized static boolean hasCache(String key) {
            return cacheMap.containsKey(key);
    }

    public synchronized static void invalidateAll() {
            cacheMap.clear();
    }

    public synchronized static void invalidate(String key) {
            cacheMap.remove(key);
    }

    private synchronized static void putCache(String key, Cache object) {
       cacheMap.put(key, object);
    }

    public static Cache getContent(String key) {
             if (hasCache(key)) {
                    Cache cache = getCache(key);
                    if (cacheExpired(cache)) {
                        //cache.setExpired(true);
                    	invalidate(key);
                    	return null;
                    }else{
                    	return cache;
                    }
             } else {
                    return null;
             }
    }


    public static void putContent(String key, Object content, long ttl) {
            Cache cache = new Cache();
            cache.setKey(key);
            cache.setValue(content);
            cache.setTimeOut(ttl*1000 + new Date().getTime());
            cache.setExpired(false);
            putCache(key, cache);
    }
    
    private static boolean cacheExpired(Cache cache) {
            if (cache == null) {
                    return false;
            }
            long milisNow = new Date().getTime();
            long milisExpire = cache.getTimeOut();

            if (milisExpire < 0) {                // Cache never expires 
                    return false;
            } else if (milisNow >= milisExpire) {
                    return true;
            } else {
                    return false;
            }
    }
}
