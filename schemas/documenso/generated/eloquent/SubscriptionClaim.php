<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SubscriptionClaim extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SubscriptionClaim';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'locked',
        'teamCount',
        'memberCount',
        'envelopeItemCount',
        'recipientCount',
        'envelopeContentCount',
        'envelopeContentImageCount',
        'flags',
        'documentRateLimits',
        'documentQuota',
        'emailRateLimits',
        'emailQuota',
        'apiRateLimits',
        'apiQuota',
        'emailTransportId',
    ];

    protected $attributes = [
        'locked' => false,
    ];

    protected $casts = [
        'locked' => 'boolean',
        'teamCount' => 'integer',
        'memberCount' => 'integer',
        'envelopeItemCount' => 'integer',
        'recipientCount' => 'integer',
        'envelopeContentCount' => 'integer',
        'envelopeContentImageCount' => 'integer',
        'flags' => 'array',
        'documentRateLimits' => 'array',
        'documentQuota' => 'integer',
        'emailRateLimits' => 'array',
        'emailQuota' => 'integer',
        'apiRateLimits' => 'array',
        'apiQuota' => 'integer',
    ];

    public function emailTransport(): BelongsTo
    {
        return $this->belongsTo(EmailTransport::class, 'emailTransportId');
    }
}