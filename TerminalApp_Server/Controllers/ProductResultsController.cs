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

        // コンストラクタでデータベースコンテキストを注入
        public ProductResultsController(ProductResultsContext context)
        {
            _context = context;
        }

        // GET: api/ProductResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResult>>> GetProductResults()
        {
            // 登録日が新しい順にすべての結果を取得
            return await _context.ProductResults
                .OrderByDescending(p => p.CreateTimestamp)
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

            // 登録日時と更新日時を設定
            product.CreateTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            product.UpdateTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                // データベースに新しい結果を追加
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
