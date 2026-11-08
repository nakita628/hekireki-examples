<?php

namespace App\Models;

enum CommentRoot: string
{
    case SOLUTION = 'SOLUTION';
    case CHALLENGE = 'CHALLENGE';
}