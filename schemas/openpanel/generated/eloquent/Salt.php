<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Salt extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'salts';

    protected $primaryKey = 'salt';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'salt',
    ];
}