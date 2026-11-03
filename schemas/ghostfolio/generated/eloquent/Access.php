<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Access extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Access';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'alias',
        'expiresAt',
        'granteeUserId',
        'lastUsedAt',
        'scopes',
        'settings',
        'type',
        'userId',
    ];

    protected $attributes = [
        'settings' => '{}',
        'type' => 'PRIVATE',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
        'lastUsedAt' => 'datetime',
        'settings' => 'array',
        'type' => AccessType::class,
    ];

    public function granteeUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'granteeUserId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}