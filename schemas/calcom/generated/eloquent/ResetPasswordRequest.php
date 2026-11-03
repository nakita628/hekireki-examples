<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ResetPasswordRequest extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ResetPasswordRequest';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'expires',
    ];

    protected $casts = [
        'expires' => 'datetime',
    ];
}