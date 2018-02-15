using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    private Vector3 startPos;
    private Transform tr;
    private Rigidbody2D rb;
    private Side side;
    public float upForce = 7.5f;
    public float sideForce = 25f;
    public float introUpForce = 10f;
    public float introSideForce = 1f;

  	private enum state {
        Idle,
		Sit,
		Jump
	}
    private state currentState;

    void Awake() {
        currentState = state.Idle;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        startPos = tr.position;

        reset();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // click on ui
            if( EventSystem.current.IsPointerOverGameObject() ){
                return;
            }

            switch(currentState) {
                case(state.Idle): {
                    GameActions.start();
                    break;
                };
                case(state.Sit): {
                    jump();
                    break;
                }
            }
  
    

        // RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        // if ( Physics.Raycast (ray,out hit)) {

        // }

        }
    }

    public void reset() {
        currentState = state.Idle;
        tr.position = startPos;
        rb.velocity = Vector3.zero;

        side = Random.value > 0.5f ? new Side("left") : new Side("right");
    }

    void sit() {
        currentState = state.Sit;
        side.flip();

        rb.velocity = Vector3.zero;
    }

    public void firstJump() {
        Vector2 upwardsVelocity = Vector2.up * introUpForce;
                                // inversed sides
        Vector2 sideVelocity = (side.side == side.left ? Vector2.right : Vector2.left) * introSideForce;

        rb.velocity = upwardsVelocity + sideVelocity;
    }

    public void jump() {
        Vector2 upwardsVelocity = Vector2.up * upForce;
                                // current == left ? jump to right : left
        Vector2 sideVelocity = (side.side == side.left ? Vector2.right : Vector2.left) * sideForce;
        currentState = state.Jump;
        rb.velocity = upwardsVelocity + sideVelocity;

    }

    void OnCollisionEnter2D(Collision2D coll) {
        print("collision");
        string tag = coll.gameObject.tag;
        if (tag == "wall") {
            //is on the right wall while current state is on left etc.
            if ((coll.gameObject.name == "wall_l" && side.side == side.right) ||
                (coll.gameObject.name == "wall_r" && side.side == side.left)
            ) {
                sit();
            }
        } else if (tag == "pod") {
            GameActions.restart();
        }
    }

    
}