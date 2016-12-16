using UnityEngine;
using System.Collections;

public class EngineStart : ShipSystem {
    public GameObject cap;
    public GameObject ship;

    AudioSource open;

	static int numReady = 0;

    [Range(0,100)]
    public float shipSpeed = 20;
	bool startedOnce = false;

    protected override void Start() {
        numReady = 0;
        base.Start();
        open = GetComponent<AudioSource>();
        unbreakable = true;
		foreach (ParticleSystem ps in ship.GetComponentsInChildren<ParticleSystem>()) {
			ps.Stop();
		}
        BreakSilently();
    }

    protected override void ResetHealth() {
        Destroy(cap);
		++numReady;
		foreach (ParticleSystem ps in ship.GetComponentsInChildren<ParticleSystem>()) {
			ps.Play();
		}
        open.Play();
    }

	void FixedUpdate(){
		if (numReady == 2) {
			if(!startedOnce){
				StartCountDown ();
			}
			startedOnce = true;
		}
	}

	void StartCountDown(){
		ship.GetComponent<Animator>().SetBool("startCount", true);
	}

}
