<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'users';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'password',
        'salt',
        'phone',
        'email',
        'avatar',
        'is_system',
        'is_admin',
        'is_trial_used',
        'lang',
        'notify_meta',
        'last_sign_time',
        'deactivated_time',
        'created_time',
        'deleted_time',
        'permanent_deleted_time',
        'ref_meta',
    ];

    protected $casts = [
        'is_system' => 'boolean',
        'is_admin' => 'boolean',
        'is_trial_used' => 'boolean',
        'last_sign_time' => 'datetime',
        'deactivated_time' => 'datetime',
        'created_time' => 'datetime',
        'deleted_time' => 'datetime',
        'permanent_deleted_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'user_id');
    }

    public function baseOrders(): HasMany
    {
        return $this->hasMany(UserBaseOrder::class, 'user_id');
    }
}