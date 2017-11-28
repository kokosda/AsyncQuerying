using AsyncQuerying.Data.Queries.DelayedQuery;
using Microsoft.Practices.ServiceLocation;
using System;

namespace AsyncQuerying.WebConsole
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            new AsyncQuerying.Infrastructure.CastleWindsor.Initializer().Register();

            Console.WriteLine("{0} started {1}", typeof(Program).Name, Environment.NewLine);

            try
            {
                DelayingQueries();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("{0}End of program. Type to close", Environment.NewLine);
            Console.ReadKey();
        }

        #region private

        private static void DelayingQueries()
        {
            var uq = ServiceLocator.Current.GetInstance<AsyncQuerying.Data.Queries.DelayedQuery.Executors.Abstract.IUsersQueryExecutor>();
            var filterModel = new AsyncQuerying.Data.Models.Users.UserFilterModel
                                    {
                                        Name = "zak"
                                    };

            var r = uq.UsersListByFilter(filterModel);

            Console.WriteLine();

            while (true)
            {
                WriteToPreviousLine("Delayed query state: {0}", r.State.ToString());

                if (r.State == QueryStatesEnum.Failed || r.State == QueryStatesEnum.Completed)
                {
                    WriteToPreviousLine("Delayed query state: {0}", r.State.ToString());

                    break;
                }
            }
        }

        private static void WriteToPreviousLine(String format, params Object[] arg)
        {
            var currentLineCursor = Console.CursorTop;

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(format, arg);
        }

        #endregion
    }
}
