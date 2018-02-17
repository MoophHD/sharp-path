using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
    private Transform tr;
    void Awake() {
        tr = GetComponent<Transform>();
    }
    public void init(bool isLeftSide, float y) {
        float offset = Constants.instance.spikeSideOffset;
        Vector3 newPos = new Vector3(isLeftSide ? -offset : offset, y, tr.position.z);
        tr.position = newPos;

        if (isLeftSide) {
            Vector3 rot = tr.eulerAngles;
            //from -90 deg t 90deg
            tr.eulerAngles = new Vector3(rot.x, rot.y, -rot.z);
        }
    }
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "player") {
            GameActions.restart();
        }
    }
}