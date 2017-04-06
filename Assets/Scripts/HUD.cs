using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public Texture2D crosshair;
	private Vector2 windowSize;
	private Rect position;

	void Start() {
		//crosshair = new Texture2D(10,10);
		windowSize = new Vector2(Screen.width, Screen.height);
		position = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) /2, crosshair.width, crosshair.height);
	}

	void Update () {
		if (windowSize.x != Screen.width || windowSize.y != Screen.height) {
			CrosshairPos();
		}
	}
	void CrosshairPos() {
		position = new Rect( (windowSize.x - crosshair.width), (windowSize.y - crosshair.height), crosshair.width, crosshair.height);
	}
	void OnGUI() {
		GUI.DrawTexture(position, crosshair);
	}
}