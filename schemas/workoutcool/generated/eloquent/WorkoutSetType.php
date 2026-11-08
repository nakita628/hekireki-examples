<?php

namespace App\Models;

enum WorkoutSetType: string
{
    case TIME = 'TIME';
    case WEIGHT = 'WEIGHT';
    case REPS = 'REPS';
    case BODYWEIGHT = 'BODYWEIGHT';
    case NA = 'NA';
}