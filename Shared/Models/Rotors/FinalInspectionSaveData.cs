using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class FinalInspectionSaveData
    {
        [Key]
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? Module { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? WorkOrder { get; set; }
        public string? MatNumber { get; set; }
        public string? Customer { get; set; }
        public string? Location { get; set; }
        public string? Received { get; set; }
        public string? Inspected { get; set; }
        public string? RotorsNumber { get; set; }
        public string? Initials { get; set; }
        public string? Make { get; set; }
        public string? Dia { get; set; }
        public string? Len { get; set; }
        public string? Fits { get; set; }
        public string? Materials { get; set; }
        public string? Others { get; set; }
        public string? RotorsDia { get; set; }
        public string? RotorStyle { get; set; }
        public string? Type { get; set; }
        public string? BearingRemoved { get; set; }
        public string? Bearing { get; set; }
        public string? BearingSeals { get; set; }
        public string? CeramicSeals { get; set; }
        public string? Right { get; set; }
        public string? yRight { get; set; }
        public string? Left { get; set; }
        public string? yLeft { get; set; }
        public string? BasicSharpening { get; set; }
        public string? IfYBasicSharpening { get; set; }
        public string? WedgelockAlignmentMarks { get; set; }
        public string? CenterGrinding { get; set; }
        public string? IfYCenterGrinding { get; set; }
        public string? Aligned { get; set; }
        public string? PlasticSleaves { get; set; }
        public string? Welding { get; set; }
        public string? WeldingNum { get; set; }
        public string? BedKnife { get; set; }
        public string? BoxReceivedWithSaddles { get; set; }
        public string? ReProfile { get; set; }
        public string? SandBlasting { get; set; }
        public string? ManualLabor { get; set; }
        public string? Bottom { get; set; }
        public string? Top { get; set; }
        public int? AddQty { get; set; }
        public string? TirLeftJournal { get; set; }
        public string? TirRightJournal { get; set; }
        public string? SaddlePartNumber { get; set; }
        public DateTime? DateTime { get; set; }
        public string? RotorCategorization { get; set; }
        public string? ComponentType { get; set; }
        public string? Users { get; set; }
        public DateTime? TargetDate { get; set; }
        public string? CustomerInstructions { get; set; }
        public string? CustomerImportance { get; set; }
        public DateTime? SubmitDate { get; set; }
        public string? SubmitedBy { get; set; }
        public string? AdvancedSharpingStatus { get; set; }
        public string? Workcenters { get; set; }
        public DateTime? ProductionSubmitDate { get; set; }
        public string? ProductionSubmitBy { get; set; }
        public string? RotorsDiaLeft { get; set; }
        public string? RotorsDiaRight { get; set; }
        public string? ReliefLand { get; set; }
        public string? ToothFaceLeft { get; set; }
        public string? ToothFaceRight { get; set; }
        public string? CentersLeft { get; set; }
        public string? CentersRight { get; set; }
        public string? VisualChecks { get; set; }
        public string? InspectedBy { get; set; }
        public DateTime? GrindingStartDate { get; set; }
        public string? GrindingEndDate { get; set; }
        public string? Notes { get; set; }
        public string? DelayReasonTracking { get; set; }
        public string? CustomerPoNum { get; set; }
        public string? DWGNum { get; set; }
        public string? AGNum { get; set; }
        public string? SpecialNoteComment { get; set; }
        public string? Dressedwithnewbearing { get; set; }
        public string? InspectorSing { get; set; }
        public string? Description { get; set; }
        public string? Oktoship { get; set; }
        public string? InspectorComments { get; set; }
        public string? Start { get; set; }
        public string? FluteDiameterStart { get; set; }
        public string? FluteDiameterFinish { get; set; }
        public string? LandWidthStart { get; set; }
        public string? LandWidthFinish { get; set; }
        public string? TIRStart { get; set; }
        public string? TIRfinish { get; set; }
        public string? TaperStart { get; set; }
        public string? Taperfinish { get; set; }
        public string? ReliefAngleStart { get; set; }
        public string? ReliefAngleFinish { get; set; }
        public string? LocknutThreadsStart { get; set; }
        public string? LocknutThreadsFinish { get; set; }
        public string? IstheRotorcleanStart { get; set; }
        public string? IstheRotorcleanfinish { get; set; }
        public string? JournalsOKStart { get; set; }
        public string? JournalsOKfinish { get; set; }
        public string? WedgelockassemblyStart { get; set; }
        public string? WedgelockassemblyFinish { get; set; }
        public string? SpecialPartWashStart { get; set; }
        public string? SpecialPartWashFinish { get; set; }
        public string? Finish { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? GrindingSubmiteddBy { get; set; }
        public string? FinalInspectionSubmiteddBy { get; set; }
        public DateTime? FinalInspectionSubmitedByDate { get; set; }
    }
}
