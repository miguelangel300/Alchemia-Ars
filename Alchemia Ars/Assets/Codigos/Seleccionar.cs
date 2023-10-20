using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seleccionar : MonoBehaviour
{
    //Ingrediente del plato
    public GameObject imagen;
    public static bool start;
    public Sprite[] imagenes;
    private SpriteRenderer ingrediente;
    private int indice;
    [Range(0f, 1f)]
    public float velocidad = 0.1f;
    private void Start()
    {
        ingrediente = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        indice = 0;
    }
    private void OnMouseOver()
    {
        InvokeRepeating("CambiarImagen", 0, velocidad);
    }
    private void OnMouseExit()
    {
        CancelInvoke("CambiarImagen");
    }
    private void CambiarImagen()
    {
        indice++;
        if (indice >= imagenes.Length)
        {
            indice = 0;
        }
        ingrediente.sprite = imagenes[indice];
    }
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
