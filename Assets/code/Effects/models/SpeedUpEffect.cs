using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class SpeedUpEffect : Effect
{
    public float speedMultiplier = 2.0f; // Example speed multiplier value

    // Called when the effect starts
    public override void OnEffectStart()
    {
        if ( player != null )
        {
            // Increase the player's speed by the speed multiplier
            player.Speed *= speedMultiplier;
        }
    }

    // Called when the effect ends
    public override void OnEffectEnd()
    {
        if ( player != null )
        {
            // Reset the player's speed back to normal
            player.Speed /= speedMultiplier;
        }

        // Remove this effect component from the player
        Destroy( this );
    }
}
