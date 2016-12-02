using UnityEngine;
using System.Collections;
using System.Linq;

public class DamageController : MonoBehaviour {

    public GameObject shipInterior;

    Hull[] hulls;

    ShipSystem target;
    ShipSystem[] systems;

    [Range(0, 1)]
    public float breachProbability = 0;

    void Awake() {
        hulls = shipInterior.GetComponentsInChildren<Hull>();
        systems = shipInterior.GetComponentsInChildren<ShipSystem>();
        foreach (ShipSystem ss in systems) ss.sc = GetComponent<ShipController>();
        target = systems.OrderBy(x => System.Guid.NewGuid()).First();
    }

    public void BreakAll() {
        Debug.LogWarning("Breaking all");
        foreach (ShipSystem ss in systems) {
            if (ss.unbreakable) continue;
            while (!ss.broken)
                ss.TakeDamage(1);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (GetComponent<ShipController>().shield.enabled) {
            if (col.gameObject.GetComponent<TurretBullet>() != null) {
                if (col.gameObject.GetComponent<TurretBullet>().bounce) {
                    return;
                }

                if (GetComponent<ShipController>().power > ShipController.reflectDrain) {
                    GetComponent<ShipController>().power -= ShipController.reflectDrain;
                    col.gameObject.GetComponent<TurretBullet>().bounce = true;
                    col.gameObject.GetComponent<TurretBullet>().speed *= -.7f;
                    col.transform.Rotate(col.transform.up, Random.Range(-30f, 30f));
                    col.transform.Rotate(col.transform.right, Random.Range(-30f, 30f));
                }
                else {
                    GetComponent<ShipController>().shield.enabled = false;
                }
            }
        }
        DealDamage(col.gameObject);
    }

    void OnCollisionEnter(Collision col) {
        DealDamage(col.gameObject);
    }

    void DealDamage(GameObject go) {
        if (Random.value <= breachProbability) {
            foreach (Hull hull in hulls) {
                if (!hull.broken) {
                    hull.BreakSelf();
                    return;
                }
            }
        }

        int damage = 0;
        switch (go.tag) {
            case "Asteroid":
                damage = 4;
                break;

            case "Bullet":
            case "Bullet1":
            case "Bullet2":
                damage = 2;
                Destroy(go);
                break;

            default:
                damage = 0;
                break;
        }

        target.TakeDamage(damage);
        if (target.broken) {
            foreach (ShipSystem ss in systems.OrderBy(x => System.Guid.NewGuid())) {
                if (!ss.broken) {
                    target = ss;
                    return;
                }
            }
        }
    }
}
