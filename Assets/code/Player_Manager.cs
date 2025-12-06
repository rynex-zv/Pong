using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEffectsUtility;
public enum EConectionType
{
    Solo,
    Local,
    Host,
    Join,
}
public static class StaticVars
{

    public static EConectionType eConectionType = EConectionType.Local;

}
public class Player_Manager : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public float Speed;
    EffectManager effectManager;

    // Start is called before the first frame update
    void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch ( StaticVars.eConectionType )
        {
            case EConectionType.Local:
            // Player 1 movement
            if ( Input.GetKey( KeyCode.UpArrow ) )
            {
                player1.Move(  Vector2.up );
            }
            if ( Input.GetKey( KeyCode.DownArrow ) )
            {
                player1.Move( Vector2.down );
            }
          
            // Player 2 movement
            if ( Input.GetKey( KeyCode.W ) )
            {
                player2.Move(  Vector2.up  );
            }
            if ( Input.GetKey( KeyCode.S ) )
            {
                player2.Move( Vector2.down   );
            }
            // Add left and right movement for player2 if needed

            break;

            default:
            // If you have other connection types to handle, add them here
            break;
        }
    }
    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.F ) )
        {
            effectManager.AddEffectToPlayer<EnlargeEffect>( player2 , 5f );
        }
        if ( Input.GetKeyDown( KeyCode.R ) )
        {
            player2.ActivateEffect( ePlayerEffects.Enlarge , 5f );
        }
    }



}
