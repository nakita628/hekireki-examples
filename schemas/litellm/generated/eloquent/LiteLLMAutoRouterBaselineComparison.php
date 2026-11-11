<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMAutoRouterBaselineComparison extends Model
{
    use PrismaDates;

    const CREATED_AT = null;

    protected $table = 'LiteLLM_AutoRouterBaselineComparison';

    protected $primaryKey = 'scope';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'scope',
        'api_key',
        'session_id',
        'router_name',
        'initial_equivalent',
        'revision',
        'published_revision',
        'history',
        'attempted_at',
        'retired',
    ];

    protected $attributes = [
        'revision' => 0,
        'published_revision' => 0,
        'retired' => false,
    ];

    protected $casts = [
        'initial_equivalent' => 'boolean',
        'revision' => 'integer',
        'published_revision' => 'integer',
        'attempted_at' => 'datetime',
        'retired' => 'boolean',
    ];
}