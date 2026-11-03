<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class JacksonIndex extends Model
{
    protected $table = 'jackson_index';

    public $timestamps = false;

    protected $fillable = [
        'key',
        'storeKey',
    ];
}