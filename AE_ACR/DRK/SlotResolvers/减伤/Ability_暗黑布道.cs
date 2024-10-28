#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_暗黑布道 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {
            if (暗黑布道.ActionReady() )
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.TargercastingIsbossaoe(target,10_000))
                    {
                        return 0;
                    }
                }
               
            }
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(暗黑布道.OriginalHook());
    }
}