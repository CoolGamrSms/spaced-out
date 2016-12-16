using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    Vector3 myScaleLoss;
    float myMassLoss;
    public GameObject parRing;
    int hitsToKill;

    void Start() {
        float speed = Random.Range(0f, 8f);
        speed /= GetComponent<Rigidbody>().mass;
        
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*speed;
        GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*8f, ForceMode.Impulse);
        
        int hitsToKill = (int)(6f * GetComponent<Rigidbody>().mass);
        
        myMassLoss = GetComponent<Rigidbody>().mass / hitsToKill;
        myScaleLoss = transform.localScale / hitsToKill;
    }

    void FixedUpdate() {
        if (Vector3.SqrMagnitude(parRing.transform.position - transform.position) > 6000f) {
            GetComponent<Rigidbody>().velocity *= -1f;
        }
    }

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Bullet1" || col.gameObject.tag == "Bullet2") {
            hitsToKill--;

            if (hitsToKill == 0) {
				Destroy(gameObject);
			}
            
            transform.localScale -= myScaleLoss;
            GetComponent<Rigidbody>().mass -= myMassLoss;
        }
	}
}
