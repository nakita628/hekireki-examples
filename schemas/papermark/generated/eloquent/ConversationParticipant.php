<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ConversationParticipant extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ConversationParticipant';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'conversationId',
        'role',
        'viewerId',
        'userId',
        'receiveNotifications',
    ];

    protected $attributes = [
        'role' => 'PARTICIPANT',
        'receiveNotifications' => false,
    ];

    protected $casts = [
        'role' => ParticipantRole::class,
        'receiveNotifications' => 'boolean',
    ];

    public function conversation(): BelongsTo
    {
        return $this->belongsTo(Conversation::class, 'conversationId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}