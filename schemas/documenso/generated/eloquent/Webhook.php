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
        'webhookUrl',
        'eventTriggers',
        'secret',
        'enabled',
        'userId',
        'teamId',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'userId' => 'integer',
        'teamId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function webhookCalls(): HasMany
    {
        return $this->hasMany(WebhookCall::class, 'webhookId');
    }
}