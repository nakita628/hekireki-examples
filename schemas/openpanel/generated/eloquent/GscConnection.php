<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class GscConnection extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'gsc_connections';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'siteUrl',
        'accessToken',
        'refreshToken',
        'accessTokenExpiresAt',
        'lastSyncedAt',
        'lastSyncStatus',
        'lastSyncError',
        'backfillStatus',
    ];

    protected $attributes = [
        'siteUrl' => '',
    ];

    protected $casts = [
        'accessTokenExpiresAt' => 'datetime',
        'lastSyncedAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}