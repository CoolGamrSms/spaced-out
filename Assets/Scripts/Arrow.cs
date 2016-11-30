using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    ShipController sc;
    public Camera cam;
    public MonoBehaviour render;

	// Use this for initialization
	void Start () {
        sc = GetComponentInParent<ShipController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 screenPos = cam.WorldToScreenPoint(sc.curRing.transform.position);
        if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            //On screen
            //GetComponent<Renderer>().enabled = false;
            render.enabled = false;
        }
        else {
            //Off screen
            //GetComponent<Renderer>().enabled = true;
            render.enabled = true;
            if (screenPos.z < 0) screenPos *= -1; //Flip if behind camera
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 4;

            screenPos -= screenCenter; //translate coordinates to center (0, 0)

            float angle = Mathf.Atan2(-screenPos.y, -screenPos.x*2)*Mathf.Rad2Deg;
            angle = Mathf.Round(angle / 90) * 90;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        }
	}
}
