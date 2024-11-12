using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField]
    private PlayerAbility.Ability ability;
    
    public bool IsUnlocked;

    public void UnlockPlayer()
    {
        IsUnlocked = true;
    }

    public PlayerAbility.Ability GetAbility()
    {
        return ability;
    }
}
