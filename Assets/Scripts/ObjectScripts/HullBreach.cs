using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HullBreach : ObjectBase {
	
	// Only enabled when Engineer in range
	void FixedUpdate () {
        if (!broken) {
            gameObject.SetActive(false);
        }
	}

}
