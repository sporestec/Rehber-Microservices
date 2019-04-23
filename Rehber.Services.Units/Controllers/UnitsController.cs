using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehber.Data.DbContexts;
using Rehber.Model.ViewModels;
using Rehber.Model.DataModels;

namespace Rehber.Services.UnitsApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private RehberUnitServiceDbContext _context = new RehberUnitServiceDbContext();

        [HttpGet]
        [Route("GetAllUnits")]
        public ActionResult<IEnumerable<Units>> GetUnits()
        {
            var UnitList = _context.Units.ToList().Select(x => new UnitViewModel
            {
                UnitId = x.UnitId,
                UnitName = x.UnitName,
                ParentId = x.ParentId
            });
            return Ok(UnitList);
        }


        [HttpGet("{id}")]
        [Route("GetUnitById")]
        public async Task<ActionResult<Units>> GetUnitById([FromQuery]int id)
        {
            var units = await _context.Units.FindAsync(id);
            var getParentName = _context.Units.Where(x => x.UnitId == units.ParentId).SingleOrDefault();
            UnitViewModel unitViewModel = new UnitViewModel();
            if (getParentName != null)
            {
                unitViewModel.UnitId = units.UnitId;
                unitViewModel.UnitName = units.UnitName;
                unitViewModel.ParentId = units.ParentId;
                unitViewModel.ParentName = getParentName.UnitName;
                return Ok(unitViewModel);
            }

            else if (getParentName == null)
            {
                unitViewModel.UnitId = units.UnitId;
                unitViewModel.UnitName = units.UnitName;
                unitViewModel.ParentId = units.ParentId;
                return Ok(unitViewModel);

            }

            else
            {
                return NotFound();
            }


        }

        [HttpGet("{parentId}")]
        [Route("GetUnitsByParentId")]
        public async Task<ActionResult<Units>> GetUnitsByParentId([FromQuery]int parentId)
        {
            var units = await _context.Units.Where(x => x.ParentId == parentId).SingleAsync();
            var unitViewModel = new UnitViewModel()
            {
                UnitId = units.UnitId,
                ParentId = units.ParentId,
                UnitName = units.UnitName
            };
            if (units == null)
            {
                return NotFound();
            }

            return Ok(unitViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnits(int id, [FromBody] UnitViewModel unitViewModel)
        {
            if (id != unitViewModel.UnitId)
            {
                return BadRequest();
            }

            else
            {
                Units unit = new Units();
                if (unitViewModel.ParentId == 0)
                {
                    unit.ParentId = null;
                    unit.UnitId = unitViewModel.UnitId;
                    unit.UnitName = unitViewModel.UnitName;
                }
                else
                {
                    unit.UnitId = unitViewModel.UnitId;
                    unit.ParentId = unitViewModel.ParentId;
                    unit.UnitName = unitViewModel.UnitName;
                }


                _context.Entry(unit).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(unit);
            }

        }


        [HttpPost]
        public IActionResult PostUnits([FromBody]UnitViewModel request)
        {
            Units unit = new Units();
            var getParentIdModel = _context.Units.Where(x => x.UnitName == request.ParentName).SingleOrDefault();
            if (getParentIdModel == null)
            {
                unit.UnitName = request.UnitName;
                _context.Units.Add(unit);
                _context.SaveChanges();
                return Ok(unit);
            }
            else
            {
                unit.UnitName = request.UnitName;
                unit.ParentId = getParentIdModel.UnitId;
                _context.Units.Add(unit);
                _context.SaveChanges();
                var unitViewModel = new UnitViewModel()
                {
                    UnitId = unit.UnitId,
                    UnitName = unit.UnitName,
                    ParentId = getParentIdModel.UnitId
                };
                return Ok(unitViewModel);
            }

        }


        [HttpDelete("{id}")]
        [Route("DeleteUnit")]
        public ActionResult<Units> DeleteUnits([FromQuery]int id)
        {
            var units = _context.Units.Where(x => x.UnitId == id).SingleOrDefault();
            if (units == null)
            {
                return NotFound();
            };
            _context.Units.Attach(units);
            _context.Units.Remove(units);
            _context.SaveChanges();

            return units;
        }


    }
}
