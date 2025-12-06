using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnlargeEffect : Effect
{
    public float PlayerSizeX = 2f;
    public override void OnEffectStart()
    {
        if ( player != null )
        {
            // Increase the player's speed by the speed multiplier
            Vector2 newScale = new(0.5f, 2.5f * PlayerSizeX );
            transform.localScale = newScale;
        }
    }
    private void Update()
    {
        base.Update();
        if ( !IsEffectComplete() )
            transform.localScale = new( 0.5f , 2.5f * PlayerSizeX );
   
    }
    public override void OnEffectEnd()
    {
        transform.localScale = new( 0.5f , 2.5f );

    }
}
