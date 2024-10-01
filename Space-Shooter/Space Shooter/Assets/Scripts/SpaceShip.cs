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

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();

            UpdateEnergyRegen();
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
        private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for(int i = 0; i < m_Turrets.Length; ++i)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }
        #endregion

        [SerializeField]
        private int m_MaxEnergy;

        [SerializeField]
        private int m_MaxAmmo;

        [SerializeField]
        private int m_EnergyRegenPerSecond;

        private float m_CurrentEnergy;
        private int m_CurrentAmmo;

        public void AddEnergy(int e)
        {
            m_CurrentEnergy = Mathf.Clamp(m_CurrentEnergy + e, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_CurrentAmmo = Mathf.Clamp(m_CurrentAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_CurrentEnergy = m_MaxEnergy;
            m_CurrentAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_CurrentEnergy += (float)m_EnergyRegenPerSecond * Time.deltaTime;
            m_CurrentEnergy = Mathf.Clamp(m_CurrentEnergy, 0, m_MaxEnergy);
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_CurrentEnergy >= count)
            {
                m_CurrentEnergy -= count;
                return true;
            }

            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if(m_CurrentAmmo >= count)
            {
                m_CurrentAmmo -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
    }
}