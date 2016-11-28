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
        if (GetComponent<ShipController>().shield.enabled)
        {
            if(col.gameObject.GetComponent<TurretBullet>() != null)
            {
                if (col.gameObject.GetComponent<TurretBullet>().bounce) return;
                col.gameObject.GetComponent<TurretBullet>().bounce = true;
                col.gameObject.GetComponent<TurretBullet>().speed *= -.7f;
                col.transform.Rotate(col.transform.up, Random.Range(-30f, 30f));
                col.transform.Rotate(col.transform.right, Random.Range(-30f, 30f));
            }
            return;
        }
        int damageDealt;
        switch (col.gameObject.tag) {
            case "Bullet":
            case "Bullet1":
            case "Bullet2":
                damageDealt = 1;
                Destroy(col.gameObject);
                break;

            default:
                damageDealt = 0;
                break;
        }
        systems.Shuffle().First<ShipSystem>().TakeDamage(damageDealt);
    }

    void OnCollisionEnter(Collision col) {
        if (GetComponent<ShipController>().shield.enabled) return;
        int damageDealt;
        switch (col.gameObject.tag) {
            case "Asteroid":
                damageDealt = 2;
                break;

            default:
                damageDealt = 0;
                break;
        }
        systems.Shuffle().First<ShipSystem>().TakeDamage(damageDealt);
    }
}
