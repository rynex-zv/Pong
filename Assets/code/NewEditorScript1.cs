using UnityEditor;
using UnityEngine;

    [CreateAssetMenu(fileName ="0000",menuName ="fuck")]
    public class NewEditorScript1 : ScriptableObject
    {
        [MenuItem( "Fuck You/MyTool/Holly,Molly" )]
       public static void DoIt()
        {
           bool x= EditorUtility.DisplayDialog( "MyTool" , "You are Fucked!" , "Why the Fuck?" , "Fucking cancel" );
        if ( x ) { 
        
        Debug.LogError( "Holly,Molly: Becouse I can do it throw Extentions :)" );
            EditorUtility.DisplayDialog( "MyTool" , "You are Fucked twise!" , "Thanks" , "Thanks So much" );
        Debug.Log( "Holly,Molly: You Are Welcome!" );
        } else
        {

        Debug.Log( "Holly,Molly: Eather way you are :)" );
        }
    }
    [MinMax( 5 , 100 )]
    public float x = 1;
        public string player_1_Name;
        public string player_2_Name;
    }
