<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SpaceApiKey extends Model
{
    use PrismaDates;

    protected $table = 'space_api_keys';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'name',
        'prefix',
        'hashed_key',
        'last_used_at',
        'expires_at',
        'revoked_at',
    ];

    protected $casts = [
        'last_used_at' => 'datetime',
        'expires_at' => 'datetime',
        'revoked_at' => 'datetime',
    ];

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }
}