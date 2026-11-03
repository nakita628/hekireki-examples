<?php

namespace App\Models;

enum AnnouncementColor: string
{
    case INFO = 'INFO';
    case WARNING = 'WARNING';
    case ERROR = 'ERROR';
}