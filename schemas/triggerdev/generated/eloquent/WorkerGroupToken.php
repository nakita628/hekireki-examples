<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;

class WorkerGroupToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WorkerGroupToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'tokenHash',
        'previousTokenHash',
        'previousTokenValidUntil',
    ];

    protected $casts = [
        'previousTokenValidUntil' => 'datetime',
    ];

    public function workerGroup(): HasOne
    {
        return $this->hasOne(WorkerInstanceGroup::class, 'tokenId');
    }
}