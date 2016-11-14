using UnityEngine;
using System.Collections;

public class GravityGenerator : ObjectBase {
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!broken) {
            //remove negative effects            GetComponent<Rigidbody>().useGravity = true;
            this.enabled = false;
        }
	}

}
