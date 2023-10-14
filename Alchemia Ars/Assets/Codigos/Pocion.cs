using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ingrediente;

public class Pocion : MonoBehaviour
{
    //nombre de la pocion
    public PocionN nombre;
    //lista de ingredientes que componen la pocion
    public List<Ingrediente> ingredientes = new List<Ingrediente>();


    public static PocionN convertirStringAPocionN(string nombre)
    {
        Enum.TryParse(nombre, out PocionN resultado);
        return resultado;
    }

    public enum PocionN {Salud,Mana,Resistencia_Calor,Resistencia_Frio,Fuerza,Debilidad,Arcoiris,Explosiva,Cambio}
}
