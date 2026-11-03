<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class HostLocation extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'HostLocation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'eventTypeId',
        'type',
        'credentialId',
        'link',
        'address',
        'phoneNumber',
    ];

    protected $casts = [
        'userId' => 'integer',
        'eventTypeId' => 'integer',
        'credentialId' => 'integer',
    ];

    public function credential(): BelongsTo
    {
        return $this->belongsTo(Credential::class, 'credentialId');
    }
}