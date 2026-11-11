<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMLensIngestionKey extends Model
{
    protected $table = 'LiteLLM_LensIngestionKey';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'data',
    ];

    protected $casts = [
        'data' => 'array',
    ];
}