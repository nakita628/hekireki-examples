<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Webhooks extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Webhooks';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'organizationId',
        'url',
        'deletedAt',
    ];

    protected $casts = [
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function integrations(): HasMany
    {
        return $this->hasMany(IntegrationsWebhooks::class, 'webhookId');
    }
}