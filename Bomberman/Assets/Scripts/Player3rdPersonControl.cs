using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3rdPersonControl : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    //SIGUIENTE A HACER, HACER QUE SE MUEVA POR LAS LINEAS Y QUE SI ESTA EN UN CUADRADO QUE PUEDA MOVERSE PARA ABAJO HAGA UN LERP PARA CENTRARSE MIENTRAS SE CENTRA O ALGO ASÍ

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hor, 0, ver) * speed;
        transform.position += movement * Time.deltaTime;

        Debug.Log("HOR = "+hor);
        Debug.Log("VER = " + ver);
    }
}
