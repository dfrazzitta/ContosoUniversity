using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling;

namespace ContosoUniversity.Pages.Courses;

public class DetailsModel : PageModel
{
    private readonly SchoolContext _context;

    public DetailsModel(SchoolContext context)
    {
        _context = context;
    }

    public Course Course { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
         
  
        if (id == null)
        {
            return NotFound();
        }

        using (MiniProfiler.Current.Step("Load Posts & Find Specific Post"))
        {
            //post = DataHelper.GetAllPosts().Find(p => p.Id.Equals(id));
            Course = await _context.Courses
                     .Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseID == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }


        /*
        Course = await _context.Courses
            .Include(c => c.Department).FirstOrDefaultAsync(m => m.CourseID == id);

        if (Course == null)
        {
            return NotFound();
        }
        return Page(); */
    }
 
}
