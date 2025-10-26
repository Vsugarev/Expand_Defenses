using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public GameObject magicPrefab;       // Prefab de la habilidad
    public float castRange = 5f;         // Rango máximo alrededor del jugador
    public float cooldown = 2f;          // Tiempo de reutilización
    private float cooldownTimer;

    public Animator anim;
    public GameObject rangeIndicator;    // Círculo o área visible
    public GameObject targetIndicator;   // Marca donde apunta el ratón

    private Camera mainCam;
    private bool hasCastThisSpell = false; // Controla que el rayo solo se instancie una vez

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        HandleTargeting();

        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer <= 0)
        {
            TryCastMagic();
        }
    }

    void HandleTargeting()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Calcula la distancia entre jugador y mouse
        Vector3 dir = mousePos - transform.position;
        float distance = dir.magnitude;

        // Limita el punto dentro del rango
        if (distance > castRange)
            mousePos = transform.position + dir.normalized * castRange;

        // Actualiza indicador visual
        if (rangeIndicator != null)
        {
            rangeIndicator.transform.position = transform.position;
            rangeIndicator.transform.localScale = Vector3.one * castRange * 2;
        }

        if (targetIndicator != null)
        {
            targetIndicator.transform.position = mousePos;
        }
    }

    void TryCastMagic()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 dir = mousePos - transform.position;

        if (dir.magnitude <= castRange)
        {
            anim.SetBool("IsMagic", true); // Solo dispara la animación
            cooldownTimer = cooldown;

            hasCastThisSpell = false; // Resetea para permitir un nuevo cast
        }
        else
        {
            Debug.Log("Objetivo fuera de rango");
        }
    }

    // Este método lo llamas desde el evento de animación
    public void CastLightningEvent()
    {
        if (hasCastThisSpell) return; // Evita duplicados
        hasCastThisSpell = true;

        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 dir = mousePos - transform.position;
        if (dir.magnitude > castRange)
            mousePos = transform.position + dir.normalized * castRange;

        Instantiate(magicPrefab, mousePos, Quaternion.identity);
    }

    public void StopMagic()
    {
        anim.SetBool("IsMagic", false);
    }
}
