using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo2D : MonoBehaviour
{
    public Animator ani;
    public Enemigo2D enemigo;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Leslie"))
        {
            Debug.Log("Colisión con Leslie detectada");
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", false);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;

            // Agregamos una línea para imprimir el estado de la animación de ataque
            Debug.Log("Estado de la animación de ataque: " + ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"));

            // Llamamos explícitamente a la función ColliderWeaponTrue
            enemigo.ColliderWeaponTrue();
        }
    }
}