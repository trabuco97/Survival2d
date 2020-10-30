//using UnityEngine;
//using UnityEditor;
//using Survival2D.Systems.Statistics.Status;

//[CustomPropertyDrawer(typeof(IModifierData))]
//public class IModifierData_CustomEditor : PropertyDrawer
//{


//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        // 2 field + 2 px of sum of separation
//        return EditorGUIUtility.singleLineHeight * 2 + 2;
//    }

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);

//        EditorGUI.LabelField(position, label);

//        var nameRect = new Rect(position.x, position.y + 18, position.width, 16);
//        var ageRect = new Rect(position.x, position.y + 36, position.width, 16);
//        var genderRect = new Rect(position.x, position.y + 54, position.width, 16);

//        EditorGUI.indentLevel++;

//        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"));
//        EditorGUI.PropertyField(ageRect, property.FindPropertyRelative("Age"));
//        EditorGUI.PropertyField(genderRect, property.FindPropertyRelative("Gender"));

//        EditorGUI.indentLevel--;

//        EditorGUI.EndProperty();
//    }

//}


////[CustomPropertyDrawer(typeof(UserInfo))]
////public class UserInfoDrawer : PropertyDrawer
////{

////    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
////    {
////        // The 6 comes from extra spacing between the fields (2px each)
////        return EditorGUIUtility.singleLineHeight * 4 + 6;
////    }

////    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
////    {
////        EditorGUI.BeginProperty(position, label, property);

////        EditorGUI.LabelField(position, label);

////        var nameRect = new Rect(position.x, position.y + 18, position.width, 16);
////        var ageRect = new Rect(position.x, position.y + 36, position.width, 16);
////        var genderRect = new Rect(position.x, position.y + 54, position.width, 16);

////        EditorGUI.indentLevel++;

////        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"));
////        EditorGUI.PropertyField(ageRect, property.FindPropertyRelative("Age"));
////        EditorGUI.PropertyField(genderRect, property.FindPropertyRelative("Gender"));

////        EditorGUI.indentLevel--;

////        EditorGUI.EndProperty();
////    }
////}