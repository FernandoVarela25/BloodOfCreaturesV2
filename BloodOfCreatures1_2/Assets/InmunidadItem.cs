using UnityEngine;

public class InmunidadItem : MonoBehaviour
{
    public Animator animator; // Referencia al Animator de las posiciones.

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colision� con el objeto de inmunidad.
        if (other.CompareTag("Leslie"))
        {
            // Acciones de activaci�n de inmunidad.
            ActivarInmunidad(other.gameObject);

            // Desactiva el objeto para simular la recolecci�n.
            gameObject.SetActive(false);

            // Activa la transici�n en el Animator de las posiciones.
            if (animator != null)
            {
                animator.SetBool("RecogerInmunidad", true);
            }
        }
    }

    void ActivarInmunidad(GameObject jugador)
    {
        // Acciones de activaci�n de inmunidad aqu� (por ejemplo, 20 segundos de inmunidad).
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.ActivarInmunidad();
            Debug.Log("Inmunidad activada por 20 segundos");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no est� presente en el jugador.");
        }
    }
}
