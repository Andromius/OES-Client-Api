using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public interface IHomeworkSubmissionAttachmentRepository : IAsyncDisposable
{
    Task SaveAttachmentAsync(IFormFile file, int submissionId);
    Task<Stream?> GetAttachmentDataAsync(int id);
}
