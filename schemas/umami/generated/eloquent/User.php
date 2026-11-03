<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class User extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at', 'deleted_at'];

    protected $table = 'user';

    protected $primaryKey = 'user_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'user_id',
        'username',
        'password',
        'role',
        'logo_url',
        'display_name',
        'two_factor_required',
        'deleted_at',
    ];

    protected $attributes = [
        'two_factor_required' => false,
    ];

    protected $casts = [
        'two_factor_required' => 'boolean',
        'deleted_at' => 'datetime',
    ];

    public function twoFactorAuth(): HasOne
    {
        return $this->hasOne(TwoFactorAuth::class, 'user_id');
    }

    public function twoFactorRateLimit(): HasOne
    {
        return $this->hasOne(TwoFactorRateLimit::class, 'user_id');
    }

    public function websites(): HasMany
    {
        return $this->hasMany(Website::class, 'user_id');
    }

    public function createdBy(): HasMany
    {
        return $this->hasMany(Website::class, 'created_by');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'user_id');
    }

    public function pixels(): HasMany
    {
        return $this->hasMany(Pixel::class, 'user_id');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(TeamUser::class, 'user_id');
    }

    public function reports(): HasMany
    {
        return $this->hasMany(Report::class, 'user_id');
    }

    public function boards(): HasMany
    {
        return $this->hasMany(Board::class, 'user_id');
    }

    public function twoFactorBackupCodes(): HasMany
    {
        return $this->hasMany(TwoFactorBackupCode::class, 'user_id');
    }

    public function twoFactorOtpUseds(): HasMany
    {
        return $this->hasMany(TwoFactorOtpUsed::class, 'user_id');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'user_id');
    }
}