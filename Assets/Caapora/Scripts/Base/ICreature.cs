using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caapora
{
    public interface ICreature
    {

        IEnumerator CharacterHit();
        void Die();
    }
}
