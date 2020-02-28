using UnityEngine;
using UnityEngine.Serialization;

namespace MLAgents
{
    /// <summary>
    /// A component for 3D Ray Perception.
    /// </summary>
    [AddComponentMenu("ML Agents/Ray Perception Sensor 3D", (int)MenuGroup.Sensors)]
    public class RayPerceptionSensorComponent3D : RayPerceptionSensorComponentBase
    {
        [HideInInspector]
        [SerializeField]
        [FormerlySerializedAs("startVerticalOffset")]
        [Range(-10f, 10f)]
        [Tooltip("Ray start is offset up or down by this amount.")]
        float m_StartVerticalOffset;

        /// <summary>
        /// Ray start is offset up or down by this amount.
        /// </summary>
        public float startVerticalOffset
        {
            get => m_StartVerticalOffset;
            set { m_StartVerticalOffset = value; UpdateSensor(); }
        }

        [HideInInspector]
        [SerializeField]
        [FormerlySerializedAs("endVerticalOffset")]
        [Range(-100f, 100f)]
        [Tooltip("Ray end is offset up or down by this amount.")]
        float m_EndVerticalOffset;

        /// <summary>
        /// Ray end is offset up or down by this amount.
        /// </summary>
        public float endVerticalOffset
        {
            get => m_EndVerticalOffset;
            set { m_EndVerticalOffset = value; UpdateSensor(); }
        }

        /// <inheritdoc/>
        public override RayPerceptionCastType GetCastType()
        {
            return RayPerceptionCastType.Cast3D;
        }

        /// <inheritdoc/>
        public override float GetStartVerticalOffset()
        {
            return startVerticalOffset;
        }

        /// <inheritdoc/>
        public override float GetEndVerticalOffset()
        {
            return endVerticalOffset;
        }
        public override ISensor CreateSensor()
        {
            var rayAngles = GetRayAngles(raysPerDirection, maxRayDegrees);
            var rayPerceptionInput = GetRayPerceptionInput();

            m_RaySensor = new RayPerceptionSensor(sensorName, rayPerceptionInput);

            if (observationStacks != 1)
            {
                var stackingSensor = new StackingSensor(m_RaySensor, observationStacks);
                return stackingSensor;
            }

            return m_RaySensor;
        }

        RayPerceptionInput GetRayPerceptionInput()
        {
            RayPerceptionInput.RayCastHitObserver rcho = null;
            var rayAngles = GetRayAngles(raysPerDirection, maxRayDegrees);
            rcho = gameObject.GetComponent<IAgentDelegate>().GetObs();

            var rayPerceptionInput = new RayPerceptionInput();
            rayPerceptionInput.rayLength = rayLength;
            rayPerceptionInput.detectableTags = detectableTags;
            rayPerceptionInput.angles = rayAngles;
            rayPerceptionInput.startOffset = GetStartVerticalOffset();
            rayPerceptionInput.endOffset = GetEndVerticalOffset();
            rayPerceptionInput.castRadius = sphereCastRadius;
            rayPerceptionInput.transform = transform;
            rayPerceptionInput.castType = GetCastType();
            rayPerceptionInput.layerMask = rayLayerMask;
            rayPerceptionInput.rayCastHitObserver = rcho;

            return rayPerceptionInput;
        }
    }
}
