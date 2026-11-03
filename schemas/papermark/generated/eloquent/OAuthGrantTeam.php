<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class OAuthGrantTeam extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OAuthGrantTeam';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'grantId',
        'teamId',
        'userId',
    ];
}