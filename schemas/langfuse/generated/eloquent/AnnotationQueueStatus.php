<?php

namespace App\Models;

enum AnnotationQueueStatus: string
{
    case PENDING = 'PENDING';
    case COMPLETED = 'COMPLETED';
}