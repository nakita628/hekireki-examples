<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Conversation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'conversation';

    protected $fillable = [
        'title',
        'isShare',
        'accountId',
    ];

    protected $attributes = [
        'title' => '',
        'isShare' => false,
    ];

    protected $casts = [
        'isShare' => 'boolean',
        'accountId' => 'integer',
    ];

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(Message::class, 'conversationId');
    }
}