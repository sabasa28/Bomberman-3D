using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public Material diyingMat;
    MeshRenderer mr;
    bool dying = false;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explotion") && !dying)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        mr.material = diyingMat;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
