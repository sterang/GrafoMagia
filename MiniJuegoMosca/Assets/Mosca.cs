using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosca : MonoBehaviour {

    private Vector3 posOriginal;
    public float velocidad = 1f;

	public void RecorrerTrayecto(Vector3[] puntos, bool alcanzoMeta)
    {
        Debug.Log("Se llamó a recorrer trayecto");
        posOriginal = transform.position;
        if(puntos.Length == 0)
        {
            Debug.LogError("Error: La mosca no puede arrancar sin puntos");
            return;
        }

        this.transform.position = puntos[0];
        StartCoroutine(Volar(puntos, alcanzoMeta));
    }

    private float tolerancia = 0.001f;
    private IEnumerator Volar(Vector3[] puntos, bool alcanzoMeta)
    {
       // if (puntos.Length == 1)
          //  Resetear();
        for(int i = 1; i < puntos.Length; i++)
        {
            Vector3 nextPos = puntos[i];
            bool yaLlego;
            do
            {
                //camina hasta el siguiente punto
                transform.position = Vector3.MoveTowards(this.transform.position, nextPos, Time.deltaTime * velocidad);
                yaLlego = (nextPos - transform.position).sqrMagnitude <= tolerancia;
                if (yaLlego)
                    transform.position = nextPos;
                yield return null;
            } while (!yaLlego);//mientras no llegue a él
        }
        
        if(!alcanzoMeta)
        {
            for(int i = puntos.Length; i>1; i--)
            {
                Vector3 nextPos = puntos[i-1];
                bool yaLlego;
                do
                {
                    Debug.Log("Ciclo recorriendo a la derecha");
                    //camina hasta el siguiente punto
                    transform.position = Vector3.MoveTowards(this.transform.position, nextPos, Time.deltaTime * velocidad*3);
                    yaLlego = (nextPos - transform.position).sqrMagnitude <= tolerancia;
                    if (yaLlego)
                        transform.position = nextPos;
                    yield return null;
                } while (!yaLlego);//mientras no llegue a él
            }
            Resetear();
        }
        else
        {
            GameManager.Singleton.PasarNivel();
        }

        //Resetear();
        //Si lego al destino (a la otra caja)
           //FEliciades
        //Sino
            //Resetear

    }

    private void Resetear()
    {
        this.transform.position = posOriginal;
        GameObject.FindObjectOfType<Linear>().ArrancarADetectar();
    }
}
