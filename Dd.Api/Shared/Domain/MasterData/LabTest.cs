namespace Dd.Api.Shared.Domain.MasterData;

public class LabTest : Service {
    
    public Guid LabDepartmentId { get; set; }
    public LabDepartment? LabDepartment { get; set; }
    
    public Guid MeasurementUnitId { get; set; }
    public MeasurementUnit? MeasurementUnit { get; set; }
    
    public double? MaleLowerLimit { get; set; }
    public double? MaleNormalValue { get; set; }
    public double? MaleUpperLimit { get; set; }
    
    public double? FemaleLowerLimit { get; set; }
    public double? FemaleNormalValue { get; set; }
    public double? FemaleUpperLimit { get; set; }
    
    public double? ChildLowerLimit { get; set; }
    public double? ChildNormalValue { get; set; }
    public double? ChildUpperLimit { get; set; }
    
    public double? InfantLowerLimit { get; set; }
    public double? InfantNormalValue { get; set; }
    public double? InfantUpperLimit { get; set; }
}