using UnityEngine;
using UnityEngine.InputSystem;

public class BolaBolos : MonoBehaviour
{
    public float fuerza = 1000f;
    public float velocidadRotacion = 100f;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial; 

    private Rigidbody rb;


    public GameObject flecha; // para la flechita
    private bool lanzada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        // Solo permitir girar antes de lanzar
        if (!lanzada)
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                transform.Rotate(0, -velocidadRotacion * Time.deltaTime, 0);
            }

            if (Keyboard.current.rightArrowKey.isPressed)
            {
                transform.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
            }

            // Lanzar con espacio
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.AddForce(transform.forward * fuerza);

                lanzada = true;

                // Ocultar flecha al disparar
                flecha.SetActive(false);
            }
        }

        // Reiniciar si cae
        if (transform.position.y < -5)
        {
            ReiniciarBola();
        }
    }

    void ReiniciarBola()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;

        // Volver a mostrar flecha
        flecha.SetActive(true);

        lanzada = false;
    }
}