﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Tests.Answers;
public record AnswerResponse(int Id, string Text, int QuestionId, double? SimilarityPercentage);
