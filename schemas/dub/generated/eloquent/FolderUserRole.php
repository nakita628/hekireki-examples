<?php

namespace App\Models;

enum FolderUserRole: string
{
    case owner = 'owner';
    case editor = 'editor';
    case viewer = 'viewer';
}