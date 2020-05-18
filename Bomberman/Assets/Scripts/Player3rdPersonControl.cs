﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3rdPersonControl : MonoBehaviour
{
    public Action <bool> updateBombAmount;
    public GameObject bomb;
    public float speed;
    float hor;
    float ver;
    Vector3 initPos;
    Rigidbody rb;
    public int lives = 2;
    public int explotionRatio;
    public int maxBombAmount;
    int bombAmount = 0;
    void Start()
    {
        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        if (hor != 0) ver = 0;

        

        if (Input.GetKeyDown(KeyCode.Space)&&bombAmount<maxBombAmount)
        {
            float aux1 = (transform.position.x+5) % 10;
            if (aux1 > 5) aux1 = transform.position.x + (10.0f - aux1);
            else aux1 = transform.position.x - aux1;

            float aux2 = (transform.position.z+5) % 10;
            if (aux2 > 5) aux2 = transform.position.z + (10.0f - aux2);
            else aux2 = transform.position.z - aux2;
            Bomb tempBomb= Instantiate(bomb, new Vector3(aux1,2.5f,aux2), Quaternion.identity).GetComponent<Bomb>();
            tempBomb.Die = SubtractBombAmount;
            tempBomb.explotionRadio = explotionRatio;
            bombAmount++;
        }

    }
    private void FixedUpdate()
    {
        float aux = (transform.position.z + 5) % 20;

        if (aux > 14 || aux < 6)
        {
            if (aux > 10.0f && ver != 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, (transform.position.z) + (20.0f - aux)), 0.1f);
            if (aux <= 10.0f && ver != 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, (transform.position.z) - aux), 0.1f);
        }
        aux = (transform.position.x + 5) % 20;

        if (aux > 14 || aux < 6)
        {
            if (aux > 10.0f && hor != 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (20.0f - aux), transform.position.y, transform.position.z), 0.1f);
            if (aux <= 10.0f && hor != 0) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - aux, transform.position.y, transform.position.z), 0.1f);
        }
        Vector3 movement = new Vector3(ver, 0, -hor) * speed;
        transform.position += movement * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explotion"))
        {
            transform.position = initPos;
            lives--;
            if (lives == 0) gameOver();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            transform.position = initPos;
            rb.velocity = Vector3.zero;
            lives--;
            if (lives == 0) gameOver();

        }
    }

    void gameOver()
    {
        GameManager.Get().GameOver(0);
    }

    void SubtractBombAmount()
    {
        bombAmount--;
    }
}
