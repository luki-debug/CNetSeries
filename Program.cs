using System;

namespace NetSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            TelaPadrao();
            string opcaoUsuario = ObterOpcaoUsuario();
            Console.Clear();

            while (opcaoUsuario.ToUpper() != "X")
            {
                TelaPadrao();
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;

                    default:
                        Console.WriteLine("Opção invalida! Digite uma opção válida");
                        Console.WriteLine();
                        Console.WriteLine("Pressione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }

                Console.Clear();
                TelaPadrao();
                opcaoUsuario = ObterOpcaoUsuario();
                Console.Clear();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void TelaPadrao()
        {
            Console.WriteLine();
            Console.WriteLine("==============================\n");
            Console.WriteLine("         Lumia Séries         \n");
            Console.WriteLine("Data: " + DateTime.Now.ToString("dd/MM/yyyy") + "  Horas: " + DateTime.Now.ToShortTimeString());
            Console.WriteLine("==============================\n");
        }
        private static void ExcluirSerie()
        {
            try
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                repositorio.Exclui(indiceSerie);
                Console.WriteLine("Item de Id:{0}, excluido com sucesso !", indiceSerie);
                Console.Write("Pressione Enter para continuar...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Erro. Não foi possivel excluir o item. Detahes: " + e.Message);
                Console.WriteLine();
                Console.Write("Pressione Enter para continuar...");
                Console.ReadLine();
            }
        }
        private static void VisualizarSerie()
        {
            try
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                var serie = repositorio.RetornaPorId(indiceSerie);
                Console.WriteLine(serie);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Erro. Não foi possivel visualizar o item. Detahes: " + e.Message);
                Console.WriteLine();
            }

            Console.Write("Pressione Enter para continuar...");
            Console.ReadLine();
        }

        private static void AtualizarSerie()
        {
            try
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Serie atualizaSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Atualiza(indiceSerie, atualizaSerie);
                Console.WriteLine("Item atualizado com sucesso !");
                Console.Write("Pressione Enter para continuar...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Erro. Não foi possivel atualizar o item. Detahes: " + e.Message);
                Console.WriteLine();
                Console.Write("Pressione Enter para continuar...");
                Console.ReadLine();
            }
        }
        private static void ListarSeries()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("*       Listar séries        *");
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                Console.Write("Pressione Enter para continuar...");
                Console.ReadLine();
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }

            Console.Write("Pressione Enter para continuar...");
            Console.ReadLine();
        }

        private static void InserirSerie()
        {
            try
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("*     Inserir nova série     *");
                Console.WriteLine("------------------------------");
                Console.WriteLine();

                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
                // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
                foreach (int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Insere(novaSerie);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Erro. Não foi possivel inserir o item. Detahes: " + e.Message);
                Console.WriteLine();
                Console.Write("Pressione Enter para continuar...");
                Console.ReadLine();
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("*     Informe uma função     *");
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("X- Sair");
            Console.Write(":");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
