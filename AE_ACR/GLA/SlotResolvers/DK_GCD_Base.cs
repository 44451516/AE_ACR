using AE_ACR_DRK_Setting;
using AE_ACR.GLA.Data;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace AE_ACR.GLA.SlotResolvers;

public class DK_GCD_Base : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    // 返回>=0表示检测通过 即将调用Build方法
    public int Check()
    {
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == Data.Data.释放Unleash)
        {
            return -1;
        }

        return 0;
    }

    public static Spell GetBaseGCD()
    {
        // 如果自己有直线射击预备buff 或者最近1秒内使用过纷乱
        // if (Core.Me.HasAura(AurasDefine.SearingLight) || SpellsDefine.Provoke.RecentlyUsed())
        // {
        // return GetStraighterShot();
        // }
        // return GetHeavyShot();
        // return AurasDefine.SearingLight;
        if (LastBaseGcd == Data.Data.单体1HardSlash)
        {
            return SpellHelper.GetSpell(Data.Data.单体2SyphonStrike);
        }

        if (LastBaseGcd == Data.Data.单体2SyphonStrike && DKSettings.Instance.留资源 == false)
        {
            if (Core.Resolve<JobApi_DarkKnight>().Blood >= 80 && Data.Data.血溅Bloodspiller.IsUnlock())
            {
                Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(Data.Data.血溅Bloodspiller).GetSpell();

                return spell;
            }
        }


        if (LastBaseGcd == Data.Data.单体2SyphonStrike)
        {
            return SpellHelper.GetSpell(Data.Data.单体3Souleater);
        }


        return SpellHelper.GetSpell(Data.Data.单体1HardSlash);
    }


    // 将指定技能加入技能队列中
    public void Build(Slot slot)

    {
        Spell spell = GetBaseGCD();
        slot.Add(spell);
    }
}