#region

using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.DRK.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR_DRK.SlotResolvers;

public class DK_GCD_Base : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 释放Unleash)
        {
            return -1;
        }

        return 0;
    }

    public override void Build(Slot slot)

    {
        var spell = GetBaseGCD();
        slot.Add(spell);
    }

    public static Spell GetBaseGCD()
    {
        if (DKSettings.Instance.日常模式)
        {
            if (Core.Me.TargetObject is IBattleChara battleChara)
            {
                if (伤残.IsUnlock())
                {
                    if (TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me) is >= 10 and <= 20)
                    {
                        return 伤残.GetSpell();
                    }
                }
            }
        }

        if (lastComboActionID == 单体1HardSlash)
        {
            return 单体2SyphonStrike.GetSpell();
        }

        if (lastComboActionID == 单体2SyphonStrike && getQTValue(BaseQTKey.攒资源) == false)
        {
            if (Core.Resolve<JobApi_DarkKnight>().Blood >= 80 && 血溅Bloodspiller.IsUnlock())
            {
                var spell = Core.Resolve<MemApiSpell>().CheckActionChange(血溅Bloodspiller).GetSpell();
                return spell;
            }
        }

        if (lastComboActionID == 单体2SyphonStrike)
        {
            return 单体3Souleater.GetSpell();
        }
        return 单体1HardSlash.GetSpell();
    }
}