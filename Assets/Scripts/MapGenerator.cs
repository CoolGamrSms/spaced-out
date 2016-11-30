using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public float spacing = 200f;
    public int rings = 15;
    public float frequency = 6f;
    public float amp = 150f;
    public GameObject ring;
    public GameObject asteroid;
    public GameObject goal;

	// Use this for initialization
	void Start () {
        GameObject prev = goal;
	    for(int i = rings-1; i >= 0; --i)
        {
            GameObject go = Instantiate(ring);
            go.transform.position = transform.position + new Vector3((i > (rings / 3)) ? amp * Mathf.Sin((i - (rings / 3)) / frequency) : 0f, amp *Mathf.Sin(i / frequency),  i* spacing);
            go.transform.LookAt(prev.transform);
            go.GetComponent<BoosterRing>().nextRing = prev;
            go.GetComponent<BoosterRing>().asteroid = asteroid;
            prev = go;
        }
        GetComponentInChildren<BoosterRing>().nextRing = prev;
        GetComponentInChildren<BoosterRing>().asteroid = asteroid;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
