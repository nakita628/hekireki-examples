<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Accounts extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'accounts';

    protected $fillable = [
        'name',
        'nickname',
        'password',
        'image',
        'apiToken',
        'description',
        'note',
        'role',
        'loginType',
        'linkAccountId',
    ];

    protected $attributes = [
        'name' => '',
        'nickname' => '',
        'password' => '',
        'image' => '',
        'apiToken' => '',
        'description' => '',
        'note' => 0,
        'role' => '',
        'loginType' => '',
    ];

    protected $casts = [
        'note' => 'integer',
        'linkAccountId' => 'integer',
    ];

    public function notes(): HasMany
    {
        return $this->hasMany(Notes::class, 'accountId');
    }

    public function configs(): HasMany
    {
        return $this->hasMany(Config::class, 'userId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(Tag::class, 'accountId');
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comments::class, 'accountId');
    }

    public function attachments(): HasMany
    {
        return $this->hasMany(Attachments::class, 'accountId');
    }

    public function follows(): HasMany
    {
        return $this->hasMany(Follows::class, 'accountId');
    }

    public function notifications(): HasMany
    {
        return $this->hasMany(Notifications::class, 'accountId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'accountId');
    }

    public function sharedNotes(): HasMany
    {
        return $this->hasMany(NoteInternalShare::class, 'accountId');
    }

    public function aiScheduledTasks(): HasMany
    {
        return $this->hasMany(AiScheduledTask::class, 'accountId');
    }
}