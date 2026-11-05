<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Bans extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'bans';

    protected $fillable = [
        'ip',
        'reason',
    ];
}