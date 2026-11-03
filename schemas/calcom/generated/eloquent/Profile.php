<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Profile extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Profile';

    protected $fillable = [
        'uid',
        'userId',
        'organizationId',
        'username',
    ];

    protected $casts = [
        'userId' => 'integer',
        'organizationId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function movedFromUser(): HasOne
    {
        return $this->hasOne(User::class, 'movedToProfileId');
    }

    public function eventTypes(): HasMany
    {
        return $this->hasMany(EventType::class, 'profileId');
    }
}