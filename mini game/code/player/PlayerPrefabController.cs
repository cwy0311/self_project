using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace esports {
    public class PlayerPrefabController : MonoBehaviour
    {
        public int id;
        private Animator anim;
      //  private float animTimer;

        private void Start()
        {
            anim = GetComponent<Animator>();
            StartCoroutine(SetAnim());
            
            //SetAnimTimer();
        }

        private void Update()
        {
            //if (GlobalGameController.Instance.CheckIsPause())
            //{
             //   return;
            //}

            //animTimer -= Time.deltaTime;
            //if (animTimer <= 0)
            //{
            //    SetAnimTimer();
            //}
        }

        private void OnMouseDown()
        {
            if (!GlobalGameController.Instance.CheckIsPause())
            {
                GlobalGameController.Instance.panelController.playerDetailView.SetPlayerViewId(id);
                GlobalGameController.Instance.panelController.OpenPanel(7);
            }
        }

        IEnumerator SetAnim()
        {
            float timeDelay = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
            anim.runtimeAnimatorController = GlobalGameController.Instance.playerPrefabManager.idleAnim;
        }

        //void SetAnimTimer()
        //{
            //animTimer = Random.Range(5f, 15f);
        //}
    }
}