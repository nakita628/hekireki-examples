<?php

namespace App\Models;

enum WebhookReceiver: string
{
    case user = 'user';
    case zapier = 'zapier';
    case make = 'make';
    case slack = 'slack';
    case segment = 'segment';
}