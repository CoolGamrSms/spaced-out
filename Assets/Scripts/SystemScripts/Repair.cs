using UnityEngine;
using System.Collections;
using System;

public class Repair : Engineer {

    virtual public void SetBroken() { }

    virtual public void StartInteraction() { }

    virtual public void EndInteraction() { }
}
