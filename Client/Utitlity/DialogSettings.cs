﻿using MudBlazor;

namespace MES.Client.Utitlity
{
    public static class DialogSettings
    {
        public static DialogOptions DialogOptionsAddEditDelete = new DialogOptions()
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = true,
            CloseOnEscapeKey = false,
            Position = DialogPosition.Center
        };

    }
}
