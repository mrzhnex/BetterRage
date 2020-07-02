using UnityEngine;
using RemoteAdmin;
using EXILED.Extensions;

namespace BetterRage
{
    class BroadcastSCP : MonoBehaviour
    {
        private readonly float TimeIsUp = 0.3f;
        private float Timer = 0.0f;
        private ReferenceHub Scp;

        public void Start()
        {
            Scp = Player.GetPlayer(gameObject);
        }

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > TimeIsUp)
            {
                Timer = 0.0f;
                if (Player.GetPlayer(Scp.GetPlayerId()).GetRole() != RoleType.Scp096)
                {
                    Global.lookerTo096 = new System.Collections.Generic.List<int>();
                    Destroy(gameObject.GetComponent<BroadcastSCP>());
                }
                foreach (ReferenceHub p in Player.GetHubs())
                {
                    if (p.GetTeam() == Team.SCP || p.GetRole() == RoleType.Spectator || p.GetRole() == RoleType.Tutorial || Global.lookerTo096.Contains(p.GetPlayerId()))
                    {
                        continue;
                    }
                    if (LookFace096(p))
                    {
                        Global.lookerTo096.Add(p.GetPlayerId());
                        p.ClearBroadcasts();
                        p.Broadcast(15, "Вы увидели лицо SCP-096", false);
                    }
                }
                if (Global.lookerTo096.Count > 0)
                {
                    Scp.serverRoles.BypassMode = true;
                    Scp.ClearBroadcasts();
                    Scp.Broadcast(2, "Ближайшая цель: " + GetClosedPlayerDistance()[0] + " расстояние: " + GetClosedPlayerDistance()[1], true);
                }
                else
                {
                    Scp.serverRoles.BypassMode = false;
                }
            }
        }

        private string[] GetClosedPlayerDistance()
        {
            float _distance = 99999.0f;
            string name = string.Empty;
            foreach (int id in Global.lookerTo096)
            {
                ReferenceHub referenceHub = Player.GetPlayer(id);
                if (Vector3.Distance(gameObject.transform.position, referenceHub.transform.position) < _distance)
                {
                    _distance = Vector3.Distance(Scp.GetPosition(), referenceHub.GetPosition());
                    name = referenceHub.nicknameSync.Network_myNickSync;
                }
            }
            return new string[] { name, System.Math.Round(_distance, 2).ToString() };
        }

        private bool LookFace096(ReferenceHub player)
        {
            try
            {
                GameObject gameObject2 = player.gameObject;
                GameObject camera = gameObject2.GetComponent<Scp049PlayerScript>().plyCam;
                float num = Vector3.Angle(camera.transform.forward, (Global.scp096obj.transform.position - gameObject2.transform.position).normalized);
                float num2 = Vector3.Angle(Global.scp096obj.GetComponent<Scp049PlayerScript>().plyCam.transform.forward, (gameObject2.transform.position - Global.scp096obj.transform.position).normalized);
                if (num <= 50f && num2 <= 50f && !Physics.Linecast(player.GetPosition(), Global.scp096obj.transform.position, 1207976449)
                    && Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 20.0f)
                    && (Vector3.Distance(gameObject2.transform.position, Global.scp096obj.transform.position) > Global.saveDistance))
                {
                    if (hit.transform.GetComponent<QueryProcessor>() != null && hit.transform.GetComponent<QueryProcessor>().PlayerId == Global.scp096obj.GetComponent<QueryProcessor>().PlayerId)
                    {
                        return true;
                    }
                }
            }
            catch (System.NullReferenceException)
            {

            }
            return false;
        }

    }
}
