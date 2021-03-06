﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Academy.Core.Batchs;
using Academy.Core.ComplexTypes;
using Academy.Core.ViewModels;
using Academy.Web.Models;

namespace Academy.Web.Controllers
{
    public class BatchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BatchesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Batches
        public async Task<ActionResult> Index(int? query = null)
        {
            IQueryable<Batch> batches = _context.Batches;
            if (query != null)
                batches = batches.Where(c => c.BatchNumber == query);
            var batchViewModel = new BatchViewModel()
            {
                Batches = await batches.ToListAsync(),
                SearchTerm = query
            };
            return View(batchViewModel);
        }
        public async Task<ActionResult> New()
        {
            await GetDropLists();
            var batch = new Batch();
            return View("BatchForm", batch);
        }

        private async Task GetDropLists()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
        }

        [HttpPost]
        public ActionResult Search(BatchViewModel batch)
        {
            return RedirectToAction("Index", "Batches", new { query = batch.SearchTerm });
        }

        public async Task<ActionResult> Edit(int id)
        {
            var batch = await _context.Batches.SingleOrDefaultAsync(s => s.Id == id);
            if (batch == null) return HttpNotFound();
            await GetDropLists();
            return View("BatchForm", batch);
        }

        public async Task<ActionResult> DeleteBatch(int id)
        {
            var batch = await _context.Batches.SingleOrDefaultAsync(x => x.Id == id);
            if (batch == null) return HttpNotFound();
            batch.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Batches");
        }

        [HttpPost]
        public async Task<ActionResult> Save(Batch batch)
        {
            if (!ModelState.IsValid)
            {
                await GetDropLists();
                return View("BatchForm", batch);
            }
            if (batch.Id == 0)
                _context.Batches.Add(batch);
            else
            {
                var batchDb = await _context.Batches.SingleAsync(x => x.Id == batch.Id);
                TryUpdateModel(batchDb);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Batches");
        }

        [HttpPost]
        public async Task<ActionResult> GetBatches(string selectedCategory)
        {
            if (string.IsNullOrEmpty(selectedCategory))
                return Json(new SelectList(new List<Batch>(), "Id", "BatchNumber",
                    JsonRequestBehavior.AllowGet));

            var id = Convert.ToInt16(selectedCategory);
            var batches = await _context.Batches.Where(x => x.CategoryId == id).ToListAsync();
            return Json(new SelectList(batches, "Id", "BatchNumber", JsonRequestBehavior.AllowGet));
        }
    }
}