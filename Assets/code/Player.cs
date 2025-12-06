using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Flags]
public enum ePlayerEffects
{
    None = 0,
    SpeedUp = 1 << 0,
    Enlarge = 1 << 1,
    Invisibility = 1 << 2,
    Shield = 1 << 3,
    DoubleScore = 1 << 4,
    // Add additional effects here .5 for 2p
}
public static class PlayerEffectsUtility
{
    public static ePlayerEffects ConvertStringToPlayerEffects( string binaryString )
    {
        int value = Convert.ToInt32( binaryString , 2 );
        return ( ePlayerEffects )value;
    }
}
public class Player : MonoBehaviour
{
    // Existing player properties
    private EffectManager effectManager;
    public float Speed = 9;
    public Text PName; // Assuming you're using TextMeshPro for UI
    public Text PScore;
    public string Name;
    public int MyPoints = 0;
    private Rigidbody2D rig;
    private ePlayerEffects activeEffects = ePlayerEffects.None;
    private Dictionary<ePlayerEffects , Coroutine> effectCoroutines = new Dictionary<ePlayerEffects , Coroutine>();

    // Delegate for movement methods
    private delegate void MovementMethod( Vector2 pos );
    private MovementMethod currentMovementMethod;
    private Vector3 originalSize;
    // Other properties and methods...
    #region gameStatus
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        originalSize = transform.localScale;  
        currentMovementMethod = MovePosition; // Set initial movement method
    }

    private void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
        effectManager.RegisterPlayer( this ); 
    }
    private void Update()
    {
        // Check and apply effects in the Update loop
     
        // Movement and other logic...
    }

    #endregion


    #region EffectsManager
    public void ToggleEffect( ePlayerEffects effect , bool state )
    {
        if ( state )
        {
            //   wthis.addeffect<tdsfds>()
            activeEffects |= effect; // Turn on the effect
            EffectOn( effect );
        } else
        {
            activeEffects &= ~effect; // Turn off the effect
            EffectOff( effect );
        }
    }

    private void EffectOn( ePlayerEffects effect )
    {
        switch ( effect )
        {
            case ePlayerEffects.Enlarge:
            EnlargeOn();
            break;
            case ePlayerEffects.SpeedUp:
            SpeedUpOn();
            break;
            // Add cases for other effects here
        }
    }

    private void EffectOff( ePlayerEffects effect )
    {
        switch ( effect )
        {
            case ePlayerEffects.Enlarge:
            EnlargeOff();
            break;
            case ePlayerEffects.SpeedUp:
            SpeedUpOff();
            break;
            // Add cases for other effects here
        }
    }

    public void ActivateEffect( ePlayerEffects effect , float duration )
    {
        if ( effectCoroutines.TryGetValue( effect , out Coroutine runningCoroutine ) )
        {
            StopCoroutine( runningCoroutine );
            effectCoroutines.Remove( effect );
        }

        Coroutine newCoroutine = StartCoroutine( EffectDuration( effect , duration ) );
        effectCoroutines[effect] = newCoroutine;
    }

    private IEnumerator EffectDuration( ePlayerEffects effect , float duration )
    {
        ToggleEffect( effect , true ); // Turn on the effect

        yield return new WaitForSeconds( duration ); // Wait for the effect's duration

        ToggleEffect( effect , false ); // Turn off the effect

        effectCoroutines.Remove( effect );
    }

    #endregion


    #region Effects
    #region Enlarge:
   
    private void EnlargeOn()
    {
        
        transform.localScale = originalSize * 1.5f; // Enlarge by 1.5 times
    }

    private void EnlargeOff()
    {
        transform.localScale = originalSize; // Reset to original size
    }

    // Call this from somewhere to start the Enlarge effect
    public void StartEnlargeEffect( float duration )
    {
        ActivateEffect( ePlayerEffects.Enlarge , duration );
    }


    #endregion

    #region Movement:

  
    private void SpeedUpOn()
    {
        print( "Sleparry started" );
        // Set sleparry state
        rig.bodyType = RigidbodyType2D.Dynamic;
        Speed = 9f;
        rig.angularDrag = 1;
        rig.drag = 0.14f;
        currentMovementMethod = MovePositionSleparry;
    }

    private void SpeedUpOff() 
    {
        // Reset the movement method and other properties to the default
        rig.bodyType = RigidbodyType2D.Kinematic;
        Speed = 9;
        currentMovementMethod = MovePosition;
        print( "Sleparry finished" );
    }

    // Call this from somewhere to start the Enlarge effect
    public void SpeedUpEffect( float duration )
    {
        ActivateEffect( ePlayerEffects.SpeedUp , duration );
    }


    // Default movement method
    private void MovePosition( Vector2 pos ) => rig.MovePosition( rig.position + pos * this.Speed * Time.deltaTime );

    private void MovePositionSleparry( Vector2 pos ) => rig.AddForce( pos * this.Speed );


    #endregion


    #endregion


    #region Methods

    public void Score()
    {
        MyPoints += 1;

    }

    void FixMovment()
    {
        float playerHalfHeight = transform.lossyScale.y * 0.5f;
        if ( playerHalfHeight + rig.position.y > 5.42f )
        {
            rig.position = ( new( rig.position.x , 5.39f - playerHalfHeight ) );
        } else if ( -playerHalfHeight + rig.position.y < -7.76f )
        {
            rig.position = ( new( rig.position.x , -7.69f + playerHalfHeight ) );
        }
    }

    public void Move( Vector2 pos )
    {
        currentMovementMethod( pos ); // Use the current movement method
        FixMovment();
    }

    #endregion


}
/*
//extra code for adjusting duration:

  private Dictionary<ePlayerEffects, float> effectDurations = new Dictionary<ePlayerEffects, float>();

    public void ActivateEffect(ePlayerEffects effect, float additionalDuration)
    {
        // If this effect is already active, just add time to it
        if (effectCoroutines.TryGetValue(effect, out Coroutine runningCoroutine))
        {
            if (effectDurations.TryGetValue(effect, out float currentDuration))
            {
                effectDurations[effect] = currentDuration + additionalDuration;
            }
            else
            {
                effectDurations[effect] = additionalDuration;
            }
        }
        else
        {
            // Turn on the effect immediately
            ToggleEffect(effect, true);

            // Start a new coroutine for the effect duration
            effectDurations[effect] = additionalDuration;
            Coroutine newCoroutine = StartCoroutine(EffectDuration(effect, additionalDuration));
            effectCoroutines[effect] = newCoroutine;
        }
    }

    private IEnumerator EffectDuration(ePlayerEffects effect, float duration)
    {
        while (effectDurations[effect] > 0)
        {
            yield return new WaitForSeconds(1.0f);
            effectDurations[effect] -= 1.0f;
        }

        ToggleEffect(effect, false);
        effectCoroutines.Remove(effect);
        effectDurations.Remove(effect);
    }

*/
//public class Player2 : MonoBehaviour
//{
//    public Text PName;
//    public Text PScore;
//    public string Name;
//    public float Speed = 9;
//    private int myPoints = 0;
//    private Rigidbody2D rig;
//    private MovementMethod currentMovementMethod;

