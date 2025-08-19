namespace Dd.Api.Shared.Domain.MasterData;

public class ContractMedicalPersonnel : MedicalPersonnel {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string? Email { get; set; }
    
    public DateOnly ContractStartDate { get; set; }
    public DateOnly ContractEndDate { get; set; }
}