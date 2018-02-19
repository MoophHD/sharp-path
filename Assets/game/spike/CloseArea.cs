using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseArea : MonoBehaviour {
	private const float REMOVE_DELTA = 0.5f;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "player") {
			GameController.closeAreas++;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "player") {
			Invoke("removeCloseArea", REMOVE_DELTA);
		}
	}

	void removeCloseArea() {
		GameController.closeAreas--;
	}
}
