using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3rdPersonControl : MonoBehaviour
{
    public GameObject bomb;
    public float speed;
    float hor;
    float ver;
    void Start()
    {
        
    }

    //SIGUIENTE A HACER, HACER QUE SE MUEVA POR LAS LINEAS Y QUE SI ESTA EN UN CUADRADO QUE PUEDA MOVERSE PARA ABAJO HAGA UN LERP PARA CENTRARSE MIENTRAS SE CENTRA O ALGO ASÍ

    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        if (hor != 0) ver = 0;

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float aux1 = (transform.position.x+5) % 10;
            if (aux1 > 5) aux1 = transform.position.x + (10.0f - aux1);
            else aux1 = transform.position.x - aux1;

            float aux2 = (transform.position.z+5) % 10;
            if (aux2 > 5) aux2 = transform.position.z + (10.0f - aux2);
            else aux2 = transform.position.z - aux2;
            
            Instantiate(bomb, new Vector3(aux1,2.5f,aux2), Quaternion.identity);
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


}
