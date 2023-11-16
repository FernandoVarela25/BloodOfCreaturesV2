using UnityEngine;
using TMPro;

public class InmunidadItem : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textoInmunidad;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Leslie"))
        {
            ActivarInmunidad(other.gameObject);
            gameObject.SetActive(false);
            MostrarTextoInmunidad();

            if (animator != null)
            {
                animator.SetBool("RecogerInmunidad", true);
            }
        }
    }

    void ActivarInmunidad(GameObject jugador)
    {
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

    void MostrarTextoInmunidad()
    {
        if (textoInmunidad != null)
        {
            textoInmunidad.text = "¡Inmunidad activada!";
            Invoke("OcultarTextoInmunidad", 2f); // Cambia el 2f al tiempo que desees que aparezca el texto.
        }
    }

    void OcultarTextoInmunidad()
    {
        if (textoInmunidad != null)
        {
            textoInmunidad.text = "";
        }
    }
}
