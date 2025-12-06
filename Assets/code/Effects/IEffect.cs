using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public float duration;
    private float elapsedTime;
    protected Player player;

    public void Initialize( Player player , float duration )
    {
        this.player = player;
        this.duration = duration;
        OnEffectStart();
    }

    public abstract void OnEffectStart();
    public abstract void OnEffectEnd();

    protected virtual void Update()
    {
        elapsedTime += Time.deltaTime;
        if ( elapsedTime >= duration )
        {
            OnEffectEnd();
        }
    }

    public bool IsEffectComplete()
    {
        return elapsedTime >= duration;
    }
}
