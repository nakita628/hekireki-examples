<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Webhook extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Webhook';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'installationId',
        'receiver',
        'name',
        'url',
        'secret',
        'triggers',
        'linkScope',
        'consecutiveFailures',
        'lastFailedAt',
        'disabledAt',
    ];

    protected $attributes = [
        'receiver' => 'user',
        'consecutiveFailures' => 0,
    ];

    protected $casts = [
        'receiver' => WebhookReceiver::class,
        'triggers' => 'array',
        'linkScope' => LinkScope::class,
        'consecutiveFailures' => 'integer',
        'lastFailedAt' => 'datetime',
        'disabledAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function installedIntegration(): BelongsTo
    {
        return $this->belongsTo(InstalledIntegration::class, 'installationId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(LinkWebhook::class, 'webhookId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(FolderWebhook::class, 'webhookId');
    }
}