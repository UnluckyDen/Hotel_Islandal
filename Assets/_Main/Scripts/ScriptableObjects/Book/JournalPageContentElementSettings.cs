using System;
using System.Globalization;
using _Main.Scripts.UI.Book.BookPages.PageElements;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.ScriptableObjects.Book
{
    [Serializable]
    public struct JournalPageContentElementSettings
    {
        public PageElementType PageElementType;
        public string TextContent;
        public Sprite ImageContent;
        public AudioClip AudioContent;
        
        #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(JournalPageContentElementSettings))]
    public class JournalPageContentElementSettingsCustomInspector : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty type = property.FindPropertyRelative("PageElementType");
            switch ((PageElementType) type.intValue)
            {
                case PageElementType.Header:
                    return 40;
                case PageElementType.Body:
                    return 40;
                case PageElementType.Recipe:
                    return 60;
                case PageElementType.Header2:
                    return 40;
                case PageElementType.RecipeStep:
                    return 60;
                case PageElementType.Spacer:
                    return 20;
                case PageElementType.AudioSample:
                    return 80;
            }

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);

            SerializedProperty type = property.FindPropertyRelative("PageElementType");
            SerializedProperty textContent = property.FindPropertyRelative("TextContent");
            SerializedProperty imageContent = property.FindPropertyRelative("ImageContent");
            SerializedProperty audioContent = property.FindPropertyRelative("AudioContent");

            type.intValue =
                    Convert.ToInt32(EditorGUI.EnumPopup(
                            new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Page Element Type",
                            (PageElementType) type.intValue));
            switch ((PageElementType) type.intValue)
            {
                case PageElementType.Header:
                    contentPosition.y += 20;
                    textContent.stringValue =
                        EditorGUI.TextField(new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Header text", textContent.stringValue);
                    break;
                case PageElementType.Body:
                    contentPosition.y += 20;
                    textContent.stringValue =
                        EditorGUI.TextField(new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Body text", textContent.stringValue);
                    break;
                case PageElementType.Recipe:
                    contentPosition.y += 20;
                    textContent.stringValue =
                        EditorGUI.TextField(new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Recipe text", textContent.stringValue);
                    contentPosition.y += 20;
                    imageContent.objectReferenceValue =
                        EditorGUI.ObjectField(
                            new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Recipe image",
                            imageContent.objectReferenceValue, typeof(Sprite));
                    break;
                case PageElementType.Header2:
                    contentPosition.y += 20;
                    textContent.stringValue =
                        EditorGUI.TextField(new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Header text", textContent.stringValue);
                    break;
                case PageElementType.RecipeStep:
                    contentPosition.y += 20;
                    textContent.stringValue =
                        EditorGUI.TextField(new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Recipe step text", textContent.stringValue);
                    contentPosition.y += 20;
                    imageContent.objectReferenceValue =
                        EditorGUI.ObjectField(
                            new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Recipe image",
                            imageContent.objectReferenceValue, typeof(Sprite));
                    break;
                case PageElementType.Spacer:
                    break;
                case PageElementType.AudioSample:
                    contentPosition.y += 20;
                    textContent.stringValue =
                        EditorGUI.TextField(new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Audio name", textContent.stringValue);
                    contentPosition.y += 20;
                    imageContent.objectReferenceValue =
                        EditorGUI.ObjectField(
                            new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Audio image image",
                            imageContent.objectReferenceValue, typeof(Sprite));
                    contentPosition.y += 20;
                    audioContent.objectReferenceValue =
                        EditorGUI.ObjectField(
                            new Rect(contentPosition.position, new Vector2(contentPosition.width, 18)), "Audio clip",
                            audioContent.objectReferenceValue, typeof(AudioClip));
                    break;
            }

            EditorGUI.EndProperty();
        }
    }
#endif
    }
}