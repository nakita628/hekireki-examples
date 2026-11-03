<?php

namespace App\Models;

enum From: string
{
    case BUYER = 'BUYER';
    case SELLER = 'SELLER';
}