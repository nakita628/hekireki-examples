<?php

namespace App\Models;

enum AnnotationQueueObjectType: string
{
    case TRACE = 'TRACE';
    case OBSERVATION = 'OBSERVATION';
    case SESSION = 'SESSION';
}