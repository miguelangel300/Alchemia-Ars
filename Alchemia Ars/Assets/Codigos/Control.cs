using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Ingrediente;
using static Pocion;

public class Control : MonoBehaviour
{
    //Lista de pociones que se pueden crear
    public List<Pocion> pociones;
    // pocion final que se ha de crear
    public PocionFinal pocionFinal;
    //Lista de ingredientes que no son compatibles entre si
    public List<IngredientesIncompatibles> ingredientesI;
    //Lista de procesos que no son compatibles con los ingredientes
    public List<Ingrediente> procesosI;
    public int puntajeFinal = 100;

    private void Start()
    {
        //inicialización de valiables
        pocionFinal = new PocionFinal();
        pociones = new List<Pocion>();
        ingredientesI = new List<IngredientesIncompatibles>();
        procesosI = new List<Ingrediente>();
        string linea;

        try
        {
            //Leemos el archivo que contiene las pociones y sus recetas
            StreamReader sr = new StreamReader("Items\\Pociones.txt");
            //leemos la primera linea
            linea = sr.ReadLine();
            //mientras haya lineas
            while (linea != null)
            {
                Pocion pocion = new Pocion();
                //dividimos la linea para tener el nombre y los ingredientes 
                string[] componentes = linea.Split(';');

                //introducimos el nombre de la pocion
                pocion.nombre = convertirStringAPocionN(componentes[0]);
                //recorremos los ingredientes y procesos
                for (int i = 0; i < componentes.Length; i++)
                {
                    //si no es el primero que es el nombre de la pocion
                    if (i > 0)
                    {
                        //dividimos el ingrediente-proceso y guardamos el nombre
                        IngredienteN nombre = convertirStringAIngredienteN(componentes[i].Split("-")[0]);
                        //dividimos el ingrediente-proceso y guardamos el proceso
                        Proceso proceso = convertirStringAProceso(componentes[i].Split("-")[1]);
                        //guardamos el ingrediente en la pocion
                        pocion.ingredientes.Add(new Ingrediente(nombre, proceso));
                    }

                }
                //guardamos la pocion en la lista de pociones
                pociones.Add(pocion);
                //leemos la siguiente linea
                linea = sr.ReadLine();

            }
            //cerramos el archivo
            sr.Close();

            //Leemos el archivo que contiene los ingredientes que son incompatibles entre si
            sr = new StreamReader("Items\\IngredientesIncompatibles.txt");
            //leemos la primera linea
            linea = sr.ReadLine();
            //mientras haya lineas
            while (linea != null)
            {
                //dividimos la linea para tener el nombre de los ingredientes
                string[] componentes = linea.Split(';');
                //guardamos los ingredientes incompatibles
                ingredientesI.Add(new IngredientesIncompatibles(componentes));
                //leemos la siguiente linea
                linea = sr.ReadLine();

            }
            //cerramos el archivo
            sr.Close();

            //Leemos el archivo que contiene el nombre de la pocion final y las pociones que la componen
            sr = new StreamReader("Items\\PocionFinal.txt");
            //leemos la primera linea
            linea = sr.ReadLine();
            //contador que nos dice en que linea estamos
            int cont = 0;
            //mientras haya lineas
            while (linea != null)
            {
                //si es la primera linea
                if (cont == 0)
                {
                    //guardamos el nombre de la pocion final
                    pocionFinal.nombre = linea;
                }
                else
                {
                    //si no, convertimos el string al nombre de la pcion 
                    PocionN nombre = convertirStringAPocionN(linea);
                    //buscamos en las pociones existentes
                    for (int i = 0; i < pociones.Count; i++)
                    {
                        //si la pocion que esta en el archivo de pocion final esta en la lista de pociones que tenemos
                        if (pociones[i].nombre == nombre)
                        {
                            //la guardamos
                            pocionFinal.pociones.Add(pociones[i]);
                        }
                    }
                }
                //leemos la siguiente linea
                linea = sr.ReadLine();
                //sumamos al contador
                cont++;

            }
            //cerramos el archivo
            sr.Close();

            //Leemos el archivo que contiene los procesos que son incompatible a determinados ingredientes
            sr = new StreamReader("Items\\ProcesosIncompatibles.txt");
            //leemos la primera linea
            linea = sr.ReadLine();
            //mientras haya lineas
            while (linea != null)
            {
                //guardamos el ingrediente y el proceso en un array
                string[] componentes = linea.Split("-");
                //los agregamos a la lista
                procesosI.Add(new Ingrediente(convertirStringAIngredienteN(componentes[0]), convertirStringAProceso(componentes[1])));
                //leemos la siguiente linea
                linea = sr.ReadLine();
            }
            //cerramos el archivo
            sr.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message + "/nOrigen:" + e.Source + "/nTrazado:" + e.StackTrace);
        }

    }
    public void CambiarPuntaje(int puntaje)
    {
        puntajeFinal += puntaje;
        if (puntajeFinal <= 0)
        {
            //pantalla final
        }
    }
    /// <summary>
    /// Clase de los ingredeintes incompatibles
    /// </summary>
    public class IngredientesIncompatibles
    {
        //lista de ingredientes que si los mezclas son incompatibles
        public List<Ingrediente> ingredientes = new List<Ingrediente>();

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="ingredientes">lista de ingredientes</param>
        public IngredientesIncompatibles(List<Ingrediente> ingredientes)
        {
            this.ingredientes = ingredientes;
        }
        /// <summary>
        /// constructor que convierte un array de string en una lista de ingredientes
        /// </summary>
        /// <param name="nombres">array de string</param>
        public IngredientesIncompatibles(String[] nombres)
        {
            for (int i = 0; i < nombres.Length; i++)
            {
                //añadimos el proceso incompatible
                ingredientes.Add(new Ingrediente(convertirStringAIngredienteN(nombres[i]), Proceso.Ninguno));
            }
        }
    }
    /// <summary>
    /// clase que define la pocion final y sus ingredientes
    /// </summary>
    public class PocionFinal
    {
        //nombre de la pocion final
        public string nombre;
        //lista de ls pociones que la componen
        public List<Pocion> pociones = new List<Pocion>();
    }
}
