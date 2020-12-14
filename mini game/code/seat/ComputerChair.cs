using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
    public class ComputerChair : MonoBehaviour
    {
        public int seatIndex;

        private void OnMouseDown()
        {
            if (!GlobalGameController.Instance.CheckIsPause())
            {
                GlobalGameController.Instance.playerPrefabManager.SetFreeSeatIndex(seatIndex);
                GlobalGameController.Instance.panelController.OpenPanel(5);
            }
        }
    }
}