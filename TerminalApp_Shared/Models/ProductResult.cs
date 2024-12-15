namespace TerminalApp_Shared.Models
{
    public class ProductResult
    {
        public int Id { get; set; } // ID (プライマリキー)
        public string ProductId { get; set; } = string.Empty; // 生産品ID
        public string ProductName { get; set; } = string.Empty; // 生産品名
        public string ProductType { get; set; } = string.Empty; // 製品タイプ (製品、半製品、部品)
        public string AdjustmentType { get; set; } = string.Empty; // 増減タイプ (仕入、製造、組立)
        public decimal Quantity { get; set; } // 数量
        public DateTime CreateTimestamp { get; set; } = DateTime.Now; // 登録日
        public DateTime UpdateTimestamp { get; set; } = DateTime.Now; // 更新日
    }
}
