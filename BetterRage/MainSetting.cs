using Exiled.API.Features;

namespace BetterRage
{
    public class MainSetting : Plugin<Config>
    {
        public override string Name => nameof(BetterRage);
        public SetEvent SetEvent { get; set; }

        public override void OnEnabled()
        {
            SetEvent = new SetEvent();
            Exiled.Events.Handlers.Server.WaitingForPlayers += SetEvent.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.ChangingRole += SetEvent.OnChangingRole;
            Exiled.Events.Handlers.Player.Hurting += SetEvent.OnHurting;
            Exiled.Events.Handlers.Scp096.CalmingDown += SetEvent.OnCalmingDown;
            Exiled.Events.Handlers.Player.Died += SetEvent.OnDied;
            Exiled.Events.Handlers.Scp096.AddingTarget += SetEvent.OnAddingTarget;
            Exiled.Events.Handlers.Server.SendingConsoleCommand += SetEvent.OnSendingConsoleCommand;
            Log.Info(Name + " on");
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= SetEvent.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.ChangingRole -= SetEvent.OnChangingRole;
            Exiled.Events.Handlers.Player.Hurting -= SetEvent.OnHurting;
            Exiled.Events.Handlers.Scp096.CalmingDown -= SetEvent.OnCalmingDown;
            Exiled.Events.Handlers.Player.Died -= SetEvent.OnDied;
            Exiled.Events.Handlers.Scp096.AddingTarget -= SetEvent.OnAddingTarget;
            Exiled.Events.Handlers.Server.SendingConsoleCommand -= SetEvent.OnSendingConsoleCommand;
            Log.Info(Name + " off");
        }
    }
}