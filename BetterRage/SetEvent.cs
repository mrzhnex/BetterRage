using Exiled.Events.EventArgs;
using System;
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
                ev.IsAllowed = false;
        }

        internal void OnCalmingDown(CalmingDownEventArgs ev)
        {
            if (Global.Targets.Count != 0)
                ev.IsAllowed = false;
        }

        internal void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (!Global.Targets.Contains(ev.Target.Id))
                Global.Targets.Add(ev.Target.Id);
            ev.Target.ClearBroadcasts();
            ev.Target.Broadcast(15, "Вы увидели лицо SCP-096", Broadcast.BroadcastFlags.Normal);
        }

        internal void OnSendingConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
            if (ev.Name.ToLower() == "stoprage")
            {
                if (ev.Player.Role == RoleType.Scp096)
                {
                    Global.Targets = new List<int>();
                    ev.ReturnMessage = "Вы очистили список целей";
                    return;
                }
                else
                {
                    ev.ReturnMessage = "Вы не можете использовать эту команду!";
                    return;
                }
            }
        }

        internal void OnDied(DiedEventArgs ev)
        {
            if (Global.Targets.Contains(ev.Target.Id))
                Global.Targets.Remove(ev.Target.Id);
        }

        internal void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (Global.Targets.Contains(ev.Player.Id))
                Global.Targets.Remove(ev.Player.Id);
            if (ev.Player.GameObject.GetComponent<ScpBehaviour>())
            {
                Global.Targets = new List<int>();
                UnityEngine.Object.Destroy(ev.Player.GameObject.GetComponent<ScpBehaviour>());
            }
            if (ev.NewRole == RoleType.Scp096)
            {
                ev.Player.GameObject.AddComponent<ScpBehaviour>();
            }
        }
    }
}