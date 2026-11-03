<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RateLimit extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RateLimit';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'apiKeyId',
        'ttl',
        'limit',
        'blockDuration',
    ];

    protected $casts = [
        'ttl' => 'integer',
        'limit' => 'integer',
        'blockDuration' => 'integer',
    ];

    public function apiKey(): BelongsTo
    {
        return $this->belongsTo(ApiKey::class, 'apiKeyId');
    }
}