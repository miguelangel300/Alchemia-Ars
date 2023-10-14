using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using static EstacionTrabajo;
using static Ingrediente;

public class Caldero : MonoBehaviour
{
    public Transform posicionFinal;
    public List<Pocion> pociones = new List<Pocion>();
    public List<Ingrediente> ingredientes = new List<Ingrediente>();
    public bool explota = false;
    private Control control;

    private void Start()
    {
        control = transform.parent.parent.GetComponent<Control>();
    }

    private void OnMouseDown()
    {
        GameObject ingrediente = GameObject.FindWithTag("Ingrediente");
        GameObject pocion = GameObject.FindWithTag("Pocion");
        if (ingrediente != null)
        {
            if (ingrediente.transform.position != posicionFinal.position)
            {
                ingredientes.Add(ingrediente.GetComponent<Ingrediente>());
                Destroy(ingrediente);
            }
        }
        if (pocion != null)
        {
            if (ingrediente.transform.position != posicionFinal.position)
            {
                pociones.Add(pocion.GetComponent<Pocion>());
                Destroy(pocion);
            }
        }
        Comprobaciones();
        if (explota)
        {
            Debug.Log("Explota");
            ingredientes = new List<Ingrediente>();
            pociones = new List<Pocion>();
            explota = false;
        }
    }

    private void Comprobaciones()
    {
        if (pociones.Count > 0 && ingredientes.Count > 0)
        {
            explota = true;
        }
        if (ingredientes.Count > 0)
        {
            for (int i = 0; i < control.ingredientesI.Count; i++)
            {
                List<Ingrediente> ingredientesRevisados = new List<Ingrediente>();
                int cont = 0;
                for (int j = 0; j < ingredientes.Count; j++)
                {
                    bool revisado = false;
                    for (int l = 0; l < ingredientesRevisados.Count; l++)
                    {
                        if (ingredientesRevisados[l].nombre == ingredientes[j].nombre)
                        {
                            revisado = true;
                            break;
                        }
                    }
                    if (!revisado)
                    {
                        for (int k = 0; k < control.ingredientesI[i].ingredientes.Count; k++)
                        {
                            ingredientesRevisados.Add(ingredientes[j]);
                            if (control.ingredientesI[i].ingredientes[k].nombre == ingredientes[j].nombre)
                            {
                                cont++;
                                if (cont > 1)
                                {
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

