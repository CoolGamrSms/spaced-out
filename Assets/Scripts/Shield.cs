using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public Transform follow;

	// Use this for initialization
	void Start () {
        transform.parent = follow;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
