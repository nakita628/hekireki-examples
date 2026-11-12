<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Notifications extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'notifications';

    protected $fillable = [
        'type',
        'title',
        'content',
        'metadata',
        'isRead',
        'accountId',
    ];

    protected $attributes = [
        'isRead' => false,
    ];

    protected $casts = [
        'metadata' => 'array',
        'isRead' => 'boolean',
        'accountId' => 'integer',
    ];

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }
}