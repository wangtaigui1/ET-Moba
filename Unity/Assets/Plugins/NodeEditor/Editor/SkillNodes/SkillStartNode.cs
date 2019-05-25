//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年5月14日 15:11:40
//------------------------------------------------------------

#if UNITY_EDITOR

using System;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using NodeEditorFramework.Utilities;
using Sirenix.OdinInspector;
using SkillDemo;
using UnityEditor;
using UnityEngine;

namespace SkillDemo
{
    [Node(false, "Skill/技能初始化结点", typeof(SkillNodeCanvas))]
    public class SkillStartNode : Node
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        private const string Id = "技能初始化结点";

        /// <summary>
        /// 内部ID
        /// </summary>
        public override string GetID => Id;

        public override Vector2 DefaultSize => new Vector2(200,160);

        [ValueConnectionKnob("NextSkill", Direction.Out, "NextSkill", NodeSide.Right)]
        public ValueConnectionKnob NextSkill;

        [ValueConnectionKnob("PrevSkill", Direction.In, "NextSkill", NodeSide.Left)]
        public ValueConnectionKnob PrevSkill;

        /// <summary>
        /// 技能数据
        /// </summary>
        public NodeDataForStartSkill m_SkillData;

        public override BaseNodeData GetNodeData()
        {
            return m_SkillData;
        }

        public override void NodeGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            PrevSkill.DisplayLayout(new GUIContent("上一个"));

            GUILayout.EndVertical();
            GUILayout.BeginVertical();

            NextSkill.DisplayLayout(new GUIContent("下一个"));

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();

            EditorGUILayout.TextField("技能名称：" + m_SkillData?.SkillName);
            EditorGUILayout.TextField("技能图标：");
            EditorGUILayout.ObjectField(m_SkillData?.SkillSprite, typeof(Sprite), false,
                GUILayout.Width(65f),
                GUILayout.Height(65f));
            GUILayout.EndVertical();
        }
    }
}
#endif