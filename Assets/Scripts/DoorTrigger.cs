using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator; // Ссылка на аниматор двери
    [SerializeField] private Transform playerTransform; // Ссылка на игрока

    [SerializeField] private float interactionDistance = 3f; // Макс. дистанция
    [SerializeField] private float cooldownTime = 1f; // Задержка между переключениями

    private bool isPlayerInTrigger = false;
    private float lastToggleTime = -Mathf.Infinity; // Время последнего переключения

    private void Start()
    {
        if (doorAnimator == null)
            doorAnimator = GetComponentInParent<Animator>();

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null && doorAnimator != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);


            if (Time.time >= lastToggleTime + cooldownTime)
            {
                if (isPlayerInTrigger || distance <= interactionDistance)
                {
                    doorAnimator.SetBool("IsOpen", true);
                }
                else
                {
                    doorAnimator.SetBool("IsOpen", false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            lastToggleTime = Time.time;
        }
    }

}