<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamInvitation extends Model
{
    protected $table = 'TeamInvitation';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'teamID',
        'creatorUid',
        'inviteeEmail',
        'inviteeRole',
    ];

    protected $casts = [
        'inviteeRole' => TeamAccessRole::class,
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamID');
    }
}