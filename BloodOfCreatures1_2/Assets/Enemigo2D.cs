using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2D : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject tarjet;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject Hit;


    public Material materialParpadeo;
    private Material materialOriginal;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererBarraVida;
    public GameObject BarraVida;

    public int Dano = 10;
    private bool haAtacado = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        tarjet = GameObject.Find("Leslie");
        materialOriginal = GetComponent<SpriteRenderer>().material;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererBarraVida = BarraVida.GetComponent<SpriteRenderer>();
    }

    public void Comportamientos()
    {
        if (tarjet != null)
        {
            if (Mathf.Abs(transform.position.x - tarjet.transform.position.x) > rango_vision && !atacando)
            {
                ani.SetBool("run", false);
                cronometro += 1 * Time.deltaTime;
                if (cronometro >= 4)
                {
                    rutina = Random.Range(0, 2);
                    cronometro = 0;
                }
                switch (rutina)
                {
                    case 0:
                        ani.SetBool("walk", false);
                        break;
                    case 1:
                        direccion = Random.Range(0, 2);
                        rutina++;
                        break;
                    case 2:
                        switch (direccion)
                        {
                            case 0:
                                // Verificar si hay un obst�culo en la direcci�n que est� intentando moverse
                                if (!Physics2D.Raycast(transform.position, Vector2.right, 1f))
                                {
                                    transform.rotation = Quaternion.Euler(0, 0, 0);
                                    transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                                }
                                else
                                {
                                    // Cambiar de direcci�n si hay un obst�culo
                                    direccion = 1;
                                }
                                break;
                            case 1:
                                // Verificar si hay un obst�culo en la direcci�n que est� intentando moverse
                                if (!Physics2D.Raycast(transform.position, Vector2.left, 1f))
                                {
                                    transform.rotation = Quaternion.Euler(0, 180, 0);
                                    transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                                }
                                else
                                {
                                    // Cambiar de direcci�n si hay un obst�culo
                                    direccion = 0;
                                }
                                break;
                        }
                        ani.SetBool("walk", true);
                        break;
                }
            }
            else
            {
                if (Mathf.Abs(transform.position.x - tarjet.transform.position.x) > rango_ataque && !atacando)
                {
                    if (transform.position.x < tarjet.transform.position.x)
                    {
                        ani.SetBool("walk", false);
                        ani.SetBool("run", true);
                        transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        ani.SetBool("attack", false);
                    }
                    else
                    {
                        ani.SetBool("walk", false);
                        ani.SetBool("run", true);
                        transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        ani.SetBool("attack", false);
                    }
                }
                else
                {
                    if (!atacando && !ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        if (transform.position.x < tarjet.transform.position.x)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        ani.SetBool("walk", false);
                        ani.SetBool("run", false);
                        ani.SetBool("attack", true);
                        Debug.Log("Atacando");
                    }
                }
            }
        }
    }

    public float salud = 100f;

    public void RecibirDanio(float CantidadDeDanio)
    {
        salud -= CantidadDeDanio;
        Debug.Log("Enemigo recibi� da�o. Salud actual: " + salud);
        ActualizarBarraVida();
        if (salud <= 0)
        {

            Debug.Log("Enemigo derrotado");
            Muere();
        }
    }

    void Muere()
    {
        ani.SetBool("dead", true);
        Desaparecer();
    }

    void Desaparecer()
    {

        StartCoroutine(DesaparecerConRetardo());
    }

    IEnumerator DesaparecerConRetardo()
    {

        int parpadeos = 5;
        float duracionParpadeo = 0.2f;

        for (int i = 0; i < parpadeos; i++)
        {
            CambiarColor(Color.white);
            yield return new WaitForSeconds(duracionParpadeo / 2);

            CambiarColor(Color.clear);
            yield return new WaitForSeconds(duracionParpadeo / 2);
        }

        Destroy(gameObject);
    }

    void ActualizarBarraVida()
    {

        float escalaX = Mathf.Clamp01((float)salud / 100f);
        BarraVida.transform.localScale = new Vector3(escalaX, 1f, 1f);

        Color nuevoColor = Color.Lerp(Color.red, Color.green, escalaX);
        spriteRendererBarraVida.color = nuevoColor;
    }


    void CambiarColor(Color nuevoColor)
    {
        spriteRenderer.color = nuevoColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Leslie") && !haAtacado)
        {
            // Llama a la funci�n para recibir da�o en el jugador
            other.GetComponent<LeslieMovement>().RecibirDanio(Dano);
            haAtacado = true;
        }
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
        haAtacado = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;

    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
        ani.SetBool("attack", true);
        Debug.Log("ColliderWeaponTrue - Atacando");
    }

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        Comportamientos();

    }
}
