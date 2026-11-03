<?php

namespace App\Models;

enum LinkScope: string
{
    case workspace = 'workspace';
    case folders = 'folders';
    case links = 'links';
}