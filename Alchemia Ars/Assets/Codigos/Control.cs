using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Ingrediente;
using static Pocion;

public class Control : MonoBehaviour
{
    public List<Pocion> pociones;
    public PocionFinal pocionFinal;
    public List<IngredientesIncompatibles> ingredientesI;
    public List<ProcesosIncompatibles> procesosI;

    private void Start()
    {
        pocionFinal = new PocionFinal();
        pociones = new List<Pocion>();
        ingredientesI = new List<IngredientesIncompatibles>();
        procesosI = new List<ProcesosIncompatibles>();
        string linea;
        try
        {
            StreamReader sr = new StreamReader("Items\\Pociones.txt");
            linea = sr.ReadLine();
            while (linea != null)
            {
                Pocion pocion = new Pocion();
                string[] componentes = linea.Split(';');

                pocion.nombre = convertirStringAPocionN(componentes[0]);
                for (int i = 0; i < componentes.Length; i++)
                {
                    if (i > 0)
                    {
                        IngredienteN nombre = convertirStringAIngredienteN(componentes[i].Split("-")[0]);
                        Proceso proceso = convertirStringAProceso(componentes[i].Split("-")[1]);
                        pocion.ingredientes.Add(new Ingrediente(nombre, proceso));
                    }

                }
                pociones.Add(pocion);
                linea = sr.ReadLine();

            }
            sr.Close();

            sr = new StreamReader("Items\\IngredientesIncompatibles.txt");
            linea = sr.ReadLine();
            while (linea != null)
            {
                string[] componentes = linea.Split(';');

                ingredientesI.Add(new IngredientesIncompatibles(componentes));
                linea = sr.ReadLine();

            }
            sr.Close();

            sr = new StreamReader("Items\\PocionFinal.txt");
            linea = sr.ReadLine();
            int cont = 0;
            while (linea != null)
            {
                if (cont == 0)
                {
                    pocionFinal.nombre = linea;
                }
                else
                {
                    PocionN nombre = convertirStringAPocionN(linea);
                    for (int i = 0; i < pociones.Count; i++)
                    {
                        if (pociones[i].nombre == nombre)
                        {
                            pocionFinal.pociones.Add(pociones[i]);
                        }
                    }
                }
                linea = sr.ReadLine();
                cont++;

            }
            sr.Close();

            sr = new StreamReader("Items\\ProcesosIncompatibles.txt");
            linea = sr.ReadLine();
            while (linea != null)
            {
                string[] componentes = linea.Split("-");
                procesosI.Add(new ProcesosIncompatibles(componentes));

                linea = sr.ReadLine();
            }
            sr.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message + "/nOrigen:" + e.Source + "/nTrazado:" + e.StackTrace);
        }

    }
    public class IngredientesIncompatibles
    {
        public List<Ingrediente> ingredientes = new List<Ingrediente>();

        public IngredientesIncompatibles(List<Ingrediente> ingredientes)
        {
            this.ingredientes = ingredientes;
        }
        public IngredientesIncompatibles(String[] nombres)
        {
            for (int i = 0; i < nombres.Length; i++)
            {
                ingredientes.Add(new Ingrediente(Ingrediente.convertirStringAIngredienteN(nombres[i]), Ingrediente.Proceso.Ninguno));
            }
        }
    }
    public class PocionFinal
    {
        public string nombre;
        public List<Pocion> pociones = new List<Pocion>();
    }
    public class ProcesosIncompatibles
    {
        public List<Ingrediente> ingredientesProcesados = new List<Ingrediente>();

        public ProcesosIncompatibles(List<Ingrediente> ingredientesProcesados)
        {
            this.ingredientesProcesados = ingredientesProcesados;
        }
        public ProcesosIncompatibles(string[] ingredientesProcesados)
        {
            IngredienteN nombre = convertirStringAIngredienteN(ingredientesProcesados[0]);
            Proceso proceso = convertirStringAProceso(ingredientesProcesados[1]);
            this.ingredientesProcesados.Add(new Ingrediente(nombre, proceso));
        }
    }
}
