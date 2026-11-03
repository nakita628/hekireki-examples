<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OutOfOfficeReason extends Model
{
    protected $table = 'OutOfOfficeReason';

    public $timestamps = false;

    protected $fillable = [
        'emoji',
        'reason',
        'enabled',
        'userId',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function entries(): HasMany
    {
        return $this->hasMany(OutOfOfficeEntry::class, 'reasonId');
    }
}