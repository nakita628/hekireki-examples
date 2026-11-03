<?php

namespace App\Models;

enum CheckpointType: string
{
    case DOCKER = 'DOCKER';
    case KUBERNETES = 'KUBERNETES';
}