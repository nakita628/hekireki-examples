<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserPassword extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'UserPassword';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'hash',
        'changeAtNextLogin',
    ];

    protected $attributes = [
        'changeAtNextLogin' => false,
    ];

    protected $casts = [
        'changeAtNextLogin' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(UserProfile::class, 'userId');
    }
}