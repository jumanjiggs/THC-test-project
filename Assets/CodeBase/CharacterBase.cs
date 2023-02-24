using System.Collections;
using UnityEngine;

namespace CodeBase
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public Rigidbody rb;
        public Vector3 jump;
        public float jumpForce = 2.0f;

        private bool _isGrounded;

        private void Start()
        {
            JumpOnHexagon();
        }

        protected virtual void JumpOnHexagon()
        {
            Debug.Log(_isGrounded.ToString());
            StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            while (true)
            {
                if (_isGrounded)
                    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
                _isGrounded = true;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
                _isGrounded = false;
        }
    }
}