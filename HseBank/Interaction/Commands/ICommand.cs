﻿namespace HseBank.Interaction.Commands;

// Этот enum не сипользуется, это просто все типы команд.
public enum CommandType
{
    CreateAccount,
    Deposit,
    Withdraw,
    Transfer,
    MakeOperation,
    GetNetAmount,
    GetStatistics,
    ExportData,
    AddCategory,
    ShowCategories,
    GetAccountInfo,
}

/// <summary>
/// Интерфейс для пользовательских команд.
/// </summary>
public interface ICommand
{
    void Execute();
}