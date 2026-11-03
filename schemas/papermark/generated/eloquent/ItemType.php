<?php

namespace App\Models;

enum ItemType: string
{
    case DATAROOM_DOCUMENT = 'DATAROOM_DOCUMENT';
    case DATAROOM_FOLDER = 'DATAROOM_FOLDER';
}