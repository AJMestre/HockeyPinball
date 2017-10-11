using UnityEngine;
using System.Collections;

public abstract class IGameState 
{
	public abstract void GS_Enter();
	public abstract void GS_Exit();
	public abstract void GS_Update();
}