//    // Delegate for movement methods
//    private delegate void MovementMethod( Vector2 pos );

//    private void Awake()
//    {
//        rig = GetComponent<Rigidbody2D>();
//        currentMovementMethod = _MovePosition; // Set initial movement method
//    }

//    public int MyPoints
//    {
//        get => myPoints;
//        set => myPoints = value;
//    }

//    public void SetSpeed( float speed , float time )
//    {
//        StartCoroutine( SetSpeedCoroutine( speed , time ) );
//    }

//    private IEnumerator SetSpeedCoroutine( float speed , float time )
//    {
//        Speed = speed;
//        yield return new WaitForSeconds( time );
//        Speed = 9f;
//    }

//    public void Move( Vector2 pos )
//    {
//        currentMovementMethod( pos ); // Use the current movement method
//    }
//    private Coroutine speedCoroutine;

//    public void SetSleparry( float speed , int time )
//    {
//        // If there is an existing coroutine, stop it
//        if ( speedCoroutine != null )
//        {
//            StopCoroutine( speedCoroutine );
//        }

//        // Start the new coroutine and store the reference

//        speedCoroutine = StartCoroutine( ResetMovementMethod(speed, time ) );
//    }


//    private IEnumerator ResetMovementMethod(float speed, float time )
//    {
//        print( "started" );
//        // Set sleparry state
//        rig.bodyType = RigidbodyType2D.Dynamic;
//        Speed = speed;
//        rig.angularDrag = 1;
//        rig.drag = 0.14f;
//        currentMovementMethod = _MovePositionSleparry;

//        // Wait for the specified time
//        yield return new WaitForSeconds( time );

//        // Reset the movement method and other properties to the default
//        rig.bodyType = RigidbodyType2D.Kinematic;
//        Speed = 9;
//        currentMovementMethod = _MovePosition;

//        // Coroutine is completed, clear the reference
//        print( "Finshed" );
//        speedCoroutine = null;
//    }


//    // Default movement method
//    private void _MovePosition( Vector2 pos )
//    {
//        // Your existing movement code for normal behavior goes here
//        rig.MovePosition( rig.position + pos * this.Speed * Time.deltaTime );
//        float playerHalfHeight = transform.lossyScale.y * 0.6f;
//        if ( playerHalfHeight + rig.position.y > 5.4f )
//        {
//            rig.MovePosition( new( rig.position.x , 5.39f - playerHalfHeight ) );
//        } else if ( -playerHalfHeight + rig.position.y < -7.7f )
//        {
//            rig.MovePosition( new( rig.position.x , -7.69f + playerHalfHeight ) );
//        }
//        // Add boundary checking and other logic as necessary
//    }

//    // Sleparry movement method
//    private void _MovePositionSleparry( Vector2 pos )
//    {
//        // Your existing movement code for sleparry behavior goes here
//        rig.AddForce(  pos * this.Speed );
//        float playerHalfHeight = transform.lossyScale.y * 0.6f;
//        if ( playerHalfHeight + rig.position.y > 5.4f )
//        {
//            rig.position=( new( rig.position.x , 5.39f - playerHalfHeight ) );
//        } else if ( -playerHalfHeight + rig.position.y < -7.7f )
//        {
//            rig.position=( new( rig.position.x , -7.69f + playerHalfHeight ) );
//        }
//        // Add boundary checking and other logic as necessary
//    }
//}