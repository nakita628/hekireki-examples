<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TaskRunCounter extends Model
{
    protected $table = 'TaskRunCounter';

    protected $primaryKey = 'taskIdentifier';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'taskIdentifier',
        'lastNumber',
    ];

    protected $attributes = [
        'lastNumber' => 0,
    ];

    protected $casts = [
        'lastNumber' => 'integer',
    ];
}