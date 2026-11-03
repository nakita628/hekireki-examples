<?php

namespace App\Models;

enum TaskEventLevel: string
{
    case TRACE = 'TRACE';
    case DEBUG = 'DEBUG';
    case LOG = 'LOG';
    case INFO = 'INFO';
    case WARN = 'WARN';
    case ERROR = 'ERROR';
}