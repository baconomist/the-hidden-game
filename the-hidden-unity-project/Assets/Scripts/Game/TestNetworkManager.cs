using Mirror;
using UnityEngine;

namespace Game
{
    public class TestNetworkManager : NetworkManager
    {
        [Header("Test Network Manager")]
        public bool startHostOnLoad = true;
        
        
        public override void Start()
        {
            base.Start();
            
            if(startHostOnLoad && !NetworkClient.active)
                StartHost();
        }
    }
}