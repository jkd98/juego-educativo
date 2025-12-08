using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    // Asigna tu jugador (Player) a esta variable desde el Inspector
    public Transform target; 
    
    // Define los límites de tu nivel
    public Vector2 minCameraLimit; // La esquina inferior izquierda
    public Vector2 maxCameraLimit; // La esquina superior derecha

    // Velocidad a la que la cámara sigue al jugador
    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        if (target == null) return;

        // 1. Calcular la posición deseada de la cámara (la posición del jugador)
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // 2. Aplicar el suavizado (opcional, pero recomendado)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // 3. Aplicar los LÍMITES de la cámara
        float clampedX = Mathf.Clamp(smoothedPosition.x, minCameraLimit.x, maxCameraLimit.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minCameraLimit.y, maxCameraLimit.y);

        // 4. Mover la cámara a la posición limitada
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}