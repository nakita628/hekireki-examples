<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMConfig extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_Config';

    protected $primaryKey = 'param_name';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'param_name',
        'param_value',
        'last_run_at',
        'reload_revision',
    ];

    protected $attributes = [
        'reload_revision' => 0,
    ];

    protected $casts = [
        'param_value' => 'array',
        'last_run_at' => 'datetime',
        'reload_revision' => 'integer',
    ];
}