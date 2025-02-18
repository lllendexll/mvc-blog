using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcBlog.Models;
using System.Linq;
using System.Threading.Tasks;

//[Authorize] // Ensures only logged-in users can access
public class ArticlesController : Controller
{
    private readonly BlogContext _context;

    public ArticlesController(BlogContext context)
    {
        _context = context;
    }

    // GET: Articles
    public async Task<IActionResult> Index()
    {
        return View(await _context.Articles.ToListAsync());
    }

    // GET: Articles/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var article = await _context.Articles.FirstOrDefaultAsync(m => m.ArticleId == id);
        if (article == null) return NotFound();

        return View(article);
    }

    // GET: Articles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Articles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ArticleId,Title,Body,StartDate,EndDate,ContributorUsername")] Article article)
    {
        if (ModelState.IsValid)
        {
            _context.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    // GET: Articles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var article = await _context.Articles.FindAsync(id);
        if (article == null) return NotFound();

        return View(article);
    }

    // POST: Articles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,Body,StartDate,EndDate,ContributorUsername")] Article article)
    {
        if (id != article.ArticleId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // ✅ Redirect to articles list
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Articles.Any(e => e.ArticleId == article.ArticleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return View(article);
    }

    // GET: Articles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var article = await _context.Articles.FirstOrDefaultAsync(m => m.ArticleId == id);
        if (article == null) return NotFound();

        return View(article);
    }

    // POST: Articles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article != null) _context.Articles.Remove(article);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
