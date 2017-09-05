using System.Collections;
using UnityEngine;

namespace Resources
{
    public class MusicController : MonoBehaviour
    {
        public static MusicController Instance;

        public float MaxVolume;
        public float FadeTime;

        public AudioSource Main;
        public AudioSource Secondary;

        private void Awake()
        {
            if (Instance == null) Instance = this; // else, this instance will not be used at all
        }

        private void Start ()
        {
            Main.Play();
        }

        public void EnterEnemy()
        {
            Secondary.Stop();
            SwapMusic(Main, Secondary);
        }

        public void ExitEnemy()
        {
            SwapMusic(Secondary, Main);
        }

        private void SwapMusic(AudioSource a, AudioSource b)
        {
            StartCoroutine(FadeOut(a));
            StartCoroutine(FadeIn(b));
        }

        public IEnumerator FadeOut (AudioSource audioSource) {
            var startVolume = audioSource.volume;
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            audioSource.Pause();
        }

        public IEnumerator FadeIn(AudioSource audioSource)
        {
            audioSource.volume = 0;
            audioSource.Play();
            while (audioSource.volume < MaxVolume)
            {
                audioSource.volume += Time.deltaTime / FadeTime;
                yield return null;
            }
        }
    }
}
