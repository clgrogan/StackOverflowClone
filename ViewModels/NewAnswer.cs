using System;

namespace StackOverflowClone.ViewModels
{
  public class NewAnswer
  {
    public int ID { get; set; }
    public string AnswerBody { get; set; }
    // public int VoteScore { get; set; }
    // public DateTime TimeStamp { get; set; }

    // QuestionId is the foreign Key and the 
    // Question is the reference to the record in Question
    public int QuestionId { get; set; }
    // public Question Question { get; set; }
  }
}