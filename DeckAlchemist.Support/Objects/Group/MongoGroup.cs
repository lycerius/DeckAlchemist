<<<<<<< HEAD:DeckAlchemist.Api/Objects/Group/MongoGroup.cs
﻿using System;
using System.Collections;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.User;

namespace DeckAlchemist.Api.Objects.Group
=======
﻿namespace DeckAlchemist.Support.Objects.Group
>>>>>>> devel:DeckAlchemist.Support/Objects/Group/MongoGroup.cs
{
    public class MongoGroup : IGroup
    {
        public string name { get; set; }
        public string owner { get; set; }
        public string game { get; set; }
        public IEnumerable<IUser> members { get; set; }
    }
}
