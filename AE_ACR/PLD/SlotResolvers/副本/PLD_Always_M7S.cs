using System.Numerics;
using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.PLD.SlotResolvers.副本;

public class PLD_Always_M7S : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (PLDSettings.Instance.M7S设置 && Core.Resolve<MemApiZoneInfo>().GetCurrTerrId() == 地区ID.m7s)
        {
            if (CanWeave())
            {
                if (沉默.ActionReady2())
                {
                    if (GetSpell_沉默() != null)
                    {
                        return 1;
                    }

                }

                if (挑衅.ActionReady2())
                {
                    if (GetSpell_挑衅() != null)
                    {
                        return 2;
                    }
                }

            }
            else
            {
                if (GetSpell_投盾() != null)
                {
                    return 3;
                }

            }


        }


        return -1;
    }

    public override void Build(Slot slot)
    {
        {
            var spell = GetSpell_沉默();
            if (spell != null)
            {
                slot.Add(spell);
            }
        }

        {
            var spell = GetSpell_挑衅();
            if (spell != null)
            {
                slot.Add(spell);
            }
        }

        {
            var spell = GetSpell_投盾();
            if (spell != null)
            {
                slot.Add(spell);
            }
        }
    }

    private Spell? GetSpell_沉默()
    {
        if (CombatTime.Instance.CombatEngageDuration().TotalSeconds >= 55 && CombatTime.Instance.CombatEngageDuration().TotalSeconds <= 77)
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
                        float 和我的距离 = TargetHelper.GetTargetDistanceFromMeTest2D(MT小怪, Core.Me);
                        if (和我的距离 <= 3)
                        {
                            return new Spell(沉默, MT小怪);
                        }
                    }
                }
                else
                {
                    IBattleChara? ST小怪 = TargetMgr.Instance.EnemysIn12.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.CastActionId == 读条ID.m7s_小怪_月环 && x.IsValid() && x is { IsDead: false, IsTargetable: true }).OrderByDescending(x => x.GameObjectId).FirstOrDefault();
                    if (ST小怪 != null)
                    {
                        float 和我的距离 = TargetHelper.GetTargetDistanceFromMeTest2D(ST小怪, Core.Me);
                        if (和我的距离 <= 3)
                        {
                            return new Spell(沉默, ST小怪);
                        }
                    }
                }
            }
        }
        return null;
    }


    private Spell? GetSpell_挑衅()
    {
        if (CombatTime.Instance.CombatEngageDuration().TotalSeconds >= 55 && CombatTime.Instance.CombatEngageDuration().TotalSeconds <= 70
            || CombatTime.Instance.CombatEngageDuration().TotalSeconds >= 495 && CombatTime.Instance.CombatEngageDuration().TotalSeconds <= 515)
        {
            IBattleChara? boss = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 怪物ID.m7s_boss && x.IsValid() && x is { IsDead: false, IsTargetable: true });

            if (boss != null)
            {
                //mt
                if (boss.仇恨是否在自己身上())
                {
                    IEnumerable<IBattleChara> battleCharas = TargetMgr.Instance.EnemysIn25.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.IsValid() && x is { IsDead: false, IsTargetable: true });

                    foreach (var battleChara in battleCharas)
                    {
                        //西 
                        //P1
                        {
                            Vector3 vector3 = new Vector3(100.790f, 0, 116.475f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10)
                            {
                                return new Spell(挑衅, battleChara);
                            }
                        }

                        //P3
                        {
                            Vector3 vector3 = new Vector3(84.236f, -200, 5.024f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10)
                            {
                                return new Spell(挑衅, battleChara);
                            }
                        }

                    }
                }
                else
                {
                    IEnumerable<IBattleChara> battleCharas = TargetMgr.Instance.EnemysIn25.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.IsValid() && x is { IsDead: false, IsTargetable: true });

                    foreach (var battleChara in battleCharas)
                    {
                        //东 
                        //P1 
                        {
                            Vector3 vector3 = new Vector3(116.214f, 0, 101.302f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10)
                            {
                                return new Spell(挑衅, battleChara);
                            }
                        }

                        //P3 
                        {
                            Vector3 vector3 = new Vector3(116.082f, -200.000f, 5.059f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10)
                            {
                                return new Spell(挑衅, battleChara);
                            }
                        }


                    }
                }
            }
        }
        return null;
    }


    private Spell? GetSpell_投盾()
    {
        if (CombatTime.Instance.CombatEngageDuration().TotalSeconds >= 55 && CombatTime.Instance.CombatEngageDuration().TotalSeconds <= 70
            || CombatTime.Instance.CombatEngageDuration().TotalSeconds >= 495 && CombatTime.Instance.CombatEngageDuration().TotalSeconds <= 515)
        {
            IBattleChara? boss = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 怪物ID.m7s_boss && x.IsValid() && x is { IsDead: false, IsTargetable: true });

            if (boss != null)
            {
                //mt
                if (boss.仇恨是否在自己身上())
                {
                    IEnumerable<IBattleChara> battleCharas = TargetMgr.Instance.EnemysIn25.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.IsValid() && x is { IsDead: false, IsTargetable: true });

                    foreach (var battleChara in battleCharas)
                    {
                        float 和我的距离 = TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me);
                        //北 P1
                        {
                            Vector3 vector3 = new Vector3(98.756f, 0, 83.583f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10 && 和我的距离 <= 15 && (!battleChara.仇恨是否在自己身上() || battleChara.CurrentHp == battleChara.MaxHp))
                            {
                                return new Spell(投盾ShieldLob, battleChara);
                            }
                        }

                        //北 P3
                        {
                            Vector3 vector3 = new Vector3(100.020f, -200.000f, -11.403f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10 && 和我的距离 <= 15 &&(!battleChara.仇恨是否在自己身上() || battleChara.CurrentHp == battleChara.MaxHp))
                            {
                                return new Spell(投盾ShieldLob, battleChara);
                            }
                        }


                    }
                }
                else
                {
                    IEnumerable<IBattleChara> battleCharas = TargetMgr.Instance.EnemysIn25.Values.Where(x => x.DataId == 怪物ID.m7s_小怪 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
                    foreach (var battleChara in battleCharas)
                    {
                        float 和我的距离 = TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me);
                        //南 P1 
                        {
                            Vector3 vector3 = new Vector3(97.718f, 0, 116.466f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10 && 和我的距离 <= 15 &&(!battleChara.仇恨是否在自己身上() || battleChara.CurrentHp == battleChara.MaxHp))
                            {
                                return new Spell(投盾ShieldLob, battleChara);
                            }
                        }

                        //南 P3
                        {
                            Vector3 vector3 = new Vector3(102.255f, -200f, 21.839f);
                            float distance = Vector3.Distance(battleChara.Position, vector3);
                            if (distance <= 10 && 和我的距离 <= 15 &&(!battleChara.仇恨是否在自己身上() || battleChara.CurrentHp == battleChara.MaxHp))
                            {
                                return new Spell(投盾ShieldLob, battleChara);
                            }
                        }
                    }
                }
            }
        }
        return null;
    }
}