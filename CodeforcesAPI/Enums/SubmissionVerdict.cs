using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeforcesAPI
{
    public enum SubmissionVerdict
    {
        Absent = 0,
        Failed, OK, Partial, CompilationError, RuntimeError,
        WrongAnswer, PresentationError, TimeLimitExceeded,
        MemoryLimitExceeded, IdlenessLimitExceeded, SecurityViolated,
        Crashed, InputPreparationCrashed, Challenged, Skipped, Testing, Rejected
    }
}
