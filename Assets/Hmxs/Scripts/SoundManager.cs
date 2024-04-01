using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hmxs.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        // [ListDrawerSettings(ShowIndexLabels = true)] [ReadOnly]
        // [SerializeField] private List<string> info = new()
        // {
        //     "人-暴躁-女-东北话",
        //     "狗-暴躁-女-东北话",
        //     "人-温柔-女-普通话",
        //     "狗-温柔-女-普通话"
        // };

        [InfoBox("" +
                 "0: 人-暴躁-女-东北话\n" +
                 "1: 狗-暴躁-女-东北话\n" +
                 "2: 人-温柔-女-普通话\n" +
                 "3: 狗-温柔-女-普通话\n" +
                 "4: 人-暴躁-男-otto\n" +
                 "5: 狗-暴躁-男-otto\n" +
                 "6: 人-阴阳-男-东北话\n" +
                 "7: 狗-阴阳-男-东北话\n" +
                 "8: 人-暴躁-男-孙笑川\n" +
                 "9: 狗-暴躁-男-孙笑川")]

        [SerializeField] private List<SoundGroup> audioClips = new();

        public void PlaySound(int id, Transform source = null)
        {
            var playPoint = source == null ? Vector3.zero : source.position;
            AudioSource.PlayClipAtPoint(audioClips[id].clips[Random.Range(0, audioClips[id].clips.Count)], playPoint);
        }

        [Serializable]
        public class SoundGroup
        {
            public List<AudioClip> clips = new();
        }
    }
}