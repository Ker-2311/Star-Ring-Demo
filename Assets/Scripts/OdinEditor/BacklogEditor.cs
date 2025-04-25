using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace OdinEditor
{
    public class BacklogEditor : OdinEditorWindow
    {
        [MenuItem("�༭��/��������༭��")]
        private static void Open()
        {
            var window = GetWindow<BacklogEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 400);
        }

        //protected override void OnEnable()
        //{
        //    base.OnEnable();
        //    log = new List<string>();
        //    //��������
        //    using (var file = new FileStream(Application.dataPath+"Assets/Scripts/OdinEditor/Data/Backlog.txt", FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        using (var reader = new StreamReader(file, Encoding.UTF8))
        //        {
        //            var text = reader.ReadToEnd().Split(",");
        //            foreach (var str in text)
        //            {
        //                log.Add(str);
        //            }
        //        }
        //    }
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    //��������
        //    using (var file = new FileStream(Application.dataPath +"Assets/Scripts/OdinEditor/Data/Backlog.txt", FileMode.Truncate, FileAccess.Write))
        //    {
        //        using (var writer = new StreamWriter(file, Encoding.UTF8))
        //        {
        //            var builder = new StringBuilder();
        //            for (int i = 0;i<log.Count;i++)
        //            {
        //                builder.Append(log[i]);
        //                if (i< log.Count -1) builder.Append(",");
        //            }
        //            writer.Write(log);
        //        }
        //    }
        //}

        [LabelText("����")]
        [ListDrawerSettings]
        public List<string> log;
    }
}

