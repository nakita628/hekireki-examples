<?php

namespace App\Models;

enum CommentObjectType: string
{
    case TRACE = 'TRACE';
    case OBSERVATION = 'OBSERVATION';
    case SESSION = 'SESSION';
    case PROMPT = 'PROMPT';
}