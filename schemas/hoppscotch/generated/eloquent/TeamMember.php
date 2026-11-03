<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamMember extends Model
{
    use HasVersion4Uuids;

    protected $table = 'TeamMember';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'role',
        'userUid',
        'teamID',
    ];

    protected $casts = [
        'role' => TeamAccessRole::class,
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamID');
    }
}