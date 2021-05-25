using UnityEngine;

namespace Runtime.LightSystem
{
    public interface ILightReceiver
    {
        public void LightReceive(Vector3 from);
        public void LightDisconnect();
    }
}
