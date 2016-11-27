using UnityEngine;
using System.Collections;

public class RotateToFace : MonoBehaviour {

    public GameObject face;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var targetPosition = face.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
	}
}
