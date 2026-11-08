<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Track extends Model
{
    protected $table = 'Track';

    public $timestamps = false;

    protected $fillable = [
        'name',
        'slug',
        'description',
        'visible',
        'isComingSoon',
    ];

    protected $attributes = [
        'visible' => true,
        'isComingSoon' => false,
    ];

    protected $casts = [
        'visible' => 'boolean',
        'isComingSoon' => 'boolean',
    ];

    public function trackChallenges(): HasMany
    {
        return $this->hasMany(TrackChallenge::class, 'trackId');
    }

    public function enrolledUsers(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_TrackToUser', 'A', 'B');
    }
}