using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ingrediente;
using static Pocion;

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
    public Sprite imageneS;
    private Sprite imageneO;
    //este objeto es el que controla que los procesos sean las correctos
    private Control control;
    //esta variable controla si explota la estación de trabajo
    public bool explota = false;
    public AudioClip[] audios;
    public AudioSource saudio;
    public Animator animacion;
    public SpriteRenderer SpriteHerramienta;
    private void Start()
    {
        //buscamos el objeto controlador
        control = transform.parent.parent.GetComponent<Control>();
        saudio = GetComponent<AudioSource>();
        animacion = GetComponent<Animator>();
        imageneO = GetComponent<SpriteRenderer>().sprite;
        SpriteHerramienta = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Fabricando()
    {
        animacion.SetBool("fabricando", false);
    }
    private void OnMouseOver()
    {
        //buscamos el ingrediente
        ingrediente = GameObject.FindWithTag("Ingrediente");
        //si no esta en la posicion del sitio de preparados
        if (ingrediente != null)
        {
            if (ingrediente.transform.position != posicionFinal.position && imageneS != null && SpriteHerramienta.sprite != imageneS)
            {
                animacion.enabled = false;
                SpriteHerramienta.sprite = imageneS;
            }
        }
    }
    private void OnMouseExit()
    {
        animacion.enabled = true;
        //buscamos el ingrediente
        ingrediente = GameObject.FindWithTag("Ingrediente");
        if (ingrediente != null)
        {
            ingrediente.GetComponent<SpriteRenderer>().enabled = true;

        }

    }
    //Al pulsar la estacion de trabajo
    private void OnMouseDown()
    {
        saudio.clip = audios[Random.Range(0, audios.Length)];
        //buscamos el ingrediente
        ingrediente = GameObject.FindWithTag("Ingrediente");
        //si no esta en la posicion del sitio de preparados
        if (ingrediente != null)
        {
            if (ingrediente.transform.position != posicionFinal.position)
            {
                // guardamos el ingrediente el la variable que creara el ingrediente procesado
                Ingrediente ingredienteAux = ingrediente.GetComponent<Ingrediente>();
                SpriteRenderer imagen = ingrediente.GetComponent<SpriteRenderer>();
                //si el ingrediente no se ha procesado
                if (ingredienteAux.proceso == Ingrediente.Proceso.Ninguno)
                {
                    saudio.Play();

                    animacion.enabled = true;
                    animacion.SetBool("fabricando", true);
                    Invoke("Fabricando", 0.6f);
                    //buscamos la estacion que se ha selecionado
                    switch (estacion)
                    {
                        case Estacion.Destileria:
                            //Comprobamos que no hay una mezcla incompatible
                            Comprobaciones(Ingrediente.Proceso.Destilado);
                            if (!explota)
                            {
                                //procesamos el ingrediente
                                ingredienteAux.proceso = Ingrediente.Proceso.Destilado;
                                //cambiamos el sprite por el que le corresponda
                                imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                                //mientras no esten los sprites finales
                                imagen.color = BuscarColor(ingredienteAux.nombre, ingredienteAux.proceso);
                            }
                            break;
                        case Estacion.Cuchillo:
                            //Comprobamos que no hay una mezcla incompatible
                            Comprobaciones(Ingrediente.Proceso.Cortado);
                            if (!explota)
                            {
                                //procesamos el ingrediente
                                ingredienteAux.proceso = Ingrediente.Proceso.Cortado;
                                //cambiamos el sprite por el que le corresponda
                                imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                                //mientras no esten los sprites finales
                                imagen.color = BuscarColor(ingredienteAux.nombre, ingredienteAux.proceso);
                            }
                            break;
                        case Estacion.Mortero:
                            //Comprobamos que no hay una mezcla incompatible
                            Comprobaciones(Ingrediente.Proceso.Machacado);
                            if (!explota)
                            {
                                //procesamos el ingrediente
                                ingredienteAux.proceso = Ingrediente.Proceso.Machacado;
                                //cambiamos el sprite por el que le corresponda
                                imagen.sprite = BuscarImagen(ingredienteAux.nombre, ingredienteAux.proceso);
                                //mientras no esten los sprites finales
                                imagen.color = BuscarColor(ingredienteAux.nombre, ingredienteAux.proceso);
                            }
                            break;
                    }
                }
                if (!explota)
                {
                    //le decimos que no siga al cursor
                    ingrediente.GetComponent<SeguirCursor>().seguir = false;
                    //lo ponemos en el sitio de preparados
                    ingrediente.transform.position = posicionFinal.position;
                    ingrediente.GetComponent<SpriteRenderer>().enabled = false;
                    //creamos el nuevo ingrediente procesado
                    Instantiate(ingrediente);
                }
                else
                {
                    explota = false;
                    Debug.Log("Explota");
                    control.CambiarPuntaje(-5);
                }
                //eliminamos el ingrediente anterior
                Destroy(ingrediente);
            }
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
    /// <summary>
    /// Aqui se realizan las comprobaciones de los procesos
    /// </summary>
    private void Comprobaciones(Proceso proceso)
    {
        for (int i = 0; i < control.procesosI.Count; i++)
        {
            if (control.procesosI[i].nombre == ingrediente.GetComponent<Ingrediente>().nombre && control.procesosI[i].proceso == proceso)
            {
                explota = true;
                break;
            }
        }
    }
    public enum Estacion { Destileria, Cuchillo, Mortero }
}
