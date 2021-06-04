using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Press.GameComponents
{
    public class GameScene
    {
        public string SourceData { get; set; }

        private Queue<string> _actions = new Queue<string>();

        private Queue<string> GetActionsFromSourceData()
        {
            var actions = new Queue<string>();
            var actionsArray = SourceData.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var action in actionsArray)
                actions.Enqueue(action);

            return actions;
        }
    }
}
