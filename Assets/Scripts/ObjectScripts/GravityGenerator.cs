using UnityEngine;
using System.Collections;

public class GravityGenerator : ObjectBase {

    ButtonSequence fixScript;
	// Use this for initialization
	void Start () {
        this.enabled = false;
        fixScript = GetComponentInChildren<ButtonSequence>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!broken) {
            //remove negative effects            GetComponent<Rigidbody>().useGravity = true;
            this.enabled = false;
        }

	}

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        fixScript.enabled = true;
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        fixScript.enabled = false;
    }
}
