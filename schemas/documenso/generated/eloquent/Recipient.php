<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Recipient extends Model
{
    use PrismaDates;

    protected $table = 'Recipient';

    public $timestamps = false;

    protected $fillable = [
        'envelopeId',
        'email',
        'name',
        'token',
        'documentDeletedAt',
        'expired',
        'expiresAt',
        'expirationNotifiedAt',
        'sentAt',
        'signedAt',
        'lastReminderSentAt',
        'nextReminderAt',
        'reminderCount',
        'authOptions',
        'signingOrder',
        'rejectionReason',
        'role',
        'readStatus',
        'signingStatus',
        'sendStatus',
    ];

    protected $attributes = [
        'name' => '',
        'reminderCount' => 0,
        'role' => 'SIGNER',
        'readStatus' => 'NOT_OPENED',
        'signingStatus' => 'NOT_SIGNED',
        'sendStatus' => 'NOT_SENT',
    ];

    protected $casts = [
        'documentDeletedAt' => 'datetime',
        'expired' => 'datetime',
        'expiresAt' => 'datetime',
        'expirationNotifiedAt' => 'datetime',
        'sentAt' => 'datetime',
        'signedAt' => 'datetime',
        'lastReminderSentAt' => 'datetime',
        'nextReminderAt' => 'datetime',
        'reminderCount' => 'integer',
        'authOptions' => 'array',
        'signingOrder' => 'integer',
        'role' => RecipientRole::class,
        'readStatus' => ReadStatus::class,
        'signingStatus' => SigningStatus::class,
        'sendStatus' => SendStatus::class,
    ];

    public function envelope(): BelongsTo
    {
        return $this->belongsTo(Envelope::class, 'envelopeId');
    }

    public function cscCredential(): HasOne
    {
        return $this->hasOne(CscCredential::class, 'recipientId');
    }

    public function cscSession(): HasOne
    {
        return $this->hasOne(CscSession::class, 'recipientId');
    }

    public function fields(): HasMany
    {
        return $this->hasMany(Field::class, 'recipientId');
    }

    public function signatures(): HasMany
    {
        return $this->hasMany(Signature::class, 'recipientId');
    }
}