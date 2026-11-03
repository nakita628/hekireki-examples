<?php

namespace App\Models;

enum WorkspaceRole: string
{
    case ADMIN = 'ADMIN';
    case MEMBER = 'MEMBER';
    case GUEST = 'GUEST';
}