using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace esports
{
    public class AdvertisementController : MonoBehaviour,IUnityAdsListener
    {
        private string playStoreId;
        private string appStoreId;

        private string interstitalAd;
        private string rewardedVideoAd;

        private bool isTargetPlayStore; //true: play store, false: app store
        private bool isTestAd;

        public int bonusType; //1=add gold from add gold view, 2=team slot
        private void Start()
        {
            Advertisement.AddListener(this);
            playStoreId = "";
            appStoreId = "";
            interstitalAd = "video";
            rewardedVideoAd = "rewardedVideo";
            //TODO setting
            isTestAd = true;
            isTargetPlayStore = true;
            InitializeAdvertisement();
        }

        private void InitializeAdvertisement()
        {
            if (isTargetPlayStore)
            {
                Advertisement.Initialize(playStoreId, isTestAd);
            }
            else
            {
                Advertisement.Initialize(appStoreId, isTestAd);
            }
        }

        public bool PlayInterstitialAd()
        {
            if (Advertisement.IsReady(interstitalAd))
            {
                Advertisement.Show(interstitalAd);
                return true;
            }
            return false;
        }

        public bool PlayRewardedVideo()
        {
            if (Advertisement.IsReady(rewardedVideoAd))
            {
                Advertisement.Show(rewardedVideoAd);
                return true;
            }
            return false;
        }

        public bool IsPlayInterstitialAdReady()
        {
            return Advertisement.IsReady(interstitalAd);
        }

        public bool IsPlayRewardedVideoReady()
        {
            return Advertisement.IsReady(rewardedVideoAd);
        }


        public void OnUnityAdsReady(string placementId)
        {
            //throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidError(string message)
        {
            //throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidStart(string placementId)
        {
           // throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Finished:
                    if (placementId == rewardedVideoAd)
                    {
                        if (bonusType == 1)
                        {
                            GlobalGameController.Instance.GetComponentInChildren<AddGoldView>().ReceiveBonus();
                        }
                        else if (bonusType == 2)
                        {
                            GlobalGameController.Instance.teamController.TeamSlot++;
                            GlobalGameController.Instance.GetComponentInChildren<PlayerView>().PlayerViewRender();
                        }
                    }
                    if (placementId == interstitalAd)
                    {
                        Debug.Log("Interstital Adv Finihed");
                    }
                    break;
            }
        }
    }
}