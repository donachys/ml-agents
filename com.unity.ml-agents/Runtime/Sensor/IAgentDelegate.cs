using System;
using UnityEngine;

namespace MLAgents
{
    public interface IAgentDelegate
    {
        RayPerceptionSensor.RayCastHitObserver GetObs();
    }
}
