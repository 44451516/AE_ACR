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
    public class Ability_战逃反应 : ISlotResolver
    {
     
        public int Check()
        {
            if (UIntExtensions.CanWeave())
            {
                if (Data.Data.战逃反应FightOrFlight.ActionReady())
                {
                    return 0;
                }
            }

            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(Data.Data.战逃反应FightOrFlight.OriginalHook());
        }
    }
}