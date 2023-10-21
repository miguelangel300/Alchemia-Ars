using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static EstacionTrabajo;
using static Ingrediente;

public class Caldero : MonoBehaviour
{

    //posición en la que va a dejar al ingrediente procesado
    public Transform posicionFinal;
    //Prefas de los pociones creadas
    public List<GameObject> pocionesCreadas;
    public GameObject pocionFinal;
    //lista de pociones introducidas en el caldero
    public static List<Pocion> pociones = new List<Pocion>();
    //lista de ingredientes introducidos en el caldero
    public static List<Ingrediente> ingredientes = new List<Ingrediente>();
    //esta variable controla si explota el caldero
    public bool explota = false;
    //este objeto es el que controla que las mezclas sean las correctas
    private Control control;
    private Animator animador;
    private ParticleSystem explosion;
    private AudioSource audio;
    public AudioClip[] audiosCaldero;

    private void Start()
    {
        //buscamos el objeto controlador
        control = transform.parent.parent.GetComponent<Control>();
        animador = transform.GetChild(0).GetComponent<Animator>();
        explosion = transform.GetChild(1).GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }
    public static void EliminarInventario()
    {
        pociones = new List<Pocion>();
        ingredientes = new List<Ingrediente>();
    }
    //al pulsar el caldero
    private void OnMouseDown()
    {
        //buscamos el ingrediente a introducir
        GameObject ingrediente = GameObject.FindWithTag("Ingrediente");
        //buscamos la poción a introducir
        GameObject pocion = GameObject.FindWithTag("Pocion");
        if (ingrediente != null)
        {
            //si hay ingrediente y no esta en el sitio de preparados 
            if (ingrediente.transform.position != posicionFinal.position)
            {
                audio.clip = audiosCaldero[0];
                audio.Play();
                animador.SetBool("introduciendo", true);
                Invoke("Salir", 0.4f);
                //añade el ingrediente a la lista y destruye el ingrediente
                ingredientes.Add(ingrediente.GetComponent<Ingrediente>());
                Destroy(ingrediente);
            }
        }
        if (pocion != null)
        {
            //si hay pocion y no esta en el sitio de preparados 
            if (pocion.transform.position != posicionFinal.position)
            {
                audio.clip = audiosCaldero[0];
                audio.Play();
                animador.SetBool("introduciendo", true);
                Invoke("Salir", 0.4f);
                //añade el ingrediente a la lista y destruye el ingrediente
                pociones.Add(pocion.GetComponent<Pocion>());
                Destroy(pocion);
            }
        }
        //Comprobamos que no hay una mezcla incompatible
        Comprobaciones();
        //si la hay
        if (explota)
        {
            //explota la caldera y se vacia
            Debug.Log("Explota");
            ingredientes = new List<Ingrediente>();
            pociones = new List<Pocion>();
            control.CambiarPuntaje(-15);
            explota = false;
            explosion.Play();
            explosion.transform.GetChild(0).GetComponent<Light2D>().enabled = true;
            AudioSource sonido = explosion.transform.GetChild(1).GetComponent<AudioSource>();
            sonido.Play();
            InvokeRepeating("Apagar", 0, 0.1f);
        }
        else
        {
            CrearPocion();
            CrearPocionFinal();
        }
    }

    public void Apagar()
    {
        Light2D luz = explosion.transform.GetChild(0).GetComponent<Light2D>();
        luz.intensity -= 100;
        if (luz.intensity <= 0)
        {
            luz.intensity = 1000;
            luz.enabled = false;
            CancelInvoke("Apagar");
        }
    }
    private void Salir()
    {
        animador.SetBool("introduciendo", false);
    }
    private void CrearPocionFinal()
    {
        if (pociones.Count >= 2)
        {
            int numIp = 0;
            for (int i = 0; i < control.pocionFinal.pociones.Count; i++)
            {
                for (int j = 0; j < pociones.Count; j++)
                {
                    if (pociones[j].nombre == control.pocionFinal.pociones[i].nombre)
                    {
                        numIp++;
                    }
                }


            }
            if (numIp == control.pocionFinal.pociones.Count && pociones.Count > control.pocionFinal.pociones.Count)
            {

                //le decimos que no siga al cursor
                pocionFinal.GetComponent<SeguirCursor>().seguir = false;
                //lo ponemos en el sitio de preparados
                pocionFinal.transform.position = posicionFinal.position;
                //creamos el nuevo ingrediente procesado
                Instantiate(pocionFinal);
                Invoke("PantallaFinal", 3f);
                control.CambiarPuntaje(15);

                audio.clip = audiosCaldero[1];
                audio.Play();
            }else if (numIp == control.pocionFinal.pociones.Count)
            {

                //le decimos que no siga al cursor
                pocionFinal.GetComponent<SeguirCursor>().seguir = false;
                //lo ponemos en el sitio de preparados
                pocionFinal.transform.position = posicionFinal.position;
                //creamos el nuevo ingrediente procesado
                Instantiate(pocionFinal);
                Invoke("PantallaFinal", 3f);

                audio.clip = audiosCaldero[1];
                audio.Play();
            }
        }
    }

