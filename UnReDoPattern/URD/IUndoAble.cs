
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URD
{
    public interface IUndoAble
    {
        void Undo();
        void Redo();

    }
}
