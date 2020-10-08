﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    /// <summary>
    /// Interface for entity classes to offer an ID.
    /// </summary>
    public interface IIdProvider
    {
        /// <summary>
        /// The ID of the entity.
        /// </summary>
        int Id { get; }
    }
}