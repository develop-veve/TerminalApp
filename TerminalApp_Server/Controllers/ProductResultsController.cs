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
            try
            {
                // データを取得
                var results = await _context.ProductResults
                    .Where(p => !string.IsNullOrEmpty(p.CreateTimestamp)) // NULL または空文字を除外
                    .ToListAsync();

                // データを安全にフィルタリングして並び替え
                var filteredResults = results
                    .Where(p => !string.IsNullOrEmpty(p.CreateTimestamp) // NULL または空文字を再チェック
                                && DateTime.TryParse(p.CreateTimestamp, out _)) // 日付として有効かをチェック
                    .OrderByDescending(p => DateTime.Parse(p.CreateTimestamp)) // 並び替え
                    .ToList();

                return filteredResults;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"データ取得中にエラー: {ex.Message}");
                return StatusCode(500, "サーバーエラーが発生しました");
            }
        }


        //[HttpGet("today")]
        //public async Task<ActionResult<IEnumerable<ProductResult>>> GetTodayProductResults()
        //{
        //    var today = DateTime.Today;
        //    var startOfDay = today.AddHours(8); // 作業開始時刻を 8:00 に固定
        //    var lunchBreakStart = today.AddHours(12); // 昼休憩開始時刻
        //    var lunchBreakEnd = today.AddHours(13); // 昼休憩終了時刻
        //    var endOfDay = today.AddDays(1).AddTicks(-1);

        //    try
        //    {
        //        // 本日分のデータを取得
        //        var results = await _context.ProductResults
        //            .Where(p => !string.IsNullOrEmpty(p.CreateTimestamp)) // 空チェックを追加
        //            .ToListAsync();

        //        // 日付を安全にパースしてフィルタリング
        //        var filteredResults = results
        //            .Where(p => DateTime.TryParse(p.CreateTimestamp, out var createDate)
        //                     && createDate >= startOfDay && createDate <= endOfDay)
        //            .OrderBy(p => DateTime.Parse(p.CreateTimestamp)) // 日付順にソート
        //            .ToList();

        //        // 各レコードの稼働時間と休憩時間を計算
        //        foreach (var product in filteredResults)
        //        {
        //            if (DateTime.TryParse(product.StartTimestamp, out var startTime) &&
        //                DateTime.TryParse(product.CreateTimestamp, out var endTime))
        //            {
        //                TimeSpan workTime = TimeSpan.Zero;
        //                TimeSpan breakTime = TimeSpan.Zero;

        //                // 午前作業時間
        //                if (endTime <= lunchBreakStart)
        //                {
        //                    workTime += endTime - startTime;
        //                }
        //                // 午後作業時間
        //                else if (startTime >= lunchBreakEnd)
        //                {
        //                    workTime += endTime - startTime;
        //                }
        //                // 昼休憩をまたぐ場合
        //                else
        //                {
        //                    if (startTime < lunchBreakStart)
        //                    {
        //                        workTime += lunchBreakStart - startTime; // 午前作業時間
        //                    }
        //                    breakTime += lunchBreakEnd - lunchBreakStart; // 昼休憩
        //                    if (endTime > lunchBreakEnd)
        //                    {
        //                        workTime += endTime - lunchBreakEnd; // 午後作業時間
        //                    }
        //                }

        //                product.WorkingMinutes = (int)workTime.TotalMinutes;
        //                product.BreaktimeMinutes = (int)breakTime.TotalMinutes;
        //            }
        //        }

        //        return filteredResults;
        //    }
        //    catch (Exception ex)
        //    {
        //        // ログにエラーを記録
        //        Console.WriteLine($"Error in GetTodayProductResults: {ex.Message}");
        //        return StatusCode(500, "サーバーでエラーが発生しました");
        //    }
        //}

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<ProductSummary>>> GetProductSummaries()
        {
            try
            {
                var results = await _context.ProductResults.ToListAsync();

                var summaries = results
                    .GroupBy(p => new { p.ProductId, p.ProductName, p.ProductType, p.AdjustmentType, p.SelectedUnit })
                    .Select(g => new ProductSummary
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName,
                        ProductType = g.Key.ProductType,
                        AdjustmentType = g.Key.AdjustmentType,
                        Unit = g.Key.SelectedUnit,
                        TotalQuantity = g.Sum(x => int.TryParse(x.Quantity, out var q) ? q : 0),
                        TotalWorkingMinutes = g.Sum(x => x.WorkingMinutes),
                        TotalBreaktimeMinutes = g.Sum(x => x.BreaktimeMinutes)
                    })
                    .ToList();

                return summaries;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラー: {ex.Message}");
                return StatusCode(500, "サマリーの取得に失敗しました");
            }
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
            //product.UpdateTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
