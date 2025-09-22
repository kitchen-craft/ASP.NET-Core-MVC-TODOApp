namespace TODOApp.Models  // 名前空間（プロジェクト内のフォルダ分けのようなもの）
{
    public class TodoItem  // クラス名（1つのタスクを表す）
    {
        // プロパティ（タスクが持つ情報）
        public int Id { get; set; }               // 一意のID
        public bool IsCompleted { get; set; }     // 完了フラグ
        public DateTime DueDate { get; set; }     // 予定日
        public string Description { get; set; }   // タスク内容
    }
}

