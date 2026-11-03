<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CalAiPhoneNumber extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CalAiPhoneNumber';

    protected $fillable = [
        'userId',
        'teamId',
        'phoneNumber',
        'provider',
        'providerPhoneNumberId',
        'stripeCustomerId',
        'stripeSubscriptionId',
        'subscriptionStatus',
        'inboundAgentId',
        'outboundAgentId',
    ];

    protected $casts = [
        'userId' => 'integer',
        'teamId' => 'integer',
        'subscriptionStatus' => PhoneNumberSubscriptionStatus::class,
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function inboundAgent(): BelongsTo
    {
        return $this->belongsTo(Agent::class, 'inboundAgentId');
    }

    public function outboundAgent(): BelongsTo
    {
        return $this->belongsTo(Agent::class, 'outboundAgentId');
    }
}