<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'User';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'emailVerified',
        'image',
        'contactId',
        'plan',
        'stripeId',
        'subscriptionId',
        'startsAt',
        'endsAt',
        'referralTrack',
        'referralTrackSelectedAt',
        'referralProfile',
    ];

    protected $attributes = [
        'plan' => 'free',
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'startsAt' => 'datetime',
        'endsAt' => 'datetime',
        'referralTrack' => ReferralTrack::class,
        'referralTrackSelectedAt' => 'datetime',
        'referralProfile' => 'array',
    ];

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function documents(): HasMany
    {
        return $this->hasMany(Document::class, 'ownerId');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(UserTeam::class, 'userId');
    }

    public function userDatarooms(): HasMany
    {
        return $this->hasMany(UserDataroom::class, 'userId');
    }

    public function domains(): HasMany
    {
        return $this->hasMany(Domain::class, 'userId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'userId');
    }

    public function restrictedTokens(): HasMany
    {
        return $this->hasMany(RestrictedToken::class, 'userId');
    }

    public function participatedConversations(): HasMany
    {
        return $this->hasMany(ConversationParticipant::class, 'userId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(Message::class, 'userId');
    }

    public function createdDataroomQuestions(): HasMany
    {
        return $this->hasMany(DataroomQuestion::class, 'createdByUserId');
    }

    public function publishedFaqItems(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'publishedByUserId');
    }

    public function createdAnnotations(): HasMany
    {
        return $this->hasMany(DocumentAnnotation::class, 'createdById');
    }

    public function createdRedactionJobs(): HasMany
    {
        return $this->hasMany(DocumentRedactionJob::class, 'createdById');
    }

    public function installedIntegrations(): HasMany
    {
        return $this->hasMany(InstalledIntegration::class, 'userId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'ownerId');
    }

    public function frozenDatarooms(): HasMany
    {
        return $this->hasMany(Dataroom::class, 'frozenBy');
    }

    public function createdTasks(): HasMany
    {
        return $this->hasMany(Task::class, 'createdByUserId');
    }

    public function taskActivities(): HasMany
    {
        return $this->hasMany(TaskActivity::class, 'userId');
    }

    public function notificationPreferences(): HasMany
    {
        return $this->hasMany(NotificationPreference::class, 'userId');
    }

    public function referralInvites(): HasMany
    {
        return $this->hasMany(ReferralInvite::class, 'userId');
    }
}