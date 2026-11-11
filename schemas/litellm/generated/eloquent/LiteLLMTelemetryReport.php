<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMTelemetryReport extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_TelemetryReport';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'window_start',
        'window_end',
        'report',
    ];

    protected $casts = [
        'window_start' => 'float',
        'window_end' => 'float',
        'report' => 'array',
    ];
}