
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ConsoleAppCRUD.Data;
using ConsoleAppCRUD.Models;

namespace ConsoleAppCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MeuDbContext>();
                context.Database.Migrate();

                int choice;
                do
                {
                    Console.WriteLine("1. Adicionar Fornecedor");
                    Console.WriteLine("2. Adicionar Produto");
                    Console.WriteLine("3. Listar Fornecedores");
                    Console.WriteLine("4. Listar Produtos");
                    Console.WriteLine("5. Atualizar Fornecedor");
                    Console.WriteLine("6. Atualizar Produto");
                    Console.WriteLine("7. Deletar Fornecedor");
                    Console.WriteLine("8. Deletar Produto");
                    Console.WriteLine("9. Sair");
                    Console.Write("Escolha uma opção: ");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddFornecedor(context);
                            break;
                        case 2:
                            AddProduto(context);
                            break;
                        case 3:
                            ListFornecedores(context);
                            break;
                        case 4:
                            ListProdutos(context);
                            break;
                        case 5:
                            UpdateFornecedor(context);
                            break;
                        case 6:
                            UpdateProduto(context);
                            break;
                        case 7:
                            DeleteFornecedor(context);
                            break;
                        case 8:
                            DeleteProduto(context);
                            break;
                    }
                } while (choice != 9);
            }
        }

        static void AddFornecedor(MeuDbContext context)
        {
            var fornecedor = new Fornecedor();
            Console.Write("Nome Fantasia: ");
            fornecedor.NomeFantasia = Console.ReadLine();
            Console.Write("CNPJ: ");
            fornecedor.CNPJ = Console.ReadLine();
            Console.Write("Telefone: ");
            fornecedor.Telefone = Console.ReadLine();
            Console.Write("Email: ");
            fornecedor.Email = Console.ReadLine();

            context.Fornecedor.Add(fornecedor);
            context.SaveChanges();
        }

        static void AddProduto(MeuDbContext context)
        {
            var produto = new Produto();
            Console.Write("Nome: ");
            produto.Nome = Console.ReadLine();
            Console.Write("Quantidade: ");
            produto.Quantidade = int.Parse(Console.ReadLine());
            Console.Write("Preço: ");
            produto.Preco = decimal.Parse(Console.ReadLine());

            context.Produto.Add(produto);
            context.SaveChanges();
        }

        static void ListFornecedores(MeuDbContext context)
        {
            var fornecedores = context.Fornecedor.ToList();
            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"ID: {fornecedor.Id}, Nome Fantasia: {fornecedor.NomeFantasia}, CNPJ: {fornecedor.CNPJ}, Telefone: {fornecedor.Telefone}, Email: {fornecedor.Email}");
            }
        }

        static void ListProdutos(MeuDbContext context)
        {
            var produtos = context.Produto.ToList();
            foreach (var produto in produtos)
            {
                Console.WriteLine($"ID: {produto.Id}, Nome: {produto.Nome}, Quantidade: {produto.Quantidade}, Preço: {produto.Preco}");
            }
        }

        static void UpdateFornecedor(MeuDbContext context)
        {
            Console.Write("ID do Fornecedor a ser atualizado: ");
            int id = int.Parse(Console.ReadLine());
            var fornecedor = context.Fornecedor.Find(id);

            if (fornecedor != null)
            {
                Console.Write("Novo Nome Fantasia: ");
                fornecedor.NomeFantasia = Console.ReadLine();
                Console.Write("Novo CNPJ: ");
                fornecedor.CNPJ = Console.ReadLine();
                Console.Write("Novo Telefone: ");
                fornecedor.Telefone = Console.ReadLine();
                Console.Write("Novo Email: ");
                fornecedor.Email = Console.ReadLine();

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Fornecedor não encontrado.");
            }
        }

        static void UpdateProduto(MeuDbContext context)
        {
            Console.Write("ID do Produto a ser atualizado: ");
            int id = int.Parse(Console.ReadLine());
            var produto = context.Produto.Find(id);

            if (produto != null)
            {
                Console.Write("Novo Nome: ");
                produto.Nome = Console.ReadLine();
                Console.Write("Nova Quantidade: ");
                produto.Quantidade = int.Parse(Console.ReadLine());
                Console.Write("Novo Preço: ");
                produto.Preco = decimal.Parse(Console.ReadLine());

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        static void DeleteFornecedor(MeuDbContext context)
        {
            Console.Write("ID do Fornecedor a ser deletado: ");
            int id = int.Parse(Console.ReadLine());
            var fornecedor = context.Fornecedor.Find(id);

            if (fornecedor != null)
            {
                context.Fornecedor.Remove(fornecedor);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Fornecedor não encontrado.");
            }
        }

        static void DeleteProduto(MeuDbContext context)
        {
            Console.Write("ID do Produto a ser deletado: ");
            int id = int.Parse(Console.ReadLine());
            var produto = context.Produto.Find(id);

            if (produto != null)
            {
                context.Produto.Remove(produto);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<MeuDbContext>(options =>
                        options.UseSqlServer(connectionString));
                });
    }
}
