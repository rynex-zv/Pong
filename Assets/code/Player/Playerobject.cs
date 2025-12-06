using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObject : MonoBehaviour
{
    public const float sizeDefault = 5f;
    public const float sizeOffsetMax = 5f;

    public const float slideVelocity = 0.3f;

    public const float frictionDefault = 4f;
    public const float frictionOffsetMax = 6f;
    public Func<float , float> frictionEffective = ( frictionOffset ) => {
        return ( frictionDefault + frictionOffset ) / 100;
    };
    public Func<float , float> minPosY = ( float size ) => {
        return 9 - ( size / 2 );
    };

    GameObject obj;
    Renderer rnd;
    Material mat;
    Rigidbody rgd;


    public float _posY;
    public float posY
    {
        get
        {
            var minPos = minPosY( sizeDefault + this.sizeOffset );
            _posY = _posY > minPos ? minPos : ( _posY < ( minPos * -1 ) ? ( minPos * -1 ) : _posY );
            return _posY;
        }
        set
        {
            var minPos = minPosY( sizeDefault + this.sizeOffset );
            _posY = value > minPos ? minPos : ( _posY < ( minPos * -1 ) ? ( minPos * -1 ) : value );
        }
    }
    public float _sizeOffset;
    public float sizeOffset
    {
        get => _sizeOffset > sizeOffsetMax ? sizeOffsetMax : ( _sizeOffset < sizeOffsetMax * -0.5f ? sizeOffsetMax * -0.5f : _sizeOffset );
        set => _sizeOffset = value > sizeOffsetMax ? sizeOffsetMax : ( value < sizeOffsetMax * -0.5f ? sizeOffsetMax * -0.5f : value );
    }

    public float _frictionOffset;
    public float frictionOffset
    {
        get => _frictionOffset > frictionOffsetMax ? frictionOffsetMax : ( _frictionOffset < frictionDefault - frictionOffsetMax ? frictionDefault - frictionOffsetMax : _frictionOffset );
        set => _frictionOffset = value > frictionOffsetMax ? frictionOffsetMax : ( value < frictionDefault - frictionOffsetMax ? frictionDefault - frictionOffsetMax : value );
    }

    private float _slide;


    private bool? IsGoingUp;

    private PlayerInputMap inputMap;

    private List<PlayerEffect> _Effects;
    public IReadOnlyList<PlayerEffect> Effects => this._Effects.AsReadOnly();


    private void Awake()
    {
        obj = GameObject.CreatePrimitive( PrimitiveType.Cube );

        rnd = obj.GetComponent<Renderer>();
        if ( rnd != null )
        {
            mat = new Material( Shader.Find( "Standard" ) );
            rnd.material = mat;
        }

        rgd = obj.AddComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        this.onInput();
        this.onMove();
    }
    public void Update()
    {
        this.onPaint();
    }

    private void onInput()
    {
        var isKeyUp = Input.GetKey( inputMap.KeyUp );
        var isKeyDown = Input.GetKey( inputMap.KeyDown );
        if ( isKeyUp == isKeyDown )
        {
            this.IsGoingUp = null;

        } else {
            this._slide = this.frictionEffective(this.frictionOffset);
            if ( isKeyUp )
            {
                this.IsGoingUp = true;
            } else if ( isKeyDown )
            {
                this.IsGoingUp = false;
            }
        } 

        var isKeyActive = Input.GetKey( inputMap.KeyActivate );
        if ( isKeyActive )
        {
            //Do da die
        }
    }
    private void onMove()
    {
        var iDir = IsGoingUp == null ? 0 : IsGoingUp.Value ? 1 : -1;
        var y = this.obj.transform.position.y;

        var posYNew = y + ( this._slide * iDir );

        for ( int x = 0 ; x < this.Effects.Count ; x++ )
        {
            posYNew = this.Effects.ElementAt( x ).onMove( iDir , posYNew )?? posYNew;
        }

        this.posY = posYNew;

        this._slide -= slideVelocity;
    }

    private void onPaint()
    {

        this.obj.transform.position = new Vector2(this.obj.transform.position.x, this.posY);
    }

    public T EffectAdd<T>() where T: PlayerEffect, new()
    {
        var effect = (T)Activator.CreateInstance( typeof(T), new object[] { this } );
        this._Effects.Add( effect );
        return effect;
    }

}