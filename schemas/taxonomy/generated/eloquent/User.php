<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    protected $table = 'users';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'emailVerified',
        'image',
        'stripe_customer_id',
        'stripe_subscription_id',
        'stripe_price_id',
        'stripe_current_period_end',
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'stripe_current_period_end' => 'datetime',
    ];

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function Post(): HasMany
    {
        return $this->hasMany(Post::class, 'authorId');
    }
}