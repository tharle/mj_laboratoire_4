using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDEnemyInfo : MonoBehaviour
{
    [SerializeField] GameObject m_MenuEnemy;
    [SerializeField] Image m_PortraitImage;
    [SerializeField] Image m_HPBarEnemyValue;
    [SerializeField] TextMeshProUGUI m_NameEnemy;

    private void Start()
    {
        m_MenuEnemy.SetActive(false);
    }

    public void UpdateInfo(EnemyController enemy)
    {
        if (enemy == null)
        {
            m_MenuEnemy.SetActive(false);
            return;
        }

        m_PortraitImage.sprite = enemy.GetPortrait();
        m_NameEnemy.text = enemy.GetNameEnemy();

        UpdateHPBarEnemy(enemy.GetPercentHP());
        m_MenuEnemy.SetActive(true);
    }

    public void UpdateHPBarEnemy(float percentHP)
    {
        Vector3 hpBarEnemyValueScale = m_HPBarEnemyValue.transform.localScale;
        hpBarEnemyValueScale.x = percentHP;
        m_HPBarEnemyValue.transform.localScale = hpBarEnemyValueScale;
    }
}
