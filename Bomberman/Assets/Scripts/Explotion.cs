using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public GameObject explotion;
    public int explotionsLeft = 0;
    public enum Side
    { 
        left,
        right,
        forward,
        back
    }
    public Side sideToInsantiate = Side.left;
    void Start()
    {
        StartCoroutine(Die());
        StartCoroutine(SpawnNextExplotion());
    }
    IEnumerator SpawnNextExplotion()
    {
        yield return null;
        yield return null;
        if (explotionsLeft > 0)
        {
            switch (sideToInsantiate)
            {
                case Side.left:
                    Explotion temp = Instantiate(explotion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity).GetComponent<Explotion>();
                    temp.explotionsLeft = explotionsLeft - 1;
                    temp.sideToInsantiate = sideToInsantiate;
                    break;
                case Side.right:
                    Explotion temp1 = Instantiate(explotion, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10), Quaternion.identity).GetComponent<Explotion>();
                    temp1.explotionsLeft = explotionsLeft - 1;
                    temp1.sideToInsantiate = sideToInsantiate;
                    break;
                case Side.forward:
                    Explotion temp2 = Instantiate(explotion, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<Explotion>();
                    temp2.explotionsLeft = explotionsLeft - 1;
                    temp2.sideToInsantiate = sideToInsantiate;
                    break;
                case Side.back:
                    Explotion temp3 = Instantiate(explotion, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<Explotion>();
                    temp3.explotionsLeft = explotionsLeft - 1;
                    temp3.sideToInsantiate = sideToInsantiate;
                    break;
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }


}
