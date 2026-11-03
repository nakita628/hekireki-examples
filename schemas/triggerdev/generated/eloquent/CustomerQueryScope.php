<?php

namespace App\Models;

enum CustomerQueryScope: string
{
    case ORGANIZATION = 'ORGANIZATION';
    case PROJECT = 'PROJECT';
    case ENVIRONMENT = 'ENVIRONMENT';
}