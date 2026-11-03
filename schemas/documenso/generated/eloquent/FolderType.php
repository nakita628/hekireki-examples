<?php

namespace App\Models;

enum FolderType: string
{
    case DOCUMENT = 'DOCUMENT';
    case TEMPLATE = 'TEMPLATE';
}