<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Viewer extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Viewer';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'verified',
        'invitedAt',
        'notificationPreferences',
        'dataroomId',
        'teamId',
    ];

    protected $attributes = [
        'verified' => false,
    ];

    protected $casts = [
        'verified' => 'boolean',
        'invitedAt' => 'datetime',
        'notificationPreferences' => 'array',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'viewerId');
    }

    public function groups(): HasMany
    {
        return $this->hasMany(ViewerGroupMembership::class, 'viewerId');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(ViewerInvitation::class, 'viewerId');
    }

    public function participatedConversations(): HasMany
    {
        return $this->hasMany(ConversationParticipant::class, 'viewerId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(Message::class, 'viewerId');
    }

    public function recipientConversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'recipientViewerId');
    }

    public function questionAssignments(): HasMany
    {
        return $this->hasMany(DataroomQuestionAssignment::class, 'viewerId');
    }

    public function uploadedDocuments(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'viewerId');
    }

    public function taskAssignments(): HasMany
    {
        return $this->hasMany(TaskAssignment::class, 'viewerId');
    }

    public function taskActivities(): HasMany
    {
        return $this->hasMany(TaskActivity::class, 'viewerId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'viewerId');
    }
}