<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'User';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'email',
        'username',
        'isAdmin',
        'paymentProcessorUserId',
        'subscriptionStatus',
        'subscriptionPlan',
        'currentPeriodEnd',
        'cancelAtPeriodEnd',
        'datePaid',
        'credits',
    ];

    protected $attributes = [
        'isAdmin' => false,
        'cancelAtPeriodEnd' => false,
        'credits' => 3,
    ];

    protected $casts = [
        'isAdmin' => 'boolean',
        'currentPeriodEnd' => 'datetime',
        'cancelAtPeriodEnd' => 'boolean',
        'datePaid' => 'datetime',
        'credits' => 'integer',
    ];

    public function gptResponses(): HasMany
    {
        return $this->hasMany(GptResponse::class, 'userId');
    }

    public function contactFormMessages(): HasMany
    {
        return $this->hasMany(ContactFormMessage::class, 'userId');
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(Task::class, 'userId');
    }

    public function files(): HasMany
    {
        return $this->hasMany(File::class, 'userId');
    }
}