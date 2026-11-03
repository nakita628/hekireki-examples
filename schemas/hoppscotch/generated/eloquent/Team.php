<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Team extends Model
{
    protected $table = 'Team';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'name',
    ];

    public function collections(): HasMany
    {
        return $this->hasMany(TeamCollection::class, 'teamID');
    }

    public function environments(): HasMany
    {
        return $this->hasMany(TeamEnvironment::class, 'teamID');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(TeamInvitation::class, 'teamID');
    }

    public function members(): HasMany
    {
        return $this->hasMany(TeamMember::class, 'teamID');
    }

    public function requests(): HasMany
    {
        return $this->hasMany(TeamRequest::class, 'teamID');
    }
}