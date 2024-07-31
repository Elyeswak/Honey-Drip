using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private TextMeshProUGUI _honeyText; public void SetHoneyText(TextMeshProUGUI honeyText)
    {
        _honeyText = honeyText;
        _honeyText.text = _playerStats.Honey.ToString() + "/" + _playerStats.HoneyMax.ToString();
    }
    [SerializeField] private PlayerStats _playerStats;

    public bool AddHoney(int amount)
    {
        _playerStats.Honey += amount;
        if (_playerStats.Honey > _playerStats.HoneyMax)
            _playerStats.Honey = _playerStats.HoneyMax;

        _honeyText.text = _playerStats.Honey.ToString() + "/" + _playerStats.HoneyMax.ToString();
        return _playerStats.Honey == _playerStats.HoneyMax;
    }

    public float GetHealth()
    {
      return _playerStats.Health;
    }

    public void SetHealth(float amount)
    {
        _playerStats.Health = amount;
    }

    public bool AddHealth(int amount)
    {
        _playerStats.Health += amount;
        if (_playerStats.Health > _playerStats.HealthMax)
            _playerStats.Health = _playerStats.HealthMax;

        return _playerStats.Health == _playerStats.HealthMax;
    }
    public bool SubtractHoney(int amount)
    {
        if (_playerStats.Honey - amount < 0)
        {
            SFXManager.PlaySFX("DM-CGS-16");
            return false;
        }
        _playerStats.Honey -= amount;
        _honeyText.text = _playerStats.Honey.ToString() + "/" + _playerStats.HoneyMax.ToString();
        SFXManager.PlaySFX("DM-CGS-40");
        return true;
    }
    public void IncreaseMaxHoney(int amount)
    {
        _playerStats.HoneyMax += amount;
        _honeyText.text = _playerStats.Honey.ToString() + "/" + _playerStats.HoneyMax.ToString();
    }
    public void DecreaseMaxHoney(int amount)
    {
        _playerStats.HoneyMax -= amount;
        _honeyText.text = _playerStats.Honey.ToString() + "/" + _playerStats.HoneyMax.ToString();
    }
    void Start()
    {
        SetHoneyText(HoneyManagementSystem.HoneyText);
    }
}

[System.Serializable]
public struct PlayerStats
{
    public int Honey;
    public int HoneyMax;
    public float Health;
    public float HealthMax;
    public float Xp;
}
