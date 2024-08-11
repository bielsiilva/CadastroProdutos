using System.Globalization;
using TarefaA2.Classes;

namespace TarefaA2
{
    class Program
    {
        static List<Cadastro> listaCadastro = new List<Cadastro>();
        static void Main()
        {
            //Fazendo a autenticação para o usuário, consegui fazer o plano do arquivo TXT e esconder a senha com *, minha fonte foi o próprio 
            //GitHub, peguei algumas dicas de diversos repositórios. É necessário que haja um arquivo TXT com a senha salva no PC.
            //Aqui utilizei a estrutura de repetição while e um contador de incremento para as chances de tentativas (3).
            //A única coisa que fiquei curioso depois foi como fazer para o usuário apagar um caractere, caso ele erre.
            string sourcePath = @"C:\AulaCC\password.txt";
            int tentativas = 3;

            while (tentativas > 0)
            {
                Console.Write("Usuário: ");
                string user = Console.ReadLine();
                Console.Write("Senha: ");
                string senha = "";
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    senha += key.KeyChar;
                    Console.Write("*");
                }
                Console.WriteLine();

                tentativas--;
                string[] lines = File.ReadAllLines(sourcePath); // Utilizei essa dica da aula sobre try catch semanas atrás, o foreach lê a linha e compara com o que o usuário digitou.
                foreach (string line in lines)
                {
                    if (line == senha)
                    {
                        Console.WriteLine("Acesso liberado!");
                        Console.WriteLine($"Bem vindo! {user}");
                        Console.WriteLine();
                        Menu();
                    }
                    else if (tentativas > 0)
                    {
                        Console.WriteLine($"Senha incorreta. Você tem mais {tentativas} tentivas.");
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta. Você não tem mais tentivas.");
                        break;
                    }
                }
            }
        }
        static void Menu()
        {
            //Parte de menu, com estrutura de repetição do while e switch case.
            //Utilizei o EnvironmentExit como uma saída de fechamento direto, pois de outras maneiras o meu programa estava dando um erro.
            while (true)
            {
                int selecao;
                do
                {
                    Console.WriteLine("Opções:");
                    Console.WriteLine("1 - Cadastrar produto(s)");
                    Console.WriteLine("2 - Visualizar produto(s)");
                    Console.WriteLine("3 - Finalizar programa");
                    Console.WriteLine();
                    Console.WriteLine("O que deseja fazer?");
                    selecao = int.Parse(Console.ReadLine());

                    switch (selecao)
                    {
                        case 1:
                            Cadastrar();
                            break;

                        case 2:
                            Visualizar();
                            break;
                        case 3:
                            Console.WriteLine("Programa finalizado.");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }
                }
                while (selecao < 1 || selecao > 3);
            }

        }
        static void Cadastrar()
        {
            string continuar;
            int contador = 0;

            //Utilizei as estrutura do while para fazer um loop caso o usuário deseje continuar cadastrando
            //Também utilizei o try catch para evitar futuros erros, além do else if caso o usuário digite algo diferente de "s" ou "n"
            //No final, há o resultado da lista, que foi utilizada também na parte de classes.
            do
            {
                try
                {
                    Console.WriteLine("Entre com os dados: ");
                    Console.WriteLine();
                    Console.Write("ID: ");
                    int? id = int.Parse(Console.ReadLine());

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    Console.Write("Preço: ");
                    double? preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    if (id == null || string.IsNullOrWhiteSpace(nome) || preco == null)
                    {
                        do
                        {
                            Console.WriteLine("Produto não cadastrado! Deseja cadastrar outro produto? (s/n):");
                            continuar = Console.ReadLine();
                            if (continuar != "n" && continuar != "s")
                            {
                                Console.WriteLine("Por favor, digite 's' ou 'n'.");
                            }
                        }
                        while (continuar != "n" && continuar != "s");

                    }
                    else
                    {
                        listaCadastro.Add(new Cadastro { ID = id, Nome = nome, Valor = preco });
                        contador++;
                        Console.WriteLine();
                        Console.WriteLine("Produto cadastrado com sucesso!");
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Erro.");
                }
                do
                {
                    Console.WriteLine("Deseja cadastrar outro produto? Digite 's' para SIM ou 'n' para NÃO.");
                    continuar = Console.ReadLine();
                    if (continuar != "n" && continuar != "s")
                    {
                        Console.WriteLine("Por favor, digite 's' para SIM ou 'n' para NÃO.");
                    }
                }
                while (continuar != "n" && continuar != "s");
            }
            while (continuar == "s");

        }
        //Por fim, há o foreach no final para demonstar a visualização de cadastros feitos. Também utilizei as classes e o F2 para limitar as casas decimais.
        //O ReadKey é utilizado para garantir o retorno ao menu
        static void Visualizar()
        {
            foreach (var item in listaCadastro)
            {
                Console.WriteLine($"ID: {item.ID}, Nome: {item.Nome}, Preço: {item.Valor:F2}");
                Console.WriteLine();
            }
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            Menu();
        }
    }
}
