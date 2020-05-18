using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameplay : MonoBehaviour
{
    public void SwapToGameplayScene()
    {
        GameManager.Get().StartGameplay();
    }
}
