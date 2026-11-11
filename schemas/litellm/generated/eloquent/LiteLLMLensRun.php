<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMLensRun extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_LensRun';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'lens_id',
        'data',
    ];

    protected $casts = [
        'data' => 'array',
    ];
}