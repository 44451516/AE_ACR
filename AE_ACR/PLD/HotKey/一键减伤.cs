﻿using System.Numerics;
using AE_ACR.PLD.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using ImGuiNET;

namespace AE_ACR.PLD.HotKey;

public class 一键减伤 : IHotkeyResolver
{
    public uint SpellId;

    public 一键减伤()
    {
        SpellId = PLDBaseSlotResolvers.铁壁;
    }

    public void Draw(Vector2 size)
    {
        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);

        if (Core.Resolve<MemApiIcon>().GetActionTexture(SpellId, out var textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        // SpellHelper.DrawSpellInfo(SpellId.GetSpell(), size, isActive);
    }



    public int Check()
    {
        if (PLDBaseSlotResolvers.铁壁.IsReady())
        {
            SpellId = PLDBaseSlotResolvers.圣盾阵;
            return 0;
        }

        if (PLDBaseSlotResolvers.圣盾阵.IsReady())
        {
            SpellId = PLDBaseSlotResolvers.预警;
            return 0;
        }


        if (PLDBaseSlotResolvers.预警.IsReady())
        {
            SpellId = PLDBaseSlotResolvers.壁垒;
            return 0;
        }

        if (PLDBaseSlotResolvers.壁垒.IsReady())
        {
            SpellId = PLDBaseSlotResolvers.神圣领域;
            return 0;
        }

        if (PLDBaseSlotResolvers.神圣领域.IsReady())
        {
            SpellId = PLDBaseSlotResolvers.圣盾阵;
            return 0;
        }


        return -1;
    }

    public void Run()
    {
        Spell spell = null;

        if (PLDBaseSlotResolvers.神圣领域.IsReady())
        {
            spell = new Spell(PLDBaseSlotResolvers.神圣领域, Core.Me);
        }


        if (PLDBaseSlotResolvers.壁垒.IsReady())
        {
            spell = new Spell(PLDBaseSlotResolvers.壁垒, Core.Me);
        }

        if (PLDBaseSlotResolvers.预警.IsReady())
        {
            spell = new Spell(PLDBaseSlotResolvers.预警, Core.Me);
        }


        if (PLDBaseSlotResolvers.圣盾阵.IsReady())
        {
            spell = new Spell(PLDBaseSlotResolvers.圣盾阵, Core.Me);
        }


        if (PLDBaseSlotResolvers.铁壁.IsReady())
        {
            spell = new Spell(PLDBaseSlotResolvers.铁壁, Core.Me);
        }


        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new();

        AI.Instance.BattleData.NextSlot.Add(spell);

    }
}