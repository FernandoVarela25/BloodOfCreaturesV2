using System.Collections;
using UnityEngine;

public class LeslieMovement : MonoBehaviour
{
    public static Vector2 shootDirection;
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    public float vidaInicial = 100f;
    private float vidaActual;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private Animator Animator;
    private bool Grounded;
    private float LastShoot;
    private bool estaMuerto = false;
    public AudioClip shootSound;
    private AudioSource audioSource;
    public AudioClip jumpSound;

    public GameObject BarraVida;
    private bool parpadeando = false;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        vidaActual = vidaInicial;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (Horizontal > 0.0f)
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1.0f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down, 1.0f))
            Grounded = true;
        else
            Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)

            Jump();

        // Verifica si el jugador está vivo antes de procesar la entrada
        if (!estaMuerto && vidaActual > 0)
        {
            // ... (código existente)

            if (!Animator.GetBool("running") && Input.GetMouseButtonDown(0) && Time.time > LastShoot + 0.25f && Grounded)
            {
                Shoot();
                LastShoot = Time.time;
                Animator.SetTrigger("disparando");
                StartCoroutine(ResetShootTrigger());
            }
        }
    }

    private IEnumerator ResetShootTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        Animator.ResetTrigger("disparando");
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);

        // Reproducir el sonido de salto
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
    private void Shoot()
    {
        Vector2 shootDirection = (transform.localScale.x == -0.5f) ? Vector2.left : Vector2.right;

        float offsetX = (shootDirection.x < 0) ? -0.6f : 0.6f;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + new Vector3(offsetX, 0.2f, 0f), Quaternion.identity);
        bullet.GetComponent<BulletScript>().Shoot(shootDirection, Speed);

        // Reproducir el sonido de disparo
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }


    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void RecibirDanio(float cantidadDanio)
    {
        if (estaMuerto)
        {
            return;
        }

        vidaActual -= cantidadDanio;

        // Actualiza la barra de vida
        ActualizarBarraVida();

        Debug.Log("Vida actual del jugador: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void ActualizarBarraVida()
    {
        float escalaX = Mathf.Clamp(vidaActual / vidaInicial, 0f, 1f);
        BarraVida.transform.localScale = new Vector3(escalaX, 1f, 1f);
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");

        estaMuerto = true;

        // Desactiva el collider u otros componentes según sea necesario
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Activa la animación "dead" en el Animator
        Animator.SetBool("dead", true);

        // Inicia la rutina de parpadeo y desaparición
        StartCoroutine(ParpadeoYDesaparecer());
    }
    public void CurarVidaAl100()
    {
        // Curar la vida al 100
        vidaActual = vidaInicial;

        // Actualiza la barra de vida
        ActualizarBarraVida();
    }
    public void AumentarVelocidad()
    {
        // Aumenta la velocidad del jugador según tu lógica.
        Speed *= 1.5f; // Por ejemplo, aumenta la velocidad en un 50%.

        Debug.Log("Velocidad aumentada: " + Speed);
    }


    IEnumerator ParpadeoYDesaparecer()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            parpadeando = true;

            // Parpadeo durante la animación de muerte
            for (int i = 0; i < 5; i++)
            {
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }

            // Desactiva el parpadeo
            parpadeando = false;
        }

        // Desactiva el objeto después de la animación de muerte
        gameObject.SetActive(false);
    }


}