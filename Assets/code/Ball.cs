using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // "[SerializeField] private float speed;" would be better. 
    // See https://docs.unity3d.com/ScriptReference/SerializeField.html
    public float speed;
    public GameObject me;
    private Vector2 direction;
    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = me.GetOrAddComponent<Rigidbody2D>();

        direction = CreateVector2ByDegree( 45 );
    }

    void FixedUpdate()
    {
        if ( speed > 20 )
        {
            speed = 20;
        }
        Vector2 movement = direction * speed; // Removed Time.deltaTime

        rigidBody.velocity = movement;
    }

    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) ) // Use GetKeyDown to detect the initial press
        {
            me.transform.position = Vector2.zero;
            rigidBody.velocity = Vector2.zero; // Also reset the velocity to stop the ball moving
        }
    }
    // This method should be better in a class of its own,
    // but as the code is simple, it's okay to leave it here ??
    private Vector2 CreateVector2ByDegree( float degrees )
    {
        var angle = degrees * Mathf.Deg2Rad;
        var x = Mathf.Cos( angle );
        var y = Mathf.Sin( angle );

        return new Vector2( x , y );
    }
    // Reference to the Player_Manager script
    public Game_Manager Game_Manager;

    public bool On_Off = false;
    void OnCollisionEnter2D( Collision2D other )
    {
        // Calculate the reflection direction based on the incoming direction and the normal at the point of collision.
        Vector2 normal = other.GetContact( 0 ).normal;
        direction = Vector2.Reflect( direction , normal ).normalized;

        // Check if the paddle was hit
        if ( other.gameObject.CompareTag( "Player" ) )
        {
            // Calculate the offset from the center of the paddle
            float paddleCenterY = other.transform.position.y;
            float hitPointY = other.GetContact( 0 ).point.y;
            float offset = hitPointY - paddleCenterY;

            // Adjust the direction based on the offset from the center
            float maxAngleOffset = 45.0f; // Maximum angle change when hit at the very top/bottom of the paddle
            float paddleHeight = other.collider.bounds.size.y;
            float normalizedOffset = offset / ( paddleHeight / 2 ); // Normalized offset (-1 to 1)
            float angleOffset = normalizedOffset * maxAngleOffset;
            direction = Quaternion.Euler( 0 , 0 , angleOffset ) * direction;
        }

        // Toggle the speed increase on and off on each collision.
        if ( On_Off )
        {
            speed *= 1.1f; // Increase speed by 10%
        }
        On_Off = !On_Off;

        // Limit the speed to a maximum value.
        speed = Mathf.Min( speed , 20 );

        // Handle scoring
        if ( other.gameObject.name == "P2_Goal" )
        {
            Game_Manager.OnPlayer2Scores(); // Player 2 scores
        } else if ( other.gameObject.name == "P1_Goal" )
        {
            Game_Manager.OnPlayer1Scores(); // Player 1 scores
        }

        // Apply the new speed to the rigidbody velocity while maintaining the direction.
        rigidBody.velocity = direction * speed;
    }

    //void OnCollisionEnter2D( Collision2D other )
    //{
    //    // Calculate the reflection direction based on the incoming direction and the normal at the point of collision.
    //    Vector2 normal = other.GetContact( 0 ).normal;
    //    direction = Vector2.Reflect( direction , normal ).normalized;

    //    // Toggle the speed increase on and off on each collision.
    //    if ( On_Off )
    //    {
    //        speed *= 1.1f; // Increase speed by 10%
    //    }
    //    On_Off = !On_Off;

    //    // Limit the speed to a maximum value.
    //    speed = Mathf.Min( speed , 20 );

    //    // Handle scoring
    //    if ( other.gameObject.name == "P2_Goal" )
    //    {
    //        Game_Manager.OnPlayer2Scores(); // Player 2 scores
    //    } else if ( other.gameObject.name == "P1_Goal" )
    //    {
    //        Game_Manager.OnPlayer1Scores(); // Player 1 scores
    //    }

    //    // Apply the new speed to the rigidbody velocity while maintaining the direction.
    //    rigidBody.velocity = direction * speed;
    //}

    //void OnCollisionEnter2D( Collision2D other )
    //{
    //    Vector2 normal = other.GetContact( 0 ).normal;
    //    Vector2 reflection = Vector2.Reflect( direction , normal );

    //    direction = reflection;
    //    if ( On_Off ){
    //        speed = speed * 1.1f;
    //    }
    //    On_Off = !On_Off;
    //    // Check for collision with P1_Goal
    //    if ( other.gameObject.name == "P2_Goal" ) // Replace with your goal's identifier
    //    {
    //        Game_Manager.OnPlayer2Scores(); // Player 2 scores if the ball enters Player 1's goal
    //    }
    //    // Check for collision with P2_Goal
    //    else if ( other.gameObject.name == "P1_Goal" ) // Replace with your goal's identifier
    //    {
    //        Game_Manager.OnPlayer1Scores(); // Player 1 scores if the ball enters Player 2's goal
    //    }
    //}
}