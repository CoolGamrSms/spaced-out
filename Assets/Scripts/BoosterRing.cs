using UnityEngine;
using System.Collections;

public class BoosterRing : MonoBehaviour {

    GameObject firstShip;
    public GameObject nextRing;
    [HideInInspector] public GameObject asteroid;

    void OnTriggerEnter(Collider coll)
    {
        ShipController sc = coll.gameObject.GetComponentInParent<ShipController>();
        if (sc != null) //Make sure it's a ship passing through
        {
            if(firstShip == null)
            {
                firstShip = coll.gameObject;
                //Spawn asteroids 2 rings ahead
                if(nextRing != null && nextRing.GetComponent<BoosterRing>() != null)
                {
                    Transform area = nextRing.GetComponent<BoosterRing>().nextRing.transform;
                    GameObject a = Instantiate(asteroid);
                    a.transform.position = area.position;
                }
            }
            sc.curRing = nextRing;
            if (nextRing != null && nextRing.GetComponent<BoosterRing>() != null) sc.nextRing = nextRing.GetComponent<BoosterRing>().nextRing;
            else sc.nextRing = null;
            sc.StartBoost();
        }
    }
}
