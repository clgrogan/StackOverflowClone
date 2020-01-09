using System;

namespace StackOverflowClone.ViewModels
{
  public class NewAnswer
  {
    public int ID { get; set; }
    public string AnswerBody { get; set; }
    public int QuestionId { get; set; }
  }
}