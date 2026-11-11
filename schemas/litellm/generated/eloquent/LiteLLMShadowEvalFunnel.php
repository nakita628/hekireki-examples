<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMShadowEvalFunnel extends Model
{
    protected $table = 'LiteLLM_ShadowEvalFunnel';

    protected $primaryKey = 'job_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'job_id',
        'not_sampled',
        'unjudgeable',
        'shed',
        'withheld',
    ];

    protected $attributes = [
        'not_sampled' => 0,
        'unjudgeable' => 0,
        'shed' => 0,
        'withheld' => 0,
    ];

    protected $casts = [
        'not_sampled' => 'integer',
        'unjudgeable' => 'integer',
        'shed' => 'integer',
        'withheld' => 'integer',
    ];
}