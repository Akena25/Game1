using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        bool isMoving = direction.magnitude > 0.01f;

        if (isMoving)
        {
            direction.Normalize();

            // Исправляем ориентацию модели:
            // W должен соответствовать направлению взгляда лисы
            direction = Quaternion.Euler(0f, -90f, 0f) * direction;

            // Поворачиваем лису в нужную сторону
            Quaternion targetRotation =
                Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Бежим туда, куда смотрит лиса
            transform.position +=
                transform.forward * speed * Time.deltaTime;
        }

        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }
}