using UnityEngine;
using Mirror;
using Cinemachine;

public class NetworkPlayer : NetworkBehaviour
{
	public override void OnStartAuthority()
	{
		base.OnStartAuthority();

		CinemachineVirtualCamera vc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
		vc.Follow = gameObject.transform;
	}
}
