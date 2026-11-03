<?php

namespace App\Models;

enum GithubRepositorySelection: string
{
    case ALL = 'ALL';
    case SELECTED = 'SELECTED';
}