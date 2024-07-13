﻿using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.GLA.SlotResolvers
{
    public class DK_Ability_暗黑锋 : ISlotResolver
    {
        public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;

        public int Check()
        {
            if (GCDHelper.GetGCDCooldown() < 600)
            {
                return -1;
            }


            if (LastSpell == Core.Resolve<MemApiSpell>().CheckActionChange(DKData.暗黑锋).GetSpell().Id)
            {

                return -1;
            }


            var gauge = Core.Resolve<JobApi_DarkKnight>();

            var darksideTimeRemaining = gauge.DarksideTimeRemaining;

            if (Core.Me.CurrentMp >= 3000 || gauge.HasDarkArts)
            {
                if (darksideTimeRemaining <= 10 * 1000)
                {
                    return 0;
                }

                
                if (Core.Me.TargetObject is IBattleChara chara)
                {
                    if (chara.CurrentHp <= DKSettings.Instance.get爆发目标血量())
                    {
                        return 0;
                    }
                }


                if (Core.Me.CurrentMp >= 9800)
                {
                    return 0;
                }


                if (Core.Me.CurrentMp >= 9800)
                {
                    return 0;
                }


                if (Core.Me.CurrentMp >= 9200 && LastBaseGcd == DKData.单体2SyphonStrike)
                {
                    return 0;
                }

                if (Core.Me.CurrentMp >= 8600 && LastBaseGcd == DKData.单体2SyphonStrike && GameObjectExtension.HasAura(Core.Me, DKData.Buffs.嗜血BloodWeapon))
                {
                    return 0;
                }

                if (Core.Me.CurrentMp >= 9200 && GameObjectExtension.HasAura(Core.Me, DKData.Buffs.嗜血BloodWeapon))
                {
                    return 0;
                }

                if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
                {
                    return -1;
                }

                if (RaidBuff.爆发期())
                {
                    return 0;
                }

                //泄蓝
                if (DKData.血乱Delirium.GetCooldownRemainingTime() > 40 && Core.Me.CurrentMp >= DKSettings.Instance.保留蓝量 + 3000)
                {
                    return 0;
                }
            }


            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(Core.Resolve<MemApiSpell>().CheckActionChange(DKData.暗黑锋).GetSpell());
        }
    }
}