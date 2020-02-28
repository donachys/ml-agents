using System;
using UnityEngine;

namespace MLAgents
{
    public interface IAgentDelegate
    {
        RayPerceptionInput.RayCastHitObserver GetObs();
    }
}
