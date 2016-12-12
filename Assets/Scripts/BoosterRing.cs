using UnityEngine;
using System.Collections;

public class BoosterRing : MonoBehaviour {

    GameObject firstShip, secondShip;
    public GameObject nextRing;

    public Plane plane;

    private bool die;

    void Start()
    {
        plane = new Plane(transform.forward, transform.position);
    }

    void OnTriggerEnter(Collider coll) {
        ShipController sc = coll.gameObject.GetComponentInParent<ShipController>();

        // Make sure it's a ship passing through
        if (sc != null) {
            if (firstShip == null)
            {
                firstShip = coll.gameObject;
                sc.StartBoost(false);
                DisableRenderer(sc.gameObject.tag);
            }
            else if (firstShip == coll.gameObject) return;
            else if (secondShip == null)
            {
                secondShip = coll.gameObject;
                sc.StartBoost(true);
                DisableRenderer(sc.gameObject.tag);
            }
            else return;

            sc.lastRing = sc.curRing;
            sc.curRing = nextRing;
            
            if (nextRing != null && nextRing.GetComponent<BoosterRing>() != null) {
                sc.nextRing = nextRing.GetComponent<BoosterRing>().nextRing;
            }
            else sc.nextRing = null;
        }
    }

    void DisableRenderer(string tag)
    {
        if (tag == "Ship1")
        {
            transform.FindChild("Pilot1").gameObject.SetActive(false);
        }
        else if (tag == "Ship2")
        {
            transform.FindChild("Pilot2").gameObject.SetActive(false);
        }
    }
}
