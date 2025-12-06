#if UNITY_EDITOR
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer( typeof( MinMaxAttribute ) )]
public class MinMaxDrawer : PropertyDrawer
{
    public override void OnGUI( Rect position , SerializedProperty property , GUIContent label )
    {
        MinMaxAttribute minMax = attribute as MinMaxAttribute;

        if ( property.propertyType == SerializedPropertyType.Integer )
        {
            EditorGUI.BeginChangeCheck();
            int newValue = EditorGUI.IntField( position , label , property.intValue );
            if ( EditorGUI.EndChangeCheck() )
            {
                property.intValue = Mathf.Clamp( newValue , minMax.Min , minMax.Max );
            }
        } else if ( property.propertyType == SerializedPropertyType.Float )
        {
            EditorGUI.BeginChangeCheck();
            float newValue = EditorGUI.FloatField( position , label , property.floatValue );
            if ( EditorGUI.EndChangeCheck() )
            {
                property.floatValue = Mathf.Clamp( newValue , minMax.Min , minMax.Max );
            }
        } else
        {
            EditorGUI.LabelField( position , label.text , "Use MinMax with int." );
        }
        }
    }


#endif
