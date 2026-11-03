<?php

namespace App\Models;

enum MonitorView: string
{
    case OBSERVATIONS = 'OBSERVATIONS';
    case SCORES_NUMERIC = 'SCORES_NUMERIC';
    case SCORES_CATEGORICAL = 'SCORES_CATEGORICAL';
    case SCORES_BOOLEAN = 'SCORES_BOOLEAN';
}