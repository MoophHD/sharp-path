using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
     void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "player") {
            GameActions.restart();
        }
    }
}