using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInformation : MonoBehaviour
{
	[SerializeField]
    private uint money;
	// crafting material list

	public void ReceiveRewards(Quest q)
	{
		Debug.Log("quest reward received: " + q.questRewards.money);
		money += q.questRewards.money;
	}
}
