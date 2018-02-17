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

	void Awake() {
		tr = GetComponent<Transform>();

		startPos = tr.position;
		speed = Constants.instance.cameraSpeed;
	}


	void FixedUpdate () {
		if (isMoving) {
			Vector3 newPos = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
		}
	}

	public void reset() {
		tr.position = startPos;
	}
}