using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmacenarPocion : MonoBehaviour
{
    //Ingrediente del plato
    public int indice = -1;
    private Sprite spriteOriginal;
    public GameObject[] pociones;
    public bool siguiendo = false;

    private void Start()
    {
        spriteOriginal = GetComponent<SpriteRenderer>().sprite;
    }
    public void EliminarInventario()
    {
        indice = -1;
        GetComponent<SpriteRenderer>().sprite = spriteOriginal;
        GetComponent<SpriteRenderer>().color = Color.white;

    }
    private void OnMouseDown()
    {
        siguiendo = false;
        if (indice > 0)
        {

            GameObject pocion = GameObject.FindWithTag("Pocion");

            GameObject ingrediente = GameObject.FindWithTag("Pocion");
            if (pocion != null)
            {
                if (pocion.GetComponent<SeguirCursor>().seguir)
                {
                    siguiendo = true;
                }

            }
            if (ingrediente != null)
            {
                if (ingrediente.GetComponent<SeguirCursor>().seguir)
                {
                    siguiendo = true;
                }

            }
            if (!siguiendo)
            {
                Instantiate(pociones[indice]);
                indice = -1;
                GetComponent<SpriteRenderer>().sprite = spriteOriginal;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            GameObject pocion = GameObject.FindWithTag("Pocion");
            if (pocion != null)
            {
                for (int i = 0; i < pociones.Length; i++)
                {
                    if (pociones[i].GetComponent<Pocion>().nombre == pocion.GetComponent<Pocion>().nombre)
                    {
                        indice = i;
                        GetComponent<SpriteRenderer>().sprite = pociones[i].GetComponent<SpriteRenderer>().sprite;
                        GetComponent<SpriteRenderer>().color = pociones[i].GetComponent<SpriteRenderer>().color;
                    }
                }
                Destroy(pocion);

            }
        }
    }
}
