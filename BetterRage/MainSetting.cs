using EXILED;

namespace BetterRage
{
    public class MainSetting : Plugin
    {
        public override string getName => "BetterRage";
        private SetEvent SetEvent;

        public override void OnEnable()
        {
            SetEvent = new SetEvent();
            Events.WaitingForPlayersEvent += SetEvent.OnWaitingForPlayers;
            Events.Scp096EnrageEvent += SetEvent.OnScp096Enrage;
            Events.RoundEndEvent += SetEvent.OnRoundEnd;
            Events.PlayerDeathEvent += SetEvent.OnPlayerDie;
            Events.PlayerHurtEvent += SetEvent.OnPlayerHurt;
            Events.PlayerSpawnEvent += SetEvent.OnSpawn;
            Events.Scp096CalmEvent += SetEvent.OnScp096Calm;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.WaitingForPlayersEvent -= SetEvent.OnWaitingForPlayers;
            Events.Scp096EnrageEvent -= SetEvent.OnScp096Enrage;
            Events.RoundEndEvent -= SetEvent.OnRoundEnd;
            Events.PlayerDeathEvent -= SetEvent.OnPlayerDie;
            Events.PlayerHurtEvent -= SetEvent.OnPlayerHurt;
            Events.PlayerSpawnEvent -= SetEvent.OnSpawn;
            Events.Scp096CalmEvent -= SetEvent.OnScp096Calm;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}