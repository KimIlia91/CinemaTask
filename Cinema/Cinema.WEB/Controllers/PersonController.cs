﻿using Cinema.WEB.Helpers;
using Cinema.WEB.Models.PersonModels;
using Cinema.WEB.Models.PersonModels.PersonVms;
using Cinema.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WEB.Controllers
{
    public class PersonController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: PersonController
        public async Task<IActionResult> PersonIndex(
            bool sort, string? search = null, int page = 1, int pageSize = 20)
        {
            var request = new PersonsFilterRequest(sort, search, pageSize, page);
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var pageResponse = await _unitOfWork.Persons.GetAllPersonsAsync(token!, request);
            return View(pageResponse);
        }

        // GET: PersonController/Create
        public ActionResult PersonCreate()
        {
            return View(new PersonCreateVm());
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonCreate(PersonCreateVm personVm)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString(SD.SessionToken);
                personVm.Person.ImageUrl = await _unitOfWork.Images.SaveImageAsync(personVm.ImageFile, personVm.Person.ImageUrl, @"images\persons");
                var response = await _unitOfWork.Persons.CreatePersonAsync(personVm.Person, token!);
                if (response.IsSuccess)
                {
                    TempData["success"] = "Информация успешно добавлена!";
                    return RedirectToAction(nameof(PersonIndex));
                }

                _unitOfWork.Images.DeleteImage(personVm.Person.ImageUrl);
                personVm.Person.ImageUrl = null; 
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    TempData["error"] = string.Join("\n", response.ErrorMessage);
                    return View(personVm);
                }
            }

            TempData["error"] = "Oops! Неудалось добавить информацию!";
            return View(personVm);
        }

        // GET: PersonController/PersonUpdate/5
        public async Task<IActionResult> PersonUpdate(Guid id)
        {
            if (id != Guid.Empty)
            {
                var token = HttpContext.Session.GetString(SD.SessionToken);
                var personUpdateVm = new PersonUpdateVm
                {
                    Person = await _unitOfWork.Persons.GetPersonAsync(id, token!)
                };

                return View(personUpdateVm);
            }

            TempData["error"] = "Oops! Не удаётся получить информацию!";
            return RedirectToAction(nameof(PersonIndex));
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonUpdate(PersonUpdateVm personVm)
        {
            if (ModelState.IsValid)
            {
                var oldImgPath = personVm.Person.ImageUrl;
                personVm.Person.ImageUrl = await _unitOfWork.Images.SaveImageAsync(personVm.ImageFile, personVm.Person.ImageUrl, @"images\persons");
                var token = HttpContext.Session.GetString(SD.SessionToken);
                var response = await _unitOfWork.Persons.UpdatePersonAsync(personVm.Person, token!);
                if (response.IsSuccess)
                {
                    TempData["success"] = "Информация успешно обновлена!";
                    _unitOfWork.Images.DeleteImage(oldImgPath);
                    return RedirectToAction(nameof(PersonIndex));
                }

                _unitOfWork.Images.DeleteImage(personVm.Person.ImageUrl);
                personVm.Person.ImageUrl = oldImgPath;
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    TempData["error"] = string.Join("\n", response.ErrorMessage);
                    return View(personVm);
                }
            }

            TempData["error"] = "Oops! Неудалось обновить информацию!";
            return View(personVm);
        }

        // POST: PersonController/Delete 
        public async Task<IActionResult> PersonDelete(Guid id)
        {
            var token = HttpContext.Session.GetString(SD.SessionToken);
            var personToDelete = await _unitOfWork.Persons.GetPersonAsync(id, token!);
            if (personToDelete is not null)
            {
                var isSuccess = await _unitOfWork.Persons.DeletePersonAsync(personToDelete.Id, token!);
                if (isSuccess)
                {
                    _unitOfWork.Images.DeleteImage(personToDelete.ImageUrl);
                    TempData["success"] = "Информация успешно удалена!";
                    return RedirectToAction(nameof(PersonIndex));
                }
            }

            TempData["error"] = "Oops! Неудалось удалить информация.";
            return RedirectToAction(nameof(PersonIndex));
        }
    }
}
