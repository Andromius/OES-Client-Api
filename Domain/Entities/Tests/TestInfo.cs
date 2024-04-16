using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests;
public record TestInfo(string Name, DateTime End, int Duration, int MaxAttempts, bool HasPassword, IEnumerable<TestAttempt> Attempts);
public record TestAttempt(int Points, string Status, DateTime Submitted);