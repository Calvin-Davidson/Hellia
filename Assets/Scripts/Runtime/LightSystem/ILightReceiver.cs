using UnityEngine;

namespace Runtime.LightSystem
{
    public interface ILightReceiver : ILightComponent
    {
        public void LightReceive(ILightComponent lightComponent);
        public void LightDisconnect(ILightComponent lightComponent);
        public void FixReceiverBeams(GameObject receivedFromBeam);
        public Vector3 GetClosestBeamTarget(GameObject beamObject);
        public void FixBeamSize(GameObject beam);
#if (UNITY_EDITOR)
        public void InstantiateBeams();
#endif
    }
}
