﻿using Domain.Entities.Courses;
using Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities.Quizzes;
public record QuizResponse(string Type,
                           int Id,
                           string Name,
                           DateTime Created,
                           int CreatedById,
                           bool IsVisible,
                           DateTime Scheduled,
                           DateTime End,
                           List<QuestionResponse> Questions)
    : CourseItemResponse(Type, Id, Name, Created, CreatedById, IsVisible);
