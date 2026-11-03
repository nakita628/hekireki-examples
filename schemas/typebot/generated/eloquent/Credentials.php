<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Credentials extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Credentials';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'workspaceId',
        'data',
        'name',
        'type',
        'iv',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }
}