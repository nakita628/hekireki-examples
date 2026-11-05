<?php

namespace App\Models;

enum ChartType: string
{
    case linear = 'linear';
    case bar = 'bar';
    case histogram = 'histogram';
    case pie = 'pie';
    case metric = 'metric';
    case area = 'area';
    case map = 'map';
    case funnel = 'funnel';
    case retention = 'retention';
    case conversion = 'conversion';
    case sankey = 'sankey';
}