<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class VerifiedNumber extends Model
{
    protected $table = 'VerifiedNumber';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'teamId',
        'phoneNumber',
    ];

    protected $casts = [
        'userId' => 'integer',
        'teamId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}