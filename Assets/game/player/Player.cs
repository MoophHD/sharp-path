using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    private Vector3 startPos;
    private Transform tr;
    private Rigidbody2D rb;
    private Side side;

    public float downForce = 2f;

    public float upForce = 7f;
    public float sideForce = 16f;
    public float introUpForce = 7.5f;
    public float introSideForce = 15f;
    public float jumpHeight;
    public float height;

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

        height = GetComponent<CircleCollider2D>().bounds.extents.y;
        float jumpTime = (Constants.instance.screenWidth - height * 2) / sideForce;
        jumpHeight = (upForce) * jumpTime;

        reset();
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            // click on ui or already jumping
            if( EventSystem.current.IsPointerOverGameObject()) return;

            switch(currentState) {
                // case(state.Jump): {
                //     //tap/click hold => add y force
                //     holdJump();
                //     break;
                // };
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

    public void jump() {
        //frozen
        if (rb.isKinematic) return;
        
        GameActions.jump();

        Vector2 upwardsVelocity = Vector2.up * upForce;
                                // current == left ? jump to right : left
        Vector2 sideVelocity = (side.side == side.left ? Vector2.right : Vector2.left) * sideForce;
        currentState = state.Jump;
        rb.velocity = upwardsVelocity + sideVelocity;
    }

    // private void holdJump() {
    //     rb.velocity = rb.velocity + (Vector2.up * Time.deltaTime * upForce);
    // }

    public void reset() {
        currentState = state.Idle;
        rb.velocity = Vector3.zero;
        
        tr.position = startPos;

        side = Random.value > 0.5f ? new Side("left") : new Side("right");
    }

    void sit() {
        currentState = state.Sit;
        side.flip();

        rb.velocity = new Vector3(0f, -downForce, 0f);
    }

    public void firstJump() {
        Vector2 upwardsVelocity = Vector2.up * introUpForce;
                                // inversed sides
        Vector2 sideVelocity = (side.side == side.left ? Vector2.right : Vector2.left) * introSideForce;

        rb.velocity = upwardsVelocity + sideVelocity;
    }


    
    // private float lastY = 0f;
    void OnCollisionEnter2D(Collision2D coll) {
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

    public void centerSelf(){
        rb.velocity = Vector3.zero;
        //saves y pos
        tr.position = new Vector3(startPos.x,tr.position.y, tr.position.z);
        currentState = state.Idle;
    }

    public void freeze(bool isFrozen) {
        rb.isKinematic = isFrozen;
        if (isFrozen) rb.velocity = Vector3.zero;
    }

}