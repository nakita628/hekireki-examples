<?php

namespace App\Models;

enum TaskRunCheckpointType: string
{
    case DOCKER = 'DOCKER';
    case KUBERNETES = 'KUBERNETES';
    case COMPUTE = 'COMPUTE';
}