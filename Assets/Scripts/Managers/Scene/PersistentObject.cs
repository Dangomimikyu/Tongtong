using UnityEngine;

public class PersistentObject : MonoBehaviour
{
	#region Monobehaviour functions
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion
}
