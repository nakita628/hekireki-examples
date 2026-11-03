<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserCredentials extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'UserCredentials';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'data',
        'name',
        'type',
        'iv',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}