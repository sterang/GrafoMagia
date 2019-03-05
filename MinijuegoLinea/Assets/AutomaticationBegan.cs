using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticationBegan : MonoBehaviour {
    public Transform puntos;
    private Transform pf;
    private float r = 0.5f;
    private LineRenderer lr;
    public Vector3 origenPartida;
    private AudioSource aus;
    public int ganador = 0;
    private List<Transform> puntosAgregados = new List<Transform>();

    // Use this for initialization
    void Start () {
        // Para pintar el triangulo completo desde el principio
        /*LineRenderer lr = GetComponent<LineRenderer>();
        int i = 0;
        lr.positionCount = transform.childCount;
        foreach(Transform punto in transform)
        {
            lr.SetPosition(i, punto.position);
            i++;

        }*/
        StartCoroutine(Automatico());

    }
    private IEnumerator Automatico()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        int i = 0;
        lr.positionCount = transform.childCount;
        foreach (Transform punto in transform)
        {
            lr.SetPosition(i, punto.position);
            i++;
            yield return new WaitForSeconds(0.5f);
        }
        lr.positionCount = 0;
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
