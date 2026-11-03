<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamProfile extends Model
{
    protected $table = 'TeamProfile';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'enabled',
        'teamId',
        'bio',
    ];

    protected $attributes = [
        'enabled' => false,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'teamId' => 'integer',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}