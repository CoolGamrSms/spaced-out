using UnityEngine;
using System.Collections;
using System.Linq;

public class DamageController : MonoBehaviour {

    ShipController shipController;
    public GameObject shipInterior;

    Hull[] hulls;

    ShipSystem target;
    ShipSystem[] systems;


    [Range(0, 10)]
    public int bulletDamage = 1;
    [Range(0, 10)]
    public int asteroidDamage = 2;

    [Range(0, 1)]
    public float breachProbability = 0;

    void Awake() {
        hulls = shipInterior.GetComponentsInChildren<Hull>();
        systems = shipInterior.GetComponentsInChildren<ShipSystem>();
        target = systems.First();

        shipController = GetComponent<ShipController>();
        foreach (ShipSystem ss in systems) ss.sc = shipController;
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
        if (col.tag == "Ring") return;
        if (shipController.shield.enabled) {
            TurretBullet bullet = col.gameObject.GetComponent<TurretBullet>();
            if (bullet != null) {
                if (bullet.bounce) {
                    return;
                }

                if (shipController.power > ShipController.reflectDrain) {
                    shipController.power -= ShipController.reflectDrain;
                    bullet.bounce = true;
                    bullet.speed *= -.7f;
                    col.transform.Rotate(col.transform.up, Random.Range(-30f, 30f));
                    col.transform.Rotate(col.transform.right, Random.Range(-30f, 30f));
                }
                else {
                    shipController.shield.enabled = false;
                }
            }
        }
        DamageDistribution(col.gameObject);
    }

    void OnCollisionEnter(Collision col) {
        DamageDistribution(col.gameObject);
    }

    void DamageDistribution(GameObject go) {
        if (Random.value <= breachProbability) {
            foreach (Hull hull in hulls) {
                if (!hull.broken) {
                    hull.BreakSelf();
                    return;
                }
            }
        }

        switch (go.tag) {
            case "Asteroid":
                DealDamage(asteroidDamage);
                break;

            case "Bullet":
            case "Bullet1":
            case "Bullet2":
                Destroy(go);
                DealDamage(bulletDamage);
                break;

            default:
                break;
        }
    }

    void DealDamage(int damage) {
        target.TakeDamage(damage);
        if (target.broken) {
            foreach (ShipSystem ss in systems) {
                if (ss.unbreakable) continue;
                if (!ss.broken) {
                    target = ss;
                    Debug.LogWarning(target.gameObject.name);
                    return;
                }
            }
        }
    }
}
