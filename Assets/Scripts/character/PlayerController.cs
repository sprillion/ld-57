using System;
using Cysharp.Threading.Tasks;
using level;
using UnityEngine;

namespace character
{
    
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public Transform cameraTransform;

        private Rigidbody rb;

        private bool _jumpDelay;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked; // Скрыть и зафиксировать курсор

            LevelService.Instance.OnLevelComplete += Disable;
            LevelService.Instance.OnLevelStart += Enable;
        }

        private void Update()
        {
            if (!Boot.HaveControl) return;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            if (!Boot.HaveControl)
            {
                if (!rb.isKinematic)
                {
                    rb.linearVelocity = Vector3.zero;
                }
                return;
            }
            Move();
        }

        private void Disable()
        {
            Boot.HaveControl = false;
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;
        }

        private void Enable()
        {
            rb.position = Vector3.up * 0.5f;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.isKinematic = false;
            Boot.HaveControl = true;
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            var cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            Vector3 direction = (cameraForward * v + cameraTransform.right * h).normalized;


            Vector3 targetVelocity = direction * moveSpeed;
            targetVelocity.y = rb.linearVelocity.y;

            rb.linearVelocity = targetVelocity;
        }

        private void Jump()
        {
            if (_jumpDelay) return;
            if (!CheckGround()) return;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            DelayJump().Forget();
        }

        private bool CheckGround()
        {
            var check = Physics.SphereCast(transform.position + Vector3.up * 1.5f, 0.5f, Vector3.down, out var hit, 2f,
                ~LayerMask.GetMask("Player"));
            
            return check;
        }

        private async UniTaskVoid DelayJump()
        {
            _jumpDelay = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _jumpDelay = false;
        }
    }

}