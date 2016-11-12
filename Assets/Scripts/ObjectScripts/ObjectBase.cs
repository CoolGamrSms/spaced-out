using UnityEngine;
using System.Collections;

public class ObjectBase : MonoBehaviour {

    //set by ship script
    public int joystickNum;
    public bool broken = false;
    public FixingBase fixScript;

	// Use this for initialization
	virtual protected void Start () {
        fixScript = GetComponentInChildren<FixingBase>();
        print(fixScript.name);
        fixScript.enabled = false;
        this.enabled = false;
        print("hello");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    virtual protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Engineer")
        {
            if (broken)
            {
                fixScript.enabled = true;
            }else{
                this.enabled = true;
            }
        }
    }

    virtual protected void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Engineer")
        {
            this.enabled = false;
        }
    }

    public void Fixed() {
        broken = false;
        this.enabled = true;
    }
}
