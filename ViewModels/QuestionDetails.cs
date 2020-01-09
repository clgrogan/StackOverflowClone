using System.Collections.Generic;
using System;

namespace StackOverflowClone.ViewModels
{
  public class QuestionDetails
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string QuestionBody { get; set; }
    public int VoteScore { get; set; }
    public DateTime TimeStamp { get; set; }

    // Foreign List used for all answer records associated with the Question
    public List<CreatedAnswer> Answers { get; set; } = new List<CreatedAnswer>();
  }
}