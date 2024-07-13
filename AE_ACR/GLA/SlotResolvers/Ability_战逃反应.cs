using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.GLA.SlotResolvers
{
    public class Ability_战逃反应 : GLABaseSlotResolvers
    {
     
        public override int Check()
        {
            if (BaseIslotResolver.CanWeave())
            {
                if (战逃反应FightOrFlight.ActionReady())
                {
                    return 0;
                }
            }

            return -1;
        }


        public override void Build(Slot slot)
        {
            slot.Add(战逃反应FightOrFlight.OriginalHook());
        }
    }
}