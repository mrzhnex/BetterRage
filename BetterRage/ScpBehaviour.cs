using UnityEngine;
using Exiled.API.Features;

namespace BetterRage
{
    public class ScpBehaviour : MonoBehaviour
    {
        private readonly float TimeIsUp = 0.3f;
        private float Timer = 0.0f;
        private Player Scp;
        private int LastCount = Global.Targets.Count;
        public void Start()
        {
            Scp = Player.Get(gameObject);
        }

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > TimeIsUp)
            {
                Timer = 0.0f;
                if (Global.Targets.Count == 0)
                {
                    if (LastCount != Global.Targets.Count)
                    {
                        LastCount = Global.Targets.Count;                  
                        Scp.ReferenceHub.serverRoles.BypassMode = false;
                    }
                }
                else
                {
                    Scp.ReferenceHub.serverRoles.BypassMode = true;
                    Scp.ClearBroadcasts();
                    Scp.Broadcast(2, "Ближайшая цель: " + GetClosedPlayerDistance()[0] + " расстояние: " + GetClosedPlayerDistance()[1], Broadcast.BroadcastFlags.Monospaced);
                }
            }
        }

        private string[] GetClosedPlayerDistance()
        {
            float distance = 99999.0f;
            string name = string.Empty;
            foreach (int id in Global.Targets)
            {
                Player player = Player.Get(id);
                if (Vector3.Distance(gameObject.transform.position, player.Position) < distance)
                {
                    distance = Vector3.Distance(Scp.Position, player.Position);
                    name = player.Nickname;
                }
            }
            return new string[] { name, System.Math.Round(distance, 2).ToString() };
        }
    }
}