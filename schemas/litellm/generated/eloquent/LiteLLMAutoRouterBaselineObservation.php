<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMAutoRouterBaselineObservation extends Model
{
    protected $table = 'LiteLLM_AutoRouterBaselineObservation';

    protected $primaryKey = 'request_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'request_id',
        'scope',
        'started_at',
        'revision',
        'data',
        'publication',
        'conflicted',
    ];

    protected $attributes = [
        'conflicted' => false,
    ];

    protected $casts = [
        'started_at' => 'float',
        'revision' => 'integer',
        'conflicted' => 'boolean',
    ];
}