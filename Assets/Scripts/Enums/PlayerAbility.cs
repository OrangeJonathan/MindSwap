using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public enum Ability
    {
        None = -1,
        PickUp = 0,
        HighJump = 1,
        Electrician = 2,
    }
    
    public override string ToString()
    {
        return Regex.Replace(ToString(), "([a-z])([A-Z])", "$1 $2");
    }
}
