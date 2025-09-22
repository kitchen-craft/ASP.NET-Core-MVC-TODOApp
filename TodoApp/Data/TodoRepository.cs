// namespace（名前空間）
// プロジェクト内でクラスをグループ分けするための「フォルダ名」みたいなもの。
// 実際のフォルダ構成と一致させるのが一般的。
namespace TODOApp.Data
{
    // 必要な機能を使えるようにするためのusingたち
    using System.Collections.Generic;          // List<T> などのコレクション型を使うため
    using Microsoft.Data.SqlClient;            // SQL Server に接続するためのクラス群（ADO.NET）
    using Microsoft.Extensions.Configuration;  // appsettings.json から設定値を取得するため
    using TODOApp.Models;                       // TodoItemモデルを使うため

    /// <summary>
    /// Todoテーブルにアクセスするためのクラス。
    /// 「DBに行ってデータを取ってくる係」みたいな役割。
    /// </summary>
    public class TodoRepository
    {
        // 接続文字列を保持する変数（DBに接続するための住所みたいなもの）
        private readonly string _connectionString;

        /// <summary>
        /// コンストラクタ（このクラスが作られるときに呼ばれる）
        /// IConfiguration はASP.NET Coreが自動で用意してくれる設定読み取り用のサービス。
        /// </summary>
        public TodoRepository(IConfiguration configuration)
        {
            // appsettings.json の "ConnectionStrings:DefaultConnection" を取得して変数に保存
            var connStr = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connStr))
            {
                throw new InvalidOperationException("接続文字列 'DefaultConnection' が設定されていません。");
            }
            _connectionString = connStr;
        }

        /// <summary>
        /// TodoItemsテーブルの全件を取得するメソッド。
        /// </summary>
        public List<TodoItem> GetAll()
        {
            // 取得したデータを入れておく箱（リスト）
            var todos = new List<TodoItem>();

            // SqlConnection は「DBとのパイプ」。使い終わったら閉じる必要があるので using で囲む。
            using (var conn = new SqlConnection(_connectionString))
            {
                // DBに接続開始
                conn.Open();

                // SQL文を組み立てる（StringBuilderを使うと複数行でも見やすい）
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("SELECT");
                sb.AppendLine("    Id,");
                sb.AppendLine("    Description,");
                sb.AppendLine("    IsCompleted,");
                sb.AppendLine("    DueDate");
                sb.AppendLine("FROM");
                sb.AppendLine("    TodoItems");

                // 完成したSQL文字列を取得
                string sql = sb.ToString();

                // SqlCommand は「このSQLをこの接続で実行してね」という命令書
                using (var cmd = new SqlCommand(sql, conn))
                // SqlDataReader は「結果を1行ずつ読み取る人」
                using (var reader = cmd.ExecuteReader())
                {
                    // 行がある限り読み続ける
                    while (reader.Read())
                    {
                        // 1行分のデータをTodoItemオブジェクトに詰める
                        todos.Add(new TodoItem
                        {
                            Id = reader.GetInt32(0),                // 0列目: ID
                            Description = reader.GetString(1),      // 1列目: タスク内容
                            IsCompleted = reader.GetBoolean(2),     // 2列目: 完了フラグ
                            DueDate = reader.GetDateTime(3)         // 3列目: 予定日
                        });
                    }
                }
            }

            // 集めたデータを返す
            return todos;
        }
    }
}

