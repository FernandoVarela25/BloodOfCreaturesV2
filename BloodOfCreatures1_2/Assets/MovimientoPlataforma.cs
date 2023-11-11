using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;
    public bool permitirMovimiento = true;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isWalking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(CambiarEstado());
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
            rb.velocity = new Vector2(velocidad, rb.velocity.y);

            if (informacionSuelo == false)
            {
                Girar();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private IEnumerator CambiarEstado()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Tiempo aleatorio para caminar
            isWalking = !isWalking;
            animator.SetBool("walk", isWalking);

            if (!isWalking)
            {
                yield return new WaitForSeconds(Random.Range(2f, 5f)); // Tiempo aleatorio para sentarse
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
