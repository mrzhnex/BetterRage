using Exiled.Events.EventArgs;
using System.Collections.Generic;

namespace BetterRage
{
    public class SetEvent
    {
        public void OnWaitingForPlayers()
        {
            Global.Targets = new List<int>();
        }

        internal void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != null && ev.Attacker.Role == RoleType.Scp096 && !Global.Targets.Contains(ev.Target.Id))
                ev.Amount = 0.0f;
        }

        internal void OnEnraging(EnragingEventArgs ev)
        {
            if (Global.Targets.Count == 0)
                ev.IsAllowed = false;
        }

        internal void OnCalmingDown(CalmingDownEventArgs ev)
        {
            if (Global.Targets.Count > 0)
                ev.IsAllowed = false;
        }

        internal void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (Global.Targets.Contains(ev.Player.Id))
                Global.Targets.Remove(ev.Player.Id);
            if (ev.Player.GameObject.GetComponent<ScpBehaviour>())
            {
                UnityEngine.Object.Destroy(ev.Player.GameObject.GetComponent<ScpBehaviour>());
            }
            if (ev.Player.Role == RoleType.Scp096)
            {
                ev.Player.GameObject.AddComponent<ScpBehaviour>();
            }
        }
    }
}