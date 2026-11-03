<?php

namespace App\Models;

enum DefaultPermissionStrategy: string
{
    case INHERIT_FROM_PARENT = 'INHERIT_FROM_PARENT';
    case ASK_EVERY_TIME = 'ASK_EVERY_TIME';
    case HIDDEN_BY_DEFAULT = 'HIDDEN_BY_DEFAULT';
}