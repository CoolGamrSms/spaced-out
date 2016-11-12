using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

    //set by ship script
    public int joystickNum;
    public bool broken = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    virtual protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Engineer" && broken)
        {
            this.enabled = true;
        }
    }

    virtual protected void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Engineer" && broken)
        {
            this.enabled = false;
        }
    }
}
