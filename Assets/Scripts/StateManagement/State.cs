using UnityEngine;

public abstract class State 
{
    public bool isComplete {  get; private set; }
    protected float startTime;
    public float time => Time.time - startTime;


    public virtual void Enter() { }
    public virtual void Enter(GameObject inventoryUI) { }
    public virtual void Exit() { }
}
