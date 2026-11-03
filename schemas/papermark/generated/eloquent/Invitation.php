<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Invitation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Invitation';

    protected $primaryKey = 'token';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'token',
        'email',
        'expires',
        'teamId',
        'role',
        'dataroomIds',
    ];

    protected $attributes = [
        'role' => 'MEMBER',
    ];

    protected $casts = [
        'expires' => 'datetime',
        'role' => Role::class,
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}