    private void PantallaFinal()
    {
        ControlCanvas.PantallaVictoria();
    }

    private void CrearPocion()
    {
        if (ingredientes != null && ingredientes.Count == 3)
        {
            for (int i = 0; i < control.pociones.Count; i++)
            {
                //lista de los elementos que ya se han revisado
                List<Ingrediente> ingredientesRevisados = new List<Ingrediente>();
                int numIp = 0;
                for (int j = 0; j < ingredientes.Count; j++)
                {
                    bool revisado = false;
                    //recorremos los ingredientes que ya se han revisado en esta lista de ingredientes incompatibles
                    for (int l = 0; l < ingredientesRevisados.Count; l++)
                    {
                        //si se encuentra es que ya se ha revisado y no hay que volver a revisar
                        if (ingredientesRevisados[l].nombre == ingredientes[j].nombre &&
                            ingredientesRevisados[l].proceso == ingredientes[j].proceso)
                        {
                            revisado = true;
                            break;
                        }
                    }
                    //si el ingrediente no se harevisado
                    if (!revisado)
                    {
                        for (int l = 0; l < control.pociones[i].ingredientes.Count; l++)
                        {
                            //añadimos el ingrediente introducido al caldero que estamos revisando a la lista de ingredientes revisados
                            ingredientesRevisados.Add(ingredientes[j]);
                            if (ingredientes[j].nombre == control.pociones[i].ingredientes[l].nombre &&
                                ingredientes[j].proceso == control.pociones[i].ingredientes[l].proceso)
                            {
                                numIp++;
                                break;
                            }
                        }
                    }
                }
                if (numIp == control.pociones[i].ingredientes.Count)
                {
                    GameObject pocion = new GameObject();
                    for (int k = 0; k < pocionesCreadas.Count; k++)
                    {
                        if (pocionesCreadas[k].GetComponent<Pocion>().nombre == control.pociones[i].nombre)
                        {
                            pocion = pocionesCreadas[k];
                        }
                    }

                    //le decimos que no siga al cursor
                    pocion.GetComponent<SeguirCursor>().seguir = false;
                    //lo ponemos en el sitio de preparados
                    pocion.transform.position = posicionFinal.position;
                    //creamos el nuevo ingrediente procesado
                    Instantiate(pocion);
                    ingredientes = new List<Ingrediente>();

                    audio.clip = audiosCaldero[1];
                    audio.Play();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Aqui se realizan las comprobaciones de las mezclas
    /// </summary>
    private void Comprobaciones()
    {
        //Si hay pociones e ingredientes en el caldero
        if (pociones.Count > 0 && ingredientes.Count > 0)
        {
            //explota
            explota = true;
        }
        //si hay mas de tres ingredientes explota
        if (ingredientes.Count > 3 && !explota)
        {
            explota = true;
        }
        if (ingredientes.Count > 0 && !explota)
        {
            //si hay ingredientes
            //recorremos la lista de los ingredientes incompatibles
            for (int i = 0; i < control.ingredientesI.Count; i++)
            {
                //lista de los elementos que ya se han revisado
                List<Ingrediente> ingredientesRevisados = new List<Ingrediente>();
                //contador de los elementos incompatibles
                int cont = 0;
                //recorremos los ingredientes introducidos en el caldero
                for (int j = 0; j < ingredientes.Count; j++)
                {
                    bool revisado = false;
                    //recorremos los ingredientes que ya se han revisado en esta lista de ingredientes incompatibles
                    for (int l = 0; l < ingredientesRevisados.Count; l++)
                    {
                        //si se encuentra es que ya se ha revisado y no hay que volver a revisar
                        if (ingredientesRevisados[l].nombre == ingredientes[j].nombre)
                        {
                            revisado = true;
                            break;
                        }
                    }
                    //si el ingrediente no se ha revisado
                    if (!revisado)
                    {
                        //recorremos los ingredientes de la lista de ingredientes incompatibles actual
                        for (int k = 0; k < control.ingredientesI[i].ingredientes.Count; k++)
                        {
                            //añadimos el ingrediente introducido al caldero que estamos revisando a la lista de ingredientes revisados
                            ingredientesRevisados.Add(ingredientes[j]);
                            //si coincide el ingrediente introducido con el incompatible
                            if (control.ingredientesI[i].ingredientes[k].nombre == ingredientes[j].nombre)
                            {
                                //añadimos uno al contador
                                cont++;
                                //si hay mas de un ingrediente incompatible
                                if (cont > 1)
                                {
                                    //el caldero explota
                                    explota = true;

                                }
                                break;
                            }

                        }
                    }
                    if (explota)
                    {
                        break;
                    }
                }
                if (explota)
                {
                    break;
                }

            }
        }
    }
}

