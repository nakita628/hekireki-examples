<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ExisingPlugData extends Model
{
    use HasVersion4Uuids;

    protected $table = 'ExisingPlugData';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'integrationId',
        'methodName',
        'value',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }
}