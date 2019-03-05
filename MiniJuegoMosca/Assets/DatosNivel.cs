using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class DatosNivel
{

    public Transform checkpoint;
    public BoxCollider meta;
    public GameObject tubo;
    

    
    public List<BoxCollider> ObtenerTuberias()
    {
        List<BoxCollider> rta = new List<BoxCollider>();
        foreach(Transform t in tubo.transform)
        {
            rta.Add(t.GetComponent<BoxCollider>());
        }
        return rta;
    }
}
