using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TerminalApp_Server.Data;
using TerminalApp_Shared.Models;

namespace TerminalApp_Server.Controllers
{
    [Route("api/ProductResults")]
    [ApiController]
    public class ProductResultsController : ControllerBase
    {
        private readonly ProductResultsContext _context;

        public ProductResultsController(ProductResultsContext context)
        {
            _context = context;
        }

        // GET: api/ProductResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResult>>> GetProductResults()
        {
            return await _context.ProductResults
                .OrderByDescending(p => p.CreateTimestamp) // 登録日が新しい順に取得
                .ToListAsync();
        }

        // POST: api/ProductResults
        [HttpPost]
        public async Task<IActionResult> CreateProductResult([FromBody] ProductResult product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // モデルのバリデーションエラー
            }

            product.CreateTimestamp = DateTime.Now.ToString();
            product.UpdateTimestamp = DateTime.Now.ToString();

            try
            {
                _context.ProductResults.Add(product);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "登録が完了しました。" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"エラーが発生しました: {ex.Message}");
            }
        }
    }
}
