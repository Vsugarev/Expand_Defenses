using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [Header("Prefabs y Configuración")]
    public GameObject lightningPrefab;
    public GameObject spikesPrefab;
    public float castRange = 5f;
    public float cooldown = 2f;

    [Header("Animación e Indicadores")]
    public Animator anim;
    public GameObject rangeIndicator;
    public GameObject targetIndicator;

    private Camera mainCam;
    private float cooldownTimer;

    private bool hasCastThisSpell = false; // Evita duplicados por animación
    private int currentButton; // 0 = click izquierdo, 1 = click derecho

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        HandleTargeting();

        // Click izquierdo → rayo
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            TryCastMagic(0);
        }

        // Click derecho → spikes
        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0)
        {
            TryCastMagic(1);
        }
    }

    void HandleTargeting()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 dir = mousePos - transform.position;
        float distance = dir.magnitude;

        if (distance > castRange)
            mousePos = transform.position + dir.normalized * castRange;

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

    void TryCastMagic(int mouseButton)
    {
        anim.SetBool("IsMagic", true); // Inicia animación de casting
        cooldownTimer = cooldown;
        hasCastThisSpell = false;
        currentButton = mouseButton; // Guarda el botón que disparó la animación
    }

    // Llamado desde el Animation Event
    public void CastMagicEvent()
    {
        if (hasCastThisSpell) return; // Evita múltiples instanciaciones

        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 dir = mousePos - transform.position;
        if (dir.magnitude > castRange)
            mousePos = transform.position + dir.normalized * castRange;

        if (currentButton == 0) // Click izquierdo → rayo
        {
            Instantiate(lightningPrefab, mousePos, Quaternion.identity);
        }
        else if (currentButton == 1) // Click derecho → spikes
        {
            Instantiate(spikesPrefab, mousePos, Quaternion.identity);
        }

        hasCastThisSpell = true;
    }

    // Llamado desde el Animation Event al terminar la animación
    public void StopMagic()
    {
        anim.SetBool("IsMagic", false);
    }
}
