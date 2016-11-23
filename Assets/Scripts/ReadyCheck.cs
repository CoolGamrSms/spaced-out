using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadyCheck : MonoBehaviour {
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject readyPanel;
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
        if(PlayerInputManager.Instance.numPlayers == 1)
            player0.GetComponent<Image>().color = readyGreen;
        if (PlayerInputManager.Instance.numPlayers == 2)
            player1.GetComponent<Image>().color = readyGreen;
        if (PlayerInputManager.Instance.numPlayers == 3)
            player2.GetComponent<Image>().color = readyGreen;
        if (PlayerInputManager.Instance.numPlayers == 4)
            player3.GetComponent<Image>().color = readyGreen;
        if(PlayerInputManager.Instance.numPlayers == 4) {
            timer += Time.deltaTime;
            if(timer >=timeToReadyScreen) {
                readyPanel.GetComponent<Image>().color = readyBlue;
                readyPanel.GetComponent<Text>().color = Color.black;
            }
        }
    }
}
