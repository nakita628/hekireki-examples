<?php

namespace App\Models;

enum AppCategories: string
{
    case calendar = 'calendar';
    case messaging = 'messaging';
    case other = 'other';
    case payment = 'payment';
    case video = 'video';
    case web3 = 'web3';
    case automation = 'automation';
    case analytics = 'analytics';
    case conferencing = 'conferencing';
    case crm = 'crm';
}