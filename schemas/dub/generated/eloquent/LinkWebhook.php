<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LinkWebhook extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'LinkWebhook';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'linkId',
        'webhookId',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function webhook(): BelongsTo
    {
        return $this->belongsTo(Webhook::class, 'webhookId');
    }
}