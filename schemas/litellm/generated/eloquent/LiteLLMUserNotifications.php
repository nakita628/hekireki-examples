<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMUserNotifications extends Model
{
    protected $table = 'LiteLLM_UserNotifications';

    protected $primaryKey = 'request_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'request_id',
        'user_id',
        'models',
        'justification',
        'status',
    ];
}