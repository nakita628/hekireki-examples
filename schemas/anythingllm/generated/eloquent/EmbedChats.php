<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EmbedChats extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'embed_chats';

    protected $fillable = [
        'prompt',
        'response',
        'session_id',
        'include',
        'connection_information',
        'embed_id',
        'usersId',
    ];

    protected $attributes = [
        'include' => true,
    ];

    protected $casts = [
        'include' => 'boolean',
        'embed_id' => 'integer',
        'usersId' => 'integer',
    ];

    public function embed_config(): BelongsTo
    {
        return $this->belongsTo(EmbedConfigs::class, 'embed_id');
    }

    public function users(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'usersId');
    }
}