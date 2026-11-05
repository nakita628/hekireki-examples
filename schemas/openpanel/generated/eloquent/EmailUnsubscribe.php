<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class EmailUnsubscribe extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'email_unsubscribes';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'category',
    ];
}