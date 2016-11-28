using UnityEngine;
using System.Collections;

public class Type : MonoBehaviour {
 
	public float letterPause = 0.2f;
	public AudioClip sound;
 
	string message;

	void Start () {
		message = GetComponent<GUIText>().text;
		GetComponent<GUIText>().text = "";
		StartCoroutine(TypeText ());
	}
 
	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			GetComponent<GUIText>().text += letter;
			if (sound)
				GetComponent<AudioSource>().PlayOneShot (sound);
				yield return 0;
			yield return new WaitForSeconds (letterPause);
		}      
	}
}
