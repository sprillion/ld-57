using UnityEngine;

namespace ground.character
{
    
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public Transform cameraTransform;

        private Rigidbody rb;
        private bool isGrounded;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked; // Скрыть и зафиксировать курсор
        }

        private void Update()
        {
            Move();

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            var cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            Vector3 direction = (cameraForward * v + cameraTransform.right * h).normalized;
            direction.y = 0;

            Vector3 targetVelocity = direction * moveSpeed;
            Vector3 velocityChange = targetVelocity - rb.linearVelocity;
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        private void Jump()
        {
            if (!CheckGround()) return;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        private bool CheckGround()
        {
            var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
            return Physics.Raycast(ray, 0.2f, ~LayerMask.NameToLayer("Player"));
        }
    }

}