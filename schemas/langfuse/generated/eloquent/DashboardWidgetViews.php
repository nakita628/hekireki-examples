<?php

namespace App\Models;

enum DashboardWidgetViews: string
{
    case TRACES = 'TRACES';
    case OBSERVATIONS = 'OBSERVATIONS';
    case SCORES_NUMERIC = 'SCORES_NUMERIC';
    case SCORES_CATEGORICAL = 'SCORES_CATEGORICAL';
    case SCORES_BOOLEAN = 'SCORES_BOOLEAN';
}