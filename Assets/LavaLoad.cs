using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLoad : MonoBehaviour {
	private AudioSource song;
	// Use this for initialization
	void Start () {
//		AudioSource audio = GetComponent<AudioSource>();
		song.Play();
		Screen.lockCursor = false;
//		audio.Play("Rocket Beans-1.mp4");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject loadingImage;

	public void LoadScene(int level)
	{
//		loadingImage.SetActive(true);
		Application.LoadLevel(level);
	}
}
