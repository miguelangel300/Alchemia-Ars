using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seleccionar : MonoBehaviour
{
    public GameObject imagen;
    private void OnMouseDown()
    {
        imagen.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        GameObject.Instantiate(imagen);

    }
}
