using Microsoft.AspNetCore.Mvc;
using PLManagementSystem.Core.Dtos.FiltersVM;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Helpers.Sheard;
using PLManagementSystem.UI.Helpers;

namespace PLManagementSystem.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        #region Filter
        private UserFilterVM FillListFiltersFromQueryString()
        {
            var filters = new UserFilterVM();
            filters.name = Request.Query["name"];
            filters.isActive = string.IsNullOrWhiteSpace(Request.Query["isActive"]) ? null : bool.Parse(Request.Query["isActive"]);

            return filters;
        }
        private Dictionary<string, string> GetFilterAsDictionary(UserFilterVM filter)
        {

            var ResultDictionary = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(filter.name))
            {
                ResultDictionary.Add("name", filter.name.Trim());
            }
            if (filter.isActive != null)
            {
                ResultDictionary.Add("isActive", filter.isActive.ToString());
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
                result.SortColumn = nameof(ResponseUserDto.Id);
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
               new TableCloumnVM {ColumnName = nameof(ResponseUserDto.Id),ColumnDisplayName="Id", IsShow = true,IsSortable=true},
               new TableCloumnVM {ColumnName = nameof(ResponseUserDto.Name),ColumnDisplayName="Name", IsShow = true,IsSortable=true},
               new TableCloumnVM {ColumnName = nameof(ResponseUserDto.IsActive),ColumnDisplayName="Active", IsShow = true,IsSortable=true},
               new TableCloumnVM {ColumnName = nameof(ResponseUserDto.UserName),ColumnDisplayName="UserName", IsShow = true,IsSortable=true},
               new TableCloumnVM {ColumnName = nameof(ResponseUserDto.IsAdmin),ColumnDisplayName="Admin", IsShow = true,IsSortable=false}
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
            var ProxyResponse = await _service.GetAllPaginantion(name: FiltersModel.name, isActive: FiltersModel.isActive,
                offset: PageIndex, limit: 10, sortColumn: SortModel.SortColumn, sortDirection: SortModel.SortDirection);

            if (ProxyResponse == null)
            {

                return RedirectToAction("Error", "Home");
            }
            var paginationResponseModelV2 = new PaginationResponseModelV2(PageIndex: PageIndex, ReturnData: (List<ResponseUserDto>)ProxyResponse.ReturnData,
                PageSize: 10, TotalItemCount: ProxyResponse.TotalItemCount, TotalPagesCount: ProxyResponse.TotalPagesCount);

            return View(paginationResponseModelV2);

        }

        public IActionResult GetListFromFilters(UserFilterVM filters)
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
            var ProxyResponse = await _service.GetAllPaginantion(name: FiltersModel.name, isActive: FiltersModel.isActive,
                            offset: PageIndex, limit: 10, sortColumn: SortModel.SortColumn, sortDirection: SortModel.SortDirection);
            if (ProxyResponse == null)
            {
                return Json(new { isValid = false });
            }
            if (PageIndex == -1)
            {
                PageIndex = Convert.ToInt32(Math.Ceiling((double)ProxyResponse.TotalItemCount / ProxyResponse.PageSize));
            }
            var paginationResponseModelV2 = new PaginationResponseModelV2(PageIndex: PageIndex, ReturnData: (List<ResponseUserDto>)ProxyResponse.ReturnData,
                PageSize: 10, TotalItemCount: ProxyResponse.TotalItemCount, TotalPagesCount: ProxyResponse.TotalPagesCount);
            string viewFromCurrentController = RazorHelper.RenderRazorViewToString(this, "ListView", paginationResponseModelV2);

            return Json(new { isValid = true, html = viewFromCurrentController, message = message });
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RequestUserDto NewRecord)
        {
            ModelState.Remove(nameof(NewRecord.Id));
            ModelState.Remove(nameof(NewRecord.Password));
            ModelState.Remove(nameof(NewRecord.IsDeleted));
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

            return PartialView(ItemToEdit);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RequestUserDto RecordUpdated, int PageIndex)
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
