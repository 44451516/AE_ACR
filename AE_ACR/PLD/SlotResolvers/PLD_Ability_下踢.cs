#region

using System.Numerics;
using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLD_Ability_下踢 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }


        if (CanWeave())
        {
            if (下踢.ActionReady2())
            {
                if (PLDSettings.Instance.M6S设置 && Core.Resolve<MemApiZoneInfo>().GetCurrTerrId() == 地区ID.m6s)
                {
                    if (GetSpell() != null)
                    {
                        return 1;
                    }
                }

            }
        }

        return -1;
    }

    public override void Build(Slot slot)
    {

        var spell = GetSpell();
        if (spell != null)
        {
            slot.Add(spell);
        }
    }


    private Spell? GetSpell()
    {
        IBattleChara? 炸脖龙 = TargetMgr.Instance.EnemysIn12.Values.FirstOrDefault(x => x.DataId == 怪物ID.m6s_炸脖龙 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
        if (炸脖龙 != null)
        {

            float 和我的距离 = TargetHelper.GetTargetDistanceFromMeTest2D(炸脖龙, Core.Me);
            //MT位置
            {
                Vector3 MT第一下 = new Vector3(100.269f, 0f, 108.292f);
                float distance = Vector3.Distance(炸脖龙.Position, MT第一下);
                if (distance < 8.0f && 和我的距离 <= 3f)
                {
                    return new Spell(下踢, 炸脖龙);
                }
            }

            //ST位置
            {
                Vector3 ST第一下 = new Vector3(100.772f, 0f, 90.656f);
                float distance = Vector3.Distance(炸脖龙.Position, ST第一下);
                if (distance < 5.0f && 和我的距离 <= 3f)
                {
                    return new Spell(下踢, 炸脖龙);
                }

            }

            //第二只 
            {
                Vector3 MT第二下 = new Vector3(107.600f, 0f, 101.370f);
                float distance = Vector3.Distance(炸脖龙.Position, MT第二下);
                if (distance < 8.0f && 和我的距离 <= 3f)
                {
                    return new Spell(下踢, 炸脖龙);
                }
            }
        }

        return null;
    }
}