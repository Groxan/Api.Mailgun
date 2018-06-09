﻿using System;

namespace Api.Mailgun.Http
{
    /// <summary>
    /// Defines how bool will be converted to string
    /// </summary>
    public enum BoolModes
    {
        /// <summary>
        /// Convert true to 'True', false to 'False'
        /// </summary>
        TrueFalse,

        /// <summary>
        /// Convert true to 'yes', false to 'no'
        /// </summary>
        YesNo,
    }
}