using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace esports {
    public class MusicController : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip baseMusic;
        public AudioClip competitionMusic;

        // Start is called before the first frame update
        void Start()
        {
            audioSource.loop = true;
            audioSource.clip = baseMusic;
          //  audioSource.volume = 1;
        }

        // Update is called once per frame
        void Update()
        {
                if (GlobalGameController.Instance.IsVolumeOn == 0)
                {
                   audioSource.mute = true;
               }
               else
               {
                   audioSource.mute = false;
               }


        }

        public void SwitchMusic(bool isCompetition)
        {
            audioSource.Stop();
            if (isCompetition)
            {
                audioSource.clip = competitionMusic;
            }
            else
            {
                audioSource.clip = baseMusic;
            }
            audioSource.Play();
        }
    }
}