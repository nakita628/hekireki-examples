<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class IdempotencyKey extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'idempotency_keys';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'key',
        'projectId',
        'method',
        'path',
        'statusCode',
        'expiresAt',
    ];

    protected $casts = [
        'statusCode' => 'integer',
        'expiresAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}