using UnityEngine;
using System.Collections;

public class RotateToFace : MonoBehaviour {

    public GameObject face;
    public bool isEngineText = false;

    void Start()
    {
        if (face == null) gameObject.SetActive(false);
    }
	void Update () {
        var targetPosition = face.transform.position;

        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        if (isEngineText) {
        	transform.Rotate(0, 180, 0);
        }
	}
}
