<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMInvitationLink extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_InvitationLink';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'user_id',
        'is_accepted',
        'accepted_at',
        'expires_at',
        'created_by',
        'updated_by',
    ];

    protected $attributes = [
        'is_accepted' => false,
    ];

    protected $casts = [
        'is_accepted' => 'boolean',
        'accepted_at' => 'datetime',
        'expires_at' => 'datetime',
    ];

    public function liteLLM_user_table_user(): BelongsTo
    {
        return $this->belongsTo(LiteLLMUserTable::class, 'user_id');
    }

    public function liteLLM_user_table_created(): BelongsTo
    {
        return $this->belongsTo(LiteLLMUserTable::class, 'created_by');
    }

    public function liteLLM_user_table_updated(): BelongsTo
    {
        return $this->belongsTo(LiteLLMUserTable::class, 'updated_by');
    }
}