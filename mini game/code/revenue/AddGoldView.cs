using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace esports {
    public class AddGoldView : MonoBehaviour
    {
        public Text watchVideoText;
        public Text restorePurchaseText;
        public Text watchVideoGetGoldText;
        public Text videoIcon;
        public Text goldIcon;
        public Text remainingText;
        public GameObject doubleOrTripleObject;
        public Text doubleOrTripleText;

        private int advertismentGoldGet;
        private bool isDouble;
        private bool isTriple;

        public void RenderAddGoldView()
        {
            remainingText.text=GlobalGameController.Instance.teamController.GoldAdvBonusCount>0?"x"+ GlobalGameController.Instance.teamController.GoldAdvBonusCount:
                GlobalGameController.Instance.I18Translate("addgold.view.text3");
            watchVideoText.text = GlobalGameController.Instance.I18Translate("addgold.view.text2");
            restorePurchaseText.text = GlobalGameController.Instance.I18Translate("addgold.view.text1");
            advertismentGoldGet = Mathf.FloorToInt((GlobalGameController.Instance.teamController.GetSponsorshipIncome() / 2 + 500) * Random.Range(0.85f, 1.15f));
            watchVideoGetGoldText.text = "+" + advertismentGoldGet;
            SetTimeLimitDoubleOrTriple();
        }

        private void Update()
        {
            if (GlobalGameController.Instance.advertisementController.IsPlayRewardedVideoReady() && GlobalGameController.Instance.teamController.GoldAdvBonusCount > 0)
            {
                watchVideoText.color = new Color(watchVideoText.color.r, watchVideoText.color.g, watchVideoText.color.b, 1f);
                watchVideoGetGoldText.color = new Color(watchVideoGetGoldText.color.r, watchVideoGetGoldText.color.g, watchVideoGetGoldText.color.b, 1f);
                videoIcon.color = new Color(videoIcon.color.r, videoIcon.color.g, videoIcon.color.b, 1f);
                goldIcon.color = new Color(goldIcon.color.r, goldIcon.color.g, goldIcon.color.b, 1f);
            }
            else
            {
                watchVideoText.color = new Color(watchVideoText.color.r, watchVideoText.color.g, watchVideoText.color.b, 0.25f);
                watchVideoGetGoldText.color = new Color(watchVideoGetGoldText.color.r, watchVideoGetGoldText.color.g, watchVideoGetGoldText.color.b, 0.25f);
                videoIcon.color = new Color(videoIcon.color.r, videoIcon.color.g, videoIcon.color.b, 0.25f);
                goldIcon.color = new Color(goldIcon.color.r, goldIcon.color.g, goldIcon.color.b, 0.25f);

            }
        }

        public void PlayVideoToEarnBonusGold()
        {
            if (GlobalGameController.Instance.teamController.GoldAdvBonusCount > 0)
            {
                GlobalGameController.Instance.advertisementController.bonusType = 1;
                GlobalGameController.Instance.advertisementController.PlayRewardedVideo();
            }
        }


        public void ReceiveBonus()
        {
            int goldToAdd = isDouble ? advertismentGoldGet * 2 : isTriple ? advertismentGoldGet * 3 : advertismentGoldGet;
            GlobalGameController.Instance.teamController.Gold += goldToAdd;
            GlobalGameController.Instance.teamController.GoldAdvBonusCount -= 1;
            ReRender();
        }


        private void ReRender()
        {
            if (GlobalGameController.Instance.teamController.GoldAdvBonusCount > 0)
            {
                RenderAddGoldView();
            }
            else
            {
                SetTimeLimitDoubleOrTriple();
                remainingText.text = GlobalGameController.Instance.I18Translate("addgold.view.text3");
            }
        }


        private void SetTimeLimitDoubleOrTriple()
        {
            isDouble = false;
            isTriple = false;
            doubleOrTripleObject.SetActive(false);
            if (GlobalGameController.Instance.teamController.GoldAdvBonusCount > 0)
            {
                float random = Random.Range(0f, 1f);
                //double probability=0.3, triple=0.15
                if (random <= 0.15)
                {
                    isTriple = true;
                }
                else if (random <= 0.45)
                {
                    isDouble = true;
                }
                if (isTriple || isDouble)
                {
                    doubleOrTripleObject.SetActive(true);
                    doubleOrTripleText.text = isTriple ? GlobalGameController.Instance.I18Translate("addgold.view.text5") : GlobalGameController.Instance.I18Translate("addgold.view.text4");
                }

            }

        }
    }

}