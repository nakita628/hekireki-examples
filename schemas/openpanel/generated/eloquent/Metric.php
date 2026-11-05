<?php

namespace App\Models;

enum Metric: string
{
    case sum = 'sum';
    case average = 'average';
    case min = 'min';
    case max = 'max';
    case count = 'count';
}