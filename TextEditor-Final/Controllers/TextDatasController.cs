using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TextEditor_Final.Areas.Identity.Data;
using TextEditor_Final.Models;

namespace TextEditor_Final.Controllers
{
    public class TextDatasController : Controller
    {
        private readonly TextEditorContext _context;
        private UserManager<TextEditorUser> _userManager;

        public TextDatasController(TextEditorContext context,UserManager<TextEditorUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TextDatas
        public async Task<IActionResult> Index()
        {
              return _context.TextData != null ? 
                          View(await _context.TextData.ToListAsync()) :
                          Problem("Entity set 'TextEditorContext.TextData'  is null.");
        }

        // GET: TextDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TextData == null)
            {
                return NotFound();
            }

            var textData = await _context.TextData
                .FirstOrDefaultAsync(m => m.FileId == id);
            if (textData == null)
            {
                return NotFound();
            }

            return View(textData);
        }

        // GET: TextDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TextDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileId,FileName,Data")] TextData textData)
        {
            var user =  await _userManager.GetUserAsync(User);
            textData.User = user;
                _context.Add(textData);
                await _context.SaveChangesAsync();
            return View(textData);
        }

        // GET: TextDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TextData == null)
            {
                return NotFound();
            }

            var textData = await _context.TextData.FindAsync(id);
            if (textData == null)
            {
                return NotFound();
            }
            return View(textData);
        }

        // POST: TextDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileId,FileName,Data")] TextData textData)
        {
            if (id != textData.FileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextDataExists(textData.FileId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(textData);
        }

        // GET: TextDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TextData == null)
            {
                return NotFound();
            }

            var textData = await _context.TextData
                .FirstOrDefaultAsync(m => m.FileId == id);
            if (textData == null)
            {
                return NotFound();
            }

            return View(textData);
        }

        // POST: TextDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TextData == null)
            {
                return Problem("Entity set 'TextEditorContext.TextData'  is null.");
            }
            var textData = await _context.TextData.FindAsync(id);
            if (textData != null)
            {
                _context.TextData.Remove(textData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextDataExists(int id)
        {
          return (_context.TextData?.Any(e => e.FileId == id)).GetValueOrDefault();
        }
    }
}
