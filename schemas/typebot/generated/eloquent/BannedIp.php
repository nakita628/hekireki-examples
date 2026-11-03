<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BannedIp extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'BannedIp';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'ip',
        'responsibleTypebotId',
        'userId',
    ];

    public function responsibleTypebot(): BelongsTo
    {
        return $this->belongsTo(Typebot::class, 'responsibleTypebotId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}