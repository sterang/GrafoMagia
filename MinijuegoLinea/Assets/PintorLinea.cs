using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintorLinea : MonoBehaviour
{

    public Transform puntos;
    private Transform pf;
    private float r = 0.5f;
    private LineRenderer lr;
    public Vector3 origenPartida;
    private AudioSource aus;
    public int figura = 9;
    public int ganador = 0;
    private List<Transform> puntosAgregados = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        /* Para pintar el triangulo completo desde el principio
        LineRenderer lr = GetComponent<LineRenderer>();
        int i = 0;
        lr.positionCount = transform.childCount;
        foreach(Transform punto in transform)
        {
            lr.SetPosition(i, punto.position);
            i++;
        }
        */

        StartCoroutine(DetectarPunto());
        lr = GetComponent<LineRenderer>();
        aus = GetComponent<AudioSource>();

    }

    private IEnumerator DetectarPunto()
    {
        while (true)
        {
            Vector3 mp = DetectarMouse();

            foreach (Transform punto in puntos)
            {
                bool mpEnVecindadDeP = CalcularVecindad(mp, punto, r);
                if (puntosAgregados.Count > 18){
                    reiniciar();
                }
                if (mpEnVecindadDeP)
                {
                    if (lr.positionCount == 0)
                    {
                        origenPartida = punto.position;
                        AgregarPuntoALR(punto);
                    }
                    else
                    {
                        bool pNoHaSidoAgregadoALR = !HaSidoAgregadoALR(punto);
                        bool pEsOrigenPartidaOtraV = HaSidoOtraVez(origenPartida, punto);
                        if (pNoHaSidoAgregadoALR || pEsOrigenPartidaOtraV)
                        {
                            int indexPunto = punto.GetSiblingIndex();
                            int indexPuntoF = pf.GetSiblingIndex();

                            //p y pf son continuos
                            if (Math.Abs(indexPunto - indexPuntoF) == 1 || Math.Abs(indexPunto - indexPuntoF) == 14 || Math.Abs(indexPunto - indexPuntoF) == 15 || Math.Abs(indexPunto - indexPuntoF) == 26 || Math.Abs(indexPunto - indexPuntoF) == 24)
                            {
                                AgregarPuntoALR(punto);
                            }
                        }
                    }
                }

            }
            yield return null;
        }
    }

    private void reiniciar()
    {
        lr.positionCount = 0;
        ganador = 0;
        puntosAgregados.Clear();
    }

    private void GanasteYupi()
    {
        Debug.Log("Ganaste Felicitaciones");
    }

    private bool HaSidoOtraVez(Vector3 origenPartida, Transform punto)
    {
        if(origenPartida.x == punto.transform.position.x && origenPartida.y == punto.transform.position.y && origenPartida.z == punto.transform.position.z)
        {
            return true;
        }
        else {
            return false;
        }

    }

    private void AgregarPuntoALR(Transform punto)
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, punto.position);
        pf = punto;
        aus.Play();
        aus.pitch += 0.2f;
        puntosAgregados.Add(punto);
        if (ganador == 1 && origenPartida.x == punto.transform.position.x && origenPartida.y == punto.transform.position.y && origenPartida.z == punto.transform.position.z)
        {
            GanasteYupi();
        }
        if (origenPartida.x == punto.transform.position.x && origenPartida.y == punto.transform.position.y && origenPartida.z == punto.transform.position.z)
        {
            ganador = 1;
        }
        
    }

    private bool HaSidoAgregadoALR(Transform punto)
    {
        return puntosAgregados.Contains(punto);
    }

    private bool CalcularVecindad(Vector3 mp, Transform punto, float r)
    {
        return (mp - punto.position).magnitude <= r;
    }

    private Vector3 DetectarMouse()
    {
        Vector3 rta;

        rta = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rta.z = puntos.position.z;
        //rta.z = -0.46f;
        return rta;
    }
}
