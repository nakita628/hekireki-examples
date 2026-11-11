<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMLensWorker extends Model
{
    protected $table = 'LiteLLM_LensWorker';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'token_hash',
        'data',
    ];

    protected $casts = [
        'data' => 'array',
    ];
}