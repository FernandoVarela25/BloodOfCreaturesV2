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
            Debug.Log("Colisi�n con Leslie detectada");
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", false);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;

            // Agregamos una l�nea para imprimir el estado de la animaci�n de ataque
            Debug.Log("Estado de la animaci�n de ataque: " + ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"));

            // Llamamos expl�citamente a la funci�n ColliderWeaponTrue
            enemigo.ColliderWeaponTrue();
        }
    }
}