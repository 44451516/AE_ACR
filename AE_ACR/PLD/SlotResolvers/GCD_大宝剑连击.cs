#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

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


        if (HasEffect(Buffs.Requiescat) && GetResourceCost(大保健连击Confiteor) <= Core.Me.CurrentMp)
        {
            if (和目标的距离() > 25f)
            {
                return Flag_超出攻击距离;
            }
            
            if (大保健连击Confiteor.OriginalHook().Id.ActionReady())
            {
                return 0;
            }

            //这里是圣灵基础AOE威力 135*5
            var aoeCount = TargetHelper.GetNearbyEnemyCount(5);

            if (aoeCount >= 5)
            {
                return Flag_伤害太低了;
            }
            
            return 0;
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        var spell = 圣灵HolySpirit.OriginalHook();

        if (BladeOfFaith.MyIsUnlock())
        {
            spell = 大保健连击Confiteor.OriginalHook();
        }

        if (HasEffect(Buffs.悔罪预备))
        {
            spell = 大保健连击Confiteor.OriginalHook();
        }


        slot.Add(spell);
    }
}