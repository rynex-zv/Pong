using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Keeps track of all effects for each player
    private Dictionary<Player , List<Effect>> activeEffects = new Dictionary<Player , List<Effect>>();

    // Call this method to register a player with the EffectManager
    public void RegisterPlayer( Player player )
    {
        // Initialize the player's effects list if not already present
        if ( !activeEffects.ContainsKey( player ) )
        {
            activeEffects[player] = new List<Effect>();
        }
    }

    // Function to add effects to players
    public void AddEffectToPlayer<T>( Player player , float duration ) where T : Effect
    {
        // Ensure the player is registered before adding an effect
        RegisterPlayer( player );

        // Check if the player already has the maximum number of effects
        if ( activeEffects[player].Count >= 3 )
        {
            Debug.Log( "Player already has 3 active effects." );
            return;
        }

        // Instantiate the effect and add it to the player as a component
        Effect newEffect = player.gameObject.AddComponent<T>( ) as Effect;
        if ( newEffect != null )
        {
            newEffect.Initialize( player , duration );
            activeEffects[player].Add( newEffect );
        }
    }

    // Function to remove effects from players
    public void RemoveEffectFromPlayer( Player player , Effect effect )
    {
        // If the player has this effect, remove it
        if ( activeEffects.TryGetValue( player , out List<Effect> effects ) && effects.Contains( effect ) )
        {
            effects.Remove( effect );
            Destroy( effect );
        }
    }

    // Function to remove all effects from a player
    public void ClearEffectsFromPlayer( Player player )
    {
        if ( activeEffects.TryGetValue( player , out List<Effect> effects ) )
        {
            foreach ( Effect effect in new List<Effect>( effects ) )
            {
                RemoveEffectFromPlayer( player , effect );
            }
        }
    }

    // Function called to check and update effects for all players
    private void Update()
    {
        foreach ( KeyValuePair<Player , List<Effect>> pair in new Dictionary<Player , List<Effect>>( activeEffects ) )
        {
            Player player = pair.Key;
            List<Effect> effectsToRemove = new List<Effect>();

            // Iterate over all effects and check if they have finished
            foreach ( Effect effect in pair.Value )
            {
                if ( effect == null )
                {
                    effectsToRemove.Add( effect );
                } else if ( effect.IsEffectComplete() )
                {
                    effect.OnEffectEnd();
                    effectsToRemove.Add( effect );
                }
            }

            // Remove completed effects
            foreach ( Effect effect in effectsToRemove )
            {
                pair.Value.Remove( effect );
                if ( effect != null )
                {
                    Destroy( effect );
                }
            }
        }
    }
}
