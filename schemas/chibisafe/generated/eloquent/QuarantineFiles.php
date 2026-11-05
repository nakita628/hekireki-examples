<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class QuarantineFiles extends Model
{
    protected $table = 'quarantineFiles';

    protected $primaryKey = 'fileId';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'fileId',
        'name',
        'reason',
    ];

    public function file(): BelongsTo
    {
        return $this->belongsTo(Files::class, 'fileId', 'uuid');
    }
}