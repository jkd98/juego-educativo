using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Variables de Movimiento y Salto
    public float runSpeed = 2.0f;
    private float originalRunSpeed; // Almacena la velocidad base (2.0f)
    public float jumpSpeed = 3.0f;
    private float originalJumpSpeed; // Almacena la fuerza de salto base
    public float doubleJumpSpeed = 2.5f;
    private float originalDoubleJumpSpeed; // Almacena la fuerza del doble salto base
    
    // Variables de Estado y Componentes
    private bool canDoubleJump = false;
    private bool controlsInverted = false; // Estado para la inversión de controles
    Rigidbody2D rb2D; 
    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // 1. Guardar las velocidades originales al inicio
        originalRunSpeed = runSpeed; 
        originalJumpSpeed = jumpSpeed;
        originalDoubleJumpSpeed = doubleJumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Lógica de Salto (Sin cambios)
        if (Input.GetKeyDown("space"))
        {
            if (CheckGround.isGrounded)
            {
                canDoubleJump = true; 
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }
            else
            {
                if (canDoubleJump)
                {
                    animator.SetBool("DoubleJump", true);
                    rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                    canDoubleJump = false; 
                }
            }
        }
        
        // Lógica de Animación (Sin cambios)
        if (CheckGround.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }
        if (rb2D.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb2D.velocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }
    }
    
    void FixedUpdate()
    {
        // 2. Lógica de Movimiento que usa 'runSpeed' y 'controlsInverted'
        float moveInput = 0;
        float direction = 0; // -1 para izquierda, 1 para derecha

        // Determinar la dirección de entrada del usuario
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            direction = 1; 
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            direction = -1; 
        }
        
        // Aplicar la inversión si el efecto está activo
        if (controlsInverted)
        {
            moveInput = -direction;
        }
        else
        {
            moveInput = direction;
        }

        // Aplicación del movimiento
        if (moveInput != 0)
        {
            // La velocidad se ajusta aquí, usando el 'runSpeed' actual (2.0f o 0.2f)
            rb2D.velocity = new Vector2(moveInput * runSpeed, rb2D.velocity.y);
            
            // Determina si el sprite debe voltearse
            // Si moveInput es positivo, el sprite debe estar mirando a la derecha (flipX = false)
            spriteRenderer.flipX = (moveInput < 0); 
            animator.SetBool("Run", true);
        }
        else
        {
            // Detiene el movimiento horizontal si no hay entrada
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }
        
        // Lógica de Salto Mejorado (Sin cambios)
        if (betterJump)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }
    
    //---------------------------------------------------------
    // --- MÉTODOS DE EFECTOS TEMPORALES ---
    //---------------------------------------------------------

    // --- EFECTO DE LENTITUD ---
    
    /// <summary>
    /// Activa cualquier cambio temporal de velocidad (reducción o aumento).
    /// </summary>
    /// <param name="nuevaVelocidad">El valor de runSpeed temporal.</param>
    /// <param name="duracion">Tiempo del efecto.</param>
    public void ActivarReducVelocidadTemporal(float nuevaVelocidad, float duracion)
    {
        // Detiene CUALQUIER corrutina de velocidad en curso
        StopCoroutine("GestionarVelocidadTemporal"); 
        
        // Inicia la única corrutina de velocidad
        StartCoroutine(GestionarVelocidadTemporal(nuevaVelocidad, duracion));
    }

    /// <summary>
    /// Corrutina UNIFICADA que gestiona cualquier velocidad temporal.
    /// </summary>
    IEnumerator GestionarVelocidadTemporal(float nuevaVelocidad, float duracion)
    {
        Debug.Log("¡Velocidad temporal activada! Nueva velocidad: " + nuevaVelocidad);

        // 1. Aplicar la nueva velocidad
        runSpeed = nuevaVelocidad;

        // 2. Esperar la duración
        yield return new WaitForSeconds(duracion);

        // 3. Restaurar la velocidad original (2.0f)
        runSpeed = originalRunSpeed;
        Debug.Log("Velocidad temporal terminada. Velocidad restaurada: " + originalRunSpeed);
    }
    
    // --- EFECTO DE INVERSIÓN DE CONTROLES ---

    /// <summary>
    /// Activa la inversión de controles por un tiempo determinado.
    /// </summary>
    public void ActivarControlesInvertidos(float duracion)
    {
        StopCoroutine("InversionTemporalCorrutina");
        StartCoroutine(InversionTemporalCorrutina(duracion));
    }

    IEnumerator InversionTemporalCorrutina(float duracion)
    {
        Debug.Log("¡Controles invertidos ACTIVADOS!");
        controlsInverted = true;
        yield return new WaitForSeconds(duracion);
        controlsInverted = false;
        Debug.Log("Controles invertidos DESACTIVADOS.");
    }
    
    // --- MÉTODOS PARA MODIFICAR EL SALTO (PERMANENTES) ---
    
    /// <summary>
    /// Modifica la fuerza de salto del jugador permanentemente.
    /// </summary>
    /// <param name="nuevaFuerzaSalto">Nueva fuerza de salto (ej: 5.0f para salto alto, 1.5f para salto bajo)</param>
    /// <param name="nuevaFuerzaDobleSalto">Nueva fuerza de doble salto (opcional, si es 0 se usa el mismo valor que el salto)</param>
    public void ModificarSalto(float nuevaFuerzaSalto)
    {
        ModificarSalto(nuevaFuerzaSalto, nuevaFuerzaSalto);
    }
    
    public void ModificarSalto(float nuevaFuerzaSalto, float nuevaFuerzaDobleSalto)
    {
        // Aplicar las nuevas fuerzas de salto PERMANENTEMENTE
        jumpSpeed = nuevaFuerzaSalto;
        doubleJumpSpeed = nuevaFuerzaDobleSalto;
        
        Debug.Log($"¡Salto modificado permanentemente! Nuevo salto: {jumpSpeed}, Nuevo doble salto: {doubleJumpSpeed}");
    }
    
    /// <summary>
    /// Restablece la fuerza de salto a sus valores normales.
    /// </summary>
    public void RestablecerSaltoNormal()
    {
        // Restaurar valores originales
        jumpSpeed = originalJumpSpeed;
        doubleJumpSpeed = originalDoubleJumpSpeed;
        
        Debug.Log($"Salto restablecido a valores normales. Salto: {jumpSpeed}, Doble salto: {doubleJumpSpeed}");
    }
    
    // --- MÉTODOS ÚTILES ADICIONALES ---
    
    /// <summary>
    /// Obtiene los valores actuales de salto
    /// </summary>
    public (float saltoActual, float dobleSaltoActual) ObtenerValoresSaltoActual()
    {
        return (jumpSpeed, doubleJumpSpeed);
    }
    
    /// <summary>
    /// Obtiene los valores originales de salto
    /// </summary>
    public (float saltoOriginal, float dobleSaltoOriginal) ObtenerValoresSaltoOriginal()
    {
        return (originalJumpSpeed, originalDoubleJumpSpeed);
    }
}