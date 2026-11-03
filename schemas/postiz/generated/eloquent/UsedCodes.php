<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UsedCodes extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'UsedCodes';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'code',
        'orgId',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'orgId');
    }
}