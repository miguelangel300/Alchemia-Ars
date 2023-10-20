using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarPapelera : MonoBehaviour
{
    Animator animador;
    AudioSource sonido;
    // Start is called before the first frame update
    void Start()
    {
        animador = GetComponent<Animator>();
        sonido = GetComponent<AudioSource>();

    }
    private void OnMouseOver()
    {
        animador.SetBool("comiendo", true);

    }
    private void OnMouseExit()
    {
        animador.SetBool("comiendo", false);

    }
    public void Maullar()
    {
        sonido.Play();
        Invoke("Maullar", Random.Range(10, 25));
    }
    public void CancelarMaullar()
    {
        CancelInvoke("Maullar");
    }
}
