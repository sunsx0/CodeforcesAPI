# CodeforcesAPI
Codeforces API declaration for .NET

# Examples
## List active users
    static void Main()
    {
        var api = new Codeforces(Langs.EN);
        foreach (var user in api.User.RatedList(true).Result)
        {
            Console.WriteLine("{0} - {1} ({2})", user.Handle, user.Rating, user.Rank);
        }
    }

## List unfinished contests
    static void Main()
    {
        var api = new Codeforces(Langs.EN);
        foreach (var contest in api.Contest.List().Result.Where(x => x.Phase != ContestPhase.Finished))
        {
            Console.WriteLine("{0} - {1}", contest.StartTime, contest.Name);
        }
    }
    
## Auth with apikey
    static void Main()
    {
        var apiKey = new ApiKey("MYKEY", "MYSECRET");
        var api = new Codeforces(Langs.EN, apiKey);
        foreach (var contest in api.Contest.List(true).Result.Where(x => x.Phase != ContestPhase.Finished))
        {
            Console.WriteLine("{0} - {1}", contest.StartTime, contest.Name);
        }
    }
