using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Helpers.Sheard;
using PLManagementSystem.service.Services;
using PLManagementSystem.UI.Helpers;

namespace PLManagementSystem.UI.Controllers
{
    public class LessonGroupsDaysController : Controller
    {
        private readonly ILessonGroupsDaysService _service;

        public LessonGroupsDaysController(ILessonGroupsDaysService service)
        {
            _service=service;
        }
        #region View
        public async Task<IActionResult> Index(int lessonGroupId)
        {
            ViewBag.LessonGroupId = lessonGroupId;
            var entities=await _service.GetAll(lessonGroupId);
            return PartialView(entities);
        }
        #endregion

        #region Add
        public async Task<IActionResult> Create(int lessonGroupId)
        {
            RequestLessonGroupsDaysDto dto = new RequestLessonGroupsDaysDto()
            {
                Id=0,
                LessonGroupId = lessonGroupId,
                IsDeleted=false
            };
            ViewBag.DayesList = (await _service.DayesList())
                ?.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            return PartialView(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RequestLessonGroupsDaysDto NewRecord)
        {
            ModelState.Remove(nameof(NewRecord.Id));
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
            return Json(new
            {
                isSucceeded = OpResult.IsSucceeded,
                message = OpResult.Message
            });
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
            return PartialView(ItemToEdit);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RequestLessonGroupsDaysDto RecordUpdated, int PageIndex)
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
            var OpResult = await _service.Edit(RecordUpdated);
            return Json(new
            {
                isSucceeded = OpResult.IsSucceeded,
                message = OpResult.Message
            });
        }
        #endregion
        #region Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int PageIndex)
        {
            var OpResult = await _service.Delete(id);
            return Json(new { isValid = false, Message = OpResult.Message });
        }
        #endregion
    }
}
