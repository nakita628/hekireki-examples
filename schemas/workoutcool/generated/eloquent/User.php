<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'user';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'firstName',
        'lastName',
        'name',
        'email',
        'emailVerified',
        'image',
        'locale',
        'role',
        'banned',
        'banReason',
        'banExpires',
        'isPremium',
        'onboardingPreferences',
    ];

    protected $attributes = [
        'firstName' => '',
        'lastName' => '',
        'locale' => 'fr',
        'role' => 'user',
        'banned' => false,
        'isPremium' => false,
    ];

    protected $casts = [
        'emailVerified' => 'boolean',
        'role' => UserRole::class,
        'banned' => 'boolean',
        'banExpires' => 'datetime',
        'isPremium' => 'boolean',
        'onboardingPreferences' => 'array',
    ];

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function feedbacks(): HasMany
    {
        return $this->hasMany(Feedbacks::class, 'userId');
    }

    public function WorkoutSession(): HasMany
    {
        return $this->hasMany(WorkoutSession::class, 'userId');
    }

    public function FavoriteExercises(): HasMany
    {
        return $this->hasMany(UserFavoriteExercise::class, 'userId');
    }

    public function subscriptions(): HasMany
    {
        return $this->hasMany(Subscription::class, 'userId');
    }

    public function licenses(): HasMany
    {
        return $this->hasMany(License::class, 'userId');
    }

    public function programEnrollments(): HasMany
    {
        return $this->hasMany(UserProgramEnrollment::class, 'userId');
    }
}