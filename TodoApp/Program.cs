namespace TODOApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Web�A�v���̃r���_�[���쐬
            // �����Őݒ��T�[�r�X�o�^���s��
            var builder = WebApplication.CreateBuilder(args);


            // �� TodoRepository ��DI�R���e�i�ɓo�^
            // AddScoped = 1���HTTP���N�G�X�g���͓����C���X�^���X���g���ݒ�
            // �����Controller�̃R���X�g���N�^�� TodoRepository �����������Ŏ����œn�����
            builder.Services.AddScoped<TODOApp.Data.TodoRepository>();


            // MVC�iController + View�j���g����悤�ɂ���T�[�r�X��o�^
            builder.Services.AddControllersWithViews();

            // �A�v���{�̂��\�z
            var app = builder.Build();

            // �� HTTP���N�G�X�g�̏����p�C�v���C�����\��
            // �J�����łȂ��ꍇ�̃G���[�n���h�����O�ݒ�
            if (!app.Environment.IsDevelopment())
            {
                // �G���[���� /Home/Error �ɔ�΂�
                app.UseExceptionHandler("/Home/Error");

                // HSTS�iHTTP Strict Transport Security�j��L����
                // �u���E�U�Ɂu���̃T�C�g�͏��HTTPS�ŃA�N�Z�X����v�Ɗo��������
                app.UseHsts();
            }

            // HTTP �� HTTPS �ւ̃��_�C���N�g
            app.UseHttpsRedirection();

            // wwwroot �t�H���_�̐ÓI�t�@�C���iCSS, JS, �摜�Ȃǁj��z�M
            app.UseStaticFiles();

            // ���[�e�B���O��L����
            app.UseRouting();

            // �F�iAuthorization�j��L����
            app.UseAuthorization();

            // �� ���[�g�iURL�p�^�[���j�̐ݒ�
            // ��: /Todo/Index �� TodoController �� Index �A�N�V����
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // �A�v�������s�i��������҂��󂯊J�n�j
            app.Run();
        }
    }
}

