namespace TODOApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Webアプリのビルダーを作成
            // ここで設定やサービス登録を行う
            var builder = WebApplication.CreateBuilder(args);


            // ★ TodoRepository をDIコンテナに登録
            // AddScoped = 1回のHTTPリクエスト中は同じインスタンスを使う設定
            // これでControllerのコンストラクタに TodoRepository を書くだけで自動で渡される
            builder.Services.AddScoped<TODOApp.Data.TodoRepository>();


            // MVC（Controller + View）を使えるようにするサービスを登録
            builder.Services.AddControllersWithViews();

            // アプリ本体を構築
            var app = builder.Build();

            // ★ HTTPリクエストの処理パイプラインを構成
            // 開発環境でない場合のエラーハンドリング設定
            if (!app.Environment.IsDevelopment())
            {
                // エラー時に /Home/Error に飛ばす
                app.UseExceptionHandler("/Home/Error");

                // HSTS（HTTP Strict Transport Security）を有効化
                // ブラウザに「このサイトは常にHTTPSでアクセスする」と覚えさせる
                app.UseHsts();
            }

            // HTTP → HTTPS へのリダイレクト
            app.UseHttpsRedirection();

            // wwwroot フォルダの静的ファイル（CSS, JS, 画像など）を配信
            app.UseStaticFiles();

            // ルーティングを有効化
            app.UseRouting();

            // 認可（Authorization）を有効化
            app.UseAuthorization();

            // ★ ルート（URLパターン）の設定
            // 例: /Todo/Index → TodoController の Index アクション
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // アプリを実行（ここから待ち受け開始）
            app.Run();
        }
    }
}

