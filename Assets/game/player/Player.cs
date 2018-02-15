using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Vector3 startPos;
    private Transform tr;

    void Awake() {
        tr = GetComponent<Transform>();
        startPos = tr.position;
    }

    public void resetPos() {
        tr.position = startPos;
    }
}