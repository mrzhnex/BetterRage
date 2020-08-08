using UnityEngine;
using RemoteAdmin;
using Exiled.API.Features;
using PlayableScps;

namespace BetterRage
{
    public class ScpBehaviour : MonoBehaviour
    {
        private readonly float TimeIsUp = 0.1f;
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
                foreach (Player player in Player.List)
                {
                    if (player.Team == Team.SCP || player.Role == RoleType.Spectator || player.Role == RoleType.Tutorial || Global.Targets.Contains(player.Id))
                    {
                        continue;
                    }
                    if (LookFace096(player))
                    {
                        Global.Targets.Add(player.Id);
                        player.ClearBroadcasts();
                        player.Broadcast(15, "Вы увидели лицо SCP-096", Broadcast.BroadcastFlags.Normal);
                    }
                }
                if (Global.Targets.Count == 0)
                {
                    if (LastCount != Global.Targets.Count)
                    {
                        LastCount = Global.Targets.Count;
                        (Scp.ReferenceHub.scpsController.CurrentScp as Scp096).EndEnrage();
                        Scp.ReferenceHub.serverRoles.BypassMode = false;
                    }
                }
                else
                {
                    Scp.ReferenceHub.serverRoles.BypassMode = true;
                    Scp.ClearBroadcasts();
                    Scp.Broadcast(2, "Ближайшая цель: " + GetClosedPlayerDistance()[0] + " расстояние: " + GetClosedPlayerDistance()[1], Broadcast.BroadcastFlags.Normal);
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

        private bool LookFace096(Player player)
        {
            if (Vector3.Distance(player.Position, transform.position) > Global.SaveDistance)
            {
                if (Vector3.Angle(player.PlayerCamera.forward, (transform.position - player.Position).normalized) <= 50f)
                {
                    if (Vector3.Angle(Scp.PlayerCamera.forward, (player.Position - gameObject.transform.position).normalized) <= 50f)
                    {
                        if (!Physics.Linecast(player.Position, transform.position, 1207976449))
                        {
                            if (Physics.Raycast(player.PlayerCamera.position, player.PlayerCamera.forward, out RaycastHit hit, 20.0f))
                            {
                                if (hit.transform.GetComponent<QueryProcessor>() != null && hit.transform.GetComponent<QueryProcessor>().PlayerId == Scp.Id)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}