using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 startPos;
	private Transform tr;
	public float smoothSpeed = 0.3f;
	private Vector3 currentVelocity;
	private float speed;
	public bool isMoving = false;

	private float lastAnchor;

	void Awake() {
		tr = GetComponent<Transform>();

		startPos = tr.position;
		speed = Constants.instance.cameraSpeed;

		lastAnchor = startPos.y;
	}


	void FixedUpdate () {
		if (isMoving) {
			Vector3 newPos = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);

			checkDelta();
		}
	}

	public void reset() {
		tr.position = startPos;
	}

	void checkDelta() {
		if (transform.position.y - lastAnchor > Constants.instance.screenHeight) {
			lastAnchor = transform.position.y;
			GameActions.screenPass();
		}
	}

	public Vector3 pos {
		get {
			return tr.position;
		}
		set {
			tr.position = value;
		}
	}
	
}