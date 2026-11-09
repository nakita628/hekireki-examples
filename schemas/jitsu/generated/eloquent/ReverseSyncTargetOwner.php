<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ReverseSyncTargetOwner extends Model
{
    protected $table = 'reverse_sync_target_owner';

    protected $primaryKey = 'target_hash';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'target_hash',
        'workspace_id',
        'sync_id',
    ];
}