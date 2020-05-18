using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Vector3 movement = Vector3.zero;
    Rigidbody rb;
    bool moving;
    public float rayDistance = 10;
    public LayerMask rayCastLayer;
    RaycastHit hit;
    public Action updateEnemyAmount;

    bool left = true;
    bool right = true;
    bool back = true;
    bool front = true;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        StartCoroutine(DelayToMove());
    }
    void Update()
    {
        if (moving)
        {
            transform.position += movement * speed * Time.deltaTime;
            float aux1 = (transform.position.z + 5) % 20;
            if (aux1 > 15 || aux1 < 5)
            {
                if (aux1 > 10.0f && movement.z == 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, (transform.position.z) + (20.0f - aux1)), 0.05f);
                if (aux1 <= 10.0f && movement.z == 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, (transform.position.z) - aux1), 0.05f);
            }

            aux1 = (transform.position.x + 5) % 20;
            if (aux1 > 15 || aux1 < 5)
            {
                if (aux1 > 10.0 && movement.x == 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (20.0f - aux1), transform.position.y, transform.position.z), 0.05f);
                if (aux1 <= 10.0f && movement.x == 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - aux1, transform.position.y, transform.position.z), 0.05f);
            }
        }
    }

    IEnumerator DelayToMove()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(SetMovingDirection());
    }

    IEnumerator SetMovingDirection()
    {
        moving = true;
        movement = new Vector3(1, 0, 0);
        while (true)
        {
            left = true;
            right = true;
            back = true;
            front = true;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
                if (hit.transform.CompareTag("Wall")) left = false;
            }
            else
            {
                left = true;
            }
            if (Physics.Raycast(transform.position, -transform.forward, out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, -transform.forward * hit.distance, Color.yellow);
                if (hit.transform.CompareTag("Wall")) right = false;
            }
            else
            {
                right = true;
            }
            if (Physics.Raycast(transform.position, transform.right, out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, transform.right * hit.distance, Color.yellow);
                if (hit.transform.CompareTag("Wall")) front = false;
            }
            else
            {
                front = true;
            }
            if (Physics.Raycast(transform.position, -transform.right, out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, -transform.right * hit.distance, Color.yellow);
                if (hit.transform.CompareTag("Wall")) back = false;
            }
            else
            {
                back = true;
            }

            bool dirSelected = false;

            if (front == true || back == true || right == true || left == true)
            {
                do
                {
                    int rand = UnityEngine.Random.Range(0, 4);
                    switch (rand)
                    {
                        case 0:
                            if (front)
                            {
                                movement = new Vector3(1, 0, 0);
                                dirSelected = true;
                            }
                            break;
                        case 1:
                            if (back)
                            {
                                movement = new Vector3(-1, 0, 0);
                                dirSelected = true;
                            }
                            break;
                        case 2:
                            if (left)
                            {
                                movement = new Vector3(0, 0, 1);
                                dirSelected = true;
                            }
                            break;
                        case 3:
                            if (right)
                            {
                                movement = new Vector3(0, 0, -1);
                                dirSelected = true;
                            }
                            break;
                    }
                } while (!dirSelected);
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag(tag))
        {
            movement = -movement;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explotion"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        updateEnemyAmount();
    }
}
