#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_黑盾 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {

            if (Buffs.暗影墙.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (Buffs.暗影墙v2.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (至黑之夜.ActionReady() && Core.Me.CurrentMp >= 3000)
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (Core.Me.CurrentHpPercent() < 0.88f)
                    {
                        if (attackMeCount() >= 3)
                        {
                            return 0;
                        }


                        if (TargetHelper.IsBoss(target))
                        {
                            return 0;
                        }
                    }

                    if (TargetHelper.TargercastingIsDeathSentence(target, 6))
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
        slot.Add(至黑之夜.OriginalHook());
    }
}