<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamEmailVerification extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'TeamEmailVerification';

    protected $primaryKey = 'teamId';

    public $incrementing = false;

    protected $fillable = [
        'teamId',
        'name',
        'email',
        'token',
        'completed',
        'expiresAt',
    ];

    protected $attributes = [
        'completed' => false,
    ];

    protected $casts = [
        'completed' => 'boolean',
        'expiresAt' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}