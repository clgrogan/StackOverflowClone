using System;

namespace StackOverflowClone.ViewModels
{
  public class CreatedAnswer
  {
    public int ID { get; set; }
    public string AnswerBody { get; set; }
    public int VoteScore { get; set; }
    public DateTime TimeStamp { get; set; }
    // QuestionId is the foreign Key and the 
    public int QuestionId { get; set; }
    // public Question Question { get; set; } removes question object/cyclic reference
  }
}