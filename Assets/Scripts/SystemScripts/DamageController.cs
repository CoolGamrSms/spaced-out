using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

        List<ShipSystem> options = new List<ShipSystem>();
        foreach (ShipSystem ss in systems)
        {
            if (!ss.broken && !ss.unbreakable && !hulls.Contains(ss))
            {
                options.Add(ss);
            }
        }
        if (options.Count > 0) target = options[Random.Range(0, options.Count)];
        Debug.Log(target.gameObject.name);

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

                bullet.bounce = true;
                bullet.speed *= -.7f;
                col.transform.Rotate(col.transform.up, Random.Range(-30f, 30f));
                col.transform.Rotate(col.transform.right, Random.Range(-30f, 30f));
				return;
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
                    break;
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
        
        if (target.broken) {
            //Select a new target (if possible)
            List<ShipSystem> options = new List<ShipSystem>();
            foreach (ShipSystem ss in systems) { 
                if (!ss.broken && !ss.unbreakable && !hulls.Contains(ss)) {
                    options.Add(ss);
                }
            }
            if(options.Count > 0) target = options[Random.Range(0, options.Count)];
        }

        target.TakeDamage(damage);
    }
}
