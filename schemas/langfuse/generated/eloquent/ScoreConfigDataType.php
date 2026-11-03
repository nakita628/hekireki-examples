<?php

namespace App\Models;

enum ScoreConfigDataType: string
{
    case CATEGORICAL = 'CATEGORICAL';
    case NUMERIC = 'NUMERIC';
    case BOOLEAN = 'BOOLEAN';
    case TEXT = 'TEXT';
}