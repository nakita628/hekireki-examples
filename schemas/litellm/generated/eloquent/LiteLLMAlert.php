<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMAlert extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_Alert';

    protected $fillable = [
        'detection_key',
        'type',
        'resource_type',
        'resource_id',
        'status',
        'window_start',
        'window_end',
        'evidence',
        'updated_by',
    ];

    protected $attributes = [
        'status' => 'open',
    ];

    protected $casts = [
        'window_start' => 'datetime',
        'window_end' => 'datetime',
        'evidence' => 'array',
    ];
}