using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoriesRepository _categoriesRepository;

        public ReportsController(ApplicationDbContext db)
        {
            _db = db;
            _categoriesRepository = new CategoriesRepository(_db);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateReport()
        {
            // Retrieve data from the database or other sources
            var data = _categoriesRepository.GetAllCategories();

            // Create a PDF document
            var memoryStream = new MemoryStream();
            var pdfWriter = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            // Add data to the PDF document
            foreach (var category in data)
            {
                document.Add(new Paragraph($"Category ID: {category.CategoryId}, Name: {category.Name}, Description: {category.Description}"));
            }

            // Close the document
            document.Close();

            // Return the PDF file to the user for download
            return File(memoryStream.ToArray(), "application/pdf", "report.pdf");
        }
    }
}
