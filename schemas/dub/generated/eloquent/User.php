<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

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
        'isMachine',
        'passwordHash',
        'invalidLoginAttempts',
        'lockedAt',
        'defaultWorkspace',
        'source',
        'sentMail',
    ];

    protected $attributes = [
        'isMachine' => false,
        'invalidLoginAttempts' => 0,
        'sentMail' => false,
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'isMachine' => 'boolean',
        'invalidLoginAttempts' => 'integer',
        'lockedAt' => 'datetime',
        'sentMail' => 'boolean',
    ];

    public function notificationPreferences(): HasOne
    {
        return $this->hasOne(UserNotificationPreferences::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(ProjectUsers::class, 'userId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'userId');
    }

    public function dashboards(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'userId');
    }

    public function tokens(): HasMany
    {
        return $this->hasMany(Token::class, 'userId');
    }

    public function restrictedTokens(): HasMany
    {
        return $this->hasMany(RestrictedToken::class, 'userId');
    }

    public function oAuthCodes(): HasMany
    {
        return $this->hasMany(OAuthCode::class, 'userId');
    }

    public function integrations(): HasMany
    {
        return $this->hasMany(Integration::class, 'userId');
    }

    public function installedIntegrations(): HasMany
    {
        return $this->hasMany(InstalledIntegration::class, 'userId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(FolderUser::class, 'userId');
    }

    public function folderAccessRequests(): HasMany
    {
        return $this->hasMany(FolderAccessRequest::class, 'userId');
    }

    public function utmTemplates(): HasMany
    {
        return $this->hasMany(UtmTemplate::class, 'userId');
    }
}