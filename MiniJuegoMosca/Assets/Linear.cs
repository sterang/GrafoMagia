using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : MonoBehaviour {

    private static int MAX_PUNTOS = 500;

    LineRenderer lr;
    //int global = 0;
    bool alcanzoMeta = false;
    //private bool presionandoMouse;
    public List<BoxCollider> tubos;
    private BoxCollider tubo;
    public BoxCollider Meta;
    public GameObject Mosca;
    private bool cercaAlPunto;
    private bool puedeDibujar = false;
    private float tolerancia = 0.1f;
    

    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, this.transform.position);
        StartCoroutine(DetectarCamino());
        //El tubo es el primer tubo
        tubo = tubos[0];
    }
    public void ArrancarADetectar()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 1;
        lr.SetPosition(0, this.transform.position);
        puedeDibujar = false;
        StopCoroutine("DetectarCamino");
        StartCoroutine(DetectarCamino());
    }

    
    private IEnumerator DetectarCamino()
    {
        while (true)
        {
            
            if (Input.GetMouseButton(0))
            {

                DejarDibujar();

                if (puedeDibujar)
                {
                    Vector3 nuevoPunto = CalcularPunto();
                    ///
                    lr.positionCount++;
                    ///
                    lr.SetPosition(lr.positionCount - 1, nuevoPunto);

                    if (lr.positionCount > MAX_PUNTOS) // Se alcanzo el limite de la linea
                    {
                        //reset
                        lr.positionCount = 0;
                        //global = 0;
                        //presionandoMouse = false;
                    }
                    else  // Se salio de la tuberia o llego a la Meta
                    {
                        bool tuboContienePunto = AlgunTuboContienePuntos(nuevoPunto);
                        if (!tuboContienePunto)
                        {
                            bool MetaContienePunto = Meta.bounds.Contains(nuevoPunto);
                            if (!MetaContienePunto)
                            {
                                alcanzoMeta = false;
                                //Detener Movimiento
                                StopCoroutine("DetectarCamino");
                                //Arrancar Mosca
                                Vector3[] puntos = new Vector3[lr.positionCount];
                                lr.GetPositions(puntos);
                                if(puntos.Length > 1)
                                    GameObject.FindObjectOfType<Mosca>().RecorrerTrayecto(puntos, alcanzoMeta);
                                break;
                            }
                            else
                            {
                                alcanzoMeta = true;
                                //Detener Movimiento
                                StopCoroutine("DetectarCamino");
                                //Arrancar Mosca
                                Vector3[] puntos = new Vector3[lr.positionCount];
                                lr.GetPositions(puntos);
                                if (puntos.Length > 1)
                                    GameObject.FindObjectOfType<Mosca>().RecorrerTrayecto(puntos, alcanzoMeta);
                                break;
                            }
                        }
                    }

                }


                
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private bool AlgunTuboContienePuntos(Vector3 punto)
    {
        foreach (BoxCollider bc in tubos)
        {
            if (bc.ContienePunto(punto))
                return true;
        }
        return false;
        
    }

    private Vector3 CalcularPunto()
    {
        ///
        Vector3 mp = Input.mousePosition;
        //Es necesario poner el valor z de la posición del mouse a un valor tal que sea la distancia entre la cámara y donde queremos poner el punto
        mp.z = -Camera.main.transform.position.z;
        ///
        return Camera.main.ScreenToWorldPoint(mp);
    }

    private bool DejarDibujar()
    {
        Vector3 primerPunto = CalcularPunto();
        cercaAlPunto = (Mosca.transform.position - primerPunto).sqrMagnitude <= tolerancia;

        if(cercaAlPunto)
        {
            puedeDibujar = true;
            Debug.Log("esta cerca");
        }
        return puedeDibujar;
    }


    void OnMouseDown()
    {
        Debug.Log("Click on De point");
        //global = 1;
    }
    // Update is called once per frame
    void Update ()
    {
       
    }
}
