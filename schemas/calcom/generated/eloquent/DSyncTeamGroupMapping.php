<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DSyncTeamGroupMapping extends Model
{
    protected $table = 'DSyncTeamGroupMapping';

    public $timestamps = false;

    protected $fillable = [
        'organizationId',
        'teamId',
        'directoryId',
        'groupName',
    ];

    protected $casts = [
        'organizationId' => 'integer',
        'teamId' => 'integer',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function directory(): BelongsTo
    {
        return $this->belongsTo(DSyncData::class, 'directoryId', 'directoryId');
    }
}