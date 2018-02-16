using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 startPos;
	private Transform tr;
	public float smoothSpeed = 0.3f;
	private Vector3 currentVelocity;
	private float speed;
    private const float checkDelta = 2.5f;
    private float passDistance;
    private float lastGenY;

	public bool isMoving = false;

	void Awake() {
		tr = GetComponent<Transform>();
        lastGenY = tr.position.y;
        passDistance = Constants.instance.cameraPassDistance;

		startPos = tr.position;
		speed = Constants.instance.cameraSpeed;
	}

    void Start() {
        InvokeRepeating("checkCamera", 0f, checkDelta);
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

    void checkCamera() {
        if ( tr.position.y - lastGenY >= passDistance) {
            GameActions.cameraPass();
            lastGenY = lastGenY + passDistance;
        }
    }
}