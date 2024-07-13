using AE_ACR_DRK_Setting;
using AE_ACR.PLD.Data;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_大宝剑连击 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (HasEffect(Buffs.悔罪预备) && GetResourceCost(大保健连击Confiteor) < Core.Me.CurrentMp)
        {
            return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(大保健连击Confiteor.OriginalHook());
    }
}