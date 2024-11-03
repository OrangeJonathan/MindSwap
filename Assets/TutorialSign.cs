using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialSign : MonoBehaviour
{

    [SerializeField]
    private MindSwap mindSwapController;

    [SerializeField]
    private GameObject[] tutorialTexts;
    private GameObject currentTutorialText;

    void Start()
    {
        currentTutorialText = tutorialTexts[0];
        mindSwapController.OnChangeAbility += ChangeTutorialText;
    }


    void ChangeTutorialText()
    {
        currentTutorialText.SetActive(false);
        currentTutorialText = tutorialTexts[(int)mindSwapController.activePlayer];
        currentTutorialText.SetActive(true);
    }


}
