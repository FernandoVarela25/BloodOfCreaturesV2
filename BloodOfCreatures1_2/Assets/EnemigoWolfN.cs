using System.Collections;
using UnityEngine;

public class EnemigoWolfN : MonoBehaviour
{
    public float rangoVision = 10f;
    public float rangoAtaque = 2f;
    public float velocidad = 3f;
    public Transform jugador;
    public Animator animator;
    public float vidaInicial = 100f;
    private float vidaActual;
    public GameObject BarraVida;
    public Material materialParpadeo;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoVision;
    public AudioClip sonidoGolpe;
    private AudioSource audioSource;
    private bool haEntradoEnRango = false;
    private bool parpadeando = false;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Leslie").transform;
        animator = GetComponent<Animator>();
        vidaActual = vidaInicial;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sonidoVision;
    }

    void Update()
    {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoVision)
        {
            if (!haEntradoEnRango)
            {
                // Reproduce el sonido de ataque solo cuando entra por primera vez en el rango de visión
                audioSource.PlayOneShot(sonidoVision);
                haEntradoEnRango = true;
            }

            if (distancia > rangoAtaque)
            {
                MoverHaciaJugador();
                animator.SetBool("run", true);
                animator.SetBool("attack", false);
            }
            else
            {
                AtacarJugador();
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
            }
        }
        else
        {
            haEntradoEnRango = false; // Restablece el indicador al salir del rango de visión
            animator.SetBool("run", false);
            animator.SetBool("attack", false);
        }
    }


    void MoverHaciaJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

        if (direccion.x < 0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        else if (direccion.x > 0)
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }


    // Añade un campo para gestionar el tiempo entre ataques
    private float tiempoEntreAtaques = 1.0f;
    private float tiempoUltimoAtaque;
    private bool puedeAtacar = true;
    void AtacarJugador()
    {
        if (puedeAtacar)
        {
            // Reproduce el sonido de ataque solo una vez
            audioSource.PlayOneShot(sonidoGolpe);
            Debug.Log("¡Atacando al jugador!");

            // Encuentra el script LeslieMovement del jugador
            LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

            if (jugadorScript != null)
            {
                // Ataca al jugador llamando a RecibirDanio
                jugadorScript.RecibirDanio(10f);
            }

            // Desactiva la posibilidad de atacar durante un tiempo
            StartCoroutine(EsperarParaAtacar());
        }
    }

    IEnumerator EsperarParaAtacar()
    {
        // Desactiva la posibilidad de atacar durante un tiempo
        puedeAtacar = false;

        // Espera un tiempo antes de permitir otro ataque
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // Reactiva la posibilidad de atacar
        puedeAtacar = true;
    }

    public void RecibirDanio(float cantidadDanio)
    {
        vidaActual -= cantidadDanio;

        if (!parpadeando)
        {
            // Inicia la rutina de parpadeo
            StartCoroutine(Parpadeo());
        }

        float escalaX = Mathf.Clamp(vidaActual / vidaInicial, 0f, 1f);
        BarraVida.transform.localScale = new Vector3(escalaX, 1f, 1f);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    IEnumerator Parpadeo()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            parpadeando = true;

            // Parpadeo al recibir daño
            for (int i = 0; i < 3; i++)
            {
                renderer.material.color = new Color(1f, 0.5f, 0.5f, 1f); // Color rojo, ajusta según tu necesidad
                yield return new WaitForSeconds(0.1f);
                renderer.material.color = Color.white; // Vuelve al color original
                yield return new WaitForSeconds(0.1f);
            }

            parpadeando = false;
        }
    }
    void Morir()
    {
        Debug.Log("Enemigo muerto");
        animator.SetBool("dead", true);
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Reproduce el sonido de muerte
        audioSource.PlayOneShot(sonidoMuerte);

        StartCoroutine(ParpadeoYDesaparecer());
    }


    IEnumerator ParpadeoYDesaparecer()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            for (int i = 0; i < 5; i++)
            {
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
        }

        gameObject.SetActive(false);
    }
}
