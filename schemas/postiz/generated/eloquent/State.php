<?php

namespace App\Models;

enum State: string
{
    case QUEUE = 'QUEUE';
    case PUBLISHED = 'PUBLISHED';
    case ERROR = 'ERROR';
    case DRAFT = 'DRAFT';
}