<?php

namespace App\Models;

enum CollaborationType: string
{
    case READ = 'READ';
    case WRITE = 'WRITE';
    case FULL_ACCESS = 'FULL_ACCESS';
}