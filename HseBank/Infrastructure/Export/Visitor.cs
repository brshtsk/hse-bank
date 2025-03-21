using HseBank.Services.Facades;

namespace HseBank.Infrastructure.Export;

public class Visitor : IVisitor
{
    private readonly JsonDataExporter _exporter;

    public Visitor(JsonDataExporter exporter)
    {
        _exporter = exporter;
    }

    public void Visit<T>(T source)
    {
        switch (source)
        {
            case BankAccountFacade bankAccountFacade:
                VisitBankAccount(bankAccountFacade);
                break;
            case CategoryFacade categoryFacade:
                VisitCategory(categoryFacade);
                break;
            case OperationFacade operationFacade:
                VisitOperation(operationFacade);
                break;
            default:
                throw new ArgumentException("Unsupported source type", nameof(source));
        }
    }

    private void VisitBankAccount(BankAccountFacade facade)
    {
        var data = facade.GetData();
        _exporter.ExportToJson(data, "BankAccountData.json");
    }

    private void VisitCategory(CategoryFacade facade)
    {
        var data = facade.GetData();
        _exporter.ExportToJson(data, "CategoryData.json");
    }

    private void VisitOperation(OperationFacade facade)
    {
        var data = facade.GetData();
        _exporter.ExportToJson(data, "OperationData.json");
    }
}