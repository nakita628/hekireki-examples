<?php

namespace App\Models;

enum PlanPeriod: string
{
    case monthly = 'monthly';
    case yearly = 'yearly';
    case quarterly = 'quarterly';
    case biweekly = 'biweekly';
}