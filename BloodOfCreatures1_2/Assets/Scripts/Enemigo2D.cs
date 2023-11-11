using System.Collections;
using UnityEngine;

public class Enemigo2D : MonoBehaviour
{
    public float speed_walk;
    public float speed_run;
    public GameObject tarjet;
    public bool atacando;
    public Animator ani;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject Hit;

    private bool playerDetected = false;
    private bool isAttacking = false;
     public MovimientoPlataforma movimientoPlataforma;
    void Start()
    {
        tarjet = GameObject.Find("Leslie");
        ani = GetComponent<Animator>();

        // Intenta obtener el script MovimientoPlataforma
        movimientoPlataforma = GetComponent<MovimientoPlataforma>();

        // Si la referencia sigue siendo nula, imprime un mensaje de advertencia
        if (movimientoPlataforma == null)
        {
            Debug.LogError("No se encontró el script MovimientoPlataforma. Asegúrate de que esté presente en el mismo GameObject.");
            return; // Salir del método para evitar más errores
        }

        StartCoroutine(DetectarJugador());
    }

    IEnumerator DetectarJugador()
    {
        while (true)
        {
            float distancia = Vector2.Distance(transform.position, tarjet.transform.position);

            if (distancia < rango_vision)
            {
                playerDetected = true;
                Debug.Log("¡Enemigo ha detectado al jugador!");
                MoverHaciaJugador();
                ani.SetBool("run", true); // Activa la animación de correr
                yield return new WaitUntil(() => distancia > rango_ataque); // Espera hasta que esté fuera del rango de ataque
            }
            else
            {
                playerDetected = false;
                ani.SetBool("run", false); // Desactiva la animación de correr
                yield return null;
            }
        }
    }

    void Update()
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, tarjet.transform.position);

        if (playerDetected)
        {
            if (distanciaAlJugador > rango_ataque)
            {
                if (!isAttacking)
                {
                    // Corre hacia el jugador
                    MoverHaciaJugador();
                    ani.SetBool("attack", false);
                }
            }
            else
            {
                if (!isAttacking)
                {
                    // Ataca al jugador
                    isAttacking = true;
                    StartCoroutine(Atacar());
                }
            }
        }
    }
    void MoverHaciaJugador()
    {
        if (Vector2.Distance(transform.position, tarjet.transform.position) > rango_ataque)
        {
            float step = speed_run * Time.deltaTime;

            // Calcula la dirección del movimiento ajustando según la escala del enemigo
            Vector2 direction = (tarjet.transform.position - transform.position).normalized;
            transform.Translate(direction * step, Space.World);
        }
    }



    IEnumerator Atacar()
    {
        // Espera hasta que el enemigo alcance al jugador
        yield return new WaitUntil(() => Vector2.Distance(transform.position, tarjet.transform.position) <= rango_ataque);

        // Ataca al jugador
        ani.SetBool("run", false);
        ani.SetBool("attack", true);
        yield return new WaitForSeconds(1f);

        ani.SetBool("attack", false);
        isAttacking = false;
    }

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
        ani.SetBool("attack", true);
    }
}
