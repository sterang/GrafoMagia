using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearFirst : MonoBehaviour {


    LineRenderer lr;
    int global = 0;
    private bool presionandoMouse;
    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, this.transform.position);
        StartCoroutine(DetectarCamino());
    }
    private IEnumerator DetectarCamino()
    {
        while (true)
        {
            if (presionandoMouse)
            {
                Vector3 nuevoPunto = CalcularPunto();
                //Vector3 puntoalterno;
                //puntoalterno.x = 6;
                Debug.Log("1 "+nuevoPunto.y);
                if (nuevoPunto.y < 3.2 && nuevoPunto.y > -1.1)
                {
                        nuevoPunto.x = -6;
                }
                Debug.Log("2 "+nuevoPunto.x);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, nuevoPunto);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private Vector3 CalcularPunto()
    {
        Vector3 mp = Input.mousePosition;
        //Es necesario poner el valor z de la posición del mouse a un valor tal que sea la distancia entre la cámara y donde queremos poner el punto
        mp.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mp);
    }
    void OnMouseDown()
    {
        Debug.Log("Click on De point");
        global = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (global == 0)
        {

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                presionandoMouse = true;
            }
            else
            {
                presionandoMouse = false;
            }
        }
    }
}
