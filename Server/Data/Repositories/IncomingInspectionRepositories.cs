using MES.Server.Contracts;
using MES.Shared.DTOs.MES.Shared.DTOs.Rotors;
using MES.Shared.Models.Rotors;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MES.Server.Data.Repositories
{
    public class IncomingInspectionRepository : IIncomingInspection
    {
        private readonly ProjectdbContext _context;

        public IncomingInspectionRepository(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<IncomingInspection> Add(IncomingInspectionDTO dto)
        {
            try
            {
                var entity = new IncomingInspection
                {
                    SalesOrderNumber = dto.SalesOrderNumber,
                    WorkOrder = dto.WorkOrder,
                    MatNumber = dto.MatNumber,
                    Customer = dto.Customer,
                    Location = dto.Location,
                    Received = dto.Received,
                    Inspected = dto.Inspected,
                    RotorsNumber = dto.RotorsNumber,
                    Initials = dto.Initials,
                    Make = dto.Make,
                    Dia = dto.Dia,
                    Len = dto.Len,
                    Fits = dto.Fits,
                    Materials = dto.Materials,
                    Others = dto.Others,
                    RotorsDia = dto.RotorsDia,
                    RotorStyle = dto.RotorStyle,
                    Type = dto.Type,
                    BearingRemoved = dto.BearingRemoved,
                    Bearing = dto.Bearing,
                    BearingSeals = dto.BearingSeals,
                    CeramicSeals = dto.CeramicSeals,
                    Right = dto.Right,
                    yRight = dto.yRight,
                    Left = dto.Left,
                    yLeft = dto.yLeft,
                    BasicSharpening = dto.BasicSharpening,
                    IfYBasicSharpening = dto.IfYBasicSharpening,
                    WedgelockAlignmentMarks = dto.WedgelockAlignmentMarks,
                    CenterGrinding = dto.CenterGrinding,
                    IfYCenterGrinding = dto.IfYCenterGrinding,
                    Aligned = dto.Aligned,
                    PlasticSleaves = dto.PlasticSleaves,
                    Welding = dto.Welding,
                    WeldingNum = dto.WeldingNum,
                    BedKnife = dto.BedKnife,
                    BoxReceivedWithSaddles = dto.BoxReceivedWithSaddles,
                    ReProfile = dto.ReProfile,
                    SandBlasting = dto.SandBlasting,
                    ManualLabor = dto.ManualLabor,
                    Bottom = dto.Bottom,
                    Top = dto.Top,
                    AddQty = dto.AddQty,
                    TirLeftJournal = dto.TirLeftJournal,
                    TirRightJournal = dto.TirRightJournal,
                    SaddlePartNumber = dto.SaddlePartNumber,
                    SerialNumber = dto.SerialNumber,
                    ADDQTYdata = dto.ADDQTYdata,
                    NewBoxRequired = dto.NewBoxRequired,
                    NewBoxRequiredBox = dto.NewBoxRequiredBox,
                    Module = dto.Module,
                    DateTime = dto.DateTime,
                    RotorCategorization = dto.RotorCategorization,
                    ComponentType = dto.ComponentType,
                    Users = dto.Users
                };

                _context.IncomingInspections.Add(entity);
                await _context.SaveChangesAsync(); // async version

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //public async Task<IEnumerable<IncomingInspection>> GetAllAsync()
        //{
        //    return await _context.IncomingInspections.Include(i => i.Images).ToListAsync();
        //}

        public async Task<IncomingInspection?> GetByIdAsync(int id)
        {
            return await _context.IncomingInspections

                                 .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> SerialNumberExistsAsync(string serialNumber)
        {
            return await _context.IncomingInspections.AnyAsync(x => x.SerialNumber == serialNumber);
        }


        public async Task<IncomingInspection> AddAsync(IncomingInspection inspection)
        {
            _context.IncomingInspections.Add(inspection);
            await _context.SaveChangesAsync();
            return inspection;
        }

        //public async Task<IncomingInspection?> UpdateAsync(int id, IncomingInspection inspection)
        //{
        //    var existing = await _context.IncomingInspections.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == id);
        //    if (existing == null) return null;

        //    _context.Entry(existing).CurrentValues.SetValues(inspection);
        //    await _context.SaveChangesAsync();
        //    return existing;
        //}

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.IncomingInspections.FindAsync(id);
            if (existing == null) return false;

            _context.IncomingInspections.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        //public Task<IncomingInspection> Add(IncomingInspectionDTO dto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}