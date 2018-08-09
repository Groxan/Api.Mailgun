namespace Api.Mailgun
{
    /// <summary>
    /// Defines how bool will be converted to string
    /// </summary>
    enum BoolModes
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
