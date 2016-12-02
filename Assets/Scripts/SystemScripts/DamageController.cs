using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DamageController : MonoBehaviour {

    public GameObject shipInterior;

    List<ShipSystem> systems;

    void Awake() {
        systems = shipInterior.GetComponentsInChildren<ShipSystem>().ToList();
        foreach (ShipSystem ss in systems) ss.sc = GetComponent<ShipController>();
    }

    // Test
    public void BreakAll() {
        Debug.LogWarning("Breaking all");
        foreach (ShipSystem ss in systems) {
            if (ss.unbreakable) continue;
            while (!ss.broken)
                ss.TakeDamage(1);
        }
    }

    void OnTriggerEnter(Collider col) {
        //terrible shield code
        if (GetComponent<ShipController>().shield.enabled) {
            if(col.gameObject.GetComponent<TurretBullet>() != null) {
                if (col.gameObject.GetComponent<TurretBullet>().bounce) {
                    return;
                }

                if(GetComponent<ShipController>().power > ShipController.reflectDrain) {
                    GetComponent<ShipController>().power -= ShipController.reflectDrain;
                    col.gameObject.GetComponent<TurretBullet>().bounce = true;
                    col.gameObject.GetComponent<TurretBullet>().speed *= -.7f;
                    col.transform.Rotate(col.transform.up, Random.Range(-30f, 30f));
                    col.transform.Rotate(col.transform.right, Random.Range(-30f, 30f));
                }
                else
                {
                    GetComponent<ShipController>().shield.enabled = false;
                }
            }
        }
        //shield code ends here

        int damageDealt = 0;

        switch (col.gameObject.tag) {
            case "Bullet":
            case "Bullet1":
            case "Bullet2":
                damageDealt = 2;
                Destroy(col.gameObject);
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
                damageDealt = 4;
                break;
            default:
                damageDealt = 0;
                break;
        }

        systems.Shuffle().First().TakeDamage(damageDealt);
    }
}
