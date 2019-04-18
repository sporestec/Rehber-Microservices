using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rehber.Core.Helpers;
using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;

namespace Rehber.Admin.Controllers
{

    public class UnitController : Controller
    {
        UnitsApiHelper unitsApiHelper = new UnitsApiHelper();

        public IActionResult UnitMainPage()
        {
            return View();
        }

        public IActionResult GetAllUnit()
        {
            var unitList = unitsApiHelper.GetAllUnits();
            return Json(unitList);
        }

        public JsonResult DeleteUnit(int unitId)
        {
            string message = unitsApiHelper.DeleteUnit(unitId);
            return Json("");
        }

        public JsonResult AddUnit(string unitName, string parentUnit)
        {
            if (unitName != "")
            {
                var responeUnit = unitsApiHelper.AddUnit(unitName, parentUnit);
                Units unit = new Units();
                unit.UnitName = responeUnit.Result.UnitName;
                unit.ParentId = responeUnit.Result.ParentId;
                unit.UnitId = responeUnit.Result.UnitId;
                return Json(unit);
            }
            return Json("");
        }

        public JsonResult GetUnitById(int unitId)
        {
            var unit = unitsApiHelper.GetById(unitId);
            return Json(unit);
        }

        [HttpPost]
        public JsonResult SaveEditing([FromBody]UnitViewModel unitViewModel)
        {
            var unit = unitsApiHelper.EditUnit(unitViewModel);
            return Json(unit);
        }

    }
}