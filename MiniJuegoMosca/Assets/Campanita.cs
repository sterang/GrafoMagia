using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Campanita : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    LineRenderer lr;
    /// <summary>
    /// 
    /// </summary>
    private bool presionandoMouse;

    // Use this for initialization
    /// <summary>
    /// 
    /// </summary>
    void Start ()
    {
        ///
        lr = GetComponent<LineRenderer>();
        ///
        lr.SetPosition(0, this.transform.position);
        ///
        StartCoroutine(DetectarCamino());

	}
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DetectarCamino()
    {
        ///
        while (true)
        {
            ///
            if(presionandoMouse)
            {
                ///
                Vector3 nuevoPunto = CalcularPunto();
                ///
                lr.positionCount++;
                ///
                lr.SetPosition(lr.positionCount - 1, nuevoPunto);
            }
            ///
            yield return new WaitForSeconds(0.2f);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 CalcularPunto()
    {
        ///
        Vector3 mp = Input.mousePosition;
        //Es necesario poner el valor z de la posición del mouse a un valor tal que sea la distancia entre la cámara y donde queremos poner el punto
        mp.z = -Camera.main.transform.position.z;
        ///
        return Camera.main.ScreenToWorldPoint(mp);
    }

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update ()
    {
        ///
        if (Input.GetMouseButton(0))
        {
            ///
            presionandoMouse = true;
        }
        ///
        else
        {
            ///
            presionandoMouse = false;
        }
	}

    //TODO: Hacer que campanita recorra el camino trazado 
    //TODO: Hacer que cuando campanita llegue a la casita, salga un mensaje de felicitaciones
}
