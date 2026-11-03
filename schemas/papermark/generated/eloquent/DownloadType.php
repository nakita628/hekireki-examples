<?php

namespace App\Models;

enum DownloadType: string
{
    case SINGLE = 'SINGLE';
    case BULK = 'BULK';
    case FOLDER = 'FOLDER';
}