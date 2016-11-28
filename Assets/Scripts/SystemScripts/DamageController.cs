using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class DamageController : MonoBehaviour {
    public GameObject shipInterior;

    List<ShipSystem> systems;
    void Awake() {
        systems = shipInterior.GetComponentsInChildren<ShipSystem>().ToList();
        foreach (ShipSystem ss in systems) ss.sc = GetComponent<ShipController>();
    }

    //For testing
    public void BreakAll() {
        Debug.LogWarning("Breaking all");
        foreach (ShipSystem ss in systems) {
            if (ss.unbreakable) continue;
            while (!ss.broken)
                ss.TakeDamage(1);
        }
    }

    void OnTriggerEnter(Collider col) {
        int damageDealt = 0;
        switch (col.gameObject.tag) {
            case "Bullet":
            case "Bullet1":
            case "Bullet2":
                damageDealt = 1;
                break;

            default:
                damageDealt = 0;
                break;
        }
        systems.Shuffle().First().TakeDamage(damageDealt);
    }

    void OnCollisionEnter(Collision col) {
        int damageDealt = 0;
        switch (col.gameObject.tag) {
            case "Asteroid":
                damageDealt = 2;
                break;

            default:
                damageDealt = 0;
                break;
        }
        systems.Shuffle().First().TakeDamage(damageDealt);
    }
}
