using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seleccionar : MonoBehaviour
{
    //Ingrediente del plato
    public GameObject imagen;
    public static bool start;
    private void OnMouseDown()
    {
        if (start)
        {
            //creamos el ingrediente que sigue al cursor
            Instantiate(imagen);

        }
    }
    public static void Inicio()
    {
        start = !start;
    }
    public static void Inicio(bool inicio)
    {
        start = inicio;
    }
}
