using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void SwapToMenuScene()
    {
        GameManager.Get().GoToMenu();
    }
}
