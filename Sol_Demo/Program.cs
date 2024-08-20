Console.WriteLine("Hello, World!");

var bankObj = new Bank();

await bankObj.Deposit(100);
await bankObj.Withdrawal(50);

await bankObj.Deposit(200);
await bankObj.Withdrawal(100);

public class Bank{
    private static readonly SemaphoreSlim _lock = new SemaphoreSlim(initialCount:1,maxCount:1);

    private decimal _balance = 0;

    public async Task Deposit(decimal amount){
        await _lock.WaitAsync();
        try{
            _balance += amount;
            Console.WriteLine($"Balance: {_balance}");
        }
        finally{
            _lock.Release();
            Console.WriteLine("Deposit Lock Released");
        }
    }

    public async Task Withdrawal (decimal amount){
        await _lock.WaitAsync();
        try{
            _balance -= amount;
            Console.WriteLine($"Balance: {_balance}");
        }
        finally{
            _lock.Release();
            Console.WriteLine("Withdrawal Lock Released");
        }
    }
}