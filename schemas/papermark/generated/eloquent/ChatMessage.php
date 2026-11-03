<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ChatMessage extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ChatMessage';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'chatId',
        'role',
        'content',
        'metadata',
    ];

    protected $casts = [
        'metadata' => 'array',
    ];

    public function chat(): BelongsTo
    {
        return $this->belongsTo(Chat::class, 'chatId');
    }
}