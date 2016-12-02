using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public float spacing = 200f;
    public int rings = 15;
    public float frequency = 6f;
    public float amp = 150f;
    public GameObject ring;
    public GameObject goal;
    public GameObject asteroidPrefab;
    public Mesh[] asteroids;

	// Use this for initialization
	void Start () {
        GameObject prev = goal;
	    for(int i = rings-1; i >= 0; --i)
        {
            GameObject go = Instantiate(ring);
            go.transform.position = transform.position + new Vector3((i > (rings / 3)) ? amp * Mathf.Sin((i - (rings / 3)) / frequency) : 0f, amp *Mathf.Sin(i / frequency),  i* spacing);
            go.transform.LookAt(prev.transform);
            go.GetComponent<BoosterRing>().nextRing = prev;
            prev = go;
            int asteroidCount = Random.Range(6, 14);
            for (int j = 0; j < asteroidCount; ++j)
            {
                GameObject ast = Instantiate(asteroidPrefab);
                int x = Random.Range(0, 21);
                ast.GetComponent<MeshFilter>().sharedMesh = asteroids[x];
                ast.GetComponent<MeshCollider>().sharedMesh = asteroids[x];
                ast.transform.position = transform.position + new Vector3((i > (rings / 3)) ? amp * Mathf.Sin((i - (rings / 3)) / frequency) : 0f, amp * Mathf.Sin(i / frequency), i * spacing)
                    +new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*Random.Range(35f, 70f);
                float siz = Random.Range(1f, 3f);
                ast.transform.localScale *= siz;
                ast.GetComponent<Rigidbody>().mass = siz / 3f;
                ast.GetComponent<Asteroid>().parRing = prev;
            }
        }
        GetComponentInChildren<BoosterRing>().nextRing = prev;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
