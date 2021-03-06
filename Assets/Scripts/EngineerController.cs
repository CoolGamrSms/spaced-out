using UnityEngine;
using System.Collections;
using InControl;

public class EngineerController : Engineer {
    public float moveSpeed = 1f;
    public float strafeSpeed = 1f;
    public float lookSpeed = 1f;

    private Vector3 moveDir;
    private Vector3 lookDir;
    public ShipController sc;
    GameObject interaction;
    bool gravity = true;
    float gravityValue = 0f;

	private Animator anim;
	private CharacterController cc;
	private AudioSource wrenchSound;

	void Start(){
		anim = GetComponent<Animator> ();
		cc = GetComponent<CharacterController>();
		wrenchSound = GetComponent<AudioSource> ();
	}

    void Update() {
        transform.Rotate(-eController.RightStickY.Value * lookSpeed, eController.RightStickX.Value * lookSpeed, 0);

        if (gravity) transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (sc.transform.rotation.eulerAngles.z) % 360);

        if (cc.isGrounded || !gravity) gravityValue = 0f;
        else gravityValue -= 9.8f * Time.deltaTime;

        Vector3 speed;

        if (gravity) speed = eController.LeftStickX.Value * transform.right * strafeSpeed
                           + eController.LeftStickY.Value * Vector3.ProjectOnPlane(transform.forward, Vector3.up) * moveSpeed
                           + Vector3.up * gravityValue;

        else speed = eController.LeftStickX.Value * transform.right * strafeSpeed
                   + eController.LeftStickY.Value * transform.forward * moveSpeed
                   + Vector3.up * gravityValue;

        float xRot = transform.eulerAngles.x;
        xRot -= (xRot > 50) ? 360f : 0f; // Euler angles doesn't like negatives
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        xRot += (xRot < 0) ? 360f : 0f;
        transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
        cc.Move(speed * Time.deltaTime);
    }

    void FixedUpdate() {
        Debug.DrawRay(transform.position + Vector3.up * 0.75f, transform.forward * 6f, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.75f, transform.forward, out hit, 6f)) {
			if (eController.AnyButtonWasPressed) {
				Debug.Log ("wrench swing");
				anim.ResetTrigger ("hit");
				anim.SetTrigger ("hit");
			}
			
			if (hit.collider.gameObject == interaction){
                return;
			}
            ShipSystem shipSystem = hit.collider.gameObject.GetComponent<ShipSystem>();
            
            if (shipSystem != null) {
                if (interaction != null) {
                    EndInteracting();
                }
                
                if (!shipSystem.GetType().Equals(typeof(Turret)) && shipSystem.broken)
                    StartInteractingWrench(hit.collider.gameObject);
                else
                    StartInteracting(hit.collider.gameObject);
            }
            else if (interaction != null) {
                EndInteracting();
            }
        }
        else if (interaction != null) {
            EndInteracting();
        }


    }

    void StartInteracting(GameObject go) {
        interaction = go;
        interaction.GetComponent<ShipSystem>().StartInteraction();
    }

    void StartInteractingWrench(GameObject go) {
        interaction = go;
        interaction.GetComponent<ShipSystem>().StartInteraction();
		anim.SetBool("interacting", true);
    }

    void EndInteracting() {
        interaction.GetComponent<ShipSystem>().EndInteraction();
        interaction = null;
		anim.SetBool ("interacting", false);
    }

    public void LoseGravity() {
        moveSpeed *= .5f;
        strafeSpeed *= .5f;
        gravity = false;
        sc.BreakGravityGenerator();
    }

    public void ResumeGravity() {
        moveSpeed *= 2f;
        strafeSpeed *= 2f;
        gravity = true;
    }

	public void PlayWrenchSFX(){
		wrenchSound.Play ();
	}
}
