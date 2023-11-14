using UnityEngine;

public class InmunidadItem : MonoBehaviour
{
    public Animator animator; // Referencia al Animator de las posiciones.

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colisionó con el objeto de inmunidad.
        if (other.CompareTag("Leslie"))
        {
            // Acciones de activación de inmunidad.
            ActivarInmunidad(other.gameObject);

            // Desactiva el objeto para simular la recolección.
            gameObject.SetActive(false);

            // Activa la transición en el Animator de las posiciones.
            if (animator != null)
            {
                animator.SetBool("RecogerInmunidad", true);
            }
        }
    }

    void ActivarInmunidad(GameObject jugador)
    {
        // Acciones de activación de inmunidad aquí (por ejemplo, 20 segundos de inmunidad).
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.ActivarInmunidad();
            Debug.Log("Inmunidad activada por 20 segundos");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no está presente en el jugador.");
        }
    }
}
