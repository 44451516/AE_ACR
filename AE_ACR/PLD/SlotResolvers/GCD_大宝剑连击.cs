#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_大宝剑连击 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!getQTValue(PLDQTKey.大宝剑连击))
        {
            return Flag_QT;
        }


        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (GetResourceCost(大保健连击Confiteor) <= Core.Me.CurrentMp)
        {
            if (和目标的距离() > 25f)
            {
                return Flag_超出攻击距离;
            }

            if (大保健连击Confiteor.OriginalHook().Id.ActionReady())
            {
                return 0;
            }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {

        var spell = 大保健连击Confiteor.OriginalHook();

        // if (PLDSettings.Instance.M6S设置 && CombatTime.Instance.CombatEngageDuration().TotalSeconds > 281)
        if (false)
        {
            IBattleChara? 鱼 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 18346 && x.IsValid() && x is { IsDead: false, IsTargetable: true } && x.CurrentHpPercent() <= 0.95f);
            if (鱼 != null)
            {
                spell = new Spell(大保健连击Confiteor.OriginalHook().Id, 鱼);
            }

            IBattleChara? 猫 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 18347 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
            if (猫 != null)
            {
                spell = new Spell(大保健连击Confiteor.OriginalHook().Id, 猫);
            }


            IBattleChara? 人马 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 18345 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
            if (人马 != null)
            {
                spell = new Spell(大保健连击Confiteor.OriginalHook().Id, 人马);
            }


            // IBattleChara? 木人厕所 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.EntityId == 1073743855 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
            // if (木人厕所 != null)
            // {
            //     spell = new Spell(大保健连击Confiteor.OriginalHook().Id, 木人厕所);
            // } 
        }

        slot.Add(spell);
    }
}