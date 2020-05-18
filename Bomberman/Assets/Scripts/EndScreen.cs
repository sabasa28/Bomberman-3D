using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        switch (GameManager.Get().gameState)
        {
            case GameManager.GameState.won:
                textMeshPro.text = "YOU WON!";
                break;
            case GameManager.GameState.lost:
                textMeshPro.text = "Game over...";
                break;
        }
    }
}
