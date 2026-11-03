<?php

namespace App\Models;

enum FolderAccessLevel: string
{
    case read = 'read';
    case write = 'write';
}