using UnityEngine;
using System.Collections;

public enum Systems {
    Hull,
    Turret,
    CommandCenter,
    GravityGenerator,
    Engine,
    LifeSupport,
};

public class SystemController : MonoBehaviour {
    ShipSystem Hull;
    ShipSystem Turret;
    ShipSystem CommandCenter;
    ShipSystem GravityGenerator;
    ShipSystem Engine;
    ShipSystem LifeSupport;

    MinMax mmHull;
    MinMax mmTurret;
    MinMax mmCommandCenter;
    MinMax mmGravityGenerator;
    MinMax mmEngine;
    MinMax mmLifeSupport;

    void Start() {
        mmHull = new MinMax(80.0f, 110.0f);
        mmTurret = new MinMax(60.0f, 80.0f);
        mmCommandCenter = new MinMax(40.0f, 60.0f);
        mmGravityGenerator = new MinMax(20.0f, 40.0f);
        mmEngine = new MinMax(5.0f, 20.0f);
        mmLifeSupport = new MinMax(0.0f, 5.0f);

        foreach (ShipSystem system in GetComponentsInChildren<ShipSystem>()) {
            if (system.gameObject.CompareTag(Systems.Hull.ToString())) {
                Hull = system;
            }
            else if (system.gameObject.CompareTag(Systems.Turret.ToString())) {
                Turret = system;
            }
            else if (system.gameObject.CompareTag(Systems.CommandCenter.ToString())) {
                CommandCenter = system;
            }
            else if (system.gameObject.CompareTag(Systems.GravityGenerator.ToString())) {
                GravityGenerator = system;
            }
            else if (system.gameObject.CompareTag(Systems.Engine.ToString())) {
                Engine = system;
            }
            else if (system.gameObject.CompareTag(Systems.LifeSupport.ToString())) {
                LifeSupport = system;
            }
        }
    }


    void OnCollisionEnter(Collision col) {
        int damageDealt;
        switch (col.gameObject.tag) {
            case "Asteroid":
                damageDealt = 2;
                break;

            case "Bullet":
                damageDealt = 1;
                break;

            default:
                damageDealt = 0;
                break;
        }

        ShipSystem baseHit = null;
        float systemToDamage = Random.Range(0.0f, 111.0f);

        if (mmHull.InRange(systemToDamage)) {
            baseHit = Hull;
        }
        else if (mmTurret.InRange(systemToDamage)) {
            baseHit = Turret;
        }
        else if (mmCommandCenter.InRange(systemToDamage)) {
            baseHit = CommandCenter;
        }
        else if (mmGravityGenerator.InRange(systemToDamage)) {
            baseHit = GravityGenerator;
        }
        else if (mmEngine.InRange(systemToDamage)) {
            baseHit = Engine;
        }
        else if (mmLifeSupport.InRange(systemToDamage)) {
            baseHit = LifeSupport;
        }
        baseHit.TakeDamage(damageDealt);
    }
}

internal class MinMax {
    public bool InRange(float value) {
        return min <= value && value < max;
    }

    public MinMax(float min_, float max_) {
        min = min_;
        max = max_;
    }
    float min;
    float max;
}
