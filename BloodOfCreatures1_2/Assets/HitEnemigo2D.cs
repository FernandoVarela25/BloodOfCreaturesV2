using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemigo"))  // Asegúrate de que el enemigo tiene el tag correcto
        {
            print("Daño al enemigo");
            // Puedes agregar aquí cualquier lógica adicional cuando la bala colisiona con el enemigo
        }
    }

    void Start()
    {
        // Cualquier inicialización necesaria
    }

    void Update()
    {
        // Cualquier lógica de actualización necesaria
    }
}
