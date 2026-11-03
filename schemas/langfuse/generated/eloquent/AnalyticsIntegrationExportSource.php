<?php

namespace App\Models;

enum AnalyticsIntegrationExportSource: string
{
    case TRACES_OBSERVATIONS = 'TRACES_OBSERVATIONS';
    case TRACES_OBSERVATIONS_EVENTS = 'TRACES_OBSERVATIONS_EVENTS';
    case EVENTS = 'EVENTS';
}