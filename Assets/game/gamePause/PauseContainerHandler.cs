using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseContainerHandler : MonoBehaviour {
	public GameObject Container;
	void OnEnable() {
		GameActions.onPause += (bool paused) => { 
				if(paused) {
					Container.SetActive(true);
				} else {
					Container.SetActive(false);
				}
			};
	}
	void OnDisable() {
		GameActions.onPause -= (bool paused) => { 
				if(paused) {
					Container.SetActive(true);
				} else {
					Container.SetActive(false);
				}
			};
	}
}
