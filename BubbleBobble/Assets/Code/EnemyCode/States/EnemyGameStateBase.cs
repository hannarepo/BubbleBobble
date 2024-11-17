using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleBobble.State
{
	public abstract class EnemyGameStateBase
	{
		public abstract EnemyStateType Type { get; }
	}
}
