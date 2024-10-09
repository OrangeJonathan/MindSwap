using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNewPlayer : MonoBehaviour
{
    [SerializeField]
    private MindSwap mindSwap;
    
    [SerializeField]
    private GameObject newPlayer;


    public void NewPlayerUnlock()
    {
        mindSwap.players.Add(newPlayer);
    }

}
