using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tretimi;

public interface IStateSwitcher
{
    void SwitchState<T>() where T : State;
}
public interface IUIService
{
    void UpdateUI();
    Task ChangeBackground(int backgroundID);
}

public interface IDataService
{
    SaveData GetData();
    void AddCoins(int coins);
    void RemoveCoins(int coins);
}