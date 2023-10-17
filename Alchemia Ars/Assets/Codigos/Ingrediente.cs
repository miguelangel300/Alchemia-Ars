using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingrediente : MonoBehaviour
{
    //nombre del ingrediente
    public IngredienteN nombre;
    //procesado que se le ha aplicado al ingrediente
    public Proceso proceso;

    public Ingrediente(IngredienteN nombre, Proceso proceso)
    {
        this.nombre = nombre;
        this.proceso = proceso;
    }

    /// <summary>
    /// cambia el nombre del ingrediente
    /// </summary>
    /// <param name="nombre">nombre que va a tener</param>
    public void CambiarNombre(IngredienteN nombre)
    {
        this.nombre = nombre;
    }
    /// <summary>
    /// cambia el proceso del ingrediente
    /// </summary>
    /// <param name="nombre">proceso que se le ha aplicado</param>
    public void CambiarProceso(Proceso proceso)
    {
        this.proceso = proceso;
    }

    /// <summary>
    /// recibe un string y devuelve un Ingrediente.IgredienteN
    /// </summary>
    /// <param name="nombre">nombre en string</param>
    /// <returns></returns>
    public static IngredienteN convertirStringAIngredienteN(string nombre)
    {
        Enum.TryParse(nombre, out IngredienteN resultado);
        return resultado;
    }

    /// <summary>
    /// recibe un string y devuelve un Ingrediente.Proceso
    /// </summary>
    /// <param name="nombre">proceso en string</param>
    /// <returns></returns>
    public static Proceso convertirStringAProceso(string proceso)
    {
        Enum.TryParse(proceso, out Proceso resultado);
        return resultado;
    }

    public enum IngredienteN {Mandragora,Cuerno_de_unicornio,Hierbas_magicas,Alas_de_hadas,Ojo_de_ciclope,Araña}
    public enum Proceso {Ninguno,Destilado,Cortado,Machacado}
}
