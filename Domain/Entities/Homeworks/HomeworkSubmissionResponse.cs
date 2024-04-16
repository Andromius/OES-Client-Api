using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public record HomeworkSubmissionResponse(int Id, string? Text, string? Comment, List<HomeworkSubmissionAttachmentResponse> Attachments);
