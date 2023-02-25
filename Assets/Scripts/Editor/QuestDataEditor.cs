using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace FarmGame
{
    [CustomEditor(typeof(QuestData))]
    public class QuestDataEditor : Editor
    {
        private static Type[] goalTypes;

        private int goalTypeIndex;
        private QuestData questData;

        private void OnEnable()
        {
            questData = target as QuestData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            goalTypeIndex = EditorGUILayout.Popup(new GUIContent("Goal to create"),
            goalTypeIndex, goalTypes.Select(type => type.Name).ToArray());

            if (GUILayout.Button("Create Goal"))
            {
                QuestGoal goal = Activator.CreateInstance(goalTypes[goalTypeIndex]) as QuestGoal;
                questData.AddGoal(goal);
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            goalTypes = GetTypes<QuestGoal>();
        }

        private static Type[] GetTypes<T>()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var interfaceType = typeof(T);
            return types.Where(
                type => type.IsSubclassOf(interfaceType) && 
                type.IsClass &&
                !type.ContainsGenericParameters
            ).ToArray();
        }
    }
}
