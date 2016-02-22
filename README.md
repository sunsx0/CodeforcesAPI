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

## Analyze contest submissions
    static void Main()
    {
        var api = new Codeforces(Langs.EN);
        var contest = api.Contest.List(false).Result.Where(x => x.Id == 629).First();
        var submissions = api.Contest.Status(629).Result;
        var contestSubmissions = submissions.Where(x => x.CreationTime >= contest.StartTime && x.CreationTime <= contest.StartTime.AddSeconds(contest.DurationSeconds));
        var problems = new Dictionary<string, List<Submission>>();
        foreach(var submission in contestSubmissions)
        {
            List<Submission> subs;
            if (!problems.TryGetValue(submission.Problem.Index, out subs))
            {
                subs = new List<Submission>();
                problems[submission.Problem.Index] = subs;
            }
            subs.Add(submission);
        }
        foreach(var problem in problems.OrderBy(x => x.Key))
        {
            var count = problem.Value.Count;
            var accepted = problem.Value.Where(x => x.Verdict == SubmissionVerdict.OK).Count();
            Console.WriteLine("{0}: {1} {2}", problem.Key, count, accepted);
        }
    }
