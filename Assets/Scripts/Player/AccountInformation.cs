using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInformation : MonoBehaviour
{
	[SerializeField]
    private uint m_money;
	[SerializeField]
	private int m_level = 1;
	[SerializeField]
	private float m_exp;
	[SerializeField]
	private float m_neededExp;

	#region Getter functions
	public int GetPlayerLevel()
    {
		return m_level;
    }
	#endregion

	#region Quest functions
	public void ReceiveRewards(Quest q)
	{
		Debug.Log("quest reward received: " + q.questRewards.money);
		m_money += q.questRewards.money;

		CheckLevelUp(q.questRewards.exp);

    }

	private void CheckLevelUp(int newExp)
    {
		m_exp += newExp;
		if(m_exp >= m_neededExp)
        {
			m_exp -= m_neededExp;
			m_level++;
        }
    }

	private void UpdateNeededExp()
    {
		m_neededExp *= 2; // temp [to remove] or more accurately to change but im too tired rn man
    }
    #endregion
}
