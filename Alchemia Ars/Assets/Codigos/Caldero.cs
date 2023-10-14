using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using static EstacionTrabajo;
using static Ingrediente;

public class Caldero : MonoBehaviour
{

    //posición en la que va a dejar al ingrediente procesado
    public Transform posicionFinal;
    //lista de pociones introducidas en el caldero
    public List<Pocion> pociones = new List<Pocion>();
    //lista de ingredientes introducidos en el caldero
    public List<Ingrediente> ingredientes = new List<Ingrediente>();
    //esta variable controla si explota el caldero
    public bool explota = false;
    //este objeto es el que controla que las mezclas sean las correctas
    private Control control;

    private void Start()
    {
        //buscamos el objeto controlador
        control = transform.parent.parent.GetComponent<Control>();
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
                //añade el ingrediente a la lista y destruye el ingrediente
                ingredientes.Add(ingrediente.GetComponent<Ingrediente>());
                Destroy(ingrediente);
            }
        }
        if (pocion != null)
        {
            //si hay pocion y no esta en el sitio de preparados 
            if (ingrediente.transform.position != posicionFinal.position)
            {
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
            explota = false;
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
        if (ingredientes.Count > 0)
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
                    //si el ingrediente no se harevisado
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

