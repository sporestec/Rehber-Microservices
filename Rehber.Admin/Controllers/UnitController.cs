using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rehber.Model.DataModels;
using Rehber.Model.ViewModels;
using Rehber.WebApps.Admin.Core;

namespace Rehber.WebApps.Admin.Controllers
{
    public class UnitController : Controller
    {
        UnitCrudWithApi unitCrudWhitApi = new UnitCrudWithApi();
        public IActionResult UnitMainPage()
        {
            return View();
        }
        public IActionResult GetAllUnit()
        {
            var unitList = unitCrudWhitApi.GetAllUnits();
            return Json(unitList);
        }
        public JsonResult DeleteUnit(int unitId)

        {
            string message = unitCrudWhitApi.DeleteUnit(unitId);
            return Json("");
        }

        public JsonResult AddUnit(string unitName, string parentUnit)
        {
            if (unitName != "")
            {
                var responeUnit = unitCrudWhitApi.AddUnit(unitName, parentUnit);
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

            var unit = unitCrudWhitApi.GetUnitById(unitId);
            return Json(unit);

        }

        [HttpPost]
        public JsonResult SaveEditing([FromBody]UnitViewModel unitViewModel)
        {
            var unit = unitCrudWhitApi.EditUnit(unitViewModel);
            return Json(unit);
        }

    }
}