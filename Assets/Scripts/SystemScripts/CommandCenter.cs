using UnityEngine;
using System.Collections;

public class CommandCenter : ShipSystem {

    const int commandHealth = 3;
    protected override void Start() {
        base.Start();
        health = commandHealth;
    }

	protected override void Break ()
	{
		base.Break ();
		//change material to all black for indication of broken
		//reverse or remove pilot controls?
	}

    protected override void ResetHealth() {
        health = commandHealth;
    }
}
