<?php

namespace App\Models;

enum DashboardWidgetChartType: string
{
    case LINE_TIME_SERIES = 'LINE_TIME_SERIES';
    case AREA_TIME_SERIES = 'AREA_TIME_SERIES';
    case BAR_TIME_SERIES = 'BAR_TIME_SERIES';
    case HORIZONTAL_BAR = 'HORIZONTAL_BAR';
    case VERTICAL_BAR = 'VERTICAL_BAR';
    case PIE = 'PIE';
    case NUMBER = 'NUMBER';
    case HISTOGRAM = 'HISTOGRAM';
    case PIVOT_TABLE = 'PIVOT_TABLE';
}