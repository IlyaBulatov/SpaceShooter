using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region Properties

        /// <summary>
        /// Automatic rigid setup mass.
        /// </summary>
        [Header(" ")]
        [SerializeField]
        private float m_Mass;

        /// <summary>
        /// Forward pushing force.
        /// </summary>
        [SerializeField]
        private float m_Thrust;

        /// <summary>
        /// Rotation force.
        /// </summary>
        [SerializeField]
        private float m_Mobility;

        /// <summary>
        /// Maximum linear force.
        /// </summary>
        [SerializeField]
        private float m_MaxLinearVelocity;

        /// <summary>
        /// Maximum rotational force. In degrees per second.
        /// </summary>
        [SerializeField]
        private float m_MaxAngularVelocity;

        /// <summary>
        /// Rigidbody2D link.
        /// </summary>
        private Rigidbody2D m_Rigid;

        #endregion

        #region Public API

        /// <summary>
        /// Linear thrust control.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotational thrust control
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Events

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();
        }

        #endregion

        #region Methods & Events

        /// <summary>
        /// Adds movement forces to a ship method.
        /// </summary>
        private void UpdateRigidbody()
        {
            m_Rigid.AddForce
                (ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce
                (-m_Rigid.velocity * (ThrustControl / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField]
        private Turret[] m_Turret;

        public void Fire(TurretMode mode)
        {
            for(int i = 0; i < m_Turret.Length; ++i)
            {
                if (m_Turret[i].Mode == mode)
                {
                    m_Turret[i].Fire();
                }
            }
        }
        #endregion
    }
}