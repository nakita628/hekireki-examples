<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EmbedConfigs extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'embed_configs';

    protected $fillable = [
        'uuid',
        'enabled',
        'chat_mode',
        'allowlist_domains',
        'allow_model_override',
        'allow_temperature_override',
        'allow_prompt_override',
        'max_chats_per_day',
        'max_chats_per_session',
        'message_limit',
        'workspace_id',
        'createdBy',
        'usersId',
    ];

    protected $attributes = [
        'enabled' => false,
        'chat_mode' => 'query',
        'allow_model_override' => false,
        'allow_temperature_override' => false,
        'allow_prompt_override' => false,
        'message_limit' => 20,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'allow_model_override' => 'boolean',
        'allow_temperature_override' => 'boolean',
        'allow_prompt_override' => 'boolean',
        'max_chats_per_day' => 'integer',
        'max_chats_per_session' => 'integer',
        'message_limit' => 'integer',
        'workspace_id' => 'integer',
        'createdBy' => 'integer',
        'usersId' => 'integer',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspace_id');
    }

    public function users(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'usersId');
    }

    public function embed_chats(): HasMany
    {
        return $this->hasMany(EmbedChats::class, 'embed_id');
    }
}