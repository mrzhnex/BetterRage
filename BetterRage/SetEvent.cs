using EXILED;
using EXILED.Extensions;
using System.Collections.Generic;

namespace BetterRage
{
    public class SetEvent
    {
        public void OnWaitingForPlayers()
        {
            Global.lookerTo096 = new List<int>();
        }

        public void OnSpawn(PlayerSpawnEvent ev)
        {
            if (Global.lookerTo096.Contains(ev.Player.GetPlayerId()))
                Global.lookerTo096.Remove(ev.Player.GetPlayerId());
            if (ev.Player.GetRole() == RoleType.Scp096)
            {
                Global.scp096obj = ev.Player.gameObject;
                if (Global.scp096obj.GetComponent<BroadcastSCP>())
                    UnityEngine.Object.Destroy(Global.scp096obj.GetComponent<BroadcastSCP>());
                Global.scp096obj.AddComponent<BroadcastSCP>();
            }
        }

        public void OnScp096Calm(ref Scp096CalmEvent ev)
        {
            if (Global.lookerTo096.Count > 0)
                ev.Allow = false;
        }

        public void OnScp096Enrage(ref Scp096EnrageEvent ev)
        {
            if (Global.lookerTo096.Count == 0)
                ev.Allow = false;
        }

        public void OnRoundEnd()
        {
            Global.lookerTo096 = new List<int>();
        }

        public void OnPlayerDie(ref PlayerDeathEvent ev)
        {
            if (Global.lookerTo096.Contains(ev.Player.GetPlayerId()))
                Global.lookerTo096.Remove(ev.Player.GetPlayerId());
            if (ev.Player.gameObject.GetComponent<BroadcastSCP>())
                UnityEngine.Object.Destroy(ev.Player.gameObject.GetComponent<BroadcastSCP>());
        }

        public void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (ev.Attacker.GetRole() == RoleType.Scp096 && !Global.lookerTo096.Contains(ev.Player.GetPlayerId()))
                ev.Amount = 0.0f;
        }
    }
}