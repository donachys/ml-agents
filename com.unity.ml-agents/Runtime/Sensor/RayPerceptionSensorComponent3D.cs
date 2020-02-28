using System;
using UnityEngine;

namespace MLAgents
{
    [AddComponentMenu("ML Agents/Ray Perception Sensor 3D", (int)MenuGroup.Sensors)]
    public class RayPerceptionSensorComponent3D : RayPerceptionSensorComponentBase
    {
        [Header("3D Properties", order = 100)]
        [Range(-10f, 10f)]
        [Tooltip("Ray start is offset up or down by this amount.")]
        public float startVerticalOffset;

        [Range(-100f, 100f)]
        [Tooltip("Ray end is offset up or down by this amount.")]
        public float endVerticalOffset;

        public override RayPerceptionSensor.CastType GetCastType()
        {
            return RayPerceptionSensor.CastType.Cast3D;
        }

        public override float GetStartVerticalOffset()
        {
            return startVerticalOffset;
        }

        public override float GetEndVerticalOffset()
        {
            return endVerticalOffset;
        }
        public override ISensor CreateSensor()
        {
            RayPerceptionSensor.RayCastHitObserver rcho = null;
            var rayAngles = GetRayAngles(raysPerDirection, maxRayDegrees);
            rcho = gameObject.GetComponent<IAgentDelegate>().GetObs();

            m_RaySensor = new RayPerceptionSensor(sensorName, rayLength, detectableTags, rayAngles,
                transform, GetStartVerticalOffset(), GetEndVerticalOffset(), sphereCastRadius, GetCastType(),
                rayLayerMask, rcho
            );

            if (observationStacks != 1)
            {
                var stackingSensor = new StackingSensor(m_RaySensor, observationStacks);
                return stackingSensor;
            }

            return m_RaySensor;
        }
    }
}
