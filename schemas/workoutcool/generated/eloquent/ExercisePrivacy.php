<?php

namespace App\Models;

enum ExercisePrivacy: string
{
    case PUBLIC = 'PUBLIC';
    case PRIVATE = 'PRIVATE';
}