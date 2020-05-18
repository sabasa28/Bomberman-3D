using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkWin : MonoBehaviour
{
    public Action checkIfWin;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkIfWin();
        }
    }
}
