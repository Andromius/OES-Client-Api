using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Homeworks;
public class HomeworkSubmission
{
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Homework Homework { get; set; }
    public int HomeworkId { get; set; }
    public string? Text { get; set; }
    public string? Comment { get; set; }
    public List<HomeworkSubmissionAttachment> Attachments { get; set; } = new();
    public HomeworkSubmission(int userId, int homeworkId, string? text)
    {
        UserId = userId;
        HomeworkId = homeworkId;
        Text = text;
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private HomeworkSubmission() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
