<?php

namespace App\Models;

enum WorkspaceRole: string
{
    case owner = 'owner';
    case member = 'member';
    case viewer = 'viewer';
    case billing = 'billing';
}