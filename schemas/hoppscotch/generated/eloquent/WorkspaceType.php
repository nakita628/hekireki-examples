<?php

namespace App\Models;

enum WorkspaceType: string
{
    case USER = 'USER';
    case TEAM = 'TEAM';
}