#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.GLA.SlotResolvers.减伤;

public class Ability_血仇 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (雪仇.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) >= 4)
            {
                return 0;
            }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(雪仇.OriginalHook());
    }
}