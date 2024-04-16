using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public class HomeworkSubmissionAttachment
{
    public int Id { get; set; }
    public HomeworkSubmission Submission { get; set; }
    public int SubmissionId { get; set; }
    public string FileName { get; set; }
    public byte[] File { get; set; }
}
