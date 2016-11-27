using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyCheck : MonoBehaviour {
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    //public GameObject readyPanel;
    Color readyGreen;
    Color readyBlue;

    // Use this for initialization
    void Start() {
        readyGreen = new Color(0, 255 , 51, 255);
        readyBlue = new Color(0, 12, 255, 255);
    }

    float timer = 0;
    float timeToReadyScreen = 2;
    // Update is called once per frame
    void Update() {
        if(PlayerInputManager.Instance.controllers[0] != null)
            player0.GetComponentInChildren<Image>().color = readyGreen;
        if (PlayerInputManager.Instance.controllers[1] != null)
            player1.GetComponentInChildren<Image>().color = readyGreen;
        if (PlayerInputManager.Instance.controllers[2] != null)
            player2.GetComponentInChildren<Image>().color = readyGreen;
        if (PlayerInputManager.Instance.controllers[3] != null)
            player3.GetComponentInChildren<Image>().color = readyGreen;
    }
}
