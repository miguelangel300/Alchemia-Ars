using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstacionTrabajo : MonoBehaviour
{
    //posición en la que va a dejar al ingrediente procesado
    public Transform posicionFinal;
    //nos indica que estación es 
    public Estacion estacion;
    //ingrediente que se le introduce
    GameObject ingrediente;
    //Prefas de los ingredientes procesados
    public List<GameObject> imagenes;


    //Al pulsar la estacion de trabajo
    private void OnMouseDown()
    {
        //buscamos el ingrediente
        ingrediente = GameObject.FindWithTag("Ingrediente");
        //si no esta en la posicion del sitio de preparados
        if (ingrediente.transform.position != posicionFinal.position)
        {
            // guardamos el ingrediente el la variable que creara el ingrediente procesado
            Ingrediente ingredienteAux = ingrediente.GetComponent<Ingrediente>();
            SpriteRenderer imagen = ingrediente.GetComponent<SpriteRenderer>();
            //si el ingrediente no se ha procesado
            if (ingredienteAux.proceso == Ingrediente.Proceso.Ninguno)
            {
                //buscamos la estacion que se ha selecionado
                switch (estacion)
                {
                    case Estacion.Destileria:
                        //procesamos el ingrediente
                        ingredienteAux.proceso = Ingrediente.Proceso.Destilado;
                        //cambiamos el sprite por el que le corresponda
                        imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                        //mientras no esten los sprites finales
                        imagen.color = BuscarColor(ingredienteAux.nombre, ingredienteAux.proceso);
                        break;
                    case Estacion.Cuchillo:
                        //procesamos el ingrediente
                        ingredienteAux.proceso = Ingrediente.Proceso.Cortado;
                        //cambiamos el sprite por el que le corresponda
                        imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                        //mientras no esten los sprites finales
                        imagen.color = BuscarColor(ingredienteAux.nombre, ingredienteAux.proceso);
                        break;
                    case Estacion.Mortero:
                        //procesamos el ingrediente
                        ingredienteAux.proceso = Ingrediente.Proceso.Machacado;
                        //cambiamos el sprite por el que le corresponda
                        imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                        //mientras no esten los sprites finales
                        imagen.color = BuscarColor(ingredienteAux.nombre, ingredienteAux.proceso);
                        break;
                }
            }
            //le decimos que no siga al cursor
            ingrediente.GetComponent<SeguirCursor>().seguir = false;
            //lo ponemos en el sitio de preparados
            ingrediente.transform.position = posicionFinal.position;
            //creamos el nuevo ingrediente procesado
            Instantiate(ingrediente);
            //eliminamos el ingrediente anterior
            Destroy(ingrediente);
        }
    }
    private Sprite BuscarImagen(Ingrediente.IngredienteN nombre, Ingrediente.Proceso proceso)
    {
        //buscamos en todos los ingredientes procesados
        for (int i = 0; i < imagenes.Count; i++)
        {
            //si coincide el ingrediente y el proceso devolvemos la imagen
            if (imagenes[i].GetComponent<IngredienteProcesado>().nombre == nombre && imagenes[i].GetComponent<IngredienteProcesado>().proceso == proceso)
            {
                return imagenes[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
        return null;
    }
    //este metodo se eliminara cuando esten los sprites finales
    private Color BuscarColor(Ingrediente.IngredienteN nombre, Ingrediente.Proceso proceso)
    {
        //buscamos en todos los ingredientes procesados
        for (int i = 0; i < imagenes.Count; i++)
        {
            //si coincide el ingrediente y el proceso devolvemos el color
            if (imagenes[i].GetComponent<IngredienteProcesado>().nombre == nombre && imagenes[i].GetComponent<IngredienteProcesado>().proceso == proceso)
            {
                return imagenes[i].GetComponent<SpriteRenderer>().color;
            }
        }
        return Color.white;
    }
    public enum Estacion { Destileria, Cuchillo, Mortero }
}
