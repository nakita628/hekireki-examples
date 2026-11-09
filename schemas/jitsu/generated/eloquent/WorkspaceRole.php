<?php

namespace App\Models;

enum WorkspaceRole: string
{
    case owner = 'owner';
    case editor = 'editor';
    case analyst = 'analyst';
}