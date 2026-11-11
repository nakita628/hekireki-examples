<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMSSOIdentityAssertion extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_SSOIdentityAssertion';

    protected $primaryKey = 'user_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'user_id',
        'assertion_b64',
    ];
}