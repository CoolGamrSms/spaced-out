using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
	
	// Only enabled when Engineer in range
	void FixedUpdate () {
        if (!broken) {
            gameObject.SetActive(false);
        }
	}
}
