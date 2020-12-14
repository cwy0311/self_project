using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace esports
{
    public class ComputerSeatController : MonoBehaviour
    {
        public ComputerChair chair;
        public GameObject playerPrefabPos;
        public int seatIndex;
        public int bindPlayerId;

        private void Awake()
        {
            //set playerid to empty before read the data from playerspref set by team controller
            bindPlayerId = -1;
        }

        private void Start()
        {
            if (chair == null)
            {
                chair = transform.Find("Chair").GetComponent<ComputerChair>();
            }
            if (playerPrefabPos == null) {
                playerPrefabPos = transform.Find("Prefab").gameObject;
            }

            chair.seatIndex = seatIndex;
            
        }
    }
}