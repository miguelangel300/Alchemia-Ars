using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingrediente : MonoBehaviour
{
    public IngredienteN nombre;
    public Proceso proceso;

    public Ingrediente(IngredienteN nombre, Proceso proceso)
    {
        this.nombre = nombre;
        this.proceso = proceso;
    }

    public void CambiarNombre(IngredienteN nombre)
    {
        this.nombre = nombre;
    }
    public void CambiarProceso(Proceso proceso)
    {
        this.proceso = proceso;
    }

    public static IngredienteN convertirStringAIngredienteN(string nombre)
    {
        Enum.TryParse(nombre, out IngredienteN resultado);
        return resultado;
    }
    public static Proceso convertirStringAProceso(string proceso)
    {
        Enum.TryParse(proceso, out Proceso resultado);
        return resultado;
    }

    public enum IngredienteN {Mandragora,Cuerno_de_unicornio,Hierbas_magicas,Polvo_de_hadas,Ojo_de_ciclope,Ingrediente6}
    public enum Proceso {Ninguno,Destilado,Cortado,Machacado}
}
