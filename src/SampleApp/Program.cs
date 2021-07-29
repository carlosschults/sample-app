using Octokit;
using System;
using System.Threading.Tasks;
using static System.Console;
using System.Linq;

namespace SampleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteLine("Please type the username for the desired user:");
            var username = ReadLine();

            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));

            try
            {
                var user = await github.User.Get(username);
                WriteLine($"The user {user.Name} was succesfully retrieved!");
                WriteLine($"{user.Name} has {user.PublicRepos} public repositories. Do you want to see the list? (y/n)");
                var response = ReadLine();

                if (string.Equals(
                    "Y",
                    response,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    var repos = await github.Repository.GetAllForUser(username);
                    foreach (var repo in repos.OrderBy(x => x.CreatedAt))
                    {
                        WriteLine($"{repo.CreatedAt:yyyy-MM-dd} | {repo.Name}");
                    }
                }
            }
            catch (NotFoundException)
            {
                WriteLine($"{username} isn't a valid GitHub user!");
            }

            WriteLine("Press any key to exit.");
            ReadLine();
        }
    }
}
