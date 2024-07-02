using IdleHeaven;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IdleHeaven.Stat))]
public class StatPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Find the StatType and Value properties
        SerializedProperty statTypeProperty = property.FindPropertyRelative("_statType");
        SerializedProperty valueProperty = property.FindPropertyRelative("_value");

        // Calculate the positions
        Rect statTypeRect = new Rect(position.x, position.y, position.width / 2f, position.height);
        Rect valueRect = new Rect(position.x + position.width / 2f, position.y * 2f, position.width / 2, position.height);

        // Draw the fields
        EditorGUI.PropertyField(statTypeRect, statTypeProperty, GUIContent.none);
        EditorGUI.PropertyField(valueRect, valueProperty, new GUIContent("drag"));

        EditorGUI.EndProperty();

        // Apply changes to the serialized properties
        if (GUI.changed)
        {
            property.serializedObject.ApplyModifiedProperties();
            NotifyPropertyChanged(property);
        }
    }

    private void NotifyPropertyChanged(SerializedProperty property)
    {
        // Access the parent object that contains the list of stats
        object parentObject = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

        if (parentObject == null) return;

        // Extract the stat list from the parent object
        var statsFields = (parentObject as Stats).stats;
        if (statsFields == null) return;

        // Find the correct Stat object within the list
        int propertyIndex = GetPropertyIndex(property);
        if (propertyIndex >= 0 && propertyIndex < statsFields.Count)
        {
            var stat = statsFields[propertyIndex];
            // Trigger the OnPropertyChanged method
            stat.OnPropertyChanged(nameof(stat.Value));
        }
    }

    private object GetParentObject(string path, object obj)
    {
        string[] fields = path.Split('.');
        foreach (var field in fields)
        {
            if (field.Contains("["))
            {
                string fieldName = field.Substring(0, field.IndexOf("["));
                string indexStr = field.Substring(field.IndexOf("[")).Replace("[", "").Replace("]", "");
                int index = int.Parse(indexStr);

                var fieldInfo = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                var list = fieldInfo.GetValue(obj) as System.Collections.IList;
                obj = list[index];
            }
            else
            {
                var fieldInfo = obj.GetType().GetField(field, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                obj = fieldInfo.GetValue(obj);
                if (obj != null) return obj;
            }

            if (obj == null) return null;
        }

        return obj;
    }

    private int GetPropertyIndex(SerializedProperty property)
    {
        string propertyPath = property.propertyPath;
        string indexString = System.Text.RegularExpressions.Regex.Match(propertyPath, @"\d+").Value;
        return int.TryParse(indexString, out int index) ? index : -1;
    }
}
