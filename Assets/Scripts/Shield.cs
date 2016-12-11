using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public Transform follow;

	void Start () {
        transform.parent = follow;
	}
}
