using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Vector3 movement = Vector3.zero;

    private void Start()
    {
        StartCoroutine(DelayToMove());
    }
    void Update()
    {
        transform.position += movement * speed * Time.deltaTime;
    }

    IEnumerator DelayToMove()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(SetMovingDirection());
    }

    IEnumerator SetMovingDirection()
    {
        while (true)
        {
            bool aux = Random.value > 0.5f;
            if (aux)
            {
                aux = Random.value > 0.5f;
                if (aux) movement = new Vector3(1, 0, 0);
                else movement = new Vector3(-1, 0, 0);
            }
            else
            {
                aux = Random.value > 0.5f;
                if (aux) movement = new Vector3(0, 0, 1);
                else movement = new Vector3(0, 0, -1);
            }
            yield return new WaitForSeconds(5);
        }
    }
}
