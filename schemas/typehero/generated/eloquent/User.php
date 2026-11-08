<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'User';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'email',
        'emailVerified',
        'image',
        'bio',
        'status',
        'banReason',
    ];

    protected $attributes = [
        'bio' => '',
        'status' => 'ACTIVE',
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'status' => UserStatus::class,
    ];

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function challenge(): HasMany
    {
        return $this->hasMany(Challenge::class, 'userId');
    }

    public function vote(): HasMany
    {
        return $this->hasMany(Vote::class, 'userId');
    }

    public function submission(): HasMany
    {
        return $this->hasMany(Submission::class, 'userId');
    }

    public function bookmark(): HasMany
    {
        return $this->hasMany(Bookmark::class, 'userId');
    }

    public function sharedSolution(): HasMany
    {
        return $this->hasMany(SharedSolution::class, 'userId');
    }

    public function comment(): HasMany
    {
        return $this->hasMany(Comment::class, 'userId');
    }

    public function reports(): HasMany
    {
        return $this->hasMany(Report::class, 'reporterId');
    }

    public function reportsAbout(): HasMany
    {
        return $this->hasMany(Report::class, 'userId');
    }

    public function moderated(): HasMany
    {
        return $this->hasMany(Report::class, 'moderatorId');
    }

    public function imageUpload(): HasMany
    {
        return $this->hasMany(ImageUpload::class, 'userId');
    }

    public function BetaTokens(): HasMany
    {
        return $this->hasMany(BetaTokens::class, 'userId');
    }

    public function donations(): HasMany
    {
        return $this->hasMany(Donation::class, 'userId');
    }

    public function shortUrls(): HasMany
    {
        return $this->hasMany(ShortURL::class, 'userId');
    }

    public function toUserNotification(): HasMany
    {
        return $this->hasMany(Notification::class, 'toUserId');
    }

    public function fromUserNotification(): HasMany
    {
        return $this->hasMany(Notification::class, 'fromUserId');
    }

    public function roles(): BelongsToMany
    {
        return $this->belongsToMany(Role::class, '_RoleToUser', 'B', 'A');
    }

    public function userLinks(): BelongsToMany
    {
        return $this->belongsToMany(UserLink::class, '_UserToUserLink', 'A', 'B');
    }

    public function tracks(): BelongsToMany
    {
        return $this->belongsToMany(Track::class, '_TrackToUser', 'B', 'A');
    }
}