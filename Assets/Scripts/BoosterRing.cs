using UnityEngine;
using System.Collections;

public class BoosterRing : MonoBehaviour {

    GameObject firstShip;
    public GameObject nextRing;

    void OnTriggerEnter(Collider coll) {
        ShipController sc = coll.gameObject.GetComponentInParent<ShipController>();

        // Make sure it's a ship passing through
        if (sc != null) {
            if(firstShip == null) {
                firstShip = coll.gameObject;
                sc.StartBoost(false);
            }
            else sc.StartBoost(true);

            sc.curRing = nextRing;
            
            if (nextRing != null && nextRing.GetComponent<BoosterRing>() != null) {
                sc.nextRing = nextRing.GetComponent<BoosterRing>().nextRing;
            }
            else sc.nextRing = null;
        }
    }
}
