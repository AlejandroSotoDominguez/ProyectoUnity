using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController>();
	}
	
	void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.tag == "Ground"){
            player.grounded = true;
        }

		if(col.gameObject.tag == "muerte"){
			player.muerte = true;
		}

        if(col.gameObject.tag == "Puerta"){
            player.fin = true;
        }

	}
	
	void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.tag == "Ground"){
            player.grounded = false;
        }
	}
	
}
