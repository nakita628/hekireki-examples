<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserPassword extends Model
{
    protected $table = 'UserPassword';

    protected $primaryKey = 'userId';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'hash',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}