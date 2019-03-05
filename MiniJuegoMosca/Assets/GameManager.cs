using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static  GameManager instancia;
    private int nivel = 0;

    public static GameManager Singleton
    {
        get
        {
            return instancia;
        }
    }


    public List<DatosNivel> fases;
    public GameObject mosca;
    private Linear linear;
    public List<Transform> posCamaras;

    
    // Start is called before the first frame update
    void Awake()
    {
        instancia = this;
        CambiarANivel(nivel);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void PasarNivel()
    {
        nivel++;
        CambiarANivel(nivel);
    }

    internal void CambiarANivel(int n)
    {        
        mosca.transform.position = fases[n].checkpoint.position;
        linear = GameObject.FindObjectOfType<Linear>();
        linear.transform.position = mosca.transform.position;
        linear.tubos = fases[n].ObtenerTuberias();
        linear.Meta = fases[n].meta;
        linear.ArrancarADetectar();
        Camera.main.transform.position = posCamaras[n].position;
    }
}
