<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMLensSignalConfig extends Model
{
    protected $table = 'LiteLLM_LensSignalConfig';

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