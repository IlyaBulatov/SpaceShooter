using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField]
        private SpaceShip m_TargetShip;

        [SerializeField]
        private VirtualJoystick m_MobileJoystick;

        [SerializeField]
        private ControlMode m_ControlMode;

        /// <summary>
        /// Get require component "SpaceShip".
        /// </summary>

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
                m_MobileJoystick.gameObject.SetActive(false);
            else
                m_MobileJoystick.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();
        }

        private void ControlMobile()
        {
            Vector3 dir = m_MobileJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;
        }

        /// <summary>
        /// Keyboard control method.
        /// </summary>
        private void ControlKeyboard()
        {
            float thrust = 0f;
            float torque = 0f;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                torque = -1.0f;

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}