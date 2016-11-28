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
	public float cooldownLimit = 1f;

	List<Transform> bulletSpawns = new List<Transform>();

    public float speed;
	float maxSpeed;

    public float rollBack;
    public float rollAngle;
    public float turnSpeed;
    float hullDamage;

    private Vector3 moveDir;
    private Vector3 lookDir;
    private Rigidbody rb;
    Vector3 vel;

    public bool commandCenterBroken {
        get; private set;
    }

	CanvasGroup[] warnings;
	Slider powerbar;
	int powerup = 25;

	enum EWarning {
		Hullbreach,
		CommandCenter,
		GravityGenerator,
		Engine
	}

    void Awake() {
        shoot = GetComponent<AudioSource>();
        sController = PlayerInputManager.Instance.controllers[playerNumber];
    }

    void Start () {
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

		powerbar = GetComponentInChildren<Slider> ();
		warnings = GetComponentsInChildren<CanvasGroup> ();
		print (warnings.Length);
    }

    void FixedUpdate() {
        if (sController.DPadUp.WasPressed) {
            gameObject.GetComponent<DamageController>().BreakAll();
        }

		if (sController.Action1.IsPressed && timer > cooldownLimit) {
            shoot.Play();
			foreach (Transform pos in bulletSpawns) {
				GameObject bullet = Instantiate(pBullet);
				bullet.transform.rotation = pos.rotation;
				bullet.transform.position = pos.position;
			}
			timer = 0f;
		}
		timer += Time.deltaTime;

        rb.AddRelativeTorque(sController.LeftStickY.Value * turnSpeed, 0, 0); // W key or the up arrow to turn upwards, S or the down arrow to turn downwards. 
        rb.AddRelativeTorque(0, sController.LeftStickX.Value * turnSpeed, 0); // A or left arrow to turn left, D or right arrow to turn right. 

        rb.AddForce(transform.forward * Mathf.Max(0f, speed - hullDamage), ForceMode.VelocityChange);

        Quaternion q = transform.rotation;
        q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, -sController.LeftStickX.Value * rollAngle);
        Quaternion rot = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rollBack);
        transform.rotation = rot;
    }

	void OnTriggerEnter(Collider coll){
		print ("powering");
		if (coll.CompareTag ("Ring")) {
			if (powerbar.value + powerup <= powerbar.maxValue) {
				powerbar.value += powerup;
			} else {
				powerbar.value = powerbar.maxValue;
			}
		}
	}

    public void HullBreach() {
        hullDamage += maxSpeed*.1f;
		ShowWarning ((int)EWarning.Hullbreach);
    }

    public void FixBreach() {
        hullDamage -= maxSpeed * .1f;
    }

	public void BreakEngine(){
		speed = maxSpeed * .5f;

		ShowWarning ((int)EWarning.Engine);
	}

	public void FixEngine(){
		speed = maxSpeed;
    }

    public void BreakCommandCenter() {
        commandCenterBroken = true;
        rollAngle /= 3;
        turnSpeed /= 3;

		ShowWarning ((int)EWarning.CommandCenter);
    }

    public void FixedCommandCeneter() {
        commandCenterBroken = false;
        rollAngle *= 3;
        turnSpeed *= 3;
    }

	public void BreakGravityGenerator(){
		ShowWarning ((int)EWarning.GravityGenerator);
	}

	void ShowWarning(int warningNum){
		warnings [warningNum].alpha = 1f;
		StartCoroutine (RemoveWarning (warningNum));
	}

	IEnumerator RemoveWarning(int warningNum){
		yield return new WaitForSeconds (1.5f);
		warnings[warningNum].alpha = 0f;
	}
}
