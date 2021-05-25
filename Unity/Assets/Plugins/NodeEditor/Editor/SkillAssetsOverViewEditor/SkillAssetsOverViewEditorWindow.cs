//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年4月11日 9:18:43
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using AllTrickOverView.Core;
using MonKey;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace AllTrickOverView
{
    public class SkillAssetsOverViewEditorWindow: OdinMenuEditorWindow
    {
        private SkillAssetsOverViewItem exampleItem;

        private Vector2 scrollPosition;

        private GUIStyle buttonStyle;

        [Command("ETEditor_SkillAssetsOverView", "技能资产概览")]
        public static void PopUp()
        {
            bool flag = Resources.FindObjectsOfTypeAll<AttributesExampleWindow>().Length == 0;
            SkillAssetsOverViewEditorWindow window = EditorWindow.GetWindow<SkillAssetsOverViewEditorWindow>();
            if (flag)
            {
                window.MenuWidth = 250f;
                window.position = GUIHelper.GetEditorWindowRect().AlignCenterXY(850f, 700f);
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree odinMenuTree = new OdinMenuTree();
            odinMenuTree.Selection.SupportsMultiSelect = true;
            odinMenuTree.Selection.SelectionChanged += this.SelectionChanged;
            odinMenuTree.Config.DrawSearchToolbar = true;
            odinMenuTree.Config.DefaultMenuStyle.Height = 22;
            SkillAssetsOverViewUtilities.BuildMenuTree(odinMenuTree);

            buttonStyle = new GUIStyle("button");
            buttonStyle.fixedHeight = 35;

            return odinMenuTree;
        }

        private void SelectionChanged(SelectionChangedType selectionChangedType)
        {
            string selectedSkillAssetPath = (string) this.MenuTree.Selection.SelectedValue;
            if (string.IsNullOrEmpty(selectedSkillAssetPath)) return;

            this.exampleItem = SkillAssetsOverViewUtilities.GetSkillAssetsOverViewItemByPath(selectedSkillAssetPath);
            this.exampleItem.NpBehaveCanvas = AssetDatabase.LoadAssetAtPath<NPBehaveCanvas>(selectedSkillAssetPath);
        }

        protected override void DrawEditors()
        {
            GUILayout.BeginArea(new Rect(4f, 0f, Mathf.Max(300f, base.position.width - this.MenuWidth - 4f),
                base.position.height));
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, GUILayoutOptions.ExpandWidth(false));
            GUILayout.Space(4f);
            if (this.exampleItem != null)
            {
                this.exampleItem.Draw();
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        protected override void DrawMenu()
        {
            base.DrawMenu();
            EditorGUILayout.BeginVertical();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("导出选中技能数据", buttonStyle))
            {
                using IEnumerator<object> ieEnumerator = this.MenuTree.Selection.SelectedValues.GetEnumerator();
                while (ieEnumerator.MoveNext())
                {
                    string skillAssetPath = ieEnumerator.Current as string;

                    SkillAssetsOverViewItem skillAssetsOverViewItem =
                            SkillAssetsOverViewUtilities.GetSkillAssetsOverViewItemByPath(skillAssetPath);
                    skillAssetsOverViewItem.NpBehaveCanvas = AssetDatabase.LoadAssetAtPath<NPBehaveCanvas>(skillAssetPath);
                    skillAssetsOverViewItem.NpBehaveCanvas.AddAllNodeData();
                    skillAssetsOverViewItem.NpBehaveCanvas.Save();
                }
            }

            if (GUILayout.Button("导出所有技能数据", buttonStyle))
            {
                Dictionary<string, SkillAssetsOverViewItem> all = SkillAssetsOverViewUtilities.GetAllSkillAssetsOverViewItems();
                foreach (var skillItem in all)
                {
                    skillItem.Value.NpBehaveCanvas = AssetDatabase.LoadAssetAtPath<NPBehaveCanvas>(skillItem.Key);
                    skillItem.Value.NpBehaveCanvas.AddAllNodeData();
                    skillItem.Value.NpBehaveCanvas.Save();
                }
            }

            EditorGUILayout.EndVertical();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            this.exampleItem = null;
        }
    }
}