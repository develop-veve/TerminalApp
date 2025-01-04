namespace TerminalApp_Shared.Models
{
    public class ProductResult
    {
        // プライマリーキー
        public int Id { get; set; }

        // 生産品情報
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string AdjustmentType { get; set; } = string.Empty;

        // 生産数量および単位
        public string Quantity { get; set; } = "0";
        public string SelectedUnit { get; set; } = "pcs";

        //作業開始時間
        public string StartTimestamp { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 作成時刻(作業完了時間)・作成者
        public string CreateTimestamp { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string CreateUserID { get; set; } = string.Empty;
        public string CreateUserName { get; set; } = string.Empty;

        // 更新時刻・更新者
        public string UpdateTimestamp { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string UpdateUserID { get; set; } = string.Empty;
        public string UpdateUserName { get; set; } = string.Empty;

        // 稼働時間および休憩時間
        public int WorkingMinutes { get; set; } = 0; // 作業時間（分単位）
        public int BreaktimeMinutes { get; set; } = 0; // 休憩時間（分単位）

        // 備考（メモなど）
        public string Note { get; set; } = string.Empty;
    }
}
