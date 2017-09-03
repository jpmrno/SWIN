using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFade : MonoBehaviour
{

	public AudioSource main;

	public AudioSource secondary;
	private static float MAX_VOLUME = 0.5f;
	private static float FADE_TIME = 1.5f;
	
	private int counter = 0;
	
	
	// Use this for initialization
	void Start ()
	{
		main.Play();
	}

	public void enterEnemy()
	{
		StartCoroutine(MusicFade.FadeOut(main, FADE_TIME));
		StartCoroutine(MusicFade.FadeIn(secondary, FADE_TIME));
	}

	public void exitEnemy()
	{
		StartCoroutine(MusicFade.FadeOut(secondary, FADE_TIME));
		StartCoroutine(MusicFade.FadeIn(main, FADE_TIME));
	}

	public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;
 
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
			yield return null;
		}
 
		audioSource.Pause();

	}
	
	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
		audioSource.volume = 0;
		audioSource.Play ();
		
		while (audioSource.volume < MAX_VOLUME) {
			audioSource.volume += Time.deltaTime / FadeTime;
 
			yield return null;
		}
	}
}
