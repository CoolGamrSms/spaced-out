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

public class DamageController : MonoBehaviour {
	public GameObject shipInterior;

	ShipSystem[] systems;
    void Awake() {
        systems = shipInterior.GetComponentsInChildren<ShipSystem>();
        foreach (ShipSystem ss in systems) ss.sc = GetComponent<ShipController>();
    }

    //For testing
    public void BreakAll()
    {
        Debug.LogWarning("Breaking all");
        foreach (ShipSystem ss in systems)
            while (!ss.broken)
                ss.TakeDamage(1);
    }

    void OnCollisionEnter(Collision col) {
        int damageDealt;
        switch (col.gameObject.tag) {
            case "Asteroid":
                damageDealt = 2;
                break;

            case "Bullet":
            case "Bullet1":
            case "Bullet2":
                damageDealt = 1;
                break;

            default:
                damageDealt = 0;
                break;
        }

        //Deal damage to the first thing that is not broken
        foreach (ShipSystem ss in systems.Shuffle()) {
            if (!ss.broken) {
                ss.TakeDamage(damageDealt);
                return;
            }
        }
    }
}
