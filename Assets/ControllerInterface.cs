using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class ControllerInterface : MonoBehaviour {

    Vector2 shouldBe;
    RectTransform rt;
    public InputDevice ind;
    Vector2 home;
    float teamHome;
    public int mySlot;

    Image up, down, left, right;

    public enum ContState { MAIN, BLUE, RED };
    public enum RoleState { ENG, PILOT, NONE };

    public ContState cont;
    public RoleState role;

    public void MoveTeam(float hom, int slot)
    {
        teamHome = hom;
        mySlot = slot;
        Vector2 should = new Vector2(teamHome + (mySlot * 2 - 1)* 50 *Mathf.Sign(-teamHome), 0f);
        shouldBe = should;
    }

    public void Home()
    {
        shouldBe = home;
        cont = ContState.MAIN;
    }
    public void TeamHome()
    {
        shouldBe = new Vector2(teamHome + (mySlot*2-1) * 50 * Mathf.Sign(-teamHome), 0f);
        role = RoleState.NONE;
    }

    public void Move(float up)
    {
        shouldBe = new Vector2(teamHome, shouldBe.y + up);
    }

    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
        shouldBe = rt.anchoredPosition;
        home = shouldBe;
        cont = ContState.MAIN;
        role = RoleState.NONE;
        up = transform.FindChild("Up").gameObject.GetComponent<Image>();
        left = transform.FindChild("Left").gameObject.GetComponent<Image>();
        right = transform.FindChild("Right").gameObject.GetComponent<Image>();
        down = transform.FindChild("Down").gameObject.GetComponent<Image>();
        up.enabled = false;
        left.enabled = false;
        right.enabled = false;
        down.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, shouldBe, 10f *Time.deltaTime);
	}

    void FixedUpdate()
    {
        //Render arrows
        left.enabled = role == RoleState.NONE && cont != ContState.BLUE;
        right.enabled = role == RoleState.NONE && cont != ContState.RED;
        up.enabled = cont != ContState.MAIN && role != RoleState.PILOT;
        down.enabled = cont != ContState.MAIN && role != RoleState.ENG;
    }
}
