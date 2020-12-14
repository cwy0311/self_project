using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace esports {
    public class RecruitmentView : MonoBehaviour
    {
        public List<GameObject> recruitmentContentList;


        public void RenderHireList()
        {
            GlobalGameController.Instance.humanResourcesController.Init();
            
            for (int i = 0; i < recruitmentContentList.Count; i++)
            {
                recruitmentContentList[i].GetComponent<Button>().onClick.RemoveAllListeners();

                if (i < GlobalGameController.Instance.humanResourcesController.GetRecruitmentListSize())
                {
                    recruitmentContentList[i].transform.Find("Player Image");
                    recruitmentContentList[i].transform.Find("Player Name").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate(GlobalGameController.Instance.humanResourcesController.GetRecruitmentPlayer(i).nameField);
                    recruitmentContentList[i].transform.Find("Ability").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text1");
                    recruitmentContentList[i].transform.Find("Ability Count").GetComponent<Text>().text = GlobalGameController.Instance.humanResourcesController.GetRecruitmentPlayer(i).skillStatus.GetAbility().ToString();
                    recruitmentContentList[i].transform.Find("Salary").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text6");
                    recruitmentContentList[i].transform.Find("Salary Count").GetComponent<Text>().text = GlobalGameController.Instance.humanResourcesController.GetRecruitmentPlayer(i).MonthSalary.ToString();

                    recruitmentContentList[i].transform.Find("Salary Count").GetComponent<Text>().color = !GlobalGameController.Instance.teamController.CanPay(GlobalGameController.Instance.humanResourcesController.GetRecruitmentPlayer(i).MonthSalary) ? new Color(1f, 0f, 0f, 1f) : new Color(220f / 255, 220f / 255, 220f / 255, 1f);


                    recruitmentContentList[i].SetActive(true);
                    AddPlayerDetailEvent(recruitmentContentList[i].GetComponent<Button>(),GlobalGameController.Instance.humanResourcesController.GetRecruitmentPlayer(i));
                }
                else
                {
                    recruitmentContentList[i].SetActive(false);
                }

            }
        }

        private void AddPlayerDetailEvent(Button button,Player playerToView)
        {
            button.onClick.AddListener(() =>
            {
                GlobalGameController.Instance.panelController.playerDetailView.SetPlayerViewId(playerToView.Id);
                GlobalGameController.Instance.panelController.OpenInnerContent(7);
            });
        }
    }
}