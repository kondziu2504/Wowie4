using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utility
{

    [System.Serializable]
    public class SoundCollection
    {
        [SerializeField] AudioClip[] clips;
        public AudioClip[] Clips => clips;

        public AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }
    }

}
