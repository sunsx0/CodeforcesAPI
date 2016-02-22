using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeforcesAPI
{
    public enum HackVerdict
    {
        Absent = 0,
        HackSuccessful, HackUnsuccessful, InvalidInput, GeneratorIncompilable, GeneratorCrashed, Ignored, Testing, Other
    }
}
