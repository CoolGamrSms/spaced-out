using UnityEngine;
using System.Collections;

public class ShipCameraController : MonoBehaviour {

    public float lerpSpeed;
    public Vector3 followOffset;

    void FixedUpdate () {
    	if (transform.name == "Ship Camera 1") {
    		GameObject[] ship = GameObject.FindGameObjectsWithTag("Ship1");
        	transform.position = Vector3.Lerp(transform.position, ship[0].transform.position, Time.deltaTime * lerpSpeed) + followOffset;
    	}
    	else if (transform.name == "Ship Camera 2") {
    		GameObject[] ship = GameObject.FindGameObjectsWithTag("Ship2");
        	transform.position = Vector3.Lerp(transform.position, ship[0].transform.position, Time.deltaTime * lerpSpeed) + followOffset;
    	}
    }
}
