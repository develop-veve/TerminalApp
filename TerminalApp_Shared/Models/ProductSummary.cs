namespace TerminalApp_Shared.Models
{
    public class ProductSummary
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string AdjustmentType { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public int TotalWorkingMinutes { get; set; }
        public int TotalBreaktimeMinutes { get; set; }
    }
}
