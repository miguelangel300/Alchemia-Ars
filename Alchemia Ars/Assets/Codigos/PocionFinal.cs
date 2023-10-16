using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocionFinal : MonoBehaviour
{
    public class Pocion : MonoBehaviour
    {
        //nombre de la pocion
        public String nombre;
        //lista de ingredientes que componen la pocion
        public List<Pocion> ingredientes = new List<Pocion>();

    }

}
