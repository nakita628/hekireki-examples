<?php

namespace App\Models;

enum WorkerDeploymentStatus: string
{
    case PENDING = 'PENDING';
    case INSTALLING = 'INSTALLING';
    case BUILDING = 'BUILDING';
    case DEPLOYING = 'DEPLOYING';
    case DEPLOYED = 'DEPLOYED';
    case FAILED = 'FAILED';
    case CANCELED = 'CANCELED';
    case TIMED_OUT = 'TIMED_OUT';
}