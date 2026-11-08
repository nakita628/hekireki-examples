<?php

namespace App\Models;

enum Difficulty: string
{
    case BEGINNER = 'BEGINNER';
    case EASY = 'EASY';
    case MEDIUM = 'MEDIUM';
    case HARD = 'HARD';
    case EXTREME = 'EXTREME';
    case EVENT = 'EVENT';
}