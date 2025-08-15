using System;
using System.Collections.Generic;

// a) Record for Transaction
public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

// b) Interface
public interface ITransactionProcessor
{
    void Process(Transaction transaction);
    }

    // c) Concrete processors
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
            {
                    Console.WriteLine($"[BankTransfer] Processing {transaction.Category} of {transaction.Amount:C} on {transaction.Date:d}.");
                        }
                        }

                        public class MobileMoneyProcessor : ITransactionProcessor
                        {
                            public void Process(Transaction transaction)
                                {
                                        Console.WriteLine($"[MobileMoney] Processing {transaction.Category} of {transaction.Amount:C} on {transaction.Date:d}.");
                                            }
                                            }

                                            public class CryptoWalletProcessor : ITransactionProcessor
                                            {
                                                public void Process(Transaction transaction)
                                                    {
                                                            Console.WriteLine($"[CryptoWallet] Processing {transaction.Category} of {transaction.Amount:C} on {transaction.Date:d}.");
                                                                }
                                                                }

                                                                // d) Base Account
                                                                public class Account
                                                                {
                                                                    public string AccountNumber { get; }
                                                                        public decimal Balance { get; protected set; }

                                                                            public Account(string accountNumber, decimal initialBalance)
                                                                                {
                                                                                        AccountNumber = accountNumber;
                                                                                                Balance = initialBalance;
                                                                                                    }

                                                                                                        public virtual void ApplyTransaction(Transaction transaction)
                                                                                                            {
                                                                                                                    Balance -= transaction.Amount;
                                                                                                                        }
                                                                                                                        }

                                                                                                                        // e) Sealed SavingsAccount
                                                                                                                        public sealed class SavingsAccount : Account
                                                                                                                        {
                                                                                                                            public SavingsAccount(string accountNumber, decimal initialBalance) 
                                                                                                                                    : base(accountNumber, initialBalance) { }

                                                                                                                                        public override void ApplyTransaction(Transaction transaction)
                                                                                                                                            {
                                                                                                                                                    if (transaction.Amount > Balance)
                                                                                                                                                            {
                                                                                                                                                                        Console.WriteLine("Insufficient funds");
                                                                                                                                                                                    return;
                                                                                                                                                                                            }

                                                                                                                                                                                                    Balance -= transaction.Amount;
                                                                                                                                                                                                            Console.WriteLine($"Deducted {transaction.Amount:C}. New Balance: {Balance:C}");
                                                                                                                                                                                                                }
                                                                                                                                                                                                                }

                                                                                                                                                                                                                // f) FinanceApp
                                                                                                                                                                                                                public class FinanceApp
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    private readonly List<Transaction> _transactions = new();

                                                                                                                                                                                                                        public void Run()
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                    var account = new SavingsAccount("ACC-001", 1000m);

                                                                                                                                                                                                                                            var t1 = new Transaction(1, DateTime.Today, 150m, "Groceries");
                                                                                                                                                                                                                                                    var t2 = new Transaction(2, DateTime.Today, 300m, "Utilities");
                                                                                                                                                                                                                                                            var t3 = new Transaction(3, DateTime.Today, 700m, "Entertainment");

                                                                                                                                                                                                                                                                    ITransactionProcessor p1 = new MobileMoneyProcessor();   // t1
                                                                                                                                                                                                                                                                            ITransactionProcessor p2 = new BankTransferProcessor();  // t2
                                                                                                                                                                                                                                                                                    ITransactionProcessor p3 = new CryptoWalletProcessor();  // t3

                                                                                                                                                                                                                                                                                            p1.Process(t1);
                                                                                                                                                                                                                                                                                                    account.ApplyTransaction(t1);

                                                                                                                                                                                                                                                                                                            p2.Process(t2);
                                                                                                                                                                                                                                                                                                                    account.ApplyTransaction(t2);

                                                                                                                                                                                                                                                                                                                            p3.Process(t3);
                                                                                                                                                                                                                                                                                                                                    account.ApplyTransaction(t3);

                                                                                                                                                                                                                                                                                                                                            _transactions.AddRange(new[] { t1, t2, t3 });

                                                                                                                                                                                                                                                                                                                                                    Console.WriteLine("\nAll transactions recorded:");
                                                                                                                                                                                                                                                                                                                                                            foreach (var t in _transactions)
                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                Console.WriteLine($"#{t.Id} {t.Category} {t.Amount:C} on {t.Date:d}");
                                                                                                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                                                                                                            }

                                                                                                                                                                                                                                                                                                                                                                                            public class Program
                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                public static void Main()
                                                                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                                                                            new FinanceApp().Run();
                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                