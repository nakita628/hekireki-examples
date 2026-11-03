<?php

namespace App\Models;

enum RootItemAccess: string
{
    case VIEW_ONLY = 'VIEW_ONLY';
    case VIEW_AND_DOWNLOAD = 'VIEW_AND_DOWNLOAD';
    case HIDDEN = 'HIDDEN';
}