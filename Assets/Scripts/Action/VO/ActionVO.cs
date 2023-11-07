using System;

namespace Action.VO
{
    public class ActionVO
    {
        public ActionVO(){}

        public ActionVO(long id, string title, string content, bool isCompleted, string createDate, string doDate)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.isCompleted = isCompleted;
            this.createDate = Convert.ToDateTime(createDate);
            this.doDate = Convert.ToDateTime(doDate);
        }
        public long id { get; }
        public string title { get; }
        public string content { get; }
        public bool isCompleted { get; }
        public DateTime createDate { get; }
        public DateTime doDate { get; }

    }

}