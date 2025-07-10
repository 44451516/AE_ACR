#region

using System.Numerics;
using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_沉默 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (CanWeave())
        {
            if (沉默.ActionReady2())
            {
                if (和目标的距离() > 3)
                {
                    return Flag_超出攻击距离;
                }

                if (GetSpell() != null)
                {
                    return 0;
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
        // if (PLDSettings.Instance.M7S设置)
        {
            IBattleChara? boss = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 怪物ID.m7s_boss && x.IsValid() && x is { IsDead: false, IsTargetable: true });

            if (boss != null)
            {
                //mt
                if (boss.仇恨是否在自己身上())
                {
                    IBattleChara? MT小怪 = TargetMgr.Instance.EnemysIn12.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.CastActionId == 读条ID.m7s_小怪_月环 && x.IsValid() && x is { IsDead: false, IsTargetable: true }).OrderBy(x => x.GameObjectId).FirstOrDefault();


                    if (MT小怪 != null)
                    {
                        return new Spell(沉默, MT小怪);
                    }
                }
                else
                {
                    IBattleChara? ST小怪 = TargetMgr.Instance.EnemysIn12.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.CastActionId == 读条ID.m7s_小怪_月环 && x.IsValid() && x is { IsDead: false, IsTargetable: true }).OrderByDescending(x => x.GameObjectId).FirstOrDefault();


                    if (ST小怪 != null)
                    {
                        return new Spell(沉默, ST小怪);
                    }
                }
            }  
        }
        return null;
    }
}