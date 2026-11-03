<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserEnvironment extends Model
{
    protected $table = 'UserEnvironment';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userUid',
        'name',
        'variables',
        'isGlobal',
    ];

    protected $casts = [
        'variables' => 'array',
        'isGlobal' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userUid');
    }
}