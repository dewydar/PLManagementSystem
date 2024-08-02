using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PLManagementSystem.Core.Dtos;
using PLManagementSystem.Core.Dtos.FiltersVM;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Helpers.Helpers;
using PLManagementSystem.Helpers.Sheard;
using PLManagementSystem.UI.Helpers;

namespace PLManagementSystem.UI.Controllers
{
    public class LessonGroupsController : Controller
    {
        private readonly ILessonGroupsService _service;
        public LessonGroupsController(ILessonGroupsService service)
        {
            _service = service;
        }
        #region Filter
        private LessonGroupsFilterVM FillListFiltersFromQueryString()
        {
            var filters = new LessonGroupsFilterVM();
            filters.name = Request.Query["name"];
            filters.classId = !string.IsNullOrWhiteSpace(Request.Query["classId"]) ? int.Parse(Request.Query["classId"]) : null;
            filters.dayId = !string.IsNullOrWhiteSpace(Request.Query["dayId"]) ? int.Parse(Request.Query["dayId"]) : null;

            return filters;
        }
        private Dictionary<string, string> GetFilterAsDictionary(LessonGroupsFilterVM filter)
        {

            var ResultDictionary = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(filter.name))
            {
                ResultDictionary.Add("name", filter.name.Trim());
            }
            if (filter.classId!=null)
            {
                ResultDictionary.Add("classId", filter.classId.ToString());
            }
            if (filter.dayId != null)
            {
                ResultDictionary.Add("dayId", filter.dayId.ToString());
            }
            return ResultDictionary;
        }

        #endregion

        #region Sort

        private SortVM FillSortModelFromQueryString()
        {
            SortVM result = new SortVM();
            if (!string.IsNullOrWhiteSpace(Request.Query["SortColumn"]))
            {
                result.SortColumn = Request.Query["SortColumn"];
            }
            else
            {
                result.SortColumn = nameof(ResponseLessonGroupsDto.OrderNo);
            }

            if (!string.IsNullOrWhiteSpace(Request.Query["SortDirection"]))
            {
                result.SortDirection = Request.Query["SortDirection"];
            }
            else
            {
                result.SortDirection = "asc";
            }

            result.Columns = GetTableCoulmns();

            return result;
        }
        private Dictionary<string, string> GetSortModelAsDictionary(SortVM SortModel)
        {

            var ResultDictionary = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(SortModel.SortColumn))
            {
                ResultDictionary.Add("SortColumn", SortModel.SortColumn.Trim());
            }
            if (!string.IsNullOrWhiteSpace(SortModel.SortDirection))
            {
                ResultDictionary.Add("SortDirection", SortModel.SortDirection.Trim());
            }

