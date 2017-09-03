using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	public float MaxVolume = 0.5f;
	public float FadeTime = 1.5f;
	
	public AudioSource main;
	public AudioSource secondary;
	
	void Start ()
	{
		main.Play();
	}

	public void enterEnemy()
	{
		swapMusic(main, secondary);
	}

	public void exitEnemy()
	{
		swapMusic(secondary, main);
	}

	private void swapMusic(AudioSource a, AudioSource b)
	{
		StartCoroutine(FadeOut(a));
		StartCoroutine(FadeIn(b));
	}

	public IEnumerator FadeOut (AudioSource audioSource) {
		float startVolume = audioSource.volume;
 
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
			yield return null;
		}

		audioSource.Pause();
		
	}
	
	public IEnumerator FadeIn(AudioSource audioSource) {
		audioSource.volume = 0;
		audioSource.Play ();
		
		while (audioSource.volume < MaxVolume) {
			audioSource.volume += Time.deltaTime / FadeTime;
 
			yield return null;
		}
	}
}
