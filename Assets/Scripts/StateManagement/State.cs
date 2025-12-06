using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected float startTime;
    public float time => Time.time - startTime;



    public virtual void Enter() { }
    public virtual void Exit() { }



}
