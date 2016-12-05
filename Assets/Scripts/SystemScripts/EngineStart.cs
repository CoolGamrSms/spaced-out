using UnityEngine;
using System.Collections;

public class EngineStart : ShipSystem {
    public GameObject cap;
    public GameObject ship;

    AudioSource open;

    [Range(0,100)]
    public float shipSpeed = 40;
    
    protected override void Start() {
        base.Start();
        open = GetComponent<AudioSource>();
        unbreakable = true;
        BreakSilently();
    }

    protected override void ResetHealth() {
        Destroy(cap);
        ship.GetComponent<ShipController>().speed = shipSpeed;
        open.Play();
    }
}
