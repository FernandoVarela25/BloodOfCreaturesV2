using UnityEngine;

public class CuracionItem : MonoBehaviour
{
    public Animator animator; // Referencia al Animator de las posiciones.

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colision� con el objeto de curaci�n.
        if (other.CompareTag("Leslie"))
        {
            // Acciones de curaci�n
            CurarJugador(other.gameObject);

            // Desactiva el objeto para simular la recolecci�n.
            gameObject.SetActive(false);

            // Activa la transici�n en el Animator de las posiciones.
            if (animator != null)
            {
                animator.SetBool("RecogerCuracion", true);
            }
        }
    }

    void CurarJugador(GameObject jugador)
    {
        // Acciones de curaci�n aqu� (aumenta la salud del jugador al 100).
        LeslieMovement jugadorScript = jugador.GetComponent<LeslieMovement>();

        if (jugadorScript != null)
        {
            jugadorScript.CurarVidaAl100();
            Debug.Log("Jugador curado al 100%");
        }
        else
        {
            Debug.LogWarning("El script LeslieMovement no est� presente en el jugador.");
        }
    }

}
