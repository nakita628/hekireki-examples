<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RealtimeStreamChunk extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'RealtimeStreamChunk';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'key',
        'value',
        'sequence',
        'runId',
    ];

    protected $casts = [
        'sequence' => 'integer',
    ];
}