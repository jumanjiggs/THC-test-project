using UnityEngine;

namespace CodeBase
{
    public abstract class UnitBase : MonoBehaviour
    {
        public Rigidbody rb;
        public Vector3 jump;
        public float jumpForce = 2.0f;
        
        private const float FallMultiplier = 7f;
        private const float LowJumpMultiplier = 7f;
        private bool _isGrounded;

        private void Update()
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                JumpOnHexagon();
            }
            
            UpdateVelocity();
        }

        protected virtual void JumpOnHexagon()
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }

        private void UpdateVelocity()
        {
            if (rb.velocity.y < 0)
                rb.velocity += Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            else if (rb.velocity.y > 0 && !_isGrounded)
                rb.velocity += Vector3.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
                _isGrounded = true;
        }
    }
}