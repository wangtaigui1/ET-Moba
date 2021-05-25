//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年4月11日 11:17:35
//------------------------------------------------------------

using System;
using Plugins.NodeEditor.Editor.Canvas;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace AllTrickOverView.Core
{
    public class SkillAssetsOverViewItem
    {
        /// <summary>
        /// 显示在treeView的条目名称
        /// </summary>
        public string Name = "NoName";

        /// <summary>
        /// 分组
        /// </summary>
        public string Category = "Uncategorized";

        /// <summary>
        /// The description of the example.
        /// </summary>
        public string Description;

        public NPBehaveCanvas NpBehaveCanvas;

        public PropertyTree PropertyTree;

        // Token: 0x06000A23 RID: 2595 RVA: 0x00030DD4 File Offset: 0x0002EFD4
        //[OnInspectorGUI]
        public void Draw()
        {
            if (PropertyTree != null)
            {
                PropertyTree.Draw(false);
            }
            else
            {
                PropertyTree = PropertyTree.Create(NpBehaveCanvas);
                PropertyTree.Draw(false);
            }
        }
    }
}