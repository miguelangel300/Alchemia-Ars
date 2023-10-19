using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarPapelera : MonoBehaviour
{
    Animator animador;
    // Start is called before the first frame update
    void Start()
    {
        animador = GetComponent<Animator>();


    }
    private void OnMouseOver()
    {
        animador.SetBool("comiendo", true);

    }
    private void OnMouseExit()
    {
        animador.SetBool("comiendo", false);

    }
}
