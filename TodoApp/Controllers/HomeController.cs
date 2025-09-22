// エラー処理やログに使う機能たちを読み込む
// ASP.NET Core MVC の基本機能（Controllerなど）を使うための名前空間
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Models;
// DBアクセス用のリポジトリを使うため
using TODOApp.Data;
// View に渡すデータモデルの定義（今回は Error 表示用）
using TODOApp.Models;

namespace TODOApp.Controllers
{
    public class HomeController : Controller
    {
        // ログを記録するための仕組み（実行中の情報や例外を残したりできる）
        private readonly ILogger<HomeController> _logger;

        // ★ DBからTODOデータを取ってくる係（TodoRepository）
        private readonly TodoRepository _repository;

        // コンストラクター（クラスが呼ばれたときに一度だけ実行される初期設定）
        // ILogger と TodoRepository は Program.cs で登録してあるので自動で渡される（DI）
        public HomeController(ILogger<HomeController> logger, TodoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // 👀 "/Home/Index" にアクセスされたときに呼ばれるメソッド
        // 画面（View）として Views/Home/Index.cshtml を表示するよ！
        public IActionResult Index()
        {
            // ★ 仮データではなく、DBから全件取得
            var todoList = _repository.GetAll();

            // 取得したTODOリストをビューに渡して表示する
            return View(todoList);
        }

        // 🔐 "/Home/Privacy" にアクセスされたときに呼ばれるメソッド
        // プライバシーポリシーなどを表示する画面（デフォルトで用意されてるやつ）
        public IActionResult Privacy()
        {
            return View();
        }

        // ⚠️ もしアプリ内でエラーが発生したら、このメソッドでエラーページを表示する
        // ResponseCache 属性で「この画面はキャッシュしないで！」という指定をしてる
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // エラー発生時に必要な情報（RequestId）を View に渡して表示
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

