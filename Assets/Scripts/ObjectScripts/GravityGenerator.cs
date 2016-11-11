using UnityEngine;
using System.Collections;

public class GravityGenerator : ObjectBase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!broken) {            GetComponent<Rigidbody>().useGravity = true;
            this.enabled = false;
        }
	}
}
