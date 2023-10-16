using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seleccionar : MonoBehaviour
{
    //Ingrediente del plato
    public GameObject imagen;
    private void OnMouseDown()
    {
        //imagen.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        //creamos el ingrediente que sigue al cursor
        Instantiate(imagen);
    }
}
