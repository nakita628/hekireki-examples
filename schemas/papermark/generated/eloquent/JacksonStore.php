<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class JacksonStore extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'modifiedAt';

    protected $table = 'jackson_store';

    protected $primaryKey = 'key';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'key',
        'value',
        'iv',
        'tag',
        'namespace',
    ];
}