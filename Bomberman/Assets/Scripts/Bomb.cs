using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int explotionRadio = 2;
    public GameObject explotion;
    Collider coll;
    public Action Die;
    void Start()
    {
        coll = GetComponent<Collider>();
        StartCoroutine(explode());
    }

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coll.isTrigger = false;
        }

    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(2);

        Instantiate(explotion, transform.position, Quaternion.identity);

        float aux;
        aux = (transform.position.z + 5) % 20;
        if (aux > 15 || aux < 5)
        {
            Explotion temp = Instantiate(explotion, new Vector3(transform.position.x - GameplayController.chunkSize, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<Explotion>();
            temp.explotionsLeft = explotionRadio - 1;
            temp.sideToInsantiate = Explotion.Side.back;

            temp = Instantiate(explotion, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<Explotion>();
            temp.explotionsLeft = explotionRadio - 1;
            temp.sideToInsantiate = Explotion.Side.forward;
        }

        aux = (transform.position.x + 5) % 20;
        if (aux > 15 || aux < 5)
        {
            Explotion temp = Instantiate(explotion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity).GetComponent<Explotion>();
            temp.explotionsLeft = explotionRadio - 1;
            temp.sideToInsantiate = Explotion.Side.left;

            temp = Instantiate(explotion, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10), Quaternion.identity).GetComponent<Explotion>();
            temp.explotionsLeft = explotionRadio - 1;
            temp.sideToInsantiate = Explotion.Side.right;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Die();
    }
}
