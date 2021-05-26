using UnityEngine;

namespace Runtime.LightSystem
{
    public interface ILightReceiver
    {
        public void LightReceive(Vector3 from);
        public void LightDisconnect();
        public void FixReceiverBeams(GameObject receivedFromBeam);
        public Vector3 GetClosestBeamTarget(GameObject beamObject);
        public void FixBeamSize(GameObject beam);

        public void InstantiateBeams();
    }
}
