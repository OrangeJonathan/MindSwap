using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField]
    private PlayerAbility.Ability ability;
    
    public PlayerAbility.Ability GetAbility()
    {
        return ability;
    }

}
