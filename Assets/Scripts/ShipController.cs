using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class ShipController : MonoBehaviour {

    public int playerNumber;
    InputDevice sController;
    AudioSource shoot;

    public GameObject pBullet;

    float timer = 1f;
    float shieldTimer;
    public float cooldownLimit = .5f;

    public float boostDur = 3f;
    public float boostSpeed = 10f;
    float boostVal;
    float boostTimer;
    bool superboost;

    float mypitch;

    List<Transform> bulletSpawns = new List<Transform>();

    public float speed = 20f;
    float maxSpeed;

    // Engine Fire
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject fire4;

    // Rings
    public GameObject curRing;
    public GameObject nextRing;

    // Controls
    public float rollBack;
    public float rollAngle;
    public float turnSpeed;

    private Vector3 moveDir;
    private Vector3 lookDir;
    private Rigidbody rb;
    Vector3 vel;

    [HideInInspector]
    public MeshRenderer shield;

	CanvasGroup warning;
	Queue<string> activeWarnings = new Queue<string>();
    float warningTimer;

	public int numHullBreaches = 0;
    float hullDamage;

    bool isVibrating = false;
    float timerVibration = 0f;
    float cooldownVibration = .5f;

    Slider powerbar;
    float maxPower = 1000;
    public float power = 0;
    public const float powerRegen = 2f;
    public const float shieldCost = 50f; //Startup cost
    public const float shieldDrain = 1.2f; //Use cost
    public const float shieldCooldown = 2.5f; //Cooldown on regen
	public const float shootCostEngineer = 25f;
	public const float shootCostShip = 50f;
    /*public const float reflectDrain = 200f;
    public const float shootDrain = 50f;
    public const float engineerShootDrain = 30f;*/

	public bool started = false;

    enum EWarning {
        Hullbreach,
        CommandCenter,
        GravityGenerator,
        Engine
    }

    void Awake() {
        //power = maxPower;
        boostTimer = 0f;
        boostVal = 0f;
        warningTimer = 0f;
        superboost = false;
        shield = transform.FindChild("Shield").GetComponent<MeshRenderer>();
        shoot = GetComponent<AudioSource>();
        mypitch = shoot.pitch;
        sController = PlayerInputManager.Instance.controllers[playerNumber];
        shield.enabled = false;
    }

    void Start() {
        nextRing = curRing.GetComponent<BoosterRing>().nextRing;
        maxSpeed = speed;
        rb = GetComponent<Rigidbody>();
        hullDamage = 0f;
        commandCenterBroken = false;

        for (int i = 0; i < transform.childCount; ++i) {
            Transform child = transform.GetChild(i);
            if (child.name == "BulletSpawnPoint") {
                bulletSpawns.Add(child);
            }
        }

        powerbar = GetComponentInChildren<Slider>();
        powerbar.maxValue = maxPower;
        warning = GetComponentInChildren<CanvasGroup>();
    }

    void DisableShield() {
        shield.enabled = false;
    }

    public void StartBoost(bool sup) {
        boostTimer = boostDur;
        superboost = sup;
    }

    void FixedUpdate() {
		if (!started)
			return;

		//Power
        shieldTimer = Mathf.Max(shieldTimer - Time.deltaTime, 0f);
        if(!shield.enabled && shieldTimer == 0f) power += powerRegen;
        else if(shield.enabled) power -= shieldDrain;
        if (power > maxPower) power = maxPower;
        if (power <= 0)
        {
            power = 0;
            if (shield.enabled)
            {
                shield.enabled = false;
                shieldTimer = shieldCooldown;
            }
        }

        // Lerp power bar
        powerbar.value = Mathf.MoveTowards(powerbar.value, power, 5f);

        // Show warnings
        warningTimer -= Time.deltaTime;

        if (warningTimer < 0) warningTimer = 0;

        if (warningTimer == 0) {
            if (activeWarnings.Count > 0) {
                warningTimer = 1.5f;
                warning.GetComponentInChildren<Text>().text = activeWarnings.Dequeue();
                warning.alpha = 1f;
            }
            else {
                warning.alpha = 0f;
            }
        }

        // Handle rings
        if (nextRing != null && Vector3.SqrMagnitude(transform.position - curRing.transform.position) > Vector3.SqrMagnitude(transform.position - nextRing.transform.position)) {
            curRing = nextRing;

            if (curRing.GetComponent<BoosterRing>() != null) {
                nextRing = curRing.GetComponent<BoosterRing>().nextRing;
            }
            else nextRing = null;
        }

        //if (sController.DPadUp.WasPressed) {
        //    gameObject.GetComponent<DamageController>().BreakAll();
        //}

        // Shield
        if(sController.Action2.WasPressed && !commandCenterBroken)
        {
            if(!shield.enabled)
            {
                if (power <= shieldCost) return;
                power -= shieldCost;
            }
            shield.enabled = !shield.enabled;
            if (!shield.enabled) shieldTimer = shieldCooldown;
        }

        // Boost
        if (boostTimer > 0) {
            boostTimer -= Time.deltaTime;
            boostVal += Time.deltaTime * boostSpeed;
            if (boostVal > boostSpeed) boostVal = boostSpeed;

            fire1.transform.localScale = new Vector3(2, 2, 2);
            fire2.transform.localScale = new Vector3(2, 2, 2);
            fire3.transform.localScale = new Vector3(2, 2, 2);
            fire4.transform.localScale = new Vector3(2, 2, 2);
        }
        else {
            boostTimer = 0;
			superboost = false;
            boostVal -= Time.deltaTime * boostSpeed / 3f;
            if (boostVal < 0) boostVal = 0;

            fire1.transform.localScale = new Vector3(1, 1, 1);
            fire2.transform.localScale = new Vector3(1, 1, 1);
            fire3.transform.localScale = new Vector3(1, 1, 1);
            fire4.transform.localScale = new Vector3(1, 1, 1);
        }

        // Fire
		if (sController.Action1.IsPressed && timer > cooldownLimit && !commandCenterBroken && power > shootCostShip) {
			power -= shootCostShip;
			shoot.pitch = mypitch + Random.Range (-0.1f, 0);
            shoot.Play();

            foreach (Transform pos in bulletSpawns) {
                GameObject bullet = Instantiate(pBullet);
                bullet.transform.rotation = pos.rotation;
                bullet.transform.position = pos.position;
				bullet.GetComponent<TurretBullet>().shipV = GetComponent<Rigidbody>().velocity;
            }

            timer = 0f;
        }

        timer += Time.deltaTime;

        rb.AddRelativeTorque(sController.LeftStickY.Value * turnSpeed, 0, 0); // W key or the up arrow to turn upwards, S or the down arrow to turn downwards. 
        rb.AddRelativeTorque(0, sController.LeftStickX.Value * turnSpeed, 0); // A or left arrow to turn left, D or right arrow to turn right. 

        rb.AddForce(transform.forward * Mathf.Max(0f, speed - hullDamage + boostVal + (superboost ? 5f : 0f)) * (shield.enabled ? 0.9f : 1f), ForceMode.VelocityChange);

        Quaternion q = transform.rotation;
        q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, -sController.LeftStickX.Value * rollAngle);
        Quaternion rot = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rollBack);
        transform.rotation = rot;

        HandleVibration();
    }

    public void BreakVibration() {
        isVibrating = true;
    }

    void HandleVibration() {
        if (isVibrating) {
            sController.Vibrate(100.0f);
            timerVibration += Time.deltaTime;
        }

        if (timerVibration > cooldownVibration) {
            sController.StopVibration();
            timerVibration = 0f;
            isVibrating = false;
        }
    }

    public void HullBreach() {
        hullDamage += maxSpeed * .075f;
        ShowWarning("Hull breached!");
		++numHullBreaches;
    }

    public void FixBreach() {
        hullDamage -= maxSpeed * .075f;
		--numHullBreaches;
    }

    public void BreakEngine() {
        speed = maxSpeed * .5f;
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            ps.Stop();
        }
        ShowWarning("Engine offline!");
    }

    public void FixEngine() {
        speed = maxSpeed;
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            ps.Play();
        }
    }

    public void BreakCommandCenter() {
        commandCenterBroken = true;
        rollAngle /= 3;
        turnSpeed /= 3;
        if (shield.enabled)
        {
            shield.enabled = false;
            shieldTimer = shieldCooldown;
        }
        ShowWarning("Command center broken!");
    }

    public void FixCommandCenter() {
        commandCenterBroken = false;
        rollAngle *= 3;
        turnSpeed *= 3;
    }

    public bool commandCenterBroken {
        get; private set;
    }

    public void BreakGravityGenerator() {
        ShowWarning("Gravity generator damaged!");
    }

    void ShowWarning(string msg) {
        activeWarnings.Enqueue(msg);
    }

}
