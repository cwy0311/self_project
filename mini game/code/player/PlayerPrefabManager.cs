using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
    public class PlayerPrefabManager : MonoBehaviour
    {
        [Header("prefab list element=id+1")]
        public List<GameObject> prefabList;
        public List<ComputerSeatController> computerSeats;

        public RuntimeAnimatorController idleAnim;
        public RuntimeAnimatorController walkAnim;
        public RuntimeAnimatorController runAnim;
        public RuntimeAnimatorController jumpAnim;
        private int freeSeatIndex;

        private void Start()
        {
            freeSeatIndex = -1;
            if (computerSeats.Count !=GameSettingConst.MAX_TEAM_COUNT)
            {
                Debug.LogError("PlayerPrefabManager: computerSeats should have length " + GameSettingConst.MAX_TEAM_COUNT);
            }
        }



        public GameObject GetPrefab(int id)
        {
            if (id<0 || id > prefabList.Count)
            {
                return null;
            }
            return prefabList[id - 1];
        }

        //TODO get data from saved data and set them
        public void SetAllComputerSeatView()
        {
            LoadComputerSeats();

            Transform prefabGO;
            GameObject newPrefab;
            for (int i = 0; i < computerSeats.Count; i++)
            {
                prefabGO = computerSeats[i].transform.Find("Prefab");
                Transform[] oldPrefabs = prefabGO.GetComponentsInChildren<Transform>();
                for (int j = 0; j < oldPrefabs.Length; j++)
                {
                    if (oldPrefabs[j].parent == prefabGO)
                    {
                        Destroy(oldPrefabs[j].gameObject);
                    }
                }

                if (computerSeats[i].bindPlayerId>0)
                {
                    computerSeats[i].chair.gameObject.SetActive(false);
                    prefabGO = computerSeats[i].transform.Find("Prefab");
                    newPrefab = (GameObject)Instantiate(GetPrefab(computerSeats[i].bindPlayerId));
                    newPrefab.AddComponent<PlayerPrefabController>();
                    newPrefab.GetComponent<PlayerPrefabController>().id = computerSeats[i].bindPlayerId;
                    newPrefab.AddComponent<BoxCollider>();
                    newPrefab.GetComponent<BoxCollider>().center = new Vector3(0, 2, 0);
                    newPrefab.GetComponent<BoxCollider>().size = new Vector3(2, 5, 1);
                    newPrefab.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    newPrefab.transform.parent = prefabGO;
                    newPrefab.transform.position = new Vector3(computerSeats[i].transform.position.x, computerSeats[i].transform.position.y, computerSeats[i].transform.position.z);
                    newPrefab.transform.localRotation = i < 4 ? Quaternion.Euler(new Vector3(0, -90, 0)) : Quaternion.Euler(new Vector3(0, 90, 0));

                    prefabGO.gameObject.SetActive(true);
                }
                else
                {
                    computerSeats[i].chair.gameObject.SetActive(true);
                    prefabGO.gameObject.SetActive(false);

                }
            }
        }


        public void RemoveComputerSeatView(int playerId)
        {
            for (int i = 0; i < computerSeats.Count; i++)
            {
                if (computerSeats[i].bindPlayerId == playerId)
                {
                    computerSeats[i].bindPlayerId = -1;
                    Transform prefabGO = computerSeats[i].transform.Find("Prefab");
                    Transform[] oldPrefabs = prefabGO.GetComponentsInChildren<Transform>();
                    for (int j = 0; j < oldPrefabs.Length; j++)
                    {
                        if (oldPrefabs[j].parent == prefabGO)
                        {
                            Destroy(oldPrefabs[j].gameObject);
                        }
                    }
                    //free chair
                    computerSeats[i].chair.gameObject.SetActive(true);
                    break;
                }
            }
            freeSeatIndex = -1;
            SaveComputerSeats();
        }

        public void SetComputerSeatView(int index)
        {
            if (freeSeatIndex < 0)
            {
                for (int i = 0; i < computerSeats.Count; i++)
                {
                    if (computerSeats[i].bindPlayerId < 0)
                    {
                        freeSeatIndex = i;
                        break;
                    }
                }
            }
            if (freeSeatIndex <0) return;

            Transform prefabGO;
            GameObject newPrefab;
            
            prefabGO = computerSeats[freeSeatIndex].transform.Find("Prefab");
            computerSeats[freeSeatIndex].bindPlayerId = GlobalGameController.Instance.teamController.team[index].Id;
            if (index < GlobalGameController.Instance.teamController.team.Count)
                {
                    computerSeats[freeSeatIndex].chair.gameObject.SetActive(false);
                    prefabGO = computerSeats[freeSeatIndex].transform.Find("Prefab");
                    Transform[] oldPrefabs = prefabGO.GetComponentsInChildren<Transform>();
                    for (int j = 0; j < oldPrefabs.Length; j++)
                    {
                        if (oldPrefabs[j].parent == computerSeats[freeSeatIndex])
                        {
                            Destroy(oldPrefabs[j].gameObject);
                        }
                    }
                    newPrefab = (GameObject)Instantiate(GetPrefab(GlobalGameController.Instance.teamController.team[index].Id));
                    newPrefab.AddComponent<PlayerPrefabController>();
                    newPrefab.GetComponent<PlayerPrefabController>().id = GlobalGameController.Instance.teamController.team[index].Id;
                    newPrefab.AddComponent<BoxCollider>();
                    newPrefab.GetComponent<BoxCollider>().center = new Vector3(0, 2, 0);
                    newPrefab.GetComponent<BoxCollider>().size = new Vector3(2, 5, 1);
                    newPrefab.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    newPrefab.transform.parent = prefabGO;

                    newPrefab.transform.position = new Vector3(computerSeats[freeSeatIndex].transform.position.x, computerSeats[freeSeatIndex].transform.position.y, computerSeats[freeSeatIndex].transform.position.z);
                    newPrefab.transform.localRotation = freeSeatIndex < 4 ? Quaternion.Euler(new Vector3(0, -90, 0)) : Quaternion.Euler(new Vector3(0, 90, 0));
                    prefabGO.gameObject.SetActive(true);
                }
                else
                {
                    computerSeats[freeSeatIndex].chair.gameObject.SetActive(true);
                    prefabGO.gameObject.SetActive(false);

                }
            freeSeatIndex = -1;
            SaveComputerSeats();
        }




        public void SetFreeSeatIndex(int index)
        {
            if (GlobalGameController.Instance.CheckIsPause())
            {
                return;
            }
            freeSeatIndex = index;
        }


        private string GetPlayerPrefsSeatsString(int index)
        {
            return "PlayerPrefabManager.Seat." + index;
        }

        private void SaveComputerSeats()
        {
            for (int i = 0; i < computerSeats.Count; i++)
            {
                PlayerPrefs.SetInt(GetPlayerPrefsSeatsString(i), computerSeats[i].bindPlayerId);
            }
        }

        private void LoadComputerSeats()
        {
            for (int i = 0; i < computerSeats.Count; i++)
            {
                if (PlayerPrefs.HasKey(GetPlayerPrefsSeatsString(i)))
                {
                    computerSeats[i].bindPlayerId = PlayerPrefs.GetInt(GetPlayerPrefsSeatsString(i));
                }
                else
                {
                    computerSeats[i].bindPlayerId = -1;
                }
            }
        }

    }
}