            return ResultDictionary;
        }

        private List<TableCloumnVM> GetTableCoulmns()
        {
            List<TableCloumnVM> result = new List<TableCloumnVM>
            {
               new TableCloumnVM {ColumnName = nameof(ResponseLessonGroupsDto.OrderNo),ColumnDisplayName="Order No", IsShow = true,IsSortable=true},
               new TableCloumnVM {ColumnName = nameof(ResponseLessonGroupsDto.Name),ColumnDisplayName="Name", IsShow = true,IsSortable=true},
               new TableCloumnVM {ColumnName = nameof(ResponseLessonGroupsDto.ClassName),ColumnDisplayName="Class", IsShow = true,IsSortable=true},
               //new TableCloumnVM {ColumnName = nameof(ResponseLessonGroupsDto.Time),ColumnDisplayName="Class", IsShow = true,IsSortable=false},
               new TableCloumnVM {ColumnName = nameof(ResponseLessonGroupsDto.Price),ColumnDisplayName="Class", IsShow = true,IsSortable=false},
               //new TableCloumnVM {ColumnName = nameof(ResponseLessonGroupsDto.DayName),ColumnDisplayName="Day", IsShow = true,IsSortable=true}
            };

            return result;
        }

        #endregion

        #region View
        public async Task<IActionResult> Index(int PageIndex = 1)
        {

            var SortModel = FillSortModelFromQueryString();
            var FiltersModel = FillListFiltersFromQueryString();

            var SortDictonary = GetSortModelAsDictionary(SortModel);
            var FilterDictonary = GetFilterAsDictionary(FiltersModel);

            ViewData["filterDictonary"] = FilterDictonary;
            ViewData["QueryStringDictionary"] = DictionaryHelper.MergeDictionaries(FilterDictonary, SortDictonary);


            ViewData["filters"] = FiltersModel;
            ViewData["SortModel"] = SortModel;
            var ProxyResponse = await _service.GetAllPaginantion(name: FiltersModel.name,
                offset: PageIndex, limit: 10, sortColumn: SortModel.SortColumn, sortDirection: SortModel.SortDirection);

            if (ProxyResponse == null)
            {

                return RedirectToAction("Error", "Home");
            }
            ViewBag.ClassesList = (await _service.ClassesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            ViewBag.DayesList = (await _service.DayesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            var paginationResponseModelV2 = new PaginationResponseModelV2(PageIndex: PageIndex, ReturnData: (List<ResponseLessonGroupsDto>)ProxyResponse.ReturnData,
                PageSize: 10, TotalItemCount: ProxyResponse.TotalItemCount, TotalPagesCount: ProxyResponse.TotalPagesCount);

            return View(paginationResponseModelV2);

        }

        public IActionResult GetListFromFilters(LessonGroupsFilterVM filters)
        {
            var FilterDictionary = GetFilterAsDictionary(filters);
            return RedirectToAction("Index", FilterDictionary);
        }

        [HttpGet]
        public async Task<IActionResult> GetListViewAsJson(int PageIndex = 1, string? message = null)
        {
            var SortModel = FillSortModelFromQueryString();
            var FiltersModel = FillListFiltersFromQueryString();

            var SortDictonary = GetSortModelAsDictionary(SortModel);
            var FilterDictonary = GetFilterAsDictionary(FiltersModel);

            ViewData["filterDictonary"] = FilterDictonary;
            ViewData["QueryStringDictionary"] = DictionaryHelper.MergeDictionaries(FilterDictonary, SortDictonary);


            ViewData["filters"] = FiltersModel;
            ViewData["SortModel"] = SortModel;
            var ProxyResponse = await _service.GetAllPaginantion(name: FiltersModel.name,
                            offset: PageIndex, limit: 10, sortColumn: SortModel.SortColumn, sortDirection: SortModel.SortDirection);
            if (ProxyResponse == null)
            {
                return Json(new { isValid = false });
            }
            if (PageIndex == -1)
            {
                PageIndex = Convert.ToInt32(Math.Ceiling((double)ProxyResponse.TotalItemCount / ProxyResponse.PageSize));
            }
            ViewBag.ClassesList = (await _service.ClassesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            ViewBag.DayesList = (await _service.DayesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            var paginationResponseModelV2 = new PaginationResponseModelV2(PageIndex: PageIndex, ReturnData: (List<ResponseLessonGroupsDto>)ProxyResponse.ReturnData,
                PageSize: 10, TotalItemCount: ProxyResponse.TotalItemCount, TotalPagesCount: ProxyResponse.TotalPagesCount);
            string viewFromCurrentController = RazorHelper.RenderRazorViewToString(this, "ListView", paginationResponseModelV2);

            return Json(new { isValid = true, html = viewFromCurrentController, message = message });
        }
        #endregion
        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ClassesList = (await _service.ClassesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            ViewBag.DayesList = (await _service.DayesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RequestLessonGroupsDto NewRecord)
        {
            ModelState.Remove(nameof(NewRecord.Id));
            ModelState.Remove(nameof(NewRecord.IsDeleted));
            ModelState.Remove(nameof(NewRecord.OrderNo));
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                            .Select(ms => new
                            {
                                Field = ms.Key,
                                Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                            });

                var errorMessages = errors.Select(e =>
                                        $"{e.Field}: {string.Join(" | ", e.Errors)}"
                                     );

                var message = string.Join(" | ", errorMessages);

                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Create", NewRecord),
                    errorMessage = message
                });
            }

            var OpResult = await _service.Create(NewRecord);
            if (!OpResult.IsSucceeded)
            {
                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Create", NewRecord),
                    errorMessage = OpResult.Message
                });
            }
            return await GetListViewAsJson(PageIndex: -1, message: OpResult.Message);
        }

        #endregion
        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int PageIndex)
        {
            var ItemToEdit = await _service.GetById(id);
            if (ItemToEdit == null)
            {
                return RedirectToAction("Error", "Home");

            }
            var filters = FillListFiltersFromQueryString();
            var filterDictonary = GetFilterAsDictionary(filters);
            var SortModel = FillSortModelFromQueryString();
            var SortDictonary = GetSortModelAsDictionary(SortModel);

            ViewData["QueryStringDictionary"] = DictionaryHelper.MergeDictionaries(filterDictonary, SortDictonary);
            ViewData["PageIndex"] = PageIndex;
            ViewBag.ClassesList = (await _service.ClassesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            ViewBag.DayesList = (await _service.DayesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            return PartialView(ItemToEdit);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RequestLessonGroupsDto RecordUpdated, int PageIndex)
        {
            if (id != RecordUpdated.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                          .Select(ms => new
                          {
                              Field = ms.Key,
                              Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                          });

                var errorMessages = errors.Select(e =>
                                        $"{e.Field}: {string.Join(" | ", e.Errors)}"
                                     );

                var message = string.Join(" | ", errorMessages);

                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Edit", RecordUpdated),
                    errorMessage = message
                });
            }


            var filters = FillListFiltersFromQueryString();
            var filterDictonary = GetFilterAsDictionary(filters);
            var SortModel = FillSortModelFromQueryString();
            var SortDictonary = GetSortModelAsDictionary(SortModel);

            ViewData["QueryStringDictionary"] = DictionaryHelper.MergeDictionaries(filterDictonary, SortDictonary);
            ViewData["PageIndex"] = PageIndex;


            var OpResult = await _service.Edit(RecordUpdated);
            if (!OpResult.IsSucceeded)
            {
                //_notyfService.Warning(OpResult.ErrorMessage);
                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Edit", RecordUpdated),
                    errorMessage = OpResult.Message
                });
            }
            ModelState.Clear();
            //_notyfService.Success("The record updated successfully");
            return await GetListViewAsJson(PageIndex: PageIndex, message: OpResult.Message);
        }
        #endregion
        #region Change Position
        [HttpGet]
        public IActionResult ChangePosition(int id, int orderNo, int maxOrder, int PageIndex)
        {
            var filters = FillListFiltersFromQueryString();
            var filterDictonary = GetFilterAsDictionary(filters);
            var SortModel = FillSortModelFromQueryString();
            var SortDictonary = GetSortModelAsDictionary(SortModel);

            ViewData["QueryStringDictionary"] = DictionaryHelper.MergeDictionaries(filterDictonary, SortDictonary);
            ViewData["PageIndex"] = PageIndex;
            return PartialView(new UpdateOrderDto
            {
                Id = id,
                MaxOrderNo = maxOrder,
                OrderNo = orderNo
            });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePosition(int id, int PageIndex, UpdateOrderDto RecordUpdated)
        {
            if (id != RecordUpdated.Id)
            {
                return NotFound();
            }
            if (RecordUpdated.OrderNo > RecordUpdated.MaxOrderNo)
                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Edit", RecordUpdated),
                    errorMessage = $"Enter a value between 1 and {RecordUpdated.MaxOrderNo}"
                });
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                          .Select(ms => new
                          {
                              Field = ms.Key,
                              Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                          });

                var errorMessages = errors.Select(e =>
                                        $"{e.Field}: {string.Join(" | ", e.Errors)}"
                                     );

                var message = string.Join(" | ", errorMessages);

                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Edit", RecordUpdated),
                    errorMessage = message
                });
            }
            var filters = FillListFiltersFromQueryString();
            var filterDictonary = GetFilterAsDictionary(filters);
            var SortModel = FillSortModelFromQueryString();
            var SortDictonary = GetSortModelAsDictionary(SortModel);
            RecordUpdated.Id = id;
            ResponseResult OpResult = await _service.UpdateOrderAsync(id, RecordUpdated);

            if (!OpResult.IsSucceeded)
            {
                return Json(new
                {
                    isValid = false,
                    html = RazorHelper.RenderRazorViewToString(this, "Edit", RecordUpdated),
                    errorMessage = OpResult.Message
                });
            }

            ModelState.Clear();
            return await GetListViewAsJson(PageIndex: PageIndex, message: OpResult.Message);
        }
        #endregion
        #region Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int PageIndex)
        {
            var OpResult = await _service.SoftDelete(id);
            if (!OpResult.IsSucceeded)
            {
                return Json(new { isValid = false, errorMessage = OpResult.Message });
            }

            var filters = FillListFiltersFromQueryString();
            var filterDictonary = GetFilterAsDictionary(filters);
            var SortModel = FillSortModelFromQueryString();
            var SortDictonary = GetSortModelAsDictionary(SortModel);

            ViewData["QueryStringDictionary"] = DictionaryHelper.MergeDictionaries(filterDictonary, SortDictonary);
            ViewData["PageIndex"] = PageIndex;



            return await GetListViewAsJson(PageIndex: PageIndex, message: OpResult.Message);
        }
        #endregion
    }
